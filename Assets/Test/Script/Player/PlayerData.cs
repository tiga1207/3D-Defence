using DesignPattern;
public class PlayerData : Singleton<PlayerData>
{
    public int hp=10;
    public int gold=20;
    public int atk = 10;

    void Awake()
    {
        SingletonInit();
    }

}