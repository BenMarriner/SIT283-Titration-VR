using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Android;

public class Liquid : MonoBehaviour
{
    private Material liquidMaterial;

    public static readonly float pHScaleMin = 0.0f, pHScaleMax = 14.0f;
    public static readonly Color pureWater = new Color(0.4f, 0.4f, 0.4f);
    public static readonly float pureWaterPH = 7.0f;
    
    [Min(0.0f)]
    public float currentVolume;
    [Min(0.0f)]
    public float maxVolume;

    public Material material;
    public Color liquidColour = pureWater;
    public readonly float liquidTransparency = 0.5f;

    // pH level for water in its purest form 
    public float pH = 7.0f;
    public float pHNormalised { get { return pH / pHScaleMax; } }
    public bool isContained = false;

    protected virtual void Start()
    {
        liquidColour.a = liquidTransparency;
        GetComponent<MeshRenderer>().material = material;
        liquidMaterial = GetComponent<MeshRenderer>().material;
    }

    protected virtual void Update()
    {
        GetComponent<MeshRenderer>().material.color = liquidColour;
        //liquidMaterial.SetColor("Colour", liquidColour);
        //liquidMaterial.SetFloat("MaxLevel", maxVolume);
        //liquidMaterial.SetFloat("Level", currentVolume);
        
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
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<Liquid>(out var otherLiquid))
        {
            // The smaller liquid merges into the larger liquid
            if (currentVolume < otherLiquid.currentVolume)
            {
                // Transfer contents of one liquid to the other
                //otherLiquid.currentVolume += currentVolume;
                PourLiquid(currentVolume);
                otherLiquid.FillLiquid(currentVolume);
            }
        }
        
        if (!isContained) Destroy(gameObject);

    }

    /// <summary>
    /// Reduce liquid volume by x amount
    /// </summary>
    /// <param name="volume"></param>
    public void PourLiquid(float volume)
    {
        currentVolume -= volume;
    }

    /// <summary>
    /// Increase liquid volume by x amount
    /// </summary>
    /// <param name="volume"></param>
    public void FillLiquid(float volume)
    {
        currentVolume += volume;
    }
}
