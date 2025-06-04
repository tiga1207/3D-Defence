using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoText : MonoBehaviour
{
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private Button atkUpbtn;
    private PlayerData player;

    void Awake()
    {
        player = PlayerData.Instance;
        atkUpbtn?.onClick.AddListener(AtkLevelUp);
        VeiwTextUi();

    }
    private void VeiwTextUi()
    {
        goldText.text = player.gold.ToString("F0");
        attackText.text = player.atk.ToString("F0");
    }

    public void AtkLevelUp()
    {
        if (player.gold <= 0)
        {
            Debug.Log("레벨업 불가");
        }
        else
        {
            player.atk += 1;
            player.gold -= 1;
            VeiwTextUi();
        }

    }
}
