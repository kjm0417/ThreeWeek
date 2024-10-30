using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    public float speed;
    public Vector2 curInputValue;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook; //어디까지 돌아가게 할건지
    public float maxXLook;


    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

   

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curInputValue = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curInputValue = Vector2.zero;
        }
    }

     private void Move()
    {
        Vector3 dir = transform.forward * curInputValue.y + transform.right * curInputValue.x;
        dir *= speed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }


  
}
