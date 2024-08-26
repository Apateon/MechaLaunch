using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MechaShootingController : MonoBehaviour
{
    InputAction shoot;

    public void SetGunner(InputActionAsset actionasset)
    {
        shoot = actionasset.FindActionMap("Shooting").FindAction("Fire");
        shoot.performed += Fire;
    }

    private void Fire(InputAction.CallbackContext ctx)
    {
        Debug.Log("Fired");
    }
}
