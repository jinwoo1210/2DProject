using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int i)
    {
        items[i].OnClick();
    }

    private void Next()
    {
        // 1.��� �������� ��Ȱ��ȭ
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // 2. ���� ������ 3�� ������ Ȱ��ȭ 
        int[] random = new int[3];
        while (true)
        {
            random[0] = Random.Range(0, items.Length);
            random[1] = Random.Range(0, items.Length);
            random[2] = Random.Range(0, items.Length);


            if (random[0] != random[1] && random[1] != random[2] && random[0] != random[2])
                break;
        }

        for (int i = 0; i < random.Length; i++)
        {
            Item randItem = items[random[i]];

            // 3. ���� ������ ��� �Һ���������� ��ü
            if (randItem.level == randItem.data.damages.Length)
            {
                items[4].gameObject.SetActive(true);
            }
            else
            {
                randItem.gameObject.SetActive(true);
            }
        }
    }
}
