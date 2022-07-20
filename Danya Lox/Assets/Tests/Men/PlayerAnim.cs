using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator anim;
    [SerializeField] float skewAngle, skewPower, skewSpeed;
    [SerializeField]Transform targetDot;
    PlayerController playerController;
    PlayerCamera playerCamera;
    Vector3 targetSkewPos, skewedInput2, defaultSkewPos;
    void Start()
    {

        anim = GetComponent<Animator>();
        playerCamera = FindObjectOfType<PlayerCamera>();
        playerController = FindObjectOfType<PlayerController>();

        defaultSkewPos = targetDot.transform.localPosition;
    }

    void Update()
    {

        
        var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // Инпути осей окда

        //var _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetDot.transform.rotation.eulerAngles.y) + skewAngle, 0));
        var _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, skewAngle, 0));
        skewedInput2 = _isoMatrix.MultiplyPoint3x4(input);


        targetSkewPos = defaultSkewPos + skewedInput2 * skewPower;
        targetDot.transform.localPosition = Vector3.MoveTowards(targetDot.transform.localPosition, targetSkewPos, skewSpeed * 0.01f);


        Ray ray1 = new Ray(playerController.transform.position, playerCamera.mouseObj.transform.position);
        Ray ray2 = new Ray(playerController.transform.position, playerController.transform.forward);
        print(Vector3.SignedAngle(ray1.direction, ray2.direction, Vector3.up));
    }
}