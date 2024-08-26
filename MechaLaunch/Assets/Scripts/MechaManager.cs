using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaManager : MonoBehaviour
{
    MechaShootingController gunnerseat;
    MechaMovementController driverseat;

    public MechaShootingController GunnerSeat
    {
        get => gunnerseat;
        set => gunnerseat = value;
    }
    public MechaMovementController DriverSeat
    {
        get => driverseat;
        set => driverseat = value;
    }

    private void Start()
    {
        gunnerseat = GetComponent<MechaShootingController>();
        driverseat = GetComponent<MechaMovementController>();
    }
}
