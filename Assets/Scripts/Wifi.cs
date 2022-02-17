using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wifi : MonoBehaviour
{
    public static Wifi Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
