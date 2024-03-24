using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public int health;
    public int maxHealth;
    public int maxHealthNow;
    public float maxHealthPer = 1f;
    [Space]

    public float attackspeed; // 1 / attackspeed;
    public float attackspeed_now;
    public float attackspeedPer = 1;
    public float SideArmAttackSpeddper;
    [Space]

    public int damage;
    public float damagePer = 1;
    [Space]

    public int ammo;
    public int maxammo;
    public int maxammonow;
    public float maxammoPer = 1;
    [Space]

    public float speed;
    public float speedPer = 1f;
    [Space]

    public float critPer = 0f;
    [Space]

    public float reloadtime;
    public float reloadtimenow;
    protected bool reloading = false;
    [Space]

    public float skillAcooltime;
    public float skillAcooltimenow;
    public float skillBcooltime;
    public float skillBcooltimenow;
    [Space]

    public float GetAttckTime;
    public float GetAttackTimenow;
    [Space]

    bool Death = false;
    public Transform shotpoint;
    SpriteRenderer spriteRenderer;
    Vector2 inputVec;
    Rigidbody2D rigid;
    public AudioClip[] Effects;
    Rewardsystem rewardsystem;


    public abstract void Attack();
    public abstract void Skill_A();
    public abstract void Skill_B();
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxammonow = maxammo;
        ammo = maxammonow;
        maxHealthNow = maxHealth;
        health = maxHealthNow;
    }
    
    public void GetRewardsystem(Rewardsystem rewardsystem)
    {
        this.rewardsystem = rewardsystem;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Death)
        {
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");

            if (rewardsystem.Selected != true)
            {
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
        Vector2 norVec = inputVec.normalized * (speed*speedPer) * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + norVec);
    }
    protected IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadtime);
        ammo = maxammonow;
        reloading = false;
    }
    public void GetItem(int idx)
    {
        switch (idx)
        {
            case 0:
                damagePer += 0.05f;
                attackspeedPer += 0.03f;
                break;
            case 1:
                attackspeedPer += 0.1f;
                damagePer -= 0.02f;
                break;
            case 2:
                maxAmmoGet(0.05f);
                attackspeedPer += 0.02f;
                speedPer -= 0.02f;
                break;
            case 3:
                critPer += 0.05f;
                damagePer += 0.03f;
                break;
            case 4:
                maxHealthGet(0.3f);
                speedPer -= 0.02f;
                break;
            case 5:
                speedPer += 0.07f;
                damagePer += 0.03f;
                attackspeedPer += 0.03f;
                maxHealthGet(-0.05f);
                break;
            case 500:
                GetUniqueItem(0);
                break;
            case 501:
                GetUniqueItem(1);
                break;
        }
    }

    void maxAmmoGet(float Per)
    {
        maxammoPer += Per;
        maxammonow = (int)(maxammo * maxammoPer);
    }
    void maxHealthGet(float Per)
    {
        maxHealthPer += Per;
        maxHealthNow = (int)(maxHealth * maxHealthPer);
        health = maxHealthNow;
    }
    public void GetDamage(int damage)
    {
        if (GetAttackTimenow <= 0)
        {
            health -= damage;
            Debug.Log("Player Damaged : " + damage);
            GetAttackTimenow = GetAttckTime;
            if(health <= 0)
            {
                Dead();
            }
        }
    }
    void Dead()
    {
        Death = true;
        GameObject.FindWithTag("System").SetActive(false);
        GameObject.FindWithTag("Deathmanager").GetComponent<Deathmanager>().Death();
    }
    abstract public void GetUniqueItem(int idx);
}
