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
    public float SideArmAttackSpeedper;
    [Space]

    public int damage;
    public float damagePer = 1;
    public float SideArmDamagePer;

    [Space]

    public int ammo;
    public int maxammo;
    public int maxammonow;
    public float maxammoPer = 1;
    [Space]

    public float speed;
    public float speedPer = 1f;
    public float slow = 0f;
    public int onSlow = 0;
    [Space]

    public float critPer = 0f;
    [Space]

    public float reloadtime;
    public float reloadtimenow;
    public float reloadtimePer = 1f;
    protected bool reloading = false;
    [Space]

    public float skillAcooltime;
    public float skillAcooltimenow;
    public float skillAcoolPer = 1f;

    public float skillBcooltime;
    public float skillBcooltimenow;
    public float skillBcoolPer = 1f;
    [Space]

    public float GetAttckTime;
    public float GetAttackTimenow;
    [Space]
    public float BloodSuck;
    public float fire;
    public int fireCount;
    public int fireDamage = 0;

    public int ExplodeCount = 0;
    int windwalk = 0;
    public float windwalk_now = 0;

    public int armor = 0;

    protected bool Death = false;
    public Transform shotpoint;
    protected SpriteRenderer spriteRenderer;
    protected Vector2 inputVec;
    protected Rigidbody2D rigid;
    public AudioClip[] Effects;
    public AudioClip[] ReloadSound;

    public AudioClip[] Pain;
    [Header("Default")]
    protected Rewardsystem rewardsystem;
    public Sprite[] SkillIcon;

  

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

            if (rewardsystem.Selected != true && ExpSystem.instance.selecting != true)
            {
                if (Input.GetMouseButton(0) && attackspeed_now <= 0 && reloading == false)
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
                if (Input.GetKeyDown(KeyCode.R) && reloading == false && maxammonow != ammo)
                {
                    Reload();
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
            if(windwalk_now > 0)
            {
                windwalk_now -= (Time.fixedDeltaTime*0.2f*windwalk);
            }

            if (reloading == true)
            {
                reloadtimenow -= Time.fixedDeltaTime;
                if (reloadtimenow <= 0)
                {
                    ammo = maxammonow;
                    reloading = false;
                    if (ReloadSound != null)
                    {
                        SFXsystem.instance.PlaySoundFX(ReloadSound[1], transform, 0.5f);
                    }
                }
            }
        }
    }
    private void Move()
    {
        Vector2 norVec = inputVec.normalized * (speed*(speedPer+windwalk_now-slow)) * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + norVec);
    }
    protected void Reload()
    {
        reloading = true;
        reloadtimenow = reloadtime * (1f / reloadtimePer);
        if(ReloadSound != null)
        {
            SFXsystem.instance.PlaySoundFX(ReloadSound[0],transform,0.5f);
        }
    }
    public void GetItem(int idx, int rare)
    {
        switch (rare)
        {
            case 0:
                switch (idx)
                {
                    case 0:
                        damagePer += 0.04f;
                        attackspeedPer += 0.03f;
                        maxHealthGet(-0.03f);
                        break;
                    case 1:
                        attackspeedPer += 0.06f;
                        damagePer -= 0.03f;
                        break;
                    case 2:
                        maxAmmoGet(0.03f);
                        attackspeedPer += 0.02f;
                        SpeedGet(-0.01f);
                        break;
                    case 3:
                        critPer += 0.03f;
                        damagePer += 0.03f;
                        attackspeedPer -= 0.03f;
                        break;
                    case 4:
                        maxHealthGet(0.04f);
                        SpeedGet(-0.01f);
                        break;
                    case 5:
                        SpeedGet(0.04f);
                        damagePer += 0.04f;
                        attackspeedPer += 0.04f;
                        maxHealthGet(-0.02f);
                        armor--;
                        break;
                    case 6:
                        attackspeedPer -= 0.01f;
                        damagePer += 0.04f;
                        break;
                }
                break;
            case 1:
                switch (idx)
                {
                    case 0:
                        armor += 2;
                        maxHealthGet(0.06f);
                        SpeedGet(-0.02f);
                        break;
                    case 1:
                        fireCount++;
                        attackspeedPer += 0.02f;
                        break;
                    case 2:
                        BloodSuck += 0.02f;
                        attackspeedPer += 0.04f;
                        critPer -= 0.02f;
                        break;
                    case 3:
                        maxHealthGet(0.02f);
                        SpeedGet(0.02f);
                        maxAmmoGet(0.02f);
                        break;
                    case 4:
                        damagePer += 0.02f;
                        attackspeedPer += 0.02f;
                        break;
                    case 5:
                        reloadtimePer += 0.02f;
                        maxAmmoGet(0.03f);
                        attackspeedPer += 0.04f;
                        damagePer -= 0.02f;
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
                        maxHealthGet(-0.01f);
                        break;
                    case 1:
                        damagePer += 0.12f;
                        attackspeedPer -= 0.03f;
                        speedPer -= 0.05f;
                        break;
                    case 2:
                        maxAmmoGet(0.05f);
                        attackspeedPer += 0.02f;
                        SpeedGet(-0.02f);
                        break;
                    case 3:
                        critPer += 0.03f;
                        damagePer += 0.03f;
                        attackspeedPer -= 0.03f;
                        break;
                    case 4:
                        maxHealthGet(0.06f);
                        SpeedGet(-0.02f);
                        break;
                    case 5:
                        SpeedGet(0.04f);
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
                        armor -= 20;
                        damagePer += 0.66f;
                        break;
                    case 1:
                        ExplodeCount++;
                        break;
                    case 2:
                        maxAmmoGet(0.05f);
                        attackspeedPer += 0.02f;
                        SpeedGet(-0.02f);
                        break;
                    case 3:
                        critPer += 0.03f;
                        damagePer += 0.03f;
                        attackspeedPer -= 0.03f;
                        break;
                    case 4:
                        maxHealthGet(0.06f);
                        SpeedGet(-0.02f);
                        break;
                    case 5:
                        SpeedGet(0.04f);
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
                        fireDamage++;
                        break;
                    case 1:
                        attackspeedPer += 0.06f;
                        damagePer -= 0.02f;
                        break;
                    case 2:
                        maxAmmoGet(0.05f);
                        attackspeedPer += 0.02f;
                        SpeedGet(-0.02f);
                        break;
                    case 3:
                        critPer += 0.03f;
                        damagePer += 0.03f;
                        attackspeedPer -= 0.03f;
                        break;
                    case 4:
                        maxHealthGet(0.06f);
                        SpeedGet(-0.02f);
                        break;
                    case 5:
                        SpeedGet(0.04f);
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
    {
        switch (idx)
        {
            case 0:
                damagePer += 0.01f * rairty;
                break;
            case 1:
                speedPer += 0.01f * rairty;
                break;
            case 2:
                maxHealthGet(0.01f * rairty);
                break;
            case 3:
                attackspeedPer += 0.01f * rairty;
                break;
            case 4:
                BloodSuck += 0.01f * rairty;
                break;
        }
    }
    public abstract void UniqueLevelUP(int idx);

    protected void SpeedGet(float Per)
    {
        speedPer += Per;
    }
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
    public void GetDamage(int damage)
    {
        if (GetAttackTimenow <= 0)
        {
            int dmg = (damage - armor);
            if (dmg > 0)
            {
                DamagePopupSystem.instance.Setup(transform, dmg, 50, "Blood");
                SFXsystem.instance.PlaySoundFX(Pain, transform, 0.75f);
                health -= dmg;
                GetAttackTimenow = GetAttckTime;
                if (health <= 0)
                {
                    Dead();
                }
            }


            Debug.Log("Player Damaged : " + damage + "armor decrese : " + armor + "result : " + (damage - armor));

        }
    }
    void Dead()
    {
        Death = true;
        GameObject.FindWithTag("System").SetActive(false);
        GameObject.FindWithTag("Deathmanager").GetComponent<Deathmanager>().Death();
    }

    public void EnemyKill()
    {
        if(windwalk >= 1)
        {
            windwalk_now = 0.1f * windwalk;
        }
    }
    abstract public void GetUniqueItem(int idx);

    public IEnumerator Slow(float Per, float time)
    {
        onSlow++;
        if (slow < Per)
        {
            slow = Per;
        }
        yield return new WaitForSeconds(time);
        onSlow--;
        if (onSlow <= 0)
        {
            slow = 0;
        }
        yield break;
    }
}
