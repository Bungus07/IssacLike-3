using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
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
    public int PlayerMaxHealth;
    public float InvincibilityTimer;
    public float InvincibilityTime;
    private bool InvincibilityTimerActive;
    public GameObject CanvasMenu;
    public GameObject Head;
    public List<GameObject> Enemies = new List<GameObject>();
    public int KeyCount = 0;
    public int BombCount = 0;
    public int CoinCount = 0;
    public GameObject Floor;
    public Projectile ProjectileScript;
    public int DefaultDamage;
    public int ExtraLife = 0;
    public bool IsInvincible;
    public float InvunrableTime;
    public HealhBar HealhBarScript;

    void Start()
    {
        AnimatorMove = gameObject.GetComponent<Animator>();
        animatorshoot = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        foreach (GameObject Enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemies.Add(Enemy);
        }
        Head.SetActive(true);
        PlayerHealth = PlayerMaxHealth;
        ProjectileScript.Damage = DefaultDamage;
    }
    void TakeDamage(int Damage)
    {
        if (!IsInvincible)
        {
            if (!InvincibilityTimerActive)
            {
                PlayerHealth -= Damage;
                InvincibilityTimerActive = true;
                HealhBarScript.UpdatePlayerHealth();
                if (PlayerHealth <= 0)
                {
                    StartCoroutine(Death());
                }
            }
        }
        
    }
    IEnumerator Death()
    {
        if (ExtraLife > 0)
        {
            --ExtraLife;
            PlayerHealth = PlayerMaxHealth;
        }
        else
        {
            AnimatorMove.SetBool("Dead", true);
            Head.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            Time.timeScale = 0;
            CanvasMenu.GetComponent<Menu>().ShowRestartScreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            rb.AddForce(new Vector2(collision.gameObject.GetComponent<EnemyScript>().Agent.velocity.x, collision.gameObject.GetComponent<EnemyScript>().Agent.velocity.y) * collision.gameObject.GetComponent<EnemyScript>().knockbackAmount, ForceMode2D.Force);
            TakeDamage(collision.gameObject.GetComponent<EnemyScript>().Damage);
        }
        if (collision.gameObject.tag == "NavMeshTrigger")
        {
            Debug.Log("PlayerHasCollidedWithNavmesh");
            Floor = collision.gameObject;
            
            if (Enemies.Count != 0)
            {
                foreach (GameObject Enemy in Enemies)
                {
                    if (Enemy.GetComponent<EnemyScript>().AiNavmeshCollier == collision)
                    {
                        Enemy.GetComponent<EnemyScript>().DebugTest();
                        Enemy.GetComponent<EnemyScript>().NavMeshEnabled = true;
                    }
                }
            }
        }
        if (collision.gameObject.tag == "Key")
        {
            KeyCount ++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Bomb")
        {
            BombCount++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Coin")
        {
            CoinCount++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "RedHeart")
        {
            if (PlayerMaxHealth > PlayerHealth)
            {
                PlayerHealth++;
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "HealthUpItem")
        {
                PlayerMaxHealth++;
                PlayerHealth++;
                Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "DamageUpItem")
        {
            ProjectileScript.Damage++; 
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "SpeedItem")
        {
            moveSpeed += 2;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "BombBundleItem")
        {
            BombCount += 10;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "CoinBundleItem")
        {
            CoinCount += 25;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "RespawnItem")
        {
            ExtraLife++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Buyable")
        {
            if(collision.gameObject.GetComponent<BuyableItems>().Price <= CoinCount)
            {
                CoinCount -= collision.gameObject.GetComponent<BuyableItems>().Price;
                if (collision.gameObject.GetComponent<BuyableItems>().Item.ItemPrefab.gameObject.tag == "Key")
                {
                    KeyCount++;
                    Destroy(collision.gameObject);
                }
                else if(collision.gameObject.GetComponent<BuyableItems>().Item.ItemPrefab.gameObject.tag == "Bomb")
                {
                    BombCount++;
                    Destroy(collision.gameObject);
                }
                else if (collision.gameObject.GetComponent<BuyableItems>().Item.ItemPrefab.gameObject.tag == "RedHeart")
                {
                    PlayerHealth++;
                    Destroy(collision.gameObject);
                }
            }
            else
            {
                return;
            }
                
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NavMeshTrigger")
        {
            
            if (Enemies.Count != 0)
            {
                foreach (GameObject Enemy in Enemies)
                {
                    if (Enemy.GetComponent<EnemyScript>().AiNavmeshCollier == collision)
                    {
                        Enemy.GetComponent<EnemyScript>().NavMeshEnabled = false;
                    }
                }
            }
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