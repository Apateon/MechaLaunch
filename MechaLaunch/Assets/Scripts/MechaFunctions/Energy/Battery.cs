using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    float maxCharge;
    float currentCharge;
    public BatteryType batteryType
    {
        private set;
        get;
    }

    protected void Start()
    {
        currentCharge = maxCharge;
    }

    public void setBattery(float MaxCharge, BatteryType type)
    {
        maxCharge = MaxCharge;
        batteryType = type;

        //fire the evernt that sets the visuals of the battery
        GameEvents.InvokeBatteryCreated(maxCharge, batteryType);
    }

    public bool CanDischarge(float amount)
    {
        if (currentCharge >= amount)
            return true;
        return false;
    }

    public void Discharge(float amount)
    {
        currentCharge -= amount;
        GameEvents.InvokeBatteryChange(currentCharge, batteryType);
    }

    public void Recharge(float amount)
    {
        currentCharge = Mathf.Min(currentCharge + amount, maxCharge);
        GameEvents.InvokeBatteryChange(currentCharge, batteryType);
    }
}
