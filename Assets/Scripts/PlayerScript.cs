using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Content;
//using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Game Over")]
    public float health;
    public float maxHealth;
    public GameObject gameOverScreen;
    public GameObject playerTrigger;
    public Image healthBar;
    public AudioSource audiosource;
    public AudioClip hurtSound;


    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Dash")]
    public float dashForce;
    public float dashCooldown;
    private float timeSinceDash;
    bool dashReady;
    public Image dashTimer;
    public AudioClip dashSound;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey = KeyCode.LeftShift;

    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Pause Menu")]
    public bool isPaused;
    public GameObject pauseMenu;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        ResetJump();
        health = maxHealth;
        gameOverScreen.SetActive(false);
        dashReady = true;
        timeSinceDash = dashCooldown;
        isPaused = false;
        pauseMenu.SetActive(false);

        Time.timeScale = 1;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();

        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        if (health <= 0)
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
        }

        dashTimer.fillAmount = timeSinceDash / dashCooldown;
        healthBar.fillAmount = health / maxHealth;

        if (dashReady == true)
        {
            dashTimer.color = Color.green;

            if (Input.GetKeyDown(dashKey) && isPaused == false)
            {
                timeSinceDash = 0;
                rb.AddForce(moveDirection.normalized * moveSpeed * dashForce, ForceMode.Force);
                audiosource.PlayOneShot(dashSound);
                dashReady = false;
            }
        }

        if (dashReady == false)
        {
            dashTimer.color = Color.red;
            timeSinceDash += Time.deltaTime;

            if (timeSinceDash >= dashCooldown)
            {
                dashReady = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && health > 0)
        {
            if (isPaused == false)
            {
                isPaused = true;
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else if (isPaused == true)
            {
                isPaused = false;
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isPaused == false)
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump == true && grounded == true)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.tag == "EnemyBullet")
        {
            health = health - 1;
            //Debug.Log("beans");
            audiosource.PlayOneShot(hurtSound);
        }

        if (collide.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
