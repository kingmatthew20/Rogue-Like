using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public float damage = 5.0f;
    public Transform bulletPrefab;
    public Transform bulletTrans;

    Vector3 gunPos;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Vector3 shootDir)
    {

        Transform bullletTransform = Instantiate(bulletPrefab, bulletTrans.position, Quaternion.identity);
        bullletTransform.forward = shootDir;

        bullletTransform.GetComponent<Bullet>().Setup(damage);

    }

}
