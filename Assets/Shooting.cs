using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject Tear;
    private float Hoz;
    private float Vert;
    public float TearSpeed;
    public Vector2 ShootingPosition;
    private bool CooldownIsActive;
    public float CooldownTime;
    private float CooldownTimer;
    public GameObject TearPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Hoz = Input.GetAxis("Horizontal2");
        Vert = Input.GetAxis("Vertical2");
        if (Hoz != 0 || Vert != 0)
        {
            if (!CooldownIsActive)
            {
                ShootingPosition = new Vector2(Hoz, -Vert);
                Tear = Instantiate(TearPrefab, gameObject.transform.position, Quaternion.identity);
                Tear.GetComponent<Rigidbody2D>().velocity = ShootingPosition * TearSpeed;
                CooldownIsActive = true;
            }
        }
        if (CooldownIsActive)
        {
            CooldownTimer += Time.deltaTime;
            if (CooldownTimer >= CooldownTime)
            {
                CooldownIsActive=false;
                CooldownTimer=0;
            }
        }
    }
}