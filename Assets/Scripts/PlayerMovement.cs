using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public enum CameraMode { ThirdPerson, FirstPerson, TopDown }
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] CoppyTransform cT;
    public bool isBeta = false;
    [SerializeField] Transform AHead;
    [SerializeField] Transform BHead;
    [SerializeField] GameObject AModel;
    [SerializeField] GameObject BModel;
    public Animator BAnimator;
    public Animator AAnimator;
    Animator animator;
    float animationMovementX = 0;
    float animationMovementY = 0;
    public static PlayerMovement PlayerInstance;
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
    void Awake()
    {
        PlayerInstance = this;
        controller = GetComponent<CharacterController>();
    }
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        //pCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
            isBeta = !isBeta;
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

            camXRotation = Mathf.Clamp(camXRotation, -60, 70);//max and min up and down rotation
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

        //if(cT != null)
        //{
        //    if (isBeta)
        //    {
        //        cT.target = BHead;
        //    }
        //    else
        //    {
        //        cT.target = AHead;
        //    }
        //}
        if (isBeta)
        {
            AModel.SetActive(false);
            BModel.SetActive(true);
            //pCamera = BHead.parent;
        }
        else
        {
            AModel.SetActive(true);
            BModel.SetActive(false);
            //pCamera = AHead.parent;
        }

        //FP
        if(Mathf.Abs(cExtander.Distance) < 0.1f)
        {
            if(AHead.localScale.x > 0.5f)
            {
                AModel.GetComponent<RigBuilder>().enabled = false;
                BModel.GetComponent<RigBuilder>().enabled = false;
                AHead.localScale = Vector3.zero;
                BHead.localScale = Vector3.zero;
            }
        }
        else
        {
            if (AHead.localScale.x < 0.5f)
            {
                AModel.GetComponent<RigBuilder>().enabled = true;
                BModel.GetComponent<RigBuilder>().enabled = true;
                AHead.localScale = Vector3.one;
                BHead.localScale = Vector3.one;
            }
        }

        //Animations
        if (isBeta)
            animator = BAnimator;
        else
            animator = AAnimator;

        if (animator != null)
        {
            animationMovementX = Vector3.MoveTowards(Vector3.one * animationMovementX, Vector3.one * inputX, Time.deltaTime * 10).x;
            animationMovementY = Vector3.MoveTowards(Vector3.one * animationMovementY, Vector3.one * inputY, Time.deltaTime * 10).x;
            animator.SetFloat("Input_X", animationMovementX);
            animator.SetFloat("Input_Y", animationMovementY);
        }
    }

    public void Teleport(Vector3 Pos)
    {
        Teleport(Pos, new Vector2(pCamera.transform.localEulerAngles.x, transform.localEulerAngles.y));
    }
    public void Teleport(Vector3 Pos, Vector2 Rot)
    {
        controller.enabled = false;
        transform.position = Pos;
        controller.enabled = true;


        camXRotation = Rot.x;
        camYRotation = Rot.y;
    }

    public Vector2 GetCamRotation()
    {
        return new Vector2(camXRotation, camYRotation);
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
