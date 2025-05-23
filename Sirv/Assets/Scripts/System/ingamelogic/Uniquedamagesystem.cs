using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uniquedamagesystem : MonoBehaviour
{
    // Start is called before the first frame update
    public static Uniquedamagesystem instance;
    public Player player;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }   

    public void BloodSuck()
    {
        if (Random.Range(0f, 1f) <= player.BloodSuck)
        {
            player.Heal(2);
        }
    }

    public void Hammer(Enemy enemy)
    {
        if(player.HammerCount > 0)
        {
            if (Random.Range(0f, 1f) <= 0.2f)
            {
                enemy.GetRawDamage((int)(player.maxHealthNow * player.HammerCount * 0.1f * player.damagePer),false,35);
            }
        }
    }
    public void Explode(Vector3 transform)
    {
        if (Random.value <= (1 - (1 / (0.2f*player.ExplodeCount + 1))))
        {
            var hit = Physics2D.OverlapCircleAll(transform, 0.3f, LayerMask.GetMask("Enemy"));
            foreach (var hitcol in hit)
            {
                hitcol.GetComponent<Enemy>().GetRawDamage((int)(8), true, 21);
            }
        }
    }
    public IEnumerator Fire(Enemy enemy)
    {
        for (int i = 0; i < player.fireCount; i++) {
            if(enemy == null)
            {
                yield break;
            }
            if (Random.Range(0f, 1f) <= 0.1f)
            {
                int damage = (int)(12);
                for(int k = 0; k < player.fireDamage; k++)
                {
                    damage = damage * 2;
                }
                
                enemy.StartCoroutine(enemy.Fire((int)(damage*player.damagePer)));
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
    public void Kill()
    {
        player.EnemyKill();
    }
}
