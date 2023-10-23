﻿using UnityEngine;
using System.Collections;

namespace UpgradeSystem
{
	static public class Helper {
		static public Quaternion Rotate2D (Vector3 start, Vector3 end) {
			Vector3 diff = start - end;
			diff.Normalize();
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			return Quaternion.Euler(0f, 0f, rot_z - 90f);
		}
	}
}
