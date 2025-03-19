using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 ShootInput;
    private Animator animatorshoot;
    private Animator AnimatorMove;
    public int PlayerHealth;
    public float InvincibilityTimer;
    public float InvincibilityTime;
    private bool InvincibilityTimerActive;

    void Start()
    {
        AnimatorMove = gameObject.transform.GetChild(1).gameObject.GetComponent<Animator>();
        animatorshoot = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void TakeDamage(int Damage)
    {  
        if (!InvincibilityTimerActive)
        {
            PlayerHealth -= Damage;
            InvincibilityTimerActive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyScript>().Damage);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("PlayerIsStayingInTrigger");
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyScript>().Damage);
        }
    }


    void Update()
    {
        // Get input from WASD keys
        moveInput.x = Input.GetAxisRaw("Horizontal"); // A (-1) & D (+1)
        moveInput.y = Input.GetAxisRaw("Vertical");   // W (+1) & S (-1)
        moveInput.Normalize(); // Normalize to prevent diagonal speed boost
        ShootInput.x = Input.GetAxisRaw("Horizontal2");
        ShootInput.y = Input.GetAxisRaw("Vertical2");
        animatorshoot.SetFloat("HozShoot", ShootInput.x);
        animatorshoot.SetFloat("VertShoot", ShootInput.y);
        AnimatorMove.SetFloat("Hoz", moveInput.x);
        AnimatorMove.SetFloat("Vert", moveInput.y);

        if (InvincibilityTimerActive == true)
        {
            if (InvincibilityTimer <= InvincibilityTime)
            {
                InvincibilityTimer += Time.deltaTime;
            }
            else
            {
                InvincibilityTimer = 0;
                InvincibilityTimerActive = false;
            }

        }
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        rb.velocity = moveInput * moveSpeed;
       
    }
}