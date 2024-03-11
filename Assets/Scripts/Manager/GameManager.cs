using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header ("# Player Inpo")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 70, 130, 190, 290, 420, 600, 800};
    [Header ("# Game Object")]
    public PoolManager pool;
    public Player player;

    protected override void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
