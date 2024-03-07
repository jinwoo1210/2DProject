using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        switch (transform.tag)
        {
            case "Ground":

                float diffX = (playerPos.x - myPos.x);
                float diffY = (playerPos.y - myPos.y);
                float dirX = diffX< 0 ? -1 : 1;               
                float dirY = diffY< 0 ? -1 : 1;
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY)
                {
                    Debug.Log("aaa");
                    transform.Translate(Vector3.right * dirX * 32); //Ground의 2배 이동해야해서 16 * 2
                }
                else if (diffX < diffY)
                {
                    Debug.Log("bbb");
                    transform.Translate(Vector3.up* dirY * 32);
                }
                break;
            case "Enemy":
                break;
        }
    }
}
