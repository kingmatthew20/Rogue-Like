using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.0f;
    public float turnSpeed = 0.0f;
    public float minLookInput = 0.8f;
    public float jumpPower = 1.0f;
    public float dashSpeed = 10.0f;
    public bool onGround = false;
        
    Rigidbody body;
    Vector2 moveInputDirection;
    Vector2 lookInputDirection;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (onGround)
        {
            Vector3 moveDirection = new Vector3(moveInputDirection.x, 0.0f, moveInputDirection.y);
            body.AddForce(moveDirection * moveSpeed * Time.fixedDeltaTime);
        }

        if (lookInputDirection.magnitude >= minLookInput)
        {
            Vector3 lookDirection = new Vector3(lookInputDirection.x, 0.0f, lookInputDirection.y);
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
        }
    }

    private void LateUpdate()
    {
        body.angularVelocity = Vector3.zero;
        transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);
    }

    void OnMove(InputValue value)
    {
        moveInputDirection = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        lookInputDirection = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (onGround == true)
        {
            body.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            onGround = false;
        }
    }

    void OnDash()
    {
        Vector3 dashDirection = new Vector3(moveInputDirection.x, 0.0f, moveInputDirection.y);
        body.AddForce(dashDirection * moveSpeed * dashSpeed, ForceMode.Impulse);
    }
}
