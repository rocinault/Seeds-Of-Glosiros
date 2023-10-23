using UnityEngine;
using System.Collections;

namespace UpgradeSystem
{
	public class DisplayNameAttribute : PropertyAttribute {
		public readonly string defaultName;

		public DisplayNameAttribute (string defaultName) {
			this.defaultName = defaultName;
		}
	}
}

