using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Beans : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        SceneManager.LoadScene("Beans");
    }
}
