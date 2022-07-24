using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCamera : MonoBehaviour
{
    public Transform mouseObj;
    [SerializeField] Camera camera;

    [Header("Camera Position Settings")]
    public float cameraSmoothTime;
    public float cameraMaxSpeed;

    [Header("Camera Displacement Settings")]
    public float maxDisplacementDistance;
    public float displacementSmoothTime;
    public float displacementMaxSpeed;

    PlayerController player;
    Vector3 cameraVelocity,displacementVelocity, defaultCamPos;

    private void Awake()
    {
        defaultCamPos = camera.transform.localPosition;
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
                screenPos = player.screenPos;


        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, playerPos, ref cameraVelocity, cameraSmoothTime * Time.deltaTime, cameraMaxSpeed);

        //camera.transform.localPosition = defaultCamPos + screenPos * maxCameraDistance;
        camera.transform.localPosition = Vector3.SmoothDamp(camera.transform.localPosition, defaultCamPos + screenPos * maxDisplacementDistance, ref displacementVelocity, displacementSmoothTime * Time.deltaTime, displacementMaxSpeed);
    }
}