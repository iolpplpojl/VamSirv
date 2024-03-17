using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid;
    public float speed = 1.0f;
    public int damage;
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
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().GetDamage(damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("wall"))
        {
            Destroy(gameObject);
        }

    }
}
