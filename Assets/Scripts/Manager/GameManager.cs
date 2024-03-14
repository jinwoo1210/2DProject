using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static GameManager instance;

    [Header("# Game Object")]
    public Player player;

    [Header ("Game Times")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;

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
