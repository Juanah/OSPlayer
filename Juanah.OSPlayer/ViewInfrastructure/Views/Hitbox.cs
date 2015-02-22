using System;

namespace ViewInfrastructure
{
	public class Hitbox : IHitbox
	{
		public Hitbox ()
		{
		}

		public float[] LeftCorner{ get; set; }
		public float[] RightCorner{ get; set; }

		#region IHitbox implementation
		public bool IsHit (float[] offset, float[] cursor)
		{
			float[] absoluteLeft = new float[]{ (LeftCorner[0] + offset [0]), (LeftCorner[1] + offset[1])};
			float[] absoluteRight = new float[]{ (RightCorner[0] + offset [0]), (RightCorner[1] + offset[1])};

			bool leftx = false, lefty = false, rightx = false, righty = false;

			if (cursor [0] >= absoluteLeft [0]) {
				leftx = true;
			}

			if (cursor [1] <= absoluteLeft [1]) {
				lefty = true;
			}

			if (cursor[0] <= absoluteRight [0]) {
				rightx = true;
			}

			if (cursor [1] >= absoluteRight [1]) {
				righty = true;
			}
			if (leftx && lefty && rightx && righty) {
				return true;
			} else {
				return false;
			}
		}
		#endregion

	}
}

