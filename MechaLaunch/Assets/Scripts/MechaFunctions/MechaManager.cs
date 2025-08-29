using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MechaManager : MonoBehaviour, IDamageable
{
    public MechaShootingController gunnerSeat
    {
        get;
        private set;
    }
    public MechaMovementController driverSeat
    {
        get;
        private set;
    }
    public MechaEnergyController engineerSeat
    {
        get;
        private set;
    }

    float batteryMaxCharge = 100f;

    //Batteries
    Container repairBattery;
    Container combatBattery;
    Container movementBattery;

    MonsterManager mainMonster;

    float maxHealth = 100f;
    float currentHealth;

    Container staminaContainer;
    float maxStamina = 100f;

    public void SetMecha(MonsterManager MainMonster)
    {
        mainMonster = MainMonster;

        currentHealth = maxHealth;
        GameEvents.InvokeContainerCreated(maxHealth, ContainerType.HEALTH,EventSender.MECHA);

        staminaContainer = gameObject.AddComponent<Container>();
        staminaContainer.setContainer(maxStamina, ContainerType.STAMINA, EventSender.MECHA);
    }

    public void StartMecha()
    {
        //set up the batteries
        combatBattery = gameObject.AddComponent<Container>();
        combatBattery.setContainer(batteryMaxCharge, ContainerType.COMBAT, EventSender.MECHA);
        repairBattery = gameObject.AddComponent<Container>();
        repairBattery.setContainer(batteryMaxCharge, ContainerType.REPAIR, EventSender.MECHA);
        movementBattery = gameObject.AddComponent<Container>();
        movementBattery.setContainer(batteryMaxCharge, ContainerType.MOVING, EventSender.MECHA);

        //set up the cockpits
        gunnerSeat = gameObject.AddComponent<MechaShootingController>();
        gunnerSeat.SetCockpit(combatBattery, mainMonster);

        driverSeat = gameObject.AddComponent<MechaMovementController>();
        driverSeat.SetCockpit(movementBattery, staminaContainer);

        engineerSeat = gameObject.AddComponent<MechaEnergyController>();
        engineerSeat.InstallBatteries(repairBattery, combatBattery, movementBattery);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        GameEvents.InvokeContainerValueChange(currentHealth, ContainerType.HEALTH, EventSender.MECHA);
        if (currentHealth <= 0)
            Death();
    }

    public void Death()
    {
        Debug.Log("Mecha Destroyed");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            TakeDamage(10);
        }
    }
}
