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

    void Start()
    {
        AnimatorMove = gameObject.transform.GetChild(1).gameObject.GetComponent<Animator>();
        animatorshoot = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        rb.velocity = moveInput * moveSpeed;
       
    }
}