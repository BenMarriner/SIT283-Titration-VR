using UnityEngine;

/// <summary>
/// Stores information and properties about an indicator liquid
/// </summary>
public struct IndicatorInfo
{
    private ColourCurve _indicatorColours;
    
    public readonly float[] minPHRanges, maxPHRanges;
    public readonly Color[] minPHColours, maxPHColours;

    public IndicatorInfo(float[] minPHs, float[] maxPHs, Color[] minPHColours, Color[] maxPHColours)
    {
        minPHRanges = minPHs;
        maxPHRanges = maxPHs;
        this.minPHColours = minPHColours;
        this.maxPHColours = maxPHColours;

        // Defines a new colour curve with the pH scale as the range
        _indicatorColours = new ColourCurve(Liquid.pHScaleMin, Liquid.pHScaleMax, minPHColours[0], maxPHColours[maxPHRanges.Length - 1]);

        if (minPHRanges.Length != maxPHRanges.Length)
        {
            Debug.LogError("Min and max pH range arrays are not equal in length. Please fix");
            return;
        }

        if (maxPHColours.Length != minPHColours.Length)
        {
            Debug.LogError("Min and max pH colour range arrays are not equal in length. Please fix");
            return;
        }

        if (minPHColours.Length != minPHRanges.Length)
        {
            Debug.LogError("Min and max pH colour range array length does not match the length of the pH range array. Please fix");
            return;
        }

        for (int i = 0; i < minPHRanges.Length; i++)
        {
            _indicatorColours.SetValueAtTime(minPHRanges[i], minPHColours[i], ColourCurve.FalloffType.Linear);
            _indicatorColours.SetValueAtTime(maxPHRanges[i], maxPHColours[i], ColourCurve.FalloffType.Linear);
        }
    }

    public Color GetPHColor(float pH)
    {
        return _indicatorColours.GetValueAtTime(pH);
    }

    public void AddColor(float pH, Color colour)
    {
        _indicatorColours.SetValueAtTime(pH, colour, ColourCurve.FalloffType.Linear);
    }
}

/// <summary>
/// Base class for pH indicators. Indicator info will be set according to the properties of each indicator used during titration procedures
/// </summary>
public abstract class Indicator : Liquid
{
    public IndicatorInfo info;

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Liquid>(out var otherLiquid))
        {
            // Apply pH
            if (currentVolume < otherLiquid.currentVolume)
            {
                otherLiquid.liquidColour = info.GetPHColor(otherLiquid.pH);
            }

            base.OnCollisionEnter(collision);
        }
    }
}

