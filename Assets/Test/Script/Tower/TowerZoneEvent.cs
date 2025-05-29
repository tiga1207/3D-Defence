using System;
using System.Collections;
using System.Collections.Generic;
using Test;
using UnityEngine;

public class TowerZoneEvent : MonoBehaviour
{
    public static Action<TriggerTower> OnTowerInteract;
    public static Action OnTowerExit;
}
