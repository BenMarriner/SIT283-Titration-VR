using UnityEngine;
using static Indicator;

public static class IndicatorFactory
{
    public enum IndicatorTypes
    {
        Universal,
        Phenolphthalein,
        Methyl,
        Thymol,
        Bromothymol,
        Phenol
    }

    public static Indicator AttachIndicator(IndicatorTypes type, GameObject liquid)
    {
        Indicator component;
        
        switch (type)
        {
            case IndicatorTypes.Universal:
                component = liquid.AddComponent<Universal>();
                break;
            case IndicatorTypes.Phenolphthalein:
                component = liquid.AddComponent<Phenolphthalein>();
                break;
                case IndicatorTypes.Methyl:
                component = liquid.AddComponent<Methyl>();
                break;
            case IndicatorTypes.Thymol:
                component = liquid.AddComponent<Thymol>();
                break;
            case IndicatorTypes.Bromothymol:
                component = liquid.AddComponent<Bromothymol>();
                break;
            case IndicatorTypes.Phenol:
                component = liquid.AddComponent<Phenol>();
                break;
            default:
                component = liquid.AddComponent<Universal>();
                break;
        }

        return component;
    }
}