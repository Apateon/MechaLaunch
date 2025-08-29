using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action<float, ContainerType, EventSender> OnContainerCreated;
    public static event Action<float, ContainerType, EventSender> OnContainerValueChanged;
    public static event Action<ContainerType> ActiveBatteryChanged;

    public static void InvokeContainerCreated(float maxCharge, ContainerType type, EventSender sender)
    {
        OnContainerCreated?.Invoke(maxCharge, type, sender);
    }

    public static void InvokeContainerValueChange(float currentCharge, ContainerType type, EventSender sender)
    {
        OnContainerValueChanged?.Invoke(currentCharge, type, sender);
    }

    public static void InvokeActiveBatteryChanged(ContainerType type)
    {
        ActiveBatteryChanged?.Invoke(type);
    }
}
