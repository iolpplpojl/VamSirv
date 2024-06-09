using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public int health;
    
    public float attackspeed; // 1 / attackspeed;
    public float attackspeed_now;
    public float attackspeedPer = 1;

    public int damage;
    public float damagePer = 1;

    public int ammo;
    public int maxammo;
    public int maxammonow;
    public float maxammoPer = 1;

    public float speed;
<<<<<<< Updated upstream
    public float speed_now;
    public float speedPer;
=======
    public float speedPer = 1f;
    [Space]
>>>>>>> Stashed changes

    public float critPer = 0f;

    public float reloadtime;
    public float reloadtimenow;
    protected bool reloading = false;

    public float skillAcooltime;
    public float skillAcooltimenow;
    public float skillBcooltime;
    public float skillBcooltimenow;

    public float GetAttckTime;
    public float GetAttackTimenow;
<<<<<<< Updated upstream
=======
    [Space]
    public float BloodSuck;
    public float fire;
    public int fireCount;

    int windwalk = 0;
    public float windwalk_now = 0;
>>>>>>> Stashed changes

    bool Death = false;
    public Transform shotpoint;
    SpriteRenderer spriteRenderer;
    Vector2 inputVec;
    Rigidbody2D rigid;
<<<<<<< Updated upstream
=======
    public AudioClip[] Effects;
    [Header("Default")]
    Rewardsystem rewardsystem;

  
>>>>>>> Stashed changes

    public abstract void Attack();
    public abstract void Skill_A();
    public abstract void Skill_B();
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxammonow = maxammo;
        ammo = maxammonow;

    }

    // Update is called once per frame
    void Update()
    {
        if (!Death)
        {
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");

            if (Input.GetMouseButton(0) && attackspeed_now <= 0)
            {
                Attack();
            }
            if (Input.GetMouseButtonDown(1) && skillAcooltimenow <= 0)
            {
                Skill_A();
            }
            if (Input.GetKeyDown(KeyCode.E) && skillBcooltimenow <= 0)
            {
                Skill_B();
            }

            if (shotpoint.rotation.eulerAngles.z < 180)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (!Death)
        {
            Move();
            if (attackspeed_now > 0)
            {
                attackspeed_now -= Time.fixedDeltaTime;
            }
            if (skillAcooltimenow > 0)
            {
                skillAcooltimenow -= Time.fixedDeltaTime;
            }
            if (skillBcooltimenow > 0)
            {
                skillBcooltimenow -= Time.fixedDeltaTime;
            }
            if (GetAttackTimenow > 0)
            {
                GetAttackTimenow -= Time.fixedDeltaTime;
            }

            if (reloading == true)
            {
                reloadtimenow -= Time.fixedDeltaTime;
                if (reloadtimenow <= 0)
                {
                    ammo = maxammonow;
                    reloading = false;
                }
            }
        }
    }
    private void Move()
    {
<<<<<<< Updated upstream
        Vector2 norVec = inputVec.normalized * speed * Time.fixedDeltaTime;
=======
        Vector2 norVec = inputVec.normalized * (speed*(speedPer+windwalk_now)) * Time.fixedDeltaTime;
>>>>>>> Stashed changes
        rigid.MovePosition(rigid.position + norVec);
    }
    protected IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadtime);
        ammo = maxammonow;
        reloading = false;
    }
<<<<<<< Updated upstream
    public void GetItem(int idx)
