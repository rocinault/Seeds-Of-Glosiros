using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPlayerInformation : MonoBehaviour {

   [HideInInspector] public static AllPlayerInformation instance;
	public List<PlayerInfoContainer> playersInfo = new List<PlayerInfoContainer>(4);

	#region Singleton
	void Awake()
    {
        // Set Instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);
    }
    #endregion
}
