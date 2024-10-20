using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PatternLibrary;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float moveSpeed = 50f;

    

    public void MoveCamera(Vector2 direction)
    {
        Debug.Log("Move Camera");
        Vector3 moveDir = transform.forward * direction.y + transform.right * direction.x;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
