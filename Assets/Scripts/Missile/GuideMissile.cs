using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideMissile : Missiles
{
    Transform target;
    [SerializeField] GameObject explosion;
    [SerializeField]float rotSpeed = 2f;
    float rotateAmount;

    bool _isEnemy = false;

    public bool isEnemy
    {
        get { return _isEnemy; }
        set
        {
            _isEnemy = value;
            if(_isEnemy == false)
                DestroyMissile();
        }
    }
    Quaternion rotTarget;
  //  Vector3 dir;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
        try
        {
            target = GameObject.Find("Enemy").transform;
            isEnemy = true;
        }
        catch { isEnemy = false;  }
     }

    // Update is called once per frame
    void FixedUpdate()
    {
        GuidedMissile();
    }
    
    void GuidedMissile()
    {
        
            Vector2 dir = (Vector2)target.position - rb.position;

            dir.Normalize();
            rotateAmount = Vector3.Cross(dir, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotSpeed;
        
        rb.velocity = transform.up * speed;

       /* dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotTarget = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotTarget, Time.deltaTime * rotSpeed);
        rb.velocity = new Vector2(dir.x * speed, dir.y * speed);*/
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Health health = col.gameObject.GetComponent<Health>();
            health?.TakeDamage(damage);
            //TODO consider Object Pooling here!
            DestroyMissile();

        }
    }

    private void DestroyMissile()
    {
        GameObject g = Instantiate(explosion, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
           
        Destroy(g, 0.5f);
        Debug.Log("hit");
    }
}
