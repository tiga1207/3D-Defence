using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPresenter : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] PlayerModel model;

    [Header("View")]
    [SerializeField] TMP_Text playerHPTextUI;
    [SerializeField] TMP_Text playerMaxTextUI;
    [SerializeField] Slider playerHPSliderUI;
    [SerializeField] TMP_Text playerSpeedTextUI;

    private void OnEnable()
    {
        model.OnHpChanged += SetHP;
        model.OnMaxHPChanged += SetMaxHP;
        SetMaxHP(model.MaxHP);
        SetHP(model.HP);
    }

    private void OnDisable()
    {
        model.OnHpChanged -= SetHP;
        model.OnMaxHPChanged -= SetMaxHP;
    }

    private void Update()
    {
        SetSpeed(model.Velocity);
    }

    public void SetHP(int hp)
    {
        playerHPTextUI.text = $"{hp}";
        playerHPSliderUI.value = hp;
    }

    public void SetMaxHP(int maxHP)
    {
        playerMaxTextUI.text = $"{maxHP}";
        playerHPSliderUI.maxValue = maxHP;
    }

    public void SetSpeed(Vector3 velocity)
    {
        playerSpeedTextUI.text = $"{velocity.magnitude}";
    }
}
