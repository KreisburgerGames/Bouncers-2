using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject[] keepOnSettingsLoad;

    private void Awake()
    {

        DontDestroyOnLoad(this.gameObject);
    }
}
