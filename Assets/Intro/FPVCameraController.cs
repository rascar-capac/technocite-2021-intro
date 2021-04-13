using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPVCameraController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera = null;
    [SerializeField] [Range(0f, 10f)] private float sensitivity = 0f;
    private Transform player;

    private void Start()
    {
        player = transform;
    }

    private void Update()
    {
        float yawRotation = Input.GetAxis("Mouse X") * sensitivity;
        player.RotateAround(player.position, Vector3.up, yawRotation);

        if(playerCamera != null)
        {
            float pitchRotation = -Input.GetAxis("Mouse Y") * sensitivity;
            playerCamera.RotateAround(playerCamera.position, playerCamera.right, pitchRotation);
        }
    }
}
