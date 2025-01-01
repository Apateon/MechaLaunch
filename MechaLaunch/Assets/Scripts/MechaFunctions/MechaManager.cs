using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaManager : MonoBehaviour
{
    float batteryMaxCharge = 100f;

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

    //Batteries
    Battery repairBattery;
    Battery combatBattery;
    Battery movementBattery;

    MonsterManager mainMonster;

    public void SetMecha(MonsterManager MainMonster)
    {
        mainMonster = MainMonster;
    }

    public void StartMecha()
    {
        //set up the batteries
        combatBattery = gameObject.AddComponent<Battery>();
        combatBattery.setBattery(batteryMaxCharge, BatteryType.COMBAT);
        repairBattery = gameObject.AddComponent<Battery>();
        repairBattery.setBattery(batteryMaxCharge, BatteryType.REPAIR);
        movementBattery = gameObject.AddComponent<Battery>();
        movementBattery.setBattery(batteryMaxCharge, BatteryType.MOVING);

        //set up the cockpits
        gunnerSeat = gameObject.AddComponent<MechaShootingController>();
        gunnerSeat.SetCockpit(combatBattery, mainMonster);

        driverSeat = gameObject.AddComponent<MechaMovementController>();
        driverSeat.SetCockpit(movementBattery);

        engineerSeat = gameObject.AddComponent<MechaEnergyController>();
        engineerSeat.InstallBatteries(repairBattery, combatBattery, movementBattery);
    }
}
