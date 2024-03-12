using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    private int level;
    private float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f));
        Debug.Log($"Level: {level}");
        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
            //if (level >= 2)
            //{
            //    level = 1;
            //}
        }
    }

    private void Spawn()
    {
        GameObject monster = GameManager.instance.pool.Get(0);
        monster.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        monster.GetComponent<Monster>().Init(spawnData[level]);
    }

    [System.Serializable]
    public class SpawnData
    {
        public float spawnTime;
        public int spriteType;
        public int hp;
        public float speed;
    }
}
