using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform monster;

    private void LateUpdate()
    {
        if(player==null || monster==null)
        {
            return;
        }
        
        Vector3 targetPos = (player.position + monster.position) / 2f;
        Vector3 offset = (player.position - monster.position).normalized;
        transform.position = targetPos - offset * (-40.0f);
        transform.LookAt(targetPos);
    }
}
