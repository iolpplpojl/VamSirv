using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Tree : Boss
{
    // Start is called before the first frame update

    public GameObject Rocks;

    public float[] patternTimeTable;
    public float attackTimenow = 3.0f;



    // Update is called once per frame  

    private void FixedUpdate()
    {
        if(attackTimenow> 0)
        {
            attackTimenow -= Time.deltaTime;
        }    
        if(attackTimenow <= 0)
        {
            Attack();
        }
    }

    public override void Move()
    {
        return;
    }

    public override void Attack()
    {
        int temp = Random.Range(0, patternTimeTable.Length);
        switch (temp)
        {
            case 0:
                Boss_Tree_Stone temps = Instantiate(Rocks, transform.position, Quaternion.identity, transform.parent).GetComponent<Boss_Tree_Stone>();
                temps.targetrigid = targetrigid;
                attackTimenow = patternTimeTable[0];
                break;
        }
    }
}
