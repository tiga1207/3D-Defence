using System.Collections;
using System.Collections.Generic;
using Test;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class scrollView : MonoBehaviour
    {
        [SerializeField] private GameObject scrollViewObj;
        [SerializeField] private Button buildBtn;
        [SerializeField] private Button sellBtn;
        [SerializeField] private Button upgradeBtn;

        public static scrollView instance;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }



        private Test.triggerTower currentTrigger;

        void Start()
        {
            scrollViewObj.SetActive(false);
            buildBtn.onClick.AddListener(OnClickBuild);
            sellBtn.onClick.AddListener(OnClickSell);
            upgradeBtn.onClick.AddListener(OnClickUpgrade);
        }

        public void OpenScrollView(Test.triggerTower trigger)
        {
            currentTrigger = trigger;
            scrollViewObj.SetActive(true);

            // 버튼 비활성화 여부
            buildBtn.interactable = trigger.CanBuild();
            sellBtn.interactable = !trigger.CanBuild();
            upgradeBtn.interactable = !trigger.CanBuild();
        }

        public void CloseScrollView()
        {
            scrollViewObj.SetActive(false);
            currentTrigger = null;
        }

        private void OnClickBuild()
        {
            if (currentTrigger != null)
            {
                currentTrigger.BuildTower();
                buildBtn.interactable = false;
                sellBtn.interactable = true;
                upgradeBtn.interactable = true;
            }
        }
        public void OnClickSell()
        {
            Debug.Log("판매 버튼 눌림");
            if (currentTrigger != null)
            {
                currentTrigger.SellTower();
                buildBtn.interactable = true;
                sellBtn.interactable = false;
                upgradeBtn.interactable = false;
            }
        }

        public void OnClickUpgrade()
        {
            if (currentTrigger != null)
            {
                currentTrigger.UpgradeTower();
            }

        }

    }
}