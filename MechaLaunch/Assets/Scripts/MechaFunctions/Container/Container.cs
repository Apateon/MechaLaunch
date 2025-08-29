using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Container : MonoBehaviour
{
    float maxCapacity;
    float currentLevel;

    bool canRegen = false;
    bool selfRegen = false;
    float regenTimer = 0f;

    public ContainerType containerType
    {
        private set;
        get;
    }

    protected void Start()
    {
        currentLevel = maxCapacity;
    }

    public void setContainer(float MaxCapacity, ContainerType type, EventSender sender)
    {
        maxCapacity = MaxCapacity;
        containerType = type;

        //fire the evernt that sets the visuals of the battery
        GameEvents.InvokeContainerCreated(maxCapacity, containerType, sender);
        if(type == ContainerType.STAMINA)
        {
            canRegen = true;
        }
    }

    public bool CanDischarge(float amount)
    {
        if (currentLevel >= amount)
        {
            return true;
        }
        return false;
    }

    public void Discharge(float amount, EventSender sender)
    {
        currentLevel -= amount;
        GameEvents.InvokeContainerValueChange(currentLevel, containerType, sender);
        selfRegen = false;
        regenTimer = 0f;
    }

    public void Recharge(float amount, EventSender sender)
    {
        currentLevel = Mathf.Min(currentLevel + amount, maxCapacity);
        GameEvents.InvokeContainerValueChange(currentLevel, containerType, sender);
    }

    private void Update()
    {
        regenTimer += Time.deltaTime;
        if(regenTimer > 1f && canRegen)
        {
            selfRegen = true;
        }

        if(selfRegen)
        {
            Recharge(0.1f, EventSender.MECHA);
        }
    }
}
