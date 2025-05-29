using System.Collections;
using System.Collections.Generic;
using Test;
using UnityEngine;
using UnityEngine.UI;
using DesignPattern;
using System;

namespace Test
{
    public class TowerBuildUI : Singleton<TowerBuildUI>
    {
        [SerializeField] private GameObject PressFTextUI;
        [SerializeField] private GameObject scrollViewObj;
        [SerializeField] private Button buildBtn;
        [SerializeField] private Button sellBtn;
        [SerializeField] private Button upgradeBtn;

        public static event Action OnTextInteractOpen;
        public static event Action OnTextInteractClose;

        public static TowerBuildUI instance;

        public static void InvokeOpen() => OnTextInteractOpen?.Invoke();
        public static void InvokeClose() => OnTextInteractClose?.Invoke();


        void Awake() => base.SingletonInit();

        private Test.TriggerTower currentTrigger;

        void Start()
        {
            PressFTextUI.gameObject.SetActive(false);
            scrollViewObj.SetActive(false);
            buildBtn.onClick.AddListener(OnClickBuild);
            sellBtn.onClick.AddListener(OnClickSell);
            upgradeBtn.onClick.AddListener(OnClickUpgrade);
        }

        void OnEnable()
        {
            TowerZoneEvent.OnTowerInteract += OpenScrollView;
            TowerZoneEvent.OnTowerExit += CloseScrollView;
            OnTextInteractOpen += OpenPressTextUI;
            OnTextInteractClose += ClosePressTextUI;
        }

        void OnDisable()
        {
            TowerZoneEvent.OnTowerInteract -= OpenScrollView;
            TowerZoneEvent.OnTowerExit -= CloseScrollView;
            OnTextInteractOpen -= OpenPressTextUI;
            OnTextInteractClose -= ClosePressTextUI;
        }

        #region ScrollVeiw

        public void OpenScrollView(Test.TriggerTower trigger)
        {
            currentTrigger = trigger;
            scrollViewObj.SetActive(true);
            
            //커서 활성화
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // 버튼 비활성화 여부
            buildBtn.interactable = trigger.CanBuild();
            sellBtn.interactable = !trigger.CanBuild();
            upgradeBtn.interactable = !trigger.CanBuild();
        }

        public void CloseScrollView()
        {
            scrollViewObj.SetActive(false);
            currentTrigger = null;

            //커서 비활성화
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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

        #endregion

        #region Press F Text UI
        public void OpenPressTextUI()
        {
            PressFTextUI.gameObject.SetActive(true);
        }
        public void ClosePressTextUI()
        {
            PressFTextUI.gameObject.SetActive(false);
        }

        #endregion




    }
}