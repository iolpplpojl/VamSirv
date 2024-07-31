using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Missile : BulletMove
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
                Enemy m_enemy = hitcol.GetComponent<Enemy>();
                if (m_enemy.Death == false)
                {
                    if (Crit == false)
                    {
                        m_enemy.GetDamage(damage, true);
                    }
                    else
                    {
                        m_enemy.GetCritDamage(damage, true);
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
