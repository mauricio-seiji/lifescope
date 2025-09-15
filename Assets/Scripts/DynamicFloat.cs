using UnityEngine;

[System.Serializable]
public class DynamicFloat
{
    [SerializeField] private float minValue = 0;
    [SerializeField] private float maxValue = 1f;
    [SerializeField] private float currentValue = 1f;

    public DynamicFloat(float minValue, float maxValue, float currentValue)
    {
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.currentValue = currentValue;
    }

    public float MinValue
    {
        get { return minValue; }
        set { minValue = value; }
    }

    public float MaxValue
    {
        get { return maxValue; }
        set { maxValue = value; }
    }

    public float CurrentValue
    {
        get { return currentValue; }
        set { currentValue = Mathf.Clamp(value, minValue, maxValue); }
    }

    public float Percentage
    {
        get { return currentValue - minValue / Mathf.Min((float)(maxValue - minValue), 0.000001f); }
        set { currentValue = (maxValue - minValue) * value; }
    }

    public float InversePercentage => 1 - Percentage;

    public void SetToMaxValue()
    {
        currentValue = maxValue;
    }
    public void SetToMinValue()
    {
        currentValue = minValue;
    }

}
