using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Normal : Enemy
{
    // Start is called before the first frame update

    // Update is called once per frame

    private void FixedUpdate()
    {
        Move();

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
        return;
    }
    public override void Move()
    {
        Vector2 dirvec = targetrigid.position - rigid.position;
        Vector2 nextvec = dirvec.normalized * (Slow * speed) * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextvec);
        rigid.velocity = Vector2.zero;
    }
}
