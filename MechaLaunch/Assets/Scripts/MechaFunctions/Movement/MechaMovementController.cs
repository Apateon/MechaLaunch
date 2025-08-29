using UnityEngine;
using UnityEngine.InputSystem;

public class MechaMovementController : MonoBehaviour
{
    CharacterController mechaCharacterController;

    InputAction movement;
    InputAction sprint;

    Transform mainCam;

    Container movingBattery;
    float batteryDrain = 3f;

    Container staminaContainer;

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
            HandleMovement();
        }
    }

    public void SetCockpit(Container moving, Container stamina)
    {
        movingBattery = moving;
        staminaContainer = stamina;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = movement.ReadValue<Vector2>();

        Vector3 forward = mainCam.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 moveVector = (forward * inputVector.y + mainCam.right * inputVector.x).normalized;

        if (inputVector.magnitude >= 0.1f)
        {
            float sprintMult = 1f;
            if(sprint.IsPressed())
            {
                if(staminaContainer.CanDischarge(0.25f))
                {
                    sprintMult = 2.5f;
                    staminaContainer.Discharge(0.25f, EventSender.MECHA);
                }
            }
            float speed = 5f;

            if(movingBattery.CanDischarge(batteryDrain * Time.deltaTime))
            {
                mechaCharacterController.Move(moveVector * speed * Time.deltaTime * sprintMult);
                movingBattery.Discharge(batteryDrain * Time.deltaTime, EventSender.MECHA);
            }

            transform.rotation = Quaternion.Euler(0f, mainCam.rotation.eulerAngles.y, 0f);
        }

        Vector3 gravity = Physics.gravity * Time.deltaTime;
        mechaCharacterController.Move(gravity);
    }
}
