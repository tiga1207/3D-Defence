using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    [Header("스폰 설정")]
    [SerializeField] private GameObject[] monsterPrefabs;

    //스폰 위치
    [SerializeField] private Transform[] spawnPoints; 
    //몬스터 스폰 수
    [SerializeField] private int spawnCount = 10;
    [SerializeField] private Transform nexusTransform;

    public int TotalSpawnCount => spawnCount;

    void Start()
    {
        SpawnMonsters();
    }

    private void SpawnMonsters()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject monsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];

            GameObject go = Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
