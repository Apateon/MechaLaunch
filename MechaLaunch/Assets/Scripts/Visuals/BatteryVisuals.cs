using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryVisuals : MonoBehaviour
{
    Slider batterySlider;
    ContainerType batteryType;

    Image activeMarker;

    private void OnEnable()
    {
        batterySlider = GetComponent<Slider>();

        activeMarker = transform.Find("ActiveMarker").GetComponent<Image>();

        //Get the type of battery that the UI is
        String[] batteryTypes = Enum.GetNames(typeof(ContainerType));
        int underscoreNumber = this.name.LastIndexOf('_');
        String extract = this.name.Substring(underscoreNumber + 1);
        batteryType = (ContainerType)Array.IndexOf(batteryTypes, extract);

        GameEvents.OnContainerCreated += BatteryInitialize;
        GameEvents.OnContainerValueChanged += ChargeChange;
        GameEvents.ActiveBatteryChanged += BatteryChange;
    }

    private void BatteryInitialize(float maxCharge, ContainerType type, EventSender sender)
    {
        if (batteryType != type) return;

        batterySlider.maxValue = maxCharge;
        batterySlider.value = maxCharge;
    }

    private void ChargeChange(float charge, ContainerType type, EventSender sender)
    {
        if (batteryType != type) return;

        batterySlider.value = charge;
    }

    private void BatteryChange(ContainerType type)
    {
        if (batteryType != type)
        {
            activeMarker.gameObject.SetActive(false);
            return;
        }

        activeMarker.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        GameEvents.OnContainerCreated -= BatteryInitialize;
        GameEvents.OnContainerValueChanged -= ChargeChange;
    }
}
