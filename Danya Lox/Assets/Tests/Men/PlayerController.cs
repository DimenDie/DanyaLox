using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 mousePos, vectorInput, skewedInput, screenPos;

    float xInput, yInput;

    [SerializeField] Camera cam;
    [SerializeField] float movementSpeed, rotationSpeed;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float playerAndCursorAngle;
    public bool trigger = false;
    Transform playerLook;

    private const float DegToRad = Mathf.PI / 180;

    private void Start()
    {
        playerLook = transform.GetChild(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            trigger = !trigger;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        vectorInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0 , Input.GetAxisRaw("Vertical"));

        PlayerRotation();
        PointerPosition();

        if (trigger)
            PlayerMovement();
        else
            PlayerMovement2();
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

        screenPos = cam.ScreenToViewportPoint(Input.mousePosition);
        screenPos.x -= 0.5f;
        screenPos.z = screenPos.y - 0.5f;
        screenPos.y = 0;
        print(screenPos);
    }    

    void PlayerMovement()
    {
        
        if (yInput != 0)
            transform.Translate((mousePos - transform.position).normalized * yInput * movementSpeed * Time.deltaTime );
        if (xInput != 0)
            transform.Translate((RotateVector(mousePos - transform.position, 90)).normalized * -xInput * movementSpeed * Time.deltaTime);
    }  
    
    void PlayerMovement2()
    {
        var _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        skewedInput = _isoMatrix.MultiplyPoint3x4(vectorInput);

        if (vectorInput.magnitude != 0)
            transform.Translate(skewedInput * Time.deltaTime * movementSpeed);
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