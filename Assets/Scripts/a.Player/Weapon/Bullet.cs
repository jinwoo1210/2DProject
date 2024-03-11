using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if(per >= 0)
        {
            rigid.velocity = dir * 10f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster") || per == -100)
            return;

        per--;

        if (per < 0)
        {
            rigid.velocity = Vector2.zero;      
            gameObject.SetActive(false);        // distroy를 쓰지 않은 이유는 오브젝트 풀링으로 관리되어져 있기 때문.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)  //플레이어 화면 밖으로 투사체가 나가면 삭제.
    {
        if (!collision.CompareTag("Area") || per == -100)
            return;

        //Debug.Log("화면밖으로 단검이 나가 사라짐");
        gameObject.SetActive(false);
    }
}
