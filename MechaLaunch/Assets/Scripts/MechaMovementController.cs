using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MechaMovementController : MonoBehaviour
{
    CharacterController mechaCharacterController;

    InputAction movement;
    InputAction sprint;

    Transform mainCam;

    private void Awake()
    {
        mechaCharacterController = GetComponent<CharacterController>();
        mainCam = Camera.main.transform;
    }

    public void SetPilot(InputActionAsset actionasset)
    {
        movement = actionasset.FindActionMap("Movement").FindAction("Walking");
        sprint = actionasset.FindActionMap("Movement").FindAction("Sprint");
    }

    private void Update()
    {
        if (movement != null || sprint != null)
        {
            float sprintMult = sprint.IsPressed() ? 5 : 1;
            Vector2 inputVector = movement.ReadValue<Vector2>();
            Vector3 dirVector = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
            dirVector = mainCam.TransformDirection(dirVector);
            dirVector.Normalize();
            float speed = 5f;
            mechaCharacterController.Move(dirVector * speed * Time.deltaTime * sprintMult);

            transform.rotation = Quaternion.Euler(0f, mainCam.rotation.eulerAngles.y, 0f);
        }
    }
}
