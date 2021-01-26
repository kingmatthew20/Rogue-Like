using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float time = 5.0f;
    public float moveSpeed = 10.0f;
    public Vector3 bulletDir;

    float bulletDmg;
    Rigidbody body;

    public void Setup(float bulletDmg)
    {
        this.bulletDmg = bulletDmg;

        body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * moveSpeed;
        Destroy(gameObject, time);
    }
    private void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();

        if(target != null)
        {
            target.TakeDamage(bulletDmg);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
       
    }
}
