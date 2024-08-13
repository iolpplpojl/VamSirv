using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Fireball : BulletMove
{
    // Start is called before the first frame update
    
    public float radius;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var hit = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Enemy"));
            foreach (var hitcol in hit)
            {
                if (hitcol.isTrigger)
                {
                    Enemy m_enemy = hitcol.GetComponent<Enemy>();
                    if (m_enemy.Death == false)
                    {
                        if (Crit == false)
                        {
                            m_enemy.GetDamage(damage, true);
                            m_enemy.StartCoroutine(m_enemy.Fire(damage / 5));
                        }
                        else
                        {
                            m_enemy.GetCritDamage(damage, true);
                            m_enemy.StartCoroutine(m_enemy.Fire(damage / 5));
                        }
                    }
                }
            }
            Destroy(gameObject);

        }
        if (collision.CompareTag("wall"))
        {
            Destroy(gameObject);
        }

    }
}
