using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 mousePos;

    float xInput, yInput, xMovement, yMovement;
    Rigidbody rb;
    [SerializeField] Camera cam;
    [SerializeField] float movementSpeed, rotationSpeed;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float playerAndCursorAngle;
    Transform playerLook;

    private const float DegToRad = Mathf.PI / 180;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerLook = transform.GetChild(0);
    }

    void Update()
    {
        

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");



        PlayerRotation();
        PointerPosition();
        PlayerMovement();
    }



    void PlayerRotation()
    {
        Ray playerSight = new Ray(transform.position, transform.forward);
        Ray cursorSight = new Ray(playerLook.position, -(playerLook.position - new Vector3(mousePos.x, playerLook.position.y, mousePos.z)));
        playerAndCursorAngle = Vector3.SignedAngle(playerSight.direction, cursorSight.direction, new Vector3(0, 1, 0));
        playerLook.rotation = Quaternion.RotateTowards(playerLook.rotation, Quaternion.Euler(0, playerAndCursorAngle, 0), rotationSpeed * 10 * Time.deltaTime);
    }    

    void PointerPosition()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); // Луч от єкрана к позіції мішкі
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
            mousePos = raycastHit.point;
    }    

    void PlayerMovement()
    {
        if (yInput == 0 && xInput == 0) return;
        else if (yInput != 0)
            transform.Translate((mousePos - transform.position) * yInput * movementSpeed * Time.deltaTime / Vector3.Distance(transform.position, mousePos));
        else
            transform.Translate(( RotateVector(mousePos - transform.position, 90)) * -xInput * movementSpeed * Time.deltaTime / Vector3.Distance(transform.position, mousePos));

    }

    Vector3 RotateVector(Vector3 defaultVector ,int degrees)
    {
        Vector3 newVector = defaultVector;

        float rad = degrees * DegToRad;

        float x2 = defaultVector.x * Mathf.Cos(rad) - defaultVector.z * Mathf.Sin(rad);
        float z2 = defaultVector.x * Mathf.Sin(rad) + defaultVector.z * Mathf.Cos(rad);
       
        newVector.x = x2;
        newVector.z = z2;
        
        return newVector;
    }
}