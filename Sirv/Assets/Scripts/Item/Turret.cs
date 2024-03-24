using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;
    public Player PlayerComp;
    public float KnifeDamagePer = 0f;
    public int Rarity;
    public int TurretNum;
    public int Require;
    public int Nowamount = 0;
    public float Radius;
    public float Attackspped;
    public float Attackspeed_Now = 0;
    protected bool Find = false;
    protected GameObject Target;
    protected SpriteRenderer Renderer;

    public bool UpgradeTool = false;

    // Update is called once per frame
    private void Start()
    {
        SetDamage();
        SetUpgradeAmount();
        Renderer = GetComponent<SpriteRenderer>();
        if(Renderer == null)
        {
            Renderer = GetComponentInChildren<SpriteRenderer>();

        }
    }
    void Update()
    {
        MoveMent();
        if(UpgradeTool == true)
        {
            Upgrade();
            UpgradeTool = false;
        }
    }

    public void GetPlayerComp(Player player)
    {
        this.PlayerComp = player;
    }
    public void Upgrade()
    {
        Nowamount++;
        if (Rarity !=3 && Require == Nowamount)
        {
            Rarity++;
            SetColor();
            SetUpgradeAmount();
            Nowamount = 0;
        }

    }
    protected void SetUpgradeAmount()
    {
        switch (Rarity)
        {
            case 0:
                Require = 2;
                break;
            case 1:
                Require = 4;
                break;
            case 2:
                Require = 8;
                break;
            case 3:
                Require = 0;
                break;
        }
    }
    protected void SetColor()
    {
        switch (Rarity)
        {
            case 0:
                Renderer.color = new Color(1f, 1f, 1f);
                break;
            case 1:
                Renderer.color = new Color(0.8791348f, 1f, 0.6179246f);
                break;
            case 2:
                Renderer.color = new Color(0.6196079f, 0.721262f, 1f);
                break;
            case 3:
                Renderer.color = new Color(0.8791348f, 0, 0.6179246f);
                break;
        }
    }
    protected void SetDamage()
    {
        switch (Rarity)
        {
            case 0:
                KnifeDamagePer = PlayerComp.damagePer * 0.12f;
                break;
            case 1:
                KnifeDamagePer = PlayerComp.damagePer * 0.24f;
                break;
            case 2:
                KnifeDamagePer = PlayerComp.damagePer * 0.48f;
                break;
            case 3:
                KnifeDamagePer = PlayerComp.damagePer * 0.96f;
                break;
        }
        BonusDamage();

    }
    abstract protected void BonusDamage();

    protected void MoveMent()
    {
        transform.position = Player.position;

        if (Find == false)
        {
            FindEnemy();
        }

        else if (Find == true)
        {
            if (Target != null)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position - Target.transform.position);
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
    void FindEnemy()
    {
        var hit = Physics2D.OverlapCircle(transform.position, Radius, LayerMask.GetMask("Enemy"));
        if (hit != null)
        {
            Target = hit.gameObject;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position - Target.transform.position);
            Find = true;
        }

        if (hit == null)
        {
            Find = false;
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
