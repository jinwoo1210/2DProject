using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharactor;
    public GameObject[] unlockCharactor;

    private enum Achive { UnlockAssassin, UnlockMagition }
    Achive[] achives;

    private void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));

        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    private void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach(Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    private void Start()
    {
        UnlockCharactor();
    }

    private void UnlockCharactor()
    {
        for (int i = 0; i < lockCharactor.Length; i++)
        {
            string achiveName = achives[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharactor[i].SetActive(!isUnlock);
            unlockCharactor[i].SetActive(isUnlock);
        }
    }

    private void LateUpdate()
    {
        foreach(Achive achive in achives)
        {
            CheckAchive(achive);
        }
    }

    private void CheckAchive(Achive achive)
    {
        bool isAchive = false;

        switch (achive)
        {
            case Achive.UnlockAssassin:
                isAchive = GameScene.instance.kill >= 10;
                break;
            case Achive.UnlockMagition:
                isAchive = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }

        if (isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0) // 해금이 안된 상태일때
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);
        }
    }
}
