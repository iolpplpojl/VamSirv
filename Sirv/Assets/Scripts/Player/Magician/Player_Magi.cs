using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Magi : Player
{

    float AttackRadius = 1.0f;
    public GameObject FireBall;
    float FireBallRadius = 3f;
    float FireBallDuration = 0.20f;

    public GameObject Attackpos;
    public bool rushing;
    bool Engage = false;
    bool damaging = true;
    int GenAmount = 1;
    int attacktarget = 5;
    Vector2 end;
    public GameObject Knifes;

    bool Powerful = false;
    bool FireSword= false;

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
    IEnumerator Knife()
    {
        attackspeed_now = 1.0f / (attackspeed * Mathf.Clamp(attackspeedPer, 0.00001f, 99999999));
        for (int i = 1;  i <= 2; i++)
        {
            List<Collider2D> nonTriggerHits = new List<Collider2D>(5);
            SFXsystem.instance.PlaySoundFX(new AudioClip[] { Effects[4], Effects[5], Effects[6] }, transform, 1f);

            var hit = Physics2D.OverlapCircleAll(Attackpos.transform.position, AttackRadius, LayerMask.GetMask("Enemy"));
            GameObject effect = Instantiate(Knifes, Attackpos.transform.position, shotpoint.rotation);
            effect.transform.localScale = new Vector3(6 * AttackRadius, 6 * AttackRadius, 1);

            if (hit != null)
            {
                foreach (var collider in hit)
                {
                    if (collider.isTrigger)
                    {
                        nonTriggerHits.Add(collider);
                    }

                    if (nonTriggerHits.Count >= attacktarget)
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
                            if(FireSword == true)
                            {
                                hitcol.StartCoroutine(hitcol.Fire((int)(damage * 2 * damagePer * 0.75f)));
                            }
                        }
                        else
                        {
                            hitcol.GetDamage((int)(damage * damagePer), true);
                        }
                        if(Powerful == true)
                        {
                            hitcol.GetDamage((int)(maxHealthNow * 0.15f), true);
                        }
                    }
                }
            }

            yield return new WaitForSeconds(0.33f*1/attackspeedPer);
        }
    }
    public override void Attack()
    {
        StartCoroutine(Knife());
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
        skillBcooltimenow = skillBcooltime * (1 / skillBcoolPer);
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        SFXsystem.instance.PlaySoundFX(new AudioClip[] { Effects[0], Effects[1] }, transform, 1f);
        yield return new WaitForSeconds(0.4f);
        GameObject Bul = Instantiate(FireBall, shotpoint.position, shotpoint.rotation);
        Bullet_Fireball BulComp = Bul.GetComponent<Bullet_Fireball>();
        BulComp.damage = (int)(75 * damagePer);
        BulComp.radius = FireBallRadius;
        BulComp.duration = FireBallDuration;
        BulComp.speed = 8;
    }
    public override void UniqueLevelUP(int idx)
    {
        switch (idx)
        {
            case 1:
                AttackRadius += 0.35f;
                break;
            case 2:
                FireBallDuration += 0.1f;
                FireBallRadius += 1.2f;
                break;
            case 3:
                Engage = true;
                break;
            case 4:
                GenAmount++;
                armor += 5;
                break;
            case 5:
                Towersystem.instance.GetTower(3);
                break;
            case 6:
                AttackRadius += 0.3f;
                FireBallDuration += 0.2f;
                FireBallRadius += 1.8f;
                break;
            case 7:
                Powerful = true;
                break;
            case 8:
                FireSword = true;
                break;
        }
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
        if (damaging)
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
                collision.GetComponent<Enemy>().GetDamage((int)(120*damagePer),true);
            }
        }
    }
    IEnumerator Rush()
    {
        skillAcooltimenow = 999;
        rushing = true;
        damaging = false;
        SFXsystem.instance.PlaySoundFX(new AudioClip[] { Effects[2], Effects[3] }, transform, 1f);
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
        if (!Engage)
        {
            damaging = true;
        }
        else
        {
            speedPer += 0.330000001f;
            yield return new WaitForSeconds(1.0f);
            speedPer -= 0.33f;
            damaging = true;
        }

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
