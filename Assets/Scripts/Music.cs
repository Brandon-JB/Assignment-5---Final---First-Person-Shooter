using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource AudioSource;
    static Music Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            AudioSource.pitch = 0f;
        }

        else if (Time.timeScale == 1f)
        {
            AudioSource.pitch = 1f;
        }
    }
}
