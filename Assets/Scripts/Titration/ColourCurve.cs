using System.Linq.Expressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.GlobalIllumination;
using System.Runtime.InteropServices.WindowsRuntime;


public struct ColourCurveKeyPoint
{
    public float time;
    public Color colour;
    public ColourCurve.FalloffType falloff;

    public ColourCurveKeyPoint(float time, Color colour, ColourCurve.FalloffType falloff)
    {
        this.time = time;
        this.colour = colour;
        this.falloff = falloff;
    }

    public override string ToString()
    {
        string message = $"Time: {time}, Colour: {colour}, Falloff Type: {falloff}";
        return message;
    }
}

/// <summary>
/// A class for plotting values on a timeline
/// </summary>
/// <typeparam name="T"></typeparam>
public class ColourCurve
{
    private LinkedList<ColourCurveKeyPoint> _timelineValues;
    private float _timelineMin = 0.0f;
    private float _timelineMax = 1.0f;
    private Color _colourMin = Color.black;
    private Color _colourMax = Color.white;

    public float StartTime { get { return _timelineMin; } }
    public float EndTime { get { return _timelineMax; } }

    public ColourCurve()
    {
        _timelineValues = new LinkedList<ColourCurveKeyPoint>();

        ColourCurveKeyPoint first = new ColourCurveKeyPoint(_timelineMin, _colourMin, FalloffType.Constant);
        ColourCurveKeyPoint last = new ColourCurveKeyPoint(_timelineMax, _colourMax, FalloffType.Constant);

        _timelineMin = 0.0f;
        _timelineMax = 1.0f;
        _colourMin = Color.black;
        _colourMax = Color.black;

        _timelineValues.AddFirst(first);
        _timelineValues.AddLast(last);
    }

    public ColourCurve(float timelineMin, float timelineMax)
    {
        _timelineValues = new LinkedList<ColourCurveKeyPoint>();

        ColourCurveKeyPoint first = new ColourCurveKeyPoint(timelineMin, _colourMin, FalloffType.Constant);
        ColourCurveKeyPoint last = new ColourCurveKeyPoint(timelineMax, _colourMax, FalloffType.Constant);

        this._timelineMin = timelineMin;
        this._timelineMax = timelineMax;
        _colourMin = Color.black;
        _colourMax = Color.black;

        _timelineValues.AddFirst(first);
        _timelineValues.AddLast(last);
    }

    public ColourCurve(float timelineMin, float timelineMax, Color colourMin, Color colourMax)
    {
        _timelineValues = new LinkedList<ColourCurveKeyPoint>();

        ColourCurveKeyPoint first = new ColourCurveKeyPoint(timelineMin, colourMin, FalloffType.Constant);
        ColourCurveKeyPoint last = new ColourCurveKeyPoint(timelineMax, colourMax, FalloffType.Constant);

        this._timelineMin = timelineMin;
        this._timelineMax = timelineMax;
        this._colourMin = colourMin;
        this._colourMax = colourMax;

        _timelineValues.AddFirst(first);
        _timelineValues.AddLast(last);
    }

    /// <summary>
    /// Plot a colour value on the timeline at a particular time.
    /// There can only be one colour value per timestamp. Setting the colour at a time that already exists will replace it.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="value"></param>
    public void SetValueAtTime(float time, Color value, FalloffType falloff)
    {
        if (time < _timelineMin || time > _timelineMax)
        {
            Debug.LogWarning($"Attempted to plot a value outside the timeline range. (Time: {time})");
            return;
        }

        var newNode = new ColourCurveKeyPoint(time, value, falloff);
        foreach (var node in _timelineValues)
        {
            // Replace existing node
            if (time == node.time)
            {
                var prevNode = _timelineValues.Find(node).Previous;
                
                _timelineValues.Remove(node);

                if (prevNode != null) _timelineValues.AddAfter(prevNode, newNode);
                else _timelineValues.AddFirst(newNode); // Implies that we are trying to replace the first node in the list, in which case, there is no previous node

                return;
            }

            // Place the node between two existing ones
            if (time < node.time)
            {
                var nextNode = _timelineValues.Find(node);
                _timelineValues.AddBefore(nextNode, newNode);
                return;
            }
        }
    }

    /// <summary>
    /// Get a colour (or an interpolated colour) at a time on the timeline
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public Color GetValueAtTime(float time)
    {
        if (time < _timelineMin || time > _timelineMax)
        {
            Debug.LogWarning($"Tried to get the value at Time: {time} which is outside the timeline range. Returning default");
            return default;
        }

        // Find the two colours that this time value lies between
        ColourCurveKeyPoint item1 = default;
        ColourCurveKeyPoint item2 = default;

        foreach (var node in _timelineValues)
        {
            if (time == node.time) return node.colour;
            else if (time > node.time)
            {
                item1 = _timelineValues.Find(node).Value;
                item2 = _timelineValues.Find(node).Next.Value;
            }
        }

        // This should never happen but if it does, something weird has gone wrong with the timeline
        if (item1.Equals(default) || item2.Equals(default))
        {
            Debug.LogError("Something may be wrong with this timeline. Please debug to check for the cause of the problem");
            return default;
        }

        // Interpolate between the two colours
        float a = item1.time;
        float b = item2.time;
        float c = time;

        float timeNormalised = (c - a) / (b - a);

        return EvaluateColor(item1.colour, item2.colour, timeNormalised, item1.falloff);
    }

    /// <summary>
    /// Evaluate the interpolated colour. Always evaluated using the first colour's falloff type
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    /// <param name="t"></param>
    /// <param name="falloff"></param>
    /// <returns></returns>
    private Color EvaluateColor(Color c1, Color c2, float t, FalloffType falloff)
    {
        switch (falloff)
        {
            case FalloffType.Constant:
                return Constant(c1, c2, t);
            case FalloffType.Linear:
                return Linear(c1, c2, t);
            default:
                return Linear(c1, c2, t);
        }
    }

    public enum FalloffType { Constant, Linear }

    private static Color Linear(Color c1, Color c2, float t)
    {
        return Color.Lerp(c1, c2, t);
    }

    private static Color Constant(Color c1, Color c2, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);

        if (t < 1.0f) return c1;
        else return c2;
    }

    public override string ToString()
    {
        string message = "";

        foreach (var node in _timelineValues)
        {
            message += $"{node}\n";
        }

        return message;
    }
}