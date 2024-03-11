using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    // 프리팹을 보관할 변수
    public GameObject[] prefab;
    // 풀 담당을 하는 리스트
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

    public GameObject Get(int i)    // Get 함수 생성
    {
        GameObject select = null;

        // 선택한 풀의 게임오브젝트  접근 

        foreach (GameObject item in pools[i])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }

        }

        if (!select) // 못찾았을 때
        {
            select = Instantiate(prefab[i], transform);     // 새롭게 생성하고 select 변수에 할당
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
