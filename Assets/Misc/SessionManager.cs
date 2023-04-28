using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        LoadingScreenController.LoadSceneWithLoadingScreen("MainMenu");
    }
}
