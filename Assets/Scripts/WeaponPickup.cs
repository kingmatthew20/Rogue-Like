using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    PlayerController playerController;
    Pickup pickup;
    GameObject weaponPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        pickup = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pickup>();
        weaponPrefab = pickup.Weapons[1];
        GameObject weaponObject = Instantiate(weaponPrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController = other.gameObject.GetComponentInParent<PlayerController>();
            playerController.AddWeapon(weaponPrefab);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
