using UnityEngine;

public class TestIndicator : Liquid
{
    IndicatorInfo indicator1;
    IndicatorInfo indicator2;
    IndicatorInfo indicator3;

    protected override void Start()
    {
        indicator1 = new IndicatorInfo(new float[] { 1.0f }, new float[] { 5.0f }, Color.red, Color.green);
        indicator2 = new IndicatorInfo(new float[] { 0.0f, 6.0f }, new float[] { 4.0f, 10.0f }, Color.blue, Color.yellow);
        indicator3 = new IndicatorInfo(new float[] { 0.0f }, new float[] { 14.0f }, Color.red, new Color(0.627f, 0.125f, 0.941f));

        indicator3.AddColor(7.0f, Color.green);

        base.Start();
    }

    protected override void Update()
    {
        var indicator = indicator3;

        liquidColour = indicator.GetPHColor(pH);

        base.Update();
    }
}