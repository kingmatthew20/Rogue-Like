using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour, IWeapon
{
    public float damage = 10.0f;
    public float range = 50.0f;
    public Transform bulletPos;
    public GameObject laserPrefab;
    public float time = 1.0f;

    private LineRenderer lineRender;
    bool firing = false;

    private void Start()
    {
        lineRender = laserPrefab.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firing)
            LaserShoot(transform.forward);
    }

    public void LaserShoot(Vector3 shootDir)
    {
        RaycastHit hit;
        bool rayHit = Physics.Raycast(bulletPos.position, shootDir, out hit, range);
        lineRender.SetPosition(0, bulletPos.position);

        if (rayHit)
        {
            Target target = hit.collider.gameObject.GetComponent<Target>();
            target.TakeDamage(damage);

            lineRender.SetPosition(1, hit.point);
            lineRender.alignment = LineAlignment.View;
            GameObject laser = Instantiate(laserPrefab, bulletPos.position, Quaternion.identity);
            Destroy(laser, time);
        }
        else
        {
            lineRender.SetPosition(1, transform.forward * range);
            lineRender.alignment = LineAlignment.View;
            GameObject laser = Instantiate(laserPrefab, bulletPos.position, Quaternion.identity);
            Destroy(laser, time);
        }
    }

    public void StartFire()
    {
        firing = true;
    }

    public void EndFire()
    {
        firing = false;
    }
}
