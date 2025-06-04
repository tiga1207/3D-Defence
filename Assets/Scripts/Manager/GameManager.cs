using DesignPattern;
using TMPro;
using UnityEngine;

// public class GameManager : Singleton<GameManager>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("UI")]
    [SerializeField] private TMP_Text monsterCountText;
    [SerializeField] private GameObject gameClearUI;
    [SerializeField] private GameObject gameOverUI;

    [Header("스폰관련")]
    [SerializeField] private MonsterSpawnManager spawnManager;
    private int totalMonster;
    private int deadMonster;
    [SerializeField] private Transform nexusTransform;
    public Transform NexusTransform => nexusTransform;
    [SerializeField] private PlayerModel player;

    private bool isRegistered = false;
    public bool IsRegister => isRegistered;


    void Awake()
    {
        // SingletonInit();
        // CursorLock();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        totalMonster = spawnManager.TotalSpawnCount;
        deadMonster = 0;
        UpdateMonsterUI();
    }
    public void RegisterEvent()
    {
        if (isRegistered) return;
        Debug.Log("이벤트 구독");
        MonsterModel.OnMonsterDied += OnMonsterDied;
        Nexus.OnMonsterEnterNexus += HandleGameOver;
        player.OnPlayerDied += HandleGameOver;
        isRegistered = true;
    }
    public void UnRegisterEvent()
    {
        if (!isRegistered) return;
        Debug.Log("이벤트 구독 해제");
        MonsterModel.OnMonsterDied -= OnMonsterDied;
        Nexus.OnMonsterEnterNexus -= HandleGameOver;
        player.OnPlayerDied -= HandleGameOver;
        isRegistered = false;
    }

    private void OnMonsterDied()
    {
        deadMonster++;
        UpdateMonsterUI();

        if (deadMonster >= totalMonster)
        {
            gameClearUI.SetActive(true);
            //커서 풀기 & 시간 정지
            CursorUnLock();
            GameTimeManager.Instance.GameOnlyStop();
        }
    }

    private void UpdateMonsterUI()
    {
        int nowMonster = totalMonster - deadMonster;
        monsterCountText.text = $"Monster Counter : {nowMonster}";
    }

    private void HandleGameOver()
    {
        gameOverUI.SetActive(true);
        //커서 풀기 & 시간 정지
        CursorUnLock();
        GameTimeManager.Instance.GameOnlyStop();
        Debug.Log("게임오버로직 실행");
    }
    

    public void SetNexusTransform(Transform nexus)
    {
        nexusTransform = nexus;
    }


    //커서 관련
    public void CursorUnLock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}