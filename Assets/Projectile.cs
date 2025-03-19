using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage;
    private Shooting ShootingScript;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(Damage);
            Destroy(gameObject);
        }
       
    }
    private void Start()
    {
        ShootingScript = GameObject.Find("PlayerHead").GetComponent<Shooting>();
    }
}
