using DesignPattern;
public class PlayerData : Singleton<PlayerData>
{
    public int hp = 10;
    public int gold = 20;
    public int atk = 10;


    //TODO: 추후 UI 설정 관리 스크립트가 있을 경우 해당 위치로 옮기기.
    public float mouseSensitivity = 1f;


    void Awake()
    {
        SingletonInit();
    }
    public void SetMouseSensitivity(float _volume)
    {
        mouseSensitivity = _volume;
    }

}