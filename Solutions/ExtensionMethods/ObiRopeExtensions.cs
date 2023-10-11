public static class ObiRopeExtensionMethods
	{
		public static Vector3 GetCenterOfRopePosition(this ObiRope rope)
		{
			int centerParticle = (rope.elements.Count - 1) / 2;
			var centralRopePoint = rope.solver.transform.TransformPoint(rope.solver.positions[centerParticle]);

			return centralRopePoint;
		}
		
		public static Vector3 GetPositionOfParticle(this ObiRope rope, int particleIndex) 
			=> rope.solver.transform.TransformPoint(rope.solver.positions[particleIndex]);
	}
