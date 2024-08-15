using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Fireball : BulletMove
{
    // Start is called before the first frame update
    
    public float radius;
    public float duration;
    public GameObject spin;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(Fire());
        transform.localScale = new Vector3(radius, radius, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy m_enemy = collision.GetComponent<Enemy>();
            m_enemy.StartCoroutine(m_enemy.Fire(damage));
        }
    }
    private void Update()
    {
        spin.transform.rotation *= Quaternion.Euler(0, 0, 2);
        spin.transform.localScale += new Vector3(0.01f, 0.01f, 0);
    }
    IEnumerator Fire()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
