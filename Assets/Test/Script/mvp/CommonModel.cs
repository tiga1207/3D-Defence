using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class CommonModel : MonoBehaviour
{
    Stat<int> Hp = new(100);
    Stat<int> MaxHp = new(100);
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
