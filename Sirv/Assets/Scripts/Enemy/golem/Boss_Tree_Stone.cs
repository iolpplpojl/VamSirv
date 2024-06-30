using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Tree_Stone : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D targetrigid;
    public Rigidbody2D rigid;
    Vector2 dirvec;
    Vector2 targetvec;
    public float speed; // %
    public float speedor;
    public int dmg;
    public BoxCollider2D col;
    bool moving = false;
    public AudioClip toss;

    public string debug;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        dirvec = targetrigid.position - rigid.position;
        StartCoroutine(setSpeed());
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (moving)
        {
            Vector2 nextvec = dirvec.normalized * (speedor * speed) * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextvec);
            rigid.velocity = Vector2.zero;
            debug = rigid.position + "\n" + targetvec;
            if (Vector2.Distance(rigid.position, targetvec) < 0.1f)
            {
                moving = false;
                col.enabled = true;
                Destroy(this);
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().GetDamage(dmg);
            col.enabled = true;
            Destroy(this);
        }
        if (collision.CompareTag("wall"))
        {
            col.enabled = true;
            Destroy(this);
        }
    }
    IEnumerator setSpeed()
    {
        yield return new WaitForSeconds(0.66f);
        targetvec = targetrigid.position;
        dirvec = targetrigid.position - rigid.position;

        yield return new WaitForSeconds(0.44f);
        SFXsystem.instance.PlaySoundFX(toss, transform, 1f);

        moving = true;
        while (speed > 0.3f)
        {
            speed -= 0.005f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
