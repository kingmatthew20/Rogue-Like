using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.0f;
    public float turnSpeed = 0.0f;
    public float minLookInput = 0.8f;
    public float jumpPower = 1.0f;
    public float dashSpeed = 0.0f;
    public bool onGround = false;
    public Transform weaponPosition;

    //public GameObject weaponPrefab;
    IWeapon weapon;

    Rigidbody body;
    public Vector2 moveInputDirection;
    public Vector2 lookInputDirection;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //GameObject weaponObject = Instantiate(weaponPrefab, weaponPosition);
        //weapon = weaponObject.GetComponent<IWeapon>();
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
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

    public void Jump()
    {
        if (onGround == true)
        {
            body.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
    public void Dash()
    {
        Vector3 dashDirection = new Vector3(moveInputDirection.x, 0.0f, moveInputDirection.y);
        body.velocity = dashDirection * dashSpeed;
    }
    public void StartFire()
    {
        if (weapon == null) return;
            weapon.StartFire();
    }
    public void EndFire()
    {
        if (weapon == null) return;
            weapon.EndFire();
    }
    public void AddWeapon(GameObject wpn)
    {
        GameObject playerWeapon = Instantiate(wpn, weaponPosition);
        weapon = playerWeapon.GetComponent<IWeapon>();
    }
}
