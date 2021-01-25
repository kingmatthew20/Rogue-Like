using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.0f;
    public float turnSpeed = 0.0f;
    public float minLookInput = 0.8f;

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
        Vector3 moveDirection = new Vector3(moveInputDirection.x, 0.0f, moveInputDirection.y);
        body.AddForce(moveDirection * moveSpeed * Time.fixedDeltaTime);

        if (lookInputDirection.magnitude >= minLookInput)
        {
            Vector3 lookDirection = new Vector3(lookInputDirection.x, 0.0f, lookInputDirection.y);
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
        }
    }

    void OnMove(InputValue value)
    {
        moveInputDirection = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        lookInputDirection = value.Get<Vector2>();
    }
}
