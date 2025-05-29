using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class HpController : MonoBehaviour
{
    Stat<int> Hp = new();
    Stat<int> MaxHp = new();
    public bool IsDead => Hp.Value <=0;

    void Start()
    {
        MaxHp.Value =100;
        Hp.Value = MaxHp.Value;

        Hp.Onchanged +=OnHpChanged;
    }
    public void TakeDamage(int amount)
    {
        if(IsDead) return;
    
        Hp.Value -=amount;
    
        if(Hp.Value <=0)
            Hp.Value =0;
    }


    void Update()
    {
        
    }
    private void OnHpChanged()
    {
        // if()
    }
}
