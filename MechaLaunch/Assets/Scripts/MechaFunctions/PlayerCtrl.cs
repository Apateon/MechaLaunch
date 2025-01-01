using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    int controllerIndex;
    Color playerColor;

    InputActionMap playerControl;

    private void OnEnable()
    {
        playerControl.Enable();
    }

    public void Config(int index, InputActionMap control)
    {
        controllerIndex = index;

        playerColor = PlayerColorManager.playerColours[controllerIndex];

        playerControl = control;
    }
}
