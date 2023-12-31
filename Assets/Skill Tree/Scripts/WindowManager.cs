﻿using UnityEngine;
using System.Collections;

namespace UpgradeSystem
{
	public class WindowManager : MonoBehaviour {
		[SerializeField] GameObject windowListing;
		[SerializeField] GameObject window1;
		[SerializeField] GameObject window2;
		[SerializeField] GameObject window3;
		[SerializeField] GameObject window4;

		[SerializeField] GameObject close;

		public void ShowWindow1 () {
			window1.SetActive(true);
			close.SetActive(true);
			windowListing.SetActive(false);
		}

		public void ShowWindow2 () {
			window2.SetActive(true);
			close.SetActive(true);
			windowListing.SetActive(false);
		}

		public void ShowWindow3 () {
			window3.SetActive(true);
			close.SetActive(true);
			windowListing.SetActive(false);
		}

		public void ShowWindow4()
		{
			window4.SetActive(true);
			close.SetActive(true);
			windowListing.SetActive(false);
		}

		public void Close () {
			close.SetActive(false);
			window1.SetActive(false);
			window2.SetActive(false);
			window3.SetActive(false);
			window4.SetActive(false);
			windowListing.SetActive(true);
		}
	}
}

