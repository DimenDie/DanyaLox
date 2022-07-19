using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 mousePos;
    public Vector3 input, skewedInput, skewedInput2;
    Rigidbody rb;
    [SerializeField] Camera cam;
    [SerializeField] float movementSpeed, rotationSpeed;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float playerAndCursorAngle;
    Transform playerLook;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerLook = transform.GetChild(0);
    }

    void Update()
    {

        GetInput();
        PlayerRotation();

        Ray ray = cam.ScreenPointToRay(Input.mousePosition); // ��� �� ������ � ������ ���
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
            mousePos = raycastHit.point;
    }
    private void FixedUpdate() // �� �� ��.
    {
        if (input.magnitude != 0)
            rb.velocity = (skewedInput * Time.deltaTime * movementSpeed * 10);
        else
            rb.velocity = Vector3.zero;
    }

    void GetInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // ������ ���� ����
        var _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0)); // ����� ���������� � ����������� ��������� ����� ��� ����������� ����� ������ �����. ���� �������, ������ ��� ��� � ����� �������. � ������3 ��� �������(((
        skewedInput = _isoMatrix.MultiplyPoint3x4(input); // ����������, ������� ������ ��������� �����
    }
    void PlayerRotation()
    {
        Ray playerSight = new Ray(transform.position, transform.forward);
        Ray cursorSight = new Ray(playerLook.position, -(playerLook.position - new Vector3(mousePos.x, playerLook.position.y, mousePos.z)));
        playerAndCursorAngle = Vector3.SignedAngle(playerSight.direction, cursorSight.direction, new Vector3(0, 1, 0));
        playerLook.rotation = Quaternion.RotateTowards(playerLook.rotation, Quaternion.Euler(0, playerAndCursorAngle, 0), rotationSpeed * 10 * Time.deltaTime);
    }    
}