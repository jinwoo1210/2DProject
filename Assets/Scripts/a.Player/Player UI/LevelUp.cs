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
        SoundManager.Instance.PlaySfx(SoundManager.Sfx.LevelUp);
        SoundManager.Instance.EffectBgm(true);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        SoundManager.Instance.PlaySfx(SoundManager.Sfx.Button);
        SoundManager.Instance.EffectBgm(false);
    }

    public void Select(int i)
    {
        items[i].OnClick();
    }

    private void Next()
    {
        // 1.모든 아이템의 비활성화
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // 2. 그중 랜덤한 3개 아이템 활성화 
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

            // 3. 만렙 아이템 경우 소비아이템으로 대체
            if (randItem.level == randItem.data.damages.Length)
            {
                items[5].gameObject.SetActive(true);
            }
            else
            {
                randItem.gameObject.SetActive(true);
            }
        }
    }
}
