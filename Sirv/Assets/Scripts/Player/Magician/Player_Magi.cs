using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Magi : Player
{

    float AttackRadius = 1.0f;
    public GameObject FireBall;
    float FireBallRadius = 1.5f;
    public GameObject Attackpos;
    public bool rushing;
    int GenAmount = 1;
    Vector2 end;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxammonow = maxammo;
        ammo = maxammonow;
        maxHealthNow = maxHealth;
        health = maxHealthNow;
        StartCoroutine(Gen());
    }

    IEnumerator Gen()
    {
        while (true)
        {
            Heal(GenAmount);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator Knife(Vector2 attackpos)
    {
        for(int i = 1;  i <= 2; i++)
        {
            List<Collider2D> nonTriggerHits = new List<Collider2D>(5);

            var hit = Physics2D.OverlapCircleAll(attackpos, AttackRadius, LayerMask.GetMask("Enemy"));

            if (hit != null)
            {
                foreach (var collider in hit)
                {
                    if (!collider.isTrigger)
                    {
                        nonTriggerHits.Add(collider);
                    }

                    if (nonTriggerHits.Count >= 5)
                    {
                        break;
                    }
                }

                foreach (var collider in nonTriggerHits)
                {
                    Enemy hitcol = collider.GetComponent<Enemy>();
                    if (hitcol != null && !hitcol.Death)
                    {
                        if ((int)(critPer * 100) > Random.Range(0, 100))
                        {
                            hitcol.GetCritDamage((int)(damage * damagePer), true);
                        }
                        else
                        {
                            hitcol.GetDamage((int)(damage * damagePer), true);
                        }
                    }
                }
            }

            attackspeed_now = 1.0f / (attackspeed * Mathf.Clamp(attackspeedPer, 0.00001f, 99999999));
            yield return new WaitForSeconds(0.33f*1/attackspeedPer);
        }
    }
    public override void Attack()
    {
        StartCoroutine(Knife(Attackpos.transform.position));
    }

    public override void GetUniqueItem(int idx)
    {
        throw new System.NotImplementedException();
    }

    public override void Skill_A()
    {
        StartCoroutine(Rush());
    }

    public override void Skill_B()
    {
        GameObject Bul = Instantiate(FireBall, shotpoint.position, shotpoint.rotation);
        Bullet_Fireball BulComp = Bul.GetComponent<Bullet_Fireball>();
        BulComp.damage = (int)(20 * damagePer);
        BulComp.bullethrough = 1;
        BulComp.radius = FireBallRadius;
        //SFXsystem.instance.PlaySoundFX(Effects[2], transform, 1f);
        skillBcooltimenow = skillBcooltime * (1 / skillBcoolPer);
    }

    public override void UniqueLevelUP(int idx)
    {
        throw new System.NotImplementedException();
    }

    override protected void Move()
    {
        if (rushing == false)
        {
            Vector2 norVec = inputVec.normalized * (speed * (speedPer + windwalk_now - slow)) * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + norVec);
        }
    }
    override public void GetDamage(int damage)
    {
        if (!rushing)
        {
            base.GetDamage(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(rushing == true)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Enemy>().GetDamage(120,true);
            }
        }
    }
    IEnumerator Rush()
    {
        skillAcooltimenow = 999;
        rushing = true;
        end = (Vector2)shotpoint.position + (Vector2)shotpoint.up.normalized * 2.5f;
        Debug.Log(end);
        Debug.Log(end.normalized);
        StartCoroutine(RushWall());
        while (rushing)
        {
            if (Vector2.Distance(shotpoint.position, end) <= 0.1f)
            {
                rushing = false;
            }
            Vector2 temp = (end - (Vector2)shotpoint.position).normalized * 9 * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + temp);
            yield return new WaitForFixedUpdate();
        }                                               
        Debug.Log("Äô");
        rushing = false;
        end = Vector2.zero;
        skillAcooltimenow = skillAcooltime * (1 / skillAcoolPer);
        yield break;
    }

    IEnumerator RushWall()
    {
        yield return new WaitForSeconds(0.2f);
        while (rushing)
        {
            var hit = Physics2D.OverlapCircle(transform.position, 0.6f, LayerMask.GetMask("Water"));
            if (hit != null)
            {
                rushing = false;
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }
        // Start is called before the first frame update
        void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Attackpos.transform.position, AttackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.6f);


    }
}
