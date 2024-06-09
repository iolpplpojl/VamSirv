using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Golem : Enemy
{
    // Start is called before the first frame update
    public int Damage;
    public float Attackspeed;
    public float Attackspeed_now;
<<<<<<< Updated upstream
=======
    public float Bulletspeed;
>>>>>>> Stashed changes
    public GameObject Bullet;
    private void FixedUpdate()
    {
        if(Attackspeed_now > 0)
        {
            Attackspeed_now -= Time.fixedDeltaTime;
        }
        if(Attackspeed_now <= 0)
        {
            Attack();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().GetDamage(Damage);
        }

    }
    override public void Attack()
    {
        GameObject m_Bull = Instantiate(Bullet, transform.position, Quaternion.LookRotation(Vector3.forward, target.transform.position-transform.position),transform.parent);
<<<<<<< Updated upstream
        m_Bull.GetComponent<Enemy_Bullet>().damage = Damage;
=======
        m_Bull.transform.localScale = new Vector3(5, 5, 1);
        Enemy_Bullet m_Bullet = m_Bull.GetComponent<Enemy_Bullet>();
        m_Bullet.damage = Damage;
        m_Bullet.speed = Bulletspeed;
>>>>>>> Stashed changes
        Attackspeed_now = Attackspeed;
    }
    public override void Move()
    {
        return;
    }
}
