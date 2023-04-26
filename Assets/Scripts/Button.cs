using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    public GameObject Doors;
    public AudioSource audiosource;

    private void Start()
    {
        Doors.SetActive(true);
    }

    public void Interact()
    {
        Doors.SetActive(false);
        audiosource.Play();
    }
}
