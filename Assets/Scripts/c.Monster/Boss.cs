using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Spawner;

public class Boss : MonoBehaviour
{
    public float speed;
    public float hp;
    public float maxHp;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    private Rigidbody2D rigid;
    private Collider2D coll;
    Animator anim;
    private SpriteRenderer spriter;
    private WaitForFixedUpdate wait;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive)
            return;

        spriter.flipX = target.position.x > rigid.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        hp = maxHp;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHp = data.hp;
        hp = data.hp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        hp -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(knockBack());

        if (hp > 0)
        {
            anim.SetTrigger("Hit");
            SoundManager.Instance.PlaySfx(SoundManager.Sfx.Hit);
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameScene.instance.kill++;
            GameScene.instance.GetExp();
            if (GameManager.instance.isLive)
                SoundManager.Instance.PlaySfx(SoundManager.Sfx.MonsterDead);
        }
    }

    IEnumerator knockBack()
    {
        //Debug.Log("aaa");
        yield return wait; // 하나의 물리 프레임을 딜레이
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 1, ForceMode2D.Impulse);
    }

    private void Dead()
    {
        gameObject.SetActive(false);

    }
}
