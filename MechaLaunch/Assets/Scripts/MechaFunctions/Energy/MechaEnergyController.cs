using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MechaEnergyController : MonoBehaviour
{
    InputAction recharge;
    InputAction changeBattery;

    Container[] batteries = new Container[3];

    int activeBattery = 0;

    float baseRecharge = 1f;

    public void InstallBatteries(Container RepairBattery, Container CombatBattery, Container MovementBattery)
    {
        batteries[0] = CombatBattery; 
        batteries[1] = RepairBattery;
        batteries[2] = MovementBattery;

        Debug.Log("Batteries Installed");

        ChangeActive(0);
    }

    public void SetEngineer(InputActionAsset actionasset)
    {
        recharge = actionasset.FindActionMap("Energy").FindAction("Recharge");
        recharge.performed += Recharge;

        changeBattery = actionasset.FindActionMap("Energy").FindAction("BatterySelect");
        changeBattery.performed += ChangeBattery;
    }

    private void Recharge(InputAction.CallbackContext context)
    {
        batteries[activeBattery].Recharge(baseRecharge, EventSender.MECHA);
    }

    private void ChangeBattery(InputAction.CallbackContext context)
    {
        ChangeActive((int)context.ReadValue<float>());
    }

    void ChangeActive(int batteryChange)
    {
        activeBattery += batteryChange;
        activeBattery = Mathf.Clamp(activeBattery, 0, 2);

        GameEvents.InvokeActiveBatteryChanged(batteries[activeBattery].containerType);
    }
}
