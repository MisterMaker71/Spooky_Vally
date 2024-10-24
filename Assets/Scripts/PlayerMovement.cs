using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMode { ThirdPerson, FirstPerson, TopDown }
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public CameraMode cameraMode = CameraMode.ThirdPerson;
    public CammeraExtander cExtander = null;
    public float movementSpeed = 4;
    public float sprintSpeed = 7;
    CharacterController controller = null;
    [Tooltip("reference of camer rotation object")]
    public Transform pCamera;
    public float mouseSensebility = 100;
    float camXRotation = 0;
    float camYRotation = 0;
    InventoryManager inventoryManager;
    public Effects effect = new Effects();
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        //pCamera = Camera.main;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        effect.Update();

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 MoveVector = new Vector2(inputX, inputY).normalized;
        float sSpeed = movementSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
            sSpeed = sprintSpeed;

        sSpeed += effect.Speed;

        controller.Move(transform.forward * MoveVector.y * sSpeed * Time.deltaTime + transform.right * MoveVector.x * sSpeed * Time.deltaTime);

        
        if(Input.GetKeyDown(KeyCode.F5))
        {
            if (cameraMode == CameraMode.ThirdPerson) cameraMode = CameraMode.FirstPerson;
            else cameraMode = CameraMode.ThirdPerson;
            //if (cameraMode == CameraMode.ThirdPerson) cameraMode = CameraMode.FirstPerson;
            //else if (cameraMode == CameraMode.FirstPerson) cameraMode = CameraMode.TopDown;
            //else if(cameraMode == CameraMode.TopDown) cameraMode = CameraMode.ThirdPerson;
            ChangeCameMode();
        }


        if(!Input.GetMouseButton(1) && !inventoryManager.InventoryIsVisibel)
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

    public void ChangeCameMode()
    {
        switch (cameraMode)
        {
            case CameraMode.ThirdPerson:
                cExtander.extendet = true;
                break;
            case CameraMode.FirstPerson:
                cExtander.extendet = false;
                break;
            case CameraMode.TopDown:
                break;
        }
    }
}
