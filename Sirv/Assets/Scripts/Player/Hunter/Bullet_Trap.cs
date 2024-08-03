using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Trap : BulletMove
{
    // Start is called before the first frame update
    public GameObject FireGrid;
    public float radius;
    void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameObject Fire = Instantiate(FireGrid, transform.position, Quaternion.identity);
            Firegrid m_Fire = Fire.GetComponent<Firegrid>();
            m_Fire.radius = radius;
            m_Fire.Damage = damage;
            Destroy(gameObject);
        }
        if (collision.CompareTag("wall"))
        {
            Destroy(gameObject);
        }

    }
}
