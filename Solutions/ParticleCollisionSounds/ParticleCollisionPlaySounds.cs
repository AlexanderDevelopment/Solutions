using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _src.Scripts.OpenUnitySolutions.GenericPooler;
using Cysharp.Threading.Tasks;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


namespace _src.Scripts.Level_01.PaintLogic
{
	public class ParticleCollisionPlaySounds : MonoBehaviour
	{
		[SerializeField]
		private int _audioPoolSize;


		[SerializeField]
		private bool _randomSound;


		[SerializeField]
		private bool _randomPitch;


		[SerializeField, ShowIf("_randomSound")]
		private AudioClip[] _audioClips;


		[SerializeField, HideIf("_randomSound", true)]
		private AudioClip _audioClip;


		[SerializeField, BoxGroup("AudioSourceSettings"), Range(0, 1)]
		private float _volume = 1;


		[SerializeField, ShowIf("_randomPitch"), Range(0, 2), BoxGroup("AudioSourceSettings")]
		private float _minPitch, _maxPitch;


		[SerializeField, BoxGroup("AudioSourceSettings")]
		private AudioMixerGroup _sfxAudioMixerGroup;


		[SerializeField, BoxGroup("AudioSourceSettings")]
		private float _minDistance, _maxDistance = 500;


		[SerializeField, BoxGroup("AudioSourceSettings"), Range(0f, 1f)]
		private float _spatialBlend = 1f;


		[SerializeField, BoxGroup("AudioSourceSettings")]
		private AudioRolloffMode _rolloffMode = AudioRolloffMode.Linear;


		private List<ParticleCollisionEvent> _collisionEvents = new();
		private ParticleSystem _particleSystem;
		private Pooler<AudioSource> _pooler;


		private void Start()
		{
			_particleSystem = GetComponent<ParticleSystem>();
			_pooler = new Pooler<AudioSource>(_audioPoolSize);
		}


		private void OnParticleCollision(GameObject other)
		{
			_particleSystem.GetCollisionEvents(other, _collisionEvents);

			if (_collisionEvents.Count <= 0)
				return;

			AudioSource newAudioSource;

			foreach (var collisionEvent in _collisionEvents)
			{
				newAudioSource = _pooler.GetFromPool(collisionEvent.intersection);

				if (!newAudioSource)
					continue;

				if (_audioClip)
					newAudioSource.clip = _audioClip;

				if (_randomSound && _audioClips.Length > 0)
					newAudioSource.clip = _audioClips[Random.Range(0, _audioClips.Length)];

				if (_randomPitch)
					newAudioSource.pitch = Random.Range(_minPitch, _maxPitch);

				if (newAudioSource.clip is null)
				{
					UnityEngine.Debug.LogError("No have sounds to play", gameObject);

					return;
				}

				newAudioSource.outputAudioMixerGroup = _sfxAudioMixerGroup;
				newAudioSource.minDistance = _minDistance;
				newAudioSource.maxDistance = _maxDistance;
				newAudioSource.spatialBlend = _spatialBlend;
				newAudioSource.rolloffMode = _rolloffMode;
				newAudioSource.volume = _volume;
				PlaySoundAndReturnToPooler(newAudioSource).Forget();
			}
		}


		private async UniTask PlaySoundAndReturnToPooler(AudioSource audioSource)
		{
			audioSource.Play();
			await UniTask.WaitWhile(() => audioSource.isPlaying);
			_pooler.ReturnToPool(audioSource);
		}


		[Button(ButtonSizes.Large)]
		protected virtual async void TestPlaySound()
		{
			AudioClip tmpAudioClip;

			if (_randomSound && _audioClips.Length > 0)
			{
				tmpAudioClip = _audioClips[Random.Range(0, _audioClips.Length)];
			}
			else
			{
				tmpAudioClip = _audioClip;
			}

			if (tmpAudioClip == null)
			{
				UnityEngine.Debug.LogError(" on " + this.gameObject.name + " can't play in editor mode, you haven't set its Sfx.");

				return;
			}

			float pitch = 1;

			if (_randomPitch)
				pitch = Random.Range(_minPitch, _maxPitch);

			GameObject temporaryAudioHost = new GameObject("EditorTestAS_WillAutoDestroy");
			SceneManager.MoveGameObjectToScene(temporaryAudioHost.gameObject, this.gameObject.scene);
			temporaryAudioHost.transform.position = this.transform.position;
			var editorAudioSource = temporaryAudioHost.AddComponent<AudioSource>();
			PlayAudioSource(editorAudioSource, tmpAudioClip, _volume, pitch, 0);
			float length = 1000 * tmpAudioClip.length;
			length = length / Mathf.Abs(pitch);
			await Task.Delay((int)length);
			DestroyImmediate(temporaryAudioHost);
		}


		protected virtual void PlayAudioSource(AudioSource audioSource, AudioClip sfx, float volume, float pitch, int timeSamples, AudioMixerGroup audioMixerGroup = null, int priority = 128)
		{
			// we set that audio source clip to the one in paramaters
			audioSource.clip = sfx;
			audioSource.timeSamples = timeSamples;

			// we set the audio source volume to the one in parameters
			audioSource.volume = volume;
			audioSource.pitch = pitch;

			// we set our loop setting
			audioSource.loop = false;
			audioSource.priority = priority;

			if (audioMixerGroup != null)
			{
				audioSource.outputAudioMixerGroup = audioMixerGroup;
			}

			// we start playing the sound
			audioSource.Play();
		}
	}
}
