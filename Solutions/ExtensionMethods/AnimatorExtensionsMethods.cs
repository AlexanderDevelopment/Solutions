
	public static class AnimatorExtensionMethods
	{
		public static async UniTask AsyncCrossFadeGesture(this Animator animator, string animationName, float fadeInDuration)
		{
			animator.CrossFade(animationName, fadeInDuration);
			while (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && 
			       animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
			{
				await UniTask.Delay(1);
			}
			
		}


		public static async UniTask PlayAnimationClipOnceWithNewController(this Animator animator, AnimationClip animation, float fadeInDuration)
		{
			var graph = PlayableGraph.Create();
			var output = AnimationPlayableOutput.Create(graph, "Animation", animator);
			var clipPlayable = AnimationClipPlayable.Create(graph, animation);
			output.SetSourcePlayable(clipPlayable);

			graph.Play();

			await UniTask.WaitUntil(() => 
				clipPlayable.GetTime() >= animation.length
			);

			graph.Stop();
			graph.Destroy();
		}
	}
