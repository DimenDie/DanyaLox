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

        targetSkewPos = defaultSkewPos + skewedInput2 * skewPower;
        var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // Инпути осей окда

        var _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, Mathf.DeltaAngle(playerController.transform.rotation.eulerAngles.y, playerCamera.mouseObj.transform.rotation.eulerAngles.y) + skewAngle, 0));
        skewedInput2 = _isoMatrix.MultiplyPoint3x4(input);

        targetDot.transform.localPosition = Vector3.MoveTowards(targetDot.transform.localPosition, targetSkewPos, skewSpeed * 0.01f);






        //targetDot.transform.localPosition = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //var _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, Mathf.DeltaAngle(playerController.transform.rotation.eulerAngles.y, targetDot.transform.rotation.eulerAngles.y) + skewAngle, 0));
        //var skewedInput = _isoMatrix.MultiplyPoint3x4(playerController.transform.position - targetDot.transform.position); // Переменная, которая хранит сдвинутий інпут
        //print(skewedInput);
        //anim.SetFloat("BlendX", skewedInput.x);
        //anim.SetFloat("BlendY", skewedInput.z);


    }
}
