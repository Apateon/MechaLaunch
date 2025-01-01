using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : ArmWeapon
{
    public Gun() // use an actual virtual fucntion instead of the contructor
    {
        fireRate = 0.5f;
        damageInflicted = 10;
        batteryDrain = 10;
        base.Init();
    }

    public override bool Fire(Vector3 position, Vector3 aimDirection)
    {
        if(fireTimer > fireRate)
        {
            if (Physics.Raycast(position, aimDirection, out RaycastHit hit))
            {
                hit.transform.GetComponent<MonsterManager>().Damage(damageInflicted);
                Debug.Log(hit.transform.name);
            }
            fireTimer = 0;
            return true;
        }
        else
        {
            fireTimer += Time.deltaTime;
        }
        Debug.Log("Weapon fired");
        return false;
    }
}
