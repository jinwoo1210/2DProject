using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using static Spawner;

public class Monster : MonoBehaviour
{
    public float speed;
    public float hp;
    public float maxHp;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    private Rigidbody2D rigid;
    Animator anim;
    private SpriteRenderer spriter;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive)
            return;

        spriter.flipX = target.position.x > rigid.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
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
        if (!collision.CompareTag("Bullet"))
            return;
        hp -= collision.GetComponent<Bullet>().damage;

        if (hp > 0)
        {

        }
        else
        {
            Dead();
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }

}
