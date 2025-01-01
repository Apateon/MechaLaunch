using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action<float, BatteryType> OnBatteryCreated;
    public static event Action<float, BatteryType> OnBatteryChanged;
    public static event Action<BatteryType> ActiveBatteryChanged;

    public static void InvokeBatteryCreated(float maxCharge, BatteryType type)
    {
        Debug.Log("Battery Created");
        OnBatteryCreated?.Invoke(maxCharge, type);
    }

    public static void InvokeBatteryChange(float currentCharge, BatteryType type)
    {
        OnBatteryChanged?.Invoke(currentCharge, type);
    }

    public static void InvokeActiveBatteryChanged(BatteryType type)
    {
        ActiveBatteryChanged?.Invoke(type);
    }
}
