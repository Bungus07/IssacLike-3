using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour

{
    public float MovementSpeed;
    private GameObject Player;
    private Rigidbody2D Rb;
    private NavMeshAgent Agent;
    private Animator BodyAnimator;
    public float EnemyHealth;
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Rb = GetComponent<Rigidbody2D>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        BodyAnimator = gameObject.GetComponent<Animator>();
    }
    public void TakeDamage(int Amount)
    {
        EnemyHealth -= Amount;
        if (EnemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(Player.transform.position);
    }
    private void FixedUpdate()
    {
        BodyAnimator.SetFloat("Hoz", Agent.velocity.x);
        BodyAnimator.SetFloat("Vert", Agent.velocity.y);
    }
}