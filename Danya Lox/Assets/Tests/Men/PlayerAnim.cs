using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Vector3 vectorCord, skewedInput;
    Animator animator; 
    int targetXCord, targetYCord;
    float XCord, YCord;
    public bool isMoving;
    [SerializeField] float animSwapSpeed, skewAngle;
    PlayerController playerController;
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
    }

    

    void Update()
    {
        if(playerController.trigger)
        {
            targetXCord = (int)Input.GetAxisRaw("Horizontal");
            targetYCord = (int)Input.GetAxisRaw("Vertical");

            XCord = Mathf.MoveTowards(XCord, targetXCord, animSwapSpeed * 0.01f);
            YCord = Mathf.MoveTowards(YCord, targetYCord, animSwapSpeed * 0.01f);

            isMoving = new Vector2(targetXCord, targetYCord).magnitude != 0 ? true : false;

            animator.SetBool("isMov", isMoving);
            animator.SetFloat("BlendX", XCord);
            animator.SetFloat("BlendY", YCord);
        }
        else
        {
            vectorCord = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            var _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, -Mathf.Abs(transform.rotation.eulerAngles.y) + skewAngle, 0));

            skewedInput = _isoMatrix.MultiplyPoint3x4(vectorCord);

            XCord = Mathf.MoveTowards(XCord, skewedInput.x, animSwapSpeed * 0.01f);
            YCord = Mathf.MoveTowards(YCord, skewedInput.z, animSwapSpeed * 0.01f);

            isMoving = skewedInput.magnitude != 0 ? true : false;

            animator.SetBool("isMov", isMoving);
            animator.SetFloat("BlendX", XCord);
            animator.SetFloat("BlendY", -YCord);
        }    
    }
}