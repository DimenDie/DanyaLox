using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCamera : MonoBehaviour
{
    public float cameraSmoothTime, cameraMaxSpeed, maxCamDistance;
    PlayerController player;
    Vector3 cameraVelocity;
    public Transform mouseObj, pivotObj;
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        CameraPos();
        mouseObj.position = player.mousePos;
    }

    void CameraPos()
    {
        Vector3 playerPos = player.transform.position,
                mousePos = mouseObj.localPosition,
                CamPosWithOffset = playerPos + mousePos;
        
        transform.position = Vector3.SmoothDamp(transform.position, pivotObj.position, ref cameraVelocity, cameraSmoothTime * Time.deltaTime, cameraMaxSpeed);
    }
}