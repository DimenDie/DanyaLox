using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator animator; 
    int targetXCord, targetYCord;
    float XCord, YCord;
    public bool isMoving;
    [SerializeField] float animSwapSpeed;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        targetXCord = (int)Input.GetAxisRaw("Horizontal");
        targetYCord = (int)Input.GetAxisRaw("Vertical");

        XCord = Mathf.MoveTowards(XCord, targetXCord, animSwapSpeed * 0.01f);
        YCord = Mathf.MoveTowards(YCord, targetYCord, animSwapSpeed * 0.01f);

        isMoving = new Vector2(targetXCord,targetYCord).magnitude != 0 ? true : false;

        animator.SetBool("isMov", isMoving);
        animator.SetFloat("BlendX", XCord);
        animator.SetFloat("BlendY", YCord);
    }
}