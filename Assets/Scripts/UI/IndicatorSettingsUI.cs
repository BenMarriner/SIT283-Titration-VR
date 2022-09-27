using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorSettingsUI : MonoBehaviour
{
    public DropletSpawner dropletSpawner;
    public Liquid beakerLiquid;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIndicatorUniversal()
    {
        SetIndicator(IndicatorFactory.IndicatorTypes.Universal);
    }

    public void SetIndicatorPhenolphthalein()
    {
        SetIndicator(IndicatorFactory.IndicatorTypes.Phenolphthalein);
    }

    public void SetIndicatorMethyl()
    {
        SetIndicator(IndicatorFactory.IndicatorTypes.Methyl);
    }

    public void SetINdicatorThymol()
    {
        SetIndicator(IndicatorFactory.IndicatorTypes.Thymol);
    }

    public void SetIndicatorBromothymol()
    {
        SetIndicator(IndicatorFactory.IndicatorTypes.Bromothymol);
    }

    public void SetIndicatorPhenol()
    {
        SetIndicator(IndicatorFactory.IndicatorTypes.Phenol);
    }

    private void SetIndicator(IndicatorFactory.IndicatorTypes type)
    {
        dropletSpawner.indicator = type;
    }

    public void RefillLiquid()
    {
        beakerLiquid.liquidColour = Liquid.pureWater;
        beakerLiquid.pH = Liquid.pureWaterPH;
    }

    public void RandomiseLiquidPH()
    {
        float newPH = Random.Range(Liquid.pHScaleMin, Liquid.pHScaleMax);
        beakerLiquid.pH = newPH;
    }
}
