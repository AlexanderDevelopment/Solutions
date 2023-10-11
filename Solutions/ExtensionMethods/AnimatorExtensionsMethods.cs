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
	}
