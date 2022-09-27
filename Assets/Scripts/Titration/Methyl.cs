using UnityEngine;
using UnityEngine.UIElements;

public class Methyl : Indicator
{
    protected override void Start()
    {
        info = new IndicatorInfo(new float[] { 3.1f }, new float[] { 4.4f }, new Color[] { Color.red }, new Color[] { Color.yellow });
        info.AddColor(Mathf.Lerp(info.minPHRanges[0], info.maxPHRanges[0], 0.5f), Color.yellow);
        base.Start();
    }
}