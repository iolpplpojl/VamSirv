using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_obelisk : Boss
{
    
    public GameObject Orc;
    public GameObject Bullet;
    public float[] patternTimeTable;
    public float attackTimenow = 3.0f;



    public override void Attack()
    {
        int temp = Random.Range(0, patternTimeTable.Length);
        switch (temp)
        {
            case 0:
                StartCoroutine(Shoot1());
                attackTimenow = patternTimeTable[temp];
                break;
            case 1: 
                break;

            case 2: 
                break;
        }
    }
    private void FixedUpdate()
    {
        if (attackTimenow > 0)
        {
            attackTimenow -= Time.deltaTime;
        }
        if (attackTimenow <= 0)
        {
            Attack();
        }
    }
    public override void DeadUniq()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        return;
    }

    // Start is called before the first frame update


    IEnumerator Shoot1()
    {
        for(int i = 0; i < 12; i++)
        {
            for(int p = -6; p <= 6; p++)
            {
                GameObject bul = Instantiate(Bullet, transform.position,Quaternion.Euler(0,0,-180 + 30 * p + 13*i),transform.parent);
                bul.transform.localScale = new Vector3(5, 5, 1);
                Enemy_Bullet BulComp = bul.GetComponent<Enemy_Bullet>();
                BulComp.damage = Damage;
                BulComp.speed = 6;
            }
            yield return new WaitForSeconds(0.15f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (flashtime > 0)
        {
            flashtime -= Time.deltaTime * 5f;
            Sprite.material.SetFloat("_FlashAmount", flashtime);
        }
    }
}
