using System;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    public static event Action OnMonsterEnterNexus;

    void Awake()
    {
        GameManager.Instance.SetNexusTransform(transform);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            OnMonsterEnterNexus?.Invoke();
        }
    }
}