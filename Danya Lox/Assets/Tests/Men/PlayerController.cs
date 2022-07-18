using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 mousePos;
    Vector3 input, skewedInput;
    Rigidbody rigidbody;
    [SerializeField] Camera camera;
    [SerializeField] float movementSpeed, rotationSpeed;
    [SerializeField] LayerMask layerMask;
    float camAndPlayerAngle, playerAndCursorAngle;
    Transform playerLook;
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerLook = transform.GetChild(0);
    }

    void Update()
    {
        GetInput();
        PlayerRotation();

        Ray ray = camera.ScreenPointToRay(Input.mousePosition); // Луч от єкрана к позіції мішкі
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
            mousePos = raycastHit.point;
    }
    private void FixedUpdate() // ну ок да.
    {
        if (input.magnitude != 0)
            rigidbody.velocity = (skewedInput * Time.deltaTime * movementSpeed * 10);
        else
            rigidbody.velocity = Vector3.zero;
    }

    void GetInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // Инпути осей окда
        camAndPlayerAngle = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, camera.transform.rotation.eulerAngles.y); //Переменная хранит данніе положения камери
        var _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, camAndPlayerAngle, 0)); // Сдвиг управления в зависимости положения камері для корректного ввода сторон света. Юзаю матрицу, потому что так в гайде сказали. У вектор3 нет ротейта(((
        skewedInput = _isoMatrix.MultiplyPoint3x4(input); // Переменная, которая хранит сдвинутий інпут
    }
    void PlayerRotation()
    {
        Ray playerSight = new Ray(transform.position, transform.forward);
        Ray cursorSight = new Ray(playerLook.position, -(playerLook.position - new Vector3(mousePos.x, playerLook.position.y, mousePos.z)));
        playerAndCursorAngle = Vector3.SignedAngle(playerSight.direction, cursorSight.direction, new Vector3(0, 1, 0));
        playerLook.rotation = Quaternion.RotateTowards(playerLook.rotation, Quaternion.Euler(0, playerAndCursorAngle, 0), rotationSpeed * 10 * Time.deltaTime);
    }
}
