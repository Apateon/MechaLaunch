using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthVisuals : MonoBehaviour
{
    Slider healthSlider;

    private void OnEnable()
    {
        healthSlider = GetComponent<Slider>();

        GameEvents.OnContainerCreated += HealthInitialize;
        GameEvents.OnContainerValueChanged += HealthChange;
    }

    private void HealthChange(float currentHealth, ContainerType type, EventSender sender)
    {
        if(type != ContainerType.HEALTH) return;

        healthSlider.value = currentHealth;
    }

    private void HealthInitialize(float maxHealth, ContainerType type, EventSender sender)
    {
        if(type != ContainerType.HEALTH) return;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    private void OnDestroy()
    {
        GameEvents.OnContainerCreated -= HealthInitialize;
        GameEvents.OnContainerValueChanged -= HealthChange;
    }
}
