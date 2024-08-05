using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Sasha : Tower
{

    public Rigidbody2D rig;
    public SpriteRenderer sprite;
    public float speed;
    public float radius;
    public int damage;
    public int nowdamage;
    public float weak;
    public float duration;
    // Update is called once per frame

    private void Start()
    {
        SetDamage();
        nowdamage = (int)(damage * KnifeDamagePer);
        StartCoroutine(DoDamage());
    }
    private void Update()
    {
        return;
    }
    void FixedUpdate()
    {
        MoveMent();
    }

    new protected void MoveMent()
    {
        if (Find == false)
        {
            FindEnemy();
        }
        else if (Find == true)
        {
            if (Target != null)
            {
                // transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position - Target.transform.position);
                Vector3 dir = (Vector2)Target.transform.position - rig.position;
                Vector3 nextVec = dir.normalized * speed * Time.fixedDeltaTime;
                if(dir.x > 0)
                {
                    sprite.flipX = false;
                }
                else
                {
                    sprite.flipX = true;
                }
                rig.MovePosition((Vector3)rig.position+nextVec);
                float distance = Vector3.Distance(transform.position, Target.transform.position);
                if (distance > Radius)
                {
                    Find = false;
                }
            }
            else
            {
                Find = false;
            }

        }
    }

    IEnumerator DoDamage()
    {
        while (true)
        {
            var hit = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach(var col in hit)
            {
                if (col.CompareTag("Enemy") && col.isTrigger)
                {
                    var enemy = col.GetComponent<Enemy>();
                    enemy.GetDamage(nowdamage, true);
                    enemy.GetWeak(weak, duration);
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
