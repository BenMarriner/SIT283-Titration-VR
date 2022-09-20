using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Android;

public abstract class Liquid : MonoBehaviour
{
    public static readonly float pHScaleMin = 0.0f, pHScaleMax = 14.0f;
    public static readonly Color pureWater = new Color(0.15f, 0.15f, 0.15f);
    
    [Min(0.0f)]
    public float currentVolume;
    [Min(0.0f)]
    public float maxVolume;

    public Material material;
    public Color liquidColour;
    public readonly float liquidTransparency = 0.5f;

    // pH level for water in its purest form 
    public float pH = 7.0f;
    public float pHNormalised { get { return pH / pHScaleMax; } }

    protected virtual void Start()
    {
        liquidColour.a = liquidTransparency;
        GetComponent<MeshRenderer>().material = material;
    }

    protected virtual void Update()
    {
        GetComponent<MeshRenderer>().material.color = liquidColour;
    }

    private void OnValidate()
    {
        if (currentVolume > maxVolume)
        {
            currentVolume = maxVolume;
        }

        if (pH < pHScaleMin) pH = pHScaleMin;
        else if (pH > pHScaleMax) pH = pHScaleMax;
    }

    // When this liquid merges with another liquid
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Liquid>(out var otherLiquid))
        {
            // The smaller liquid merges into the larger liquid
            if (otherLiquid.currentVolume < currentVolume)
            {
                otherLiquid.currentVolume += currentVolume;
                Destroy(gameObject);
            }
        }
    }
}