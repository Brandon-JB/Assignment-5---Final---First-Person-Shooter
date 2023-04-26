using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 20f;

    private float timeAirborn = 0f;
    public float maxTime;

    private void Start()
    {
        rb.velocity = transform.forward * speed;
    }

    private void Update()
    {
        timeAirborn += Time.deltaTime;

        if (timeAirborn >= maxTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    { 
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy (this.gameObject);
    }
}