=======
    public void GetItem(int idx, int rare)
    {
        switch (rare) {
            case 0:
                    switch (idx)
                    {
                        case 0:
                            damagePer += 0.05f;
                            attackspeedPer += 0.03f;
                            maxHealthGet(-0.03f);
                            break;
                        case 1:
                            attackspeedPer += 0.06f;
                            damagePer -= 0.02f;
                            break;
                        case 2:
                            maxAmmoGet(0.05f);
                            attackspeedPer += 0.02f;
                            speedPer -= 0.02f;
                            break;
                        case 3:
                            critPer += 0.03f;
                            damagePer += 0.03f;
                            attackspeedPer -= 0.03f;
                            break;
                        case 4:
                            maxHealthGet(0.06f);
                            speedPer -= 0.02f;
                            break;
                        case 5:
                            speedPer += 0.04f;
                            damagePer += 0.03f;
                            attackspeedPer += 0.03f;
                            maxHealthGet(-0.05f);
                            break;
                        case 6:
                            attackspeedPer -= 0.01f;
                            damagePer += 0.04f;
                            break;
                        case 7:
                            skillAcoolPer -= 0.01f;
                            critPer -= 0.01f;
                            break;
                        case 8:
                            skillBcoolPer -= 0.01f;
                            critPer -= 0.01f;
                            break;
                    }
                    break;
            case 1:
                switch (idx)
                {
                    case 0:
                        damagePer += 0.05f;
                        attackspeedPer += 0.03f;
                        maxHealthGet(-0.03f);
                        break;
                    case 1:
                        attackspeedPer += 0.06f;
                        damagePer -= 0.02f;
                        break;
                    case 2:
                        maxAmmoGet(0.05f);
                        attackspeedPer += 0.02f;
                        speedPer -= 0.02f;
                        break;
                    case 3:
                        critPer += 0.03f;
                        damagePer += 0.03f;
                        attackspeedPer -= 0.03f;
                        break;
                    case 4:
                        maxHealthGet(0.06f);
                        speedPer -= 0.02f;
                        break;
                    case 5:
                        speedPer += 0.04f;
                        damagePer += 0.03f;
                        attackspeedPer += 0.03f;
                        maxHealthGet(-0.05f);
                        break;
                    case 6:
                        Towersystem.instance.GetTower(0);
                        break;
                    case 500:
                        GetUniqueItem(0);
                        break;
                    case 501:
                        GetUniqueItem(1);
                        break;
                }
                break;
            case 2:
                switch (idx)
                {
                    case 0:
                        windwalk++;
                        maxHealthGet(-0.03f);
                        break;
                    case 1:
                        fireCount++;
                        break;
                    case 2:
                        maxAmmoGet(0.05f);
                        attackspeedPer += 0.02f;
                        speedPer -= 0.02f;
                        break;
                    case 3:
                        critPer += 0.03f;
                        damagePer += 0.03f;
                        attackspeedPer -= 0.03f;
                        break;
                    case 4:
                        maxHealthGet(0.06f);
                        speedPer -= 0.02f;
                        break;
                    case 5:
                        speedPer += 0.04f;
                        damagePer += 0.03f;
                        attackspeedPer += 0.03f;
                        maxHealthGet(-0.05f);
                        break;
                    case 6:
                        attackspeedPer -= 0.01f;
                        damagePer += 0.04f;
                        break;
                    case 500:
                        GetUniqueItem(0);
                        break;
                    case 501:
                        GetUniqueItem(1);
                        break;
                }
                break;
            case 3:
                switch (idx)
                {
                    case 0:
                        damagePer += 0.05f;
                        attackspeedPer += 0.03f;
                        maxHealthGet(-0.03f);
                        break;
                    case 1:
                        attackspeedPer += 0.06f;
                        damagePer -= 0.02f;
                        break;
                    case 2:
                        maxAmmoGet(0.05f);
                        attackspeedPer += 0.02f;
                        speedPer -= 0.02f;
                        break;
                    case 3:
                        critPer += 0.03f;
                        damagePer += 0.03f;
                        attackspeedPer -= 0.03f;
                        break;
                    case 4:
                        maxHealthGet(0.06f);
                        speedPer -= 0.02f;
                        break;
                    case 5:
                        speedPer += 0.04f;
                        damagePer += 0.03f;
                        attackspeedPer += 0.03f;
                        maxHealthGet(-0.05f);
                        break;
                    case 6:
                        attackspeedPer -= 0.01f;
                        damagePer += 0.04f;
                        break;
                    case 500:
                        GetUniqueItem(0);
                        break;
                    case 501:
                        GetUniqueItem(1);
                        break;
                }
                break;
            case 4:
                switch (idx)
                {
                    case 0:
                        damagePer += 0.05f;
                        attackspeedPer += 0.03f;
                        maxHealthGet(-0.03f);
                        break;
                    case 1:
                        attackspeedPer += 0.06f;
                        damagePer -= 0.02f;
                        break;
                    case 2:
                        maxAmmoGet(0.05f);
                        attackspeedPer += 0.02f;
                        speedPer -= 0.02f;
                        break;
                    case 3:
                        critPer += 0.03f;
                        damagePer += 0.03f;
                        attackspeedPer -= 0.03f;
                        break;
                    case 4:
                        maxHealthGet(0.06f);
                        speedPer -= 0.02f;
                        break;
                    case 5:
                        speedPer += 0.04f;
                        damagePer += 0.03f;
                        attackspeedPer += 0.03f;
                        maxHealthGet(-0.05f);
                        break;
                    case 6:
                        attackspeedPer -= 0.01f;
                        damagePer += 0.04f;
                        break;
                    case 500:
                        GetUniqueItem(0);
                        break;
                    case 501:
                        GetUniqueItem(1);
                        break;
                }
                break;
        }
    }

    public void LevelUP(int idx, int rairty)
>>>>>>> Stashed changes
    {
        switch (idx)
        {
            case 0:
                damagePer += 0.05f;
                break;
            case 1:
                attackspeedPer += 0.03f;
                break;
            case 2:
                maxammoPer+= 0.05f;
                maxammonow = (int)(maxammo * maxammoPer);
                break;
            case 3:
                critPer += 0.03f;
                break;
            case 500:
                GetUniqueItem(0);
                break;
            case 501:
                GetUniqueItem(1);
                break;
        }
    }

<<<<<<< Updated upstream
=======
    protected void maxAmmoGet(float Per)
    {
        maxammoPer += Per;
        maxammonow = (int)(maxammo * maxammoPer);
    }
    protected void maxHealthGet(float Per)
    {
        maxHealthPer += Per;
        maxHealthNow = (int)(maxHealth * maxHealthPer);
        health = maxHealthNow;
    }
>>>>>>> Stashed changes
    public void GetDamage(int damage)
    {
        if (GetAttackTimenow <= 0)
        {
            health -= damage;
            Debug.Log("Player Damaged : " + damage);
            GetAttackTimenow = GetAttckTime;
            if(health < 0)
            {
                Dead();
            }
        }
    }
    void Dead()
    {
        Death = true;
        GameObject.FindWithTag("Deathmanager").GetComponent<Deathmanager>().Death();
    }
    abstract public void GetUniqueItem(int idx);
}
