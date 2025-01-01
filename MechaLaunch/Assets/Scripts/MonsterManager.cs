using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterManager : MonoBehaviour
{

    float maxhealth = 100f;

    float health;

    private void Start()
    {
        health = maxhealth;
    }

    public virtual void Damage(float damage)
    {
        health -= damage;
        if(health <= 0)
            Death();
    }

    protected virtual void Death()
    {
        Debug.Log("Monster died");
    }
}
