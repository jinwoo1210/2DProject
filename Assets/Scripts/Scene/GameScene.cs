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
    public int hp;
    public int maxHp = 100;
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
        uiLvevlUp.Select(0);  // �ӽ� ��ũ��Ʈ (ù��° ĳ���� ����)
        GameManager.instance.isLive = true;
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