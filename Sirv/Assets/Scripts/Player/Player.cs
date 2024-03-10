using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public int health;
    
    public float attackspeed; // 1 / attackspeed;
    public float attackspeed_now;
    
    public int damage;
    
    public int ammo;
    public int maxammo;
    
    public float speed;
    
    public float reloadtime;
    public float reloadtimenow;

    protected bool reloading = false;
    public float skillAcooltime;
    public float skillAcooltimenow;
    public float skillBcooltime;
    public float skillBcooltimenow;



    public Transform shotpoint;
    SpriteRenderer spriteRenderer;
    Vector2 inputVec;
    Rigidbody2D rigid;

    public abstract void Attack();
    public abstract void Skill_A();
    public abstract void Skill_B();
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ammo = maxammo;

    }

    // Update is called once per frame
    void Update()
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

        if(shotpoint.rotation.eulerAngles.z < 180)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    private void FixedUpdate()
    {
        Move();
        if(attackspeed_now > 0)
        {
            attackspeed_now -= Time.fixedDeltaTime;
        }
        if(skillAcooltimenow > 0)
        {
            skillAcooltimenow -= Time.fixedDeltaTime;
        }
        if(skillBcooltimenow > 0)
        {
            skillBcooltimenow -= Time.fixedDeltaTime;
        }
        if(reloading == true)
        {
            reloadtimenow -= Time.fixedDeltaTime;
            if(reloadtimenow <= 0)
            {
                ammo = maxammo;
                reloading = false;
            }
        }
    }
    private void Move()
    {
        Vector2 norVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + norVec);
    }
    protected IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadtime);
        ammo = maxammo;
        reloading = false;
    }
}
