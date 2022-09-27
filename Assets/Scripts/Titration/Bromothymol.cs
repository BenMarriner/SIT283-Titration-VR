using UnityEngine;

public class Bromothymol : Indicator
{
    protected override void Start()
    {
        info = new IndicatorInfo
        (
            new float[] { 6.0f },
            new float[] { 7.6f }, 
            new Color[] { Color.yellow },
            new Color[] { new Color(0.0f, 0.514f, 0.796f) }
        );
        info.AddColor(Mathf.Lerp(info.minPHRanges[0], info.maxPHRanges[0], 0.5f), new Color(0.0f, 0.537f, 0.357f));

        base.Start();
    }
}