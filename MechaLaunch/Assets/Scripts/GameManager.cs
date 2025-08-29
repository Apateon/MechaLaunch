using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.DefaultInputActions;

public class GameManager : MonoBehaviour
{
    [SerializeField] MechaManager playerMecha;
    [SerializeField] MonsterManager mainMonster;

    PlayerInputManager playerManager;

    void Awake()
    {
        playerManager = GetComponent<PlayerInputManager>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        playerManager.onPlayerJoined += playerjoined;
    }

    private void OnDisable()
    {
        playerManager.onPlayerJoined -= playerjoined;
    }

    private void Start()
    {
        MechaMaker();
    }

    private void playerjoined(PlayerInput input)
    {
        PlayerCtrl player = input.gameObject.GetComponent<PlayerCtrl>();

        Debug.Log("Player " + input.playerIndex + " has joined");

        switch (input.playerIndex)
        {
            case 1:
                Debug.Log("Movement set for player " + input.playerIndex);
                player.Config(input.playerIndex, input.actions.FindActionMap("Movement"));
                playerMecha.driverSeat.SetPilot(input.actions);
                break;
            case 0:
                Debug.Log("shooting set for player " + input.playerIndex);
                player.Config(input.playerIndex, input.actions.FindActionMap("Shooting"));
                playerMecha.gunnerSeat.SetGunner(input.actions);
                break;
            case 2:
                Debug.Log("energy set for player " + input.playerIndex);
                player.Config(input.playerIndex, input.actions.FindActionMap("Energy"));
                playerMecha.engineerSeat.SetEngineer(input.actions);
                break;
        }
    }

    private void MechaMaker()
    {
        playerMecha.SetMecha(mainMonster);
        playerMecha.StartMecha();
    }
}
