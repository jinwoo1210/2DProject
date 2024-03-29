using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    public float speed;
    public Scanner scanner;
    public RuntimeAnimatorController[] animCon;


    public Vector2 moveDir;
    private SpriteRenderer sprite;
    private Animator animator;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    private void OnEnable()
    {
        speed *= Character.Speed;
        animator.runtimeAnimatorController = animCon[GameScene.instance.playerId];
    }

    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;
    }


    private void Move()
    {
        Vector2 vector2 = moveDir * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + vector2);
    }

    private void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        Move();
    }
    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        animator.SetFloat("Speed", moveDir.magnitude);

        if (moveDir.x != 0)
        {
            sprite.flipX = moveDir.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        GameScene.instance.hp -= Time.deltaTime * 10;

        if (GameScene.instance.hp < 0)
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            animator.SetTrigger("Dead");
            GameScene.instance.GameOver();
        }
    }
}
