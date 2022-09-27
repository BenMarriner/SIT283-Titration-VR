using UnityEngine;
using UnityEngine.UIElements;

public class Phenolphthalein : Indicator
{
    protected override void Start()
    {
        info = new IndicatorInfo(new float[] { 8.3f }, new float[] { 10.0f }, new Color[] { pureWater }, new Color[] { new Color(1.0f, 0.0f, 1.0f) });
        info.AddColor(Mathf.Lerp(info.minPHRanges[0], info.maxPHRanges[0], 0.5f), pureWater);
        base.Start();
    }
}