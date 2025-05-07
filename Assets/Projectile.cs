using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage;
    private Shooting ShootingScript;
    public float Knockback;
    private Rigidbody2D TearRb;
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().Rb.AddForce(TearRb.velocity * Knockback);
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(Damage);
            Destroy(gameObject);
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        ShootingScript = GameObject.Find("PlayerHead").GetComponent<Shooting>();
        TearRb = gameObject.GetComponent<Rigidbody2D>();
    }
}
