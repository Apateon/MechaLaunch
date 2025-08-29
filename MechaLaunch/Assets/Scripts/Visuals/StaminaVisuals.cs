using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaVisuals : MonoBehaviour
{
    Slider staminaSlider;

    private void OnEnable()
    {
        staminaSlider = GetComponent<Slider>();

        GameEvents.OnContainerCreated += StaminaInitialize;
        GameEvents.OnContainerValueChanged += StaminaChange;
    }

    private void StaminaInitialize(float maxStamina, ContainerType type, EventSender sender)
    {
        if (type != ContainerType.STAMINA) return;

        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
    }

    private void StaminaChange(float currentStamina, ContainerType type, EventSender sender)
    {
        if (type != ContainerType.STAMINA) return;

        staminaSlider.value = currentStamina;
    }
    private void OnDestroy()
    {
        GameEvents.OnContainerCreated -= StaminaInitialize;
        GameEvents.OnContainerValueChanged -= StaminaChange;
    }
}
