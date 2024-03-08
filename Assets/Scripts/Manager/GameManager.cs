using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static GameManager instance;

    public float gameTime;
    public float maxGameTime = 2 * 30f;

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

}
