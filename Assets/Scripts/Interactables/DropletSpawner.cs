using System;
using System.CodeDom;
using UnityEditor;
using UnityEngine;

public class DropletSpawner : MonoBehaviour
{
    private float _currTime;
    
    public bool timerEnabled = true;
    public float frequency = 1.0f;
    public GameObject dropletPrefab;
    public float dropletSize;
    public IndicatorFactory.IndicatorTypes indicator;

    private void Start()
    {
        _currTime = frequency;
    }

    private void Update()
    {
        if (timerEnabled)
            _currTime -= Time.deltaTime;
        else
            TimerReset();

        if (_currTime <= 0)
        {
            SpawnDroplet();
            TimerReset();
        }
    }

    private void SpawnDroplet()
    {
        var droplet = Instantiate(dropletPrefab, transform.position, transform.rotation);
        var indicatorComponent = IndicatorFactory.AttachIndicator(indicator, droplet);
        indicatorComponent.currentVolume = dropletSize;
        indicatorComponent.isContained = false;
    }

    private void TimerReset()
    {
        _currTime = frequency;
    }
}