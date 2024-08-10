using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Orc : Enemy_Normal
{
    // Start is called before the first frame update
    public Animator anim;
    bool attacking;

    public float Radius;
    private void FixedUpdate()
    {
        if (!attacking)
        {
            Move();
        }
        Attack();

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().GetDamage(Damage/2);
        }
    }

    public override void Attack()
    {
        if (!attacking)
        {
            var hit = Physics2D.OverlapCircle(transform.position, Radius*0.44f, LayerMask.GetMask("Player"));
            if (hit != null)
            {
                StartCoroutine(boom());
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Radius*0.44f);

    }
    IEnumerator boom()
    {
        attacking = true;
        anim.Play("orc_boom");
        yield return new WaitForSeconds(1.35f);
        var hit = Physics2D.OverlapCircle(transform.position, Radius, LayerMask.GetMask("Player"));
        if(hit != null)
        {
            hit.GetComponent<Player>().GetDamage(Damage);
        }
        yield return new WaitForSeconds(0.33f);
        attacking = false;
        yield break;   
    }
}
