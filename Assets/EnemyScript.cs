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
    public Collider2D AiNavmeshCollier;
    public bool NavMeshEnabled;
    private Vector2 StartPosition;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Rb = GetComponent<Rigidbody2D>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        BodyAnimator = gameObject.GetComponent<Animator>();
        StartPosition = gameObject.transform.position;
    }
    public void TakeDamage(int Amount)
    {
        EnemyHealth -= Amount;
        if (EnemyHealth <= 0)
        {
            EnemyDeath();
        }
    }
    public void EnemyDeath()
    {
        Player.GetComponent<PlayerController>().Floor.GetComponent<FloorScript>().RemoveEnemy(gameObject);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (NavMeshEnabled) 
        {
            Agent.SetDestination(Player.transform.position);
        }
        else
        {
            Agent.SetDestination(StartPosition);
        }
    }
    private void FixedUpdate()
    {
        BodyAnimator.SetFloat("Hoz", Agent.velocity.x);
        BodyAnimator.SetFloat("Vert", Agent.velocity.y);
    }
}