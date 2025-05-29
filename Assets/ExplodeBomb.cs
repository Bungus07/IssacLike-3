using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBomb : MonoBehaviour
{
    public Collider2D ExplosionRadius;
    public float ExplosionTime;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ExplosionRadius.enabled = false;
        StartCoroutine(Explosion());
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(ExplosionTime);
        ExplosionRadius.enabled = true;
        animator.SetTrigger("Boom");
    }
    public void DestroyObject()
    {
        GameObject.Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
