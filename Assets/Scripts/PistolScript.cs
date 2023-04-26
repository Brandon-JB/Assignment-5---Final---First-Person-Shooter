using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PistolScript : MonoBehaviour
{
    public int maxAmmo;
    private int Ammo;
    public AudioSource audiosource;
    public AudioClip gunshot;
    public AudioClip reload;

    public Transform firepoint;
    public GameObject bullet;
    public TMP_Text ammoText;
    bool reloading;

    private void Start()
    {
        Ammo = maxAmmo;
        reloading = false;
    }

    private void Update()
    {
        if (Ammo > 0)
        { 
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale > 0)
            {
                audiosource.PlayOneShot(gunshot);
                Instantiate(bullet, firepoint.position, firepoint.rotation);
                Ammo--;
            }
        }

        if (Ammo < maxAmmo)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && Time.timeScale > 0 && reloading == false)
            {
                reloading = true;
                audiosource.PlayOneShot(reload);
                Invoke(nameof(Reload), 0.5f);
            }
        }

        ammoText.SetText("Ammo: " + Ammo + "/" + maxAmmo);
    }

    private void Reload()
    {
        Ammo = maxAmmo;
        reloading = false;
    }
}
