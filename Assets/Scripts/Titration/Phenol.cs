using UnityEngine;

public class Phenol : Indicator
{
    protected override void Start()
    {
        info = new IndicatorInfo
        (
            new float[] { 6.4f },
            new float[] { 8.0f }, 
            new Color[] { new Color(0.0f, 0.859f, 0.345f) },
            new Color[] { new Color(1.0f, 0.0f, 1.0f) }
        );
        info.AddColor(Mathf.Lerp(info.minPHRanges[0], info.maxPHRanges[0], 0.5f), new Color(0.969f, 0.722f, 0.651f));

        base.Start();
    }
}