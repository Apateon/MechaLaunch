using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class MechaShootingController : MonoBehaviour
{
    InputAction shoot;
    InputAction aimLeft;
    InputAction aimRight;
    InputAction aiming;

    bool isAiming = false;
    bool isAimingRight = false;
    bool isAimingLeft = false;
    float aimSense = 0.0005f;
    Vector3 aimDirection;

    public GameObject marker;

    ArmWeapon leftWeapon = new Gun();
    ArmWeapon rightWeapon = new RailGun();

    MonsterManager monsterKaiju;

    Battery shootingBattery;

    public void SetGunner(InputActionAsset actionasset)
    {
        shoot = actionasset.FindActionMap("Shooting").FindAction("Fire");

        aimLeft = actionasset.FindActionMap("Shooting").FindAction("AimLeft");
        aimLeft.performed += StartAim;
        aimLeft.canceled += StopAim;

        aimRight = actionasset.FindActionMap("Shooting").FindAction("AimRight");
        aimRight.performed += StartAim;
        aimRight.canceled += StopAim;

        aiming = actionasset.FindActionMap("Shooting").FindAction("Aiming");

        //initialize armpons
    }

    public void SetCockpit(Battery shooting, MonsterManager MainMonster)
    {

        monsterKaiju = MainMonster;

        aimDirection = (monsterKaiju.transform.position - transform.position);
        aimDirection.Normalize();

        marker = GameObject.Find("ShotDot");
        marker.SetActive(false);

        shootingBattery = shooting;
    }

    private void StartAim(InputAction.CallbackContext ctx)
    {
        if (ctx.action == aimLeft && !isAimingRight)
        {
            isAiming = true;
            isAimingLeft = true;
        }
        else if (ctx.action == aimRight && !isAimingLeft)
        {
            isAiming = true;
            isAimingRight = true;
        }
    }

    private void StopAim(InputAction.CallbackContext ctx)
    {
        if (ctx.action == aimLeft && isAimingLeft)
        {
            isAiming = false;
            isAimingLeft = false;
        }
        else if (ctx.action == aimRight && isAimingRight)
        {
            isAiming = false;
            isAimingRight = false;
        }
    }

    private void Update()
    {
        if (monsterKaiju == null) return;
        if (isAiming)
        {
            Vector2 input = aiming.ReadValue<Vector2>();
            aimDirection += new Vector3(input.x, input.y, 0) * aimSense;
            if (Physics.Raycast(transform.position, aimDirection, out RaycastHit hit))
            {
                marker.SetActive(true);
                marker.transform.position = hit.point;
            }
            else
            {
                marker.SetActive(false);
            }

            if(shoot.IsPressed())
            {
                if (isAimingLeft)
                {
                    Debug.Log("firing left");
                    if(shootingBattery.CanDischarge(leftWeapon.batteryDrain))
                    {
                        if(leftWeapon.Fire(transform.position, aimDirection))
                        {
                            shootingBattery.Discharge(leftWeapon.batteryDrain);
                        }
                    }
                }
                else if (isAimingRight)
                {
                    Debug.Log("firing right");
                    if(shootingBattery.CanDischarge(rightWeapon.batteryDrain))
                    {
                        if(rightWeapon.Fire(transform.position, aimDirection))
                        {
                            shootingBattery.Discharge(rightWeapon.batteryDrain);
                        }
                    }
                }
            }
        }
        else
        {
            marker.SetActive(false); 
            aimDirection = (monsterKaiju.transform.position - transform.position);
            aimDirection.Normalize();
        }
    }
}