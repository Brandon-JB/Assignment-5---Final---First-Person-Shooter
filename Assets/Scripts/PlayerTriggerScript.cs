using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerScript : MonoBehaviour
{
    [Header("Game Over")]
    public int health;
    public int maxHealth;
    public GameObject gameOverScreen;

    private void Start()
    {
        health = maxHealth;
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (health <= 0)
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.tag == "EnemyBullet")
        {
            health--;
            //Debug.Log("beans");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            health--;
            //Debug.Log("beans");
        }
    }
}
