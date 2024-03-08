using UnityEngine;

public class Reposition : MonoBehaviour
{
    private new Collider2D  collider;

    [SerializeField]
    private Transform pos;


    private void Awake()
    {
        Debug.Log($"{gameObject.name} : {transform.position}");
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = pos.position;

        switch (transform.tag)
        {
            case "Ground":

                float diffX = (playerPos.x - myPos.x);
                float diffY = (playerPos.y - myPos.y);
                Debug.Log($"{gameObject.name} : X = {diffX}, Y = {diffY}");
                Debug.Log($"{playerPos}");
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;
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
                    transform.Translate(Vector3.up * dirY * 32);
                }
                break;
            case "Monster":
                if (collider.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 rand = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f);
                    transform.Translate(rand + dist * 2);
                }

                break;
        }
    }
}
