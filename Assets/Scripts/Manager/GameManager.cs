using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Inpo")]
    public int hp;
    public int maxHp = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 70, 130, 190, 290, 420, 600, 800 };
    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLvevlUp;

    protected override void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!isLive)
            return;
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    private void Start()
    {
        hp = maxHp;

        // 임시 스크립트 (첫번째 캐릭터 선택)
        uiLvevlUp.Select(0);
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

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
