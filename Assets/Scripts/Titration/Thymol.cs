using UnityEngine;

public class Thymol : Indicator
{
    protected override void Start()
    {
        info = new IndicatorInfo
        (
            new float[] { 1.2f, 8.0f },
            new float[] { 2.8f, 9.6f }, 
            new Color[] { new Color (1.0f, 0.0f, 1.0f), Color.yellow },
            new Color[] { Color.yellow, new Color(0.173f, 0.353f, 0.651f) }
        );

        base.Start();
    }
}