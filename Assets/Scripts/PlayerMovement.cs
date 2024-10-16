using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 4;
    CharacterController controller;
    [Tooltip("reference of camer rotation object")]
    [SerializeField] Transform pCamera;
    [SerializeField] float mouseSensebility = 100;
    float camXRotation = 0;
    float camYRotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        //pCamera = Camera.main;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 MoveVector = new Vector2(inputX, inputY).normalized;
        controller.Move(transform.forward * MoveVector.y * movementSpeed * Time.deltaTime + transform.right * MoveVector.x * movementSpeed * Time.deltaTime);

        
        if(!Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;

            float mouseX = Input.GetAxis("Mouse X") * mouseSensebility * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensebility * Time.deltaTime;

            float Xrot = mouseX;
            float Yrot = mouseY;

            camXRotation -= Yrot;
            camYRotation += Xrot;

            camXRotation = Mathf.Clamp(camXRotation, -90, 90);
            //camYRotation = Mathf.Clamp(camYRotation, -90, 90);

            if (pCamera != null)
                pCamera.localRotation = Quaternion.Euler(camXRotation, 0, 0);
            transform.localRotation = Quaternion.Euler(0, camYRotation, 0);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }
}
