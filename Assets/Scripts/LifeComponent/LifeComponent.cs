using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeComponent : MonoBehaviour
{
    [SerializeField]
    private uint maxValue = 100;

    public uint Value
    {
        get => value;
        set
        {
            if (this.value != value)
            {
                this.value = value;
                if(ValueChanged != null)
                {
                    ValueChanged.Invoke(value);
                }
            }
        }
    }

    public uint MaxValue => maxValue;

    private uint value = 0;

    public event Action<uint> ValueChanged;

    private void Awake()
    {
        Value = maxValue;
    }

    public void TackDamage(uint damage)
    {
        if(Value > damage)
        {
            Value -= damage;
        }
        else
        {
            Value = 0;
        }
    }

    public void ResetValue()
    {
        Value = MaxValue;
    }
}
