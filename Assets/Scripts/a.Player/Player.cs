using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    public float speed;
    public Scanner scanner;

    public Vector2 moveDir;
    SpriteRenderer sprite;
    Animator animator;


    private void Awake()
    {
        // ������ ���ϰ� ������ �� �������� ���X ����Ż�� �����.. (���� ž�ٿ��̶� �׷���?)
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    private void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 vector2 = moveDir * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + vector2);
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", moveDir.magnitude);

        if (moveDir.x != 0)
        {
            sprite.flipX = moveDir.x < 0;
        }
    }
}
