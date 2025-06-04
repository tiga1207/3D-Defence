using DesignPattern;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
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

    void OnEnable()
    {
        MonsterModel.OnMonsterDied += OnMonsterDied;
        Nexus.OnMonsterEnterNexus += HandleGameOver;
        player.OnPlayerDied += HandleGameOver;
    }

    void OnDisable()
    {
        MonsterModel.OnMonsterDied -= OnMonsterDied;
        Nexus.OnMonsterEnterNexus -= HandleGameOver;
        player.OnPlayerDied -= HandleGameOver;
    }

    void Start()
    {
        totalMonster = spawnManager.TotalSpawnCount;
        deadMonster = 0;
        UpdateMonsterUI();
    }

    private void OnMonsterDied()
    {
        deadMonster++;
        UpdateMonsterUI();

        if (deadMonster >= totalMonster)
        {
            gameClearUI.SetActive(true);
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
    }
    public void SetNexusTransform(Transform nexus)
    {
        nexusTransform = nexus;
    }
}