using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //플레이어 움직임
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundlayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity; //민감도
    private Vector2 mouseDelta;
    public bool canLook = true;
    public Action inventroy;


    private Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //커서를 락 시킨다
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if(canLook)
        {
            CameraLook();
        }
 
    }

    void Move() 
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *=moveSpeed;
        dir.y = _rigidbody.velocity.y; //점프를 했을 때만 위 아래로 움직인다.
         
        _rigidbody.velocity = dir;
    }

    void CameraLook() //Rotation 공부
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);//camCurXRot 최대값 최소값을 넘어가지 않는다
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);

    }

    public void OnMove(InputAction.CallbackContext context) //CallbackContext 현재상태을 받아옴
    {
        if(context.phase == InputActionPhase.Performed) //눌렸을 때
        {
            curMovementInput = context.ReadValue<Vector2>(); //방향별로 벡터가 들어감
        }
        else if(context.phase ==InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        if(context.phase ==InputActionPhase.Started && IsGrounded()) //눌렀을 때 한번
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up*0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up*0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up*0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up*0.01f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            Debug.DrawRay(rays[i].origin, rays[i].direction * 0.1f, Color.red);


            if (Physics.Raycast(rays[i], 0.1f, groundlayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            inventroy?.Invoke();
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
