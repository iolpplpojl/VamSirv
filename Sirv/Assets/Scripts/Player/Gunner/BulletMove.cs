using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int damage;
    public int bullethrough;
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
            bullethrough--;
            if (bullethrough == 0)
            {
                collision.GetComponent<Enemy>().GetDamage(damage);
                Destroy(gameObject);
            }
            else if( bullethrough > 0)
            {
                collision.GetComponent<Enemy>().GetDamage(damage);
            }
        }
        else if (collision.CompareTag("Dynamite"))
        {
            bullethrough--;
            if (bullethrough == 0)
            {
                Destroy(gameObject);
            }

        }

    }
}
