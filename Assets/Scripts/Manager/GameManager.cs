public class GameManager : Singleton<GameManager>
{
    public static GameManager instance;
    public PoolManager pool;
    public Player player;

    protected override void Awake()
    {
        instance = this;
    }
}
