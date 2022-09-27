using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    private GameObject _canvas;
    public bool active = true;

    private void Start()
    {
        _canvas = transform.Find("Canvas").gameObject;
    }

    private void Update()
    {
        _canvas.SetActive(active);
    }

    public void StartButtonClicked()
    {
        active = !active;
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
