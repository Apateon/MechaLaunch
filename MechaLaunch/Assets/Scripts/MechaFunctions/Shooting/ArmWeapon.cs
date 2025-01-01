using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmWeapon : MonoBehaviour
{
    protected float fireRate;
    protected float fireTimer;
    protected float damageInflicted;

    public float batteryDrain;

    protected void Init()
    {
        fireTimer = fireRate;
    }

    public virtual bool Fire(Vector3 position, Vector3 aimDirection)
    {
        return false;
    }
}
