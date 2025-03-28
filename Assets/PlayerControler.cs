using System.Collections;
using System.Collections.Generic;
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
    public GameObject CanvasMenu;
    public GameObject Head;

    void Start()
    {
        AnimatorMove = gameObject.GetComponent<Animator>();
        animatorshoot = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Head.SetActive(true);
    }
    void TakeDamage(int Damage)
    {  
        if (!InvincibilityTimerActive)
        {
            PlayerHealth -= Damage;
            InvincibilityTimerActive = true;
            if (PlayerHealth <= 0)
            {
                StartCoroutine(Death());
            }
        }
    }
    IEnumerator Death()
    {
        AnimatorMove.SetBool("Dead", true);
        Head.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0;
        CanvasMenu.GetComponent<Menu>().ShowRestartScreen();
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
        // Debug.Log("PlayerIsStayingInTrigger");
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