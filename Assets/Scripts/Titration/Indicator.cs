using UnityEngine;

/// <summary>
/// Stores information and properties about an indicator liquid
/// </summary>
public struct IndicatorInfo
{
    private ColourCurve _indicatorColours;
    
    public readonly float[] minPHRanges, maxPHRanges;
    public readonly Color minPHColour, maxPHColour;

    public IndicatorInfo(float[] minPHs, float[] maxPHs, Color minPHColour, Color maxPHColour)
    {
        minPHRanges = minPHs;
        maxPHRanges = maxPHs;
        this.minPHColour = minPHColour;
        this.maxPHColour = maxPHColour;

        // Defines a new colour curve with the pH scale as the range
        _indicatorColours = new ColourCurve(Liquid.pHScaleMin, Liquid.pHScaleMax, minPHColour, maxPHColour);

        if (minPHRanges.Length != maxPHRanges.Length)
        {
            Debug.LogError("Min and max pH range arrays are not equal in length. Please fix");
            return;
        }

        for (int i = 0; i < minPHRanges.Length; i++)
        {
            _indicatorColours.SetValueAtTime(minPHRanges[i], minPHColour, ColourCurve.FalloffType.Linear);
            _indicatorColours.SetValueAtTime(maxPHRanges[i], maxPHColour, ColourCurve.FalloffType.Constant);
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

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Liquid>(out var otherLiquid))
        {
            // The smaller liquid merges into the larger liquid
            if (otherLiquid.currentVolume > currentVolume)
            {
                otherLiquid.liquidColour = info.GetPHColor(otherLiquid.pH);
            }

            base.OnTriggerEnter(other);
        }
    }
}

