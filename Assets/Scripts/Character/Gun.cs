using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour, IWeapon
{
    public float damage = 5.0f;
    public Transform bulletPrefab;
    public Transform bulletTrans;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {

        Transform bullletTransform = Instantiate(bulletPrefab, bulletTrans.position, Quaternion.identity);
        bullletTransform.forward = transform.forward;

        bullletTransform.GetComponent<Bullet>().Setup(damage);

    }

    public void StartFire()
    {
        Shoot();
    }

    public void EndFire()
    {
        
    }
}
