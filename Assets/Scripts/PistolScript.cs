using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour
{
    public int maxAmmo;
    private int Ammo;

    public Transform firepoint;
    public GameObject bullet;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(bullet, firepoint.position, firepoint.rotation);
        }
    }
}
