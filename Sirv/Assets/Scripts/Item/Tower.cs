using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    public Player PlayerComp;
    public float KnifeDamagePer = 0f;
    public float Radius;
    public float Attackspped;
    public float Attackspeed_Now = 0;
    protected bool Find = false;
    protected GameObject Target;

    public bool UpgradeTool = false;

    // Update is called once per frame
    private void Start()
    {
        SetDamage();
    }
    void Update()
    {
        MoveMent();
    }

    public void GetPlayerComp(Player player)
    {
        this.PlayerComp = player;
    }

    protected void SetDamage()
    {
        KnifeDamagePer = PlayerComp.damagePer;
    }

    protected void MoveMent()
    {
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
