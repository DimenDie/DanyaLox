using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator anim;
    [SerializeField]Transform dot;
    PlayerController playerController;
    void Start()
    {
        anim = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        dot.transform.position = playerController.transform.position + new Vector3(Input.GetAxisRaw("Horizontal") , 2 , Input.GetAxisRaw("Vertical")) ;
        anim.SetFloat("BlendX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("BlendY", Input.GetAxisRaw("Vertical"));
    }
}
