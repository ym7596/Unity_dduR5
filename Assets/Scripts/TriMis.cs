using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriMis : Missiles
{
    Rigidbody2D rb;

    [SerializeField]
    private GameObject smoke;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        rb.AddForce(new Vector2(0, speed),ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Health health = col.gameObject.GetComponent<Health>();
            health?.TakeDamage(damage);
            //TODO consider Object Pooling here!
            Destroy(gameObject);
            GameObject g = Instantiate(smoke, this.transform.position, Quaternion.identity);
            Destroy(g, 0.5f);
            Debug.Log("hit");

        }
    }
 
   
}
