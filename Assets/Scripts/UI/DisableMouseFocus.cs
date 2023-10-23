using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DisableMouseFocus : MonoBehaviour {

	private EventSystem eventSystem;
	private GameObject lastSelected;

	void Awake ()
	{
		eventSystem = GetComponent<EventSystem>();
	}

	void Update ()
	{
		if (eventSystem.currentSelectedGameObject == null)
			eventSystem.SetSelectedGameObject(lastSelected);

		else
			lastSelected = eventSystem.currentSelectedGameObject;
	}
}
