using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;
    Quaternion OriginalRotation;
    public Player PlayerComp;
    public float KnifeDamagePer = 0f;
    public int Rarity; 
    public float Radius;
    public float Attackspped;
    public float Attackspeed_Now = 0;
    protected bool Find = false;
    protected GameObject Target;

    public bool UpgradeTool = false;

    // Update is called once per frame
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
    protected void Upgrade()
    {
        Rarity++;
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
            case 3:
                KnifeDamagePer = PlayerComp.damagePer * 0.48f;
                break;
            case 4:
                KnifeDamagePer = PlayerComp.damagePer * 0.96f;
                break;
        }

    }
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
            transform.rotation = OriginalRotation;
            Find = false;
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
