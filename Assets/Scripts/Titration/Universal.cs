using UnityEngine;

public class Universal : Indicator
{
    private void Start()
    {
        info = new IndicatorInfo(new float[] { 0.0f }, new float[] { 14.0f }, Color.red, new Color(0.627f, 0.125f, 0.941f));
    }
}