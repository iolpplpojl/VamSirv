using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int damage;
    public int bullethrough;
    public bool Crit;
    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        rigid.velocity = transform.up * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy m_enemy = collision.GetComponent<Enemy>();
            if (m_enemy.Death == false)
            {
                bullethrough--;
                if (bullethrough == 0)
                {
                    if (Crit == false)
                    {
                        m_enemy.GetDamage(damage);
                    }
                    else
                    {
                        m_enemy.GetCritDamage(damage);
                    }
                    Destroy(gameObject);
                }
                else if (bullethrough > 0)
                {
                    if (Crit == false)
                    {
                        collision.GetComponent<Enemy>().GetDamage(damage);
                    }
                    else
                    {
                        collision.GetComponent<Enemy>().GetCritDamage(damage);
                    }
                }
            }
        }
        if (collision.CompareTag("wall"))
        {
            Destroy(gameObject);
        }

    }
}