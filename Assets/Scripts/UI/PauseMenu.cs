using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(StartScreen))]
public class PauseMenu : MonoBehaviour
{
    private StartScreen startScreen;

    public SteamVR_Action_Boolean pauseAction;
    public SteamVR_Input_Sources handType;

    private void Start()
    {
        startScreen = GetComponent<StartScreen>();
        startScreen.active = false;
    }

    private void Update()
    {
        if (pauseAction.GetStateDown(handType))
        {
            startScreen.active = !startScreen.active;
        }
    }
}
