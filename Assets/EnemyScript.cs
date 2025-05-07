using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour

{
    public float MovementSpeed;
    private GameObject Player;
    public Rigidbody2D Rb;
    public NavMeshAgent Agent;
    private Animator BodyAnimator;
    public float EnemyHealth;
    public int Damage;
    public Collider2D AiNavmeshCollier;
    public bool NavMeshEnabled;
    private Vector2 StartPosition;
    public float knockbackAmount;
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
        Agent.speed = MovementSpeed;
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
        Player.GetComponent<PlayerController>().Enemies.Remove(gameObject);
        Destroy(gameObject);
    }
    public void DebugTest()
    {
        Debug.Log("NavMeshEnabled =" + NavMeshEnabled);
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
        Debug.Log("AgentVelocityX = " + Agent.velocity.x + "AgentVelocityY = " + Agent.velocity.y);
    }
}