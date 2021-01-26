using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float time = 5.0f;
    public float moveSpeed = 10.0f;
    public Vector3 bulletDir;

    Rigidbody body;

    public void Setup()
    {

        body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * moveSpeed;
        Destroy(gameObject, time);
    }

    private void Update()
    {
       
    }
}
