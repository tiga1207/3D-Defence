using System;
using System.Collections;
using System.Collections.Generic;
using Test;
using UnityEngine;

public class TowerZoneEvent : MonoBehaviour
{
    public static event Action<TriggerTower> OnTowerInteract;
    public static event Action OnTowerExit;

    public static void InvokeInteract(TriggerTower towerzone) => OnTowerInteract?.Invoke(towerzone);
    public static void InvokeExit() => OnTowerExit?.Invoke();
}
