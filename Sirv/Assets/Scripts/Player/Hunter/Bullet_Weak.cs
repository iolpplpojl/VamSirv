using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Weak : BulletMove
{

    public float Weakness;
    public float WeakDuration;
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
                        m_enemy.GetDamage(damage, true);
                        m_enemy.GetWeak(Weakness, WeakDuration);
                    }
                    else
                    {
                        m_enemy.GetCritDamage(damage, true);
                        m_enemy.GetWeak(Weakness, WeakDuration);
                    }
                    Destroy(gameObject);
                }
                else if (bullethrough > 0)
                {
                    if (Crit == false)
                    {
                        collision.GetComponent<Enemy>().GetDamage(damage, true);
                        m_enemy.GetWeak(Weakness, WeakDuration);
                    }
                    else
                    {
                        collision.GetComponent<Enemy>().GetCritDamage(damage, true);
                        m_enemy.GetWeak(Weakness, WeakDuration);
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
