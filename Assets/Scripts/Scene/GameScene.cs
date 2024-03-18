using System.Collections;
using UnityEngine;

public class GameScene : BaseScene
{
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }

    public static GameScene instance;

    [Header("# Player Inpo")]
    public int playerId;
    public float hp;
    public float maxHp = 100;
    public int level;
    public int kill;
    public int exp;

    [Header("# Game Control")]
    public int[] nextExp = { 3, 5, 10, 70, 130, 190, 290, 420, 600, 800 };
    public LevelUp uiLvevlUp;
    public GameObject monsterCleaner;

    protected void Awake()
    {
        instance = this;
    }

    public void GameStart(int id)
    {
        playerId = id;
        hp = maxHp;
        GameManager.instance.player.gameObject.SetActive(true);
        uiLvevlUp.Select(playerId % 3);
        GameManager.instance.Resume();

        SoundManager.Instance.PlayBgm(true);
        SoundManager.Instance.PlaySfx(SoundManager.Sfx.Button);
    }

    public void GameRetry()
    {
        Manager.Scene.LoadScene("GameScene");
        SoundManager.Instance.PlaySfx(SoundManager.Sfx.Button);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        GameManager.instance.isLive = false;

        yield return new WaitForSeconds(0.5f);

        GameManager.instance.uiResult.gameObject.SetActive(true);
        GameManager.instance.uiResult.Lose();
        GameManager.instance.Stop();

        SoundManager.Instance.PlayBgm(false);
        SoundManager.Instance.PlaySfx(SoundManager.Sfx.Lose);
        SoundManager.Instance.PlaySfx(SoundManager.Sfx.Haha);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        GameManager.instance.isLive = false;
        monsterCleaner.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        GameManager.instance.uiResult.gameObject.SetActive(true);
        GameManager.instance.uiResult.Win();
        GameManager.instance.Stop();

        SoundManager.Instance.PlayBgm(false);
        SoundManager.Instance.PlaySfx(SoundManager.Sfx.Win);
        SoundManager.Instance.PlaySfx(SoundManager.Sfx.Cheers);
    }

    public void GetExp()
    {
        if (!GameManager.instance.isLive)       // 게임 승리시 몬스터가 죽어서 레벨업이 되는 상황을 방지하는 코드
            return;

        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLvevlUp.show();
        }
    }

}