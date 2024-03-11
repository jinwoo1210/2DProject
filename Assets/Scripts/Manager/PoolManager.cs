using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    // �������� ������ ����
    public GameObject[] prefab;
    // Ǯ ����� �ϴ� ����Ʈ
    List<GameObject>[] pools;

    protected override void Awake()
    {
        pools = new List<GameObject>[prefab.Length];

        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }

        //Debug.Log(pools.Length);
    }

    public GameObject Get(int i)    // Get �Լ� ����
    {
        GameObject select = null;

        // ������ Ǯ�� ���ӿ�����Ʈ  ���� 

        foreach (GameObject item in pools[i])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }

        }

        if (!select) // ��ã���� ��
        {
            select = Instantiate(prefab[i], transform);     // ���Ӱ� �����ϰ� select ������ �Ҵ�
            pools[i].Add(select);
        }
        
        return select;
    }

    private Dictionary<int, ObjectPool> poolDic = new Dictionary<int, ObjectPool>();

    public void CreatePool(PooledObject prefab, int size, int capacity)
    {
        GameObject gameObject = new GameObject();
        gameObject.name = $"Pool_{prefab.name}";

        ObjectPool objectPool = gameObject.AddComponent<ObjectPool>();
        objectPool.CreatePool(prefab, size, capacity);

        poolDic.Add(prefab.GetInstanceID(), objectPool);
    }

    public void DestroyPool(PooledObject prefab)
    {
        ObjectPool objectPool = poolDic[prefab.GetInstanceID()];
        Destroy(objectPool.gameObject);

        poolDic.Remove(prefab.GetInstanceID());
    }

    public void ClearPool()
    {
        foreach (ObjectPool objectPool in poolDic.Values)
        {
            Destroy(objectPool.gameObject);
        }

        poolDic.Clear();
    }

    public PooledObject GetPool(PooledObject prefab, Vector3 position, Quaternion rotation)
    {
        return poolDic[prefab.GetInstanceID()].GetPool(position, rotation);
    }
}
