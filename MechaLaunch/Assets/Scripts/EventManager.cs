using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager 
{
    public static UnityAction<Component, int> OnValueChanged;

    public static UnityAction OnTriggerHappened;
}
