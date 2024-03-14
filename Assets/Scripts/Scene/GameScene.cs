using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
    public static GameScene instance;

    [Header("# Player Inpo")]
    public float hp;
    public float maxHp = 100;
    public int level;
    public int kill;
    public int exp;

    [Header("# Game Control")]
    public int[] nextExp = { 3, 5, 10, 70, 130, 190, 290, 420, 600, 800 };
    public LevelUp uiLvevlUp;

    protected void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        hp = maxHp;
        uiLvevlUp.Select(0);  // 임시 스크립트 (첫번째 캐릭터 선택)
        GameManager.instance.isLive = true;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        GameManager.instance.isLive = false;

        yield return new WaitForSeconds(0.5f);

        GameManager.instance.uiResult.SetActive(true);
        GameManager.instance.Stop();
    }


    public void GameRetry()
    {
        Manager.Scene.LoadScene("GameScene");
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLvevlUp.show();
        }
    }

}