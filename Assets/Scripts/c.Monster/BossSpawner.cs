using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnDataBoss[] spawnData;

    private int level;
    private float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 30f), spawnData.Length - 1);
        //Debug.Log($"Level: {level}");
        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject boss = PoolManager.Instance.Get(4);
        boss.transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)].position;
        boss.GetComponent<Monster>().Init(spawnData[level]);
    }

    [System.Serializable]
    public class SpawnDataBoss
    {
        public float spawnTime;
        public int spriteType;
        public int hp;
        public float speed;
    }
}
