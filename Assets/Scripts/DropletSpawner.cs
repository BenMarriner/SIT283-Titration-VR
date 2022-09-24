using UnityEditor;
using UnityEngine;

public class DropletSpawner : MonoBehaviour
{
    private float _currTime;
    
    public bool timerEnabled = true;
    public float frequency = 1.0f;
    public GameObject dropletPrefab;
    public float dropletSize;

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
            var droplet = Instantiate(dropletPrefab, transform.position, transform.rotation);
            droplet.GetComponent<Liquid>().currentVolume = dropletSize;
            TimerReset();
        }
    }

    private void TimerReset()
    {
        _currTime = frequency;
    }
}