using System.Collections;
using System.Collections.Generic;
using Test;
using TMPro;
using UnityEngine;


namespace Test
{
    public class triggerTower : MonoBehaviour
    {

        //텍스트 패널(Presss to Interact)
        [SerializeField] private GameObject text;
        // [SerializeField] private GameObject scrollView;
        private static scrollView scrollView;

        //타워 프리펩
        [SerializeField] private GameObject towerPrefab;

        // 생성된 타워
        private GameObject builtTower;



        //플레이어가 콜라이더 내부에 있을 때
        [SerializeField] private bool isPlayerInside;
        [SerializeField] private bool CanBuildTower = true;

        //머터리얼

        [SerializeField] private Renderer triggerRenderer;
        [SerializeField] private Material visibleMaterial;
        [SerializeField] private Material invisibleMaterial;

        // PlayerTest player;


        void Start()
        {
            text.gameObject.SetActive(false);
            if (scrollView == null)
                scrollView = FindObjectOfType<scrollView>();
            // player = GetComponent<PlayerTest>();
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.F) && isPlayerInside)
            {
                Debug.Log("F키 눌림");
                text.gameObject.SetActive(false);
                // scrollView.gameObject.SetActive(false);
                scrollView.OpenScrollView(this); // 자신을 전달
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("트리거 진입.");
                PlayerTest player = collision.GetComponent<PlayerTest>();
                text.gameObject.SetActive(true);
                player.IsCanInteract = true;
                isPlayerInside = true;

            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("트리거 탈출.");
                PlayerTest player = collision.GetComponent<PlayerTest>();
                text.gameObject.SetActive(false);
                player.IsCanInteract = false;
                isPlayerInside = false;
                scrollView.CloseScrollView();
            }
        }


        public void BuildTower()
        {
            if (!CanBuildTower) return;

            builtTower = Instantiate(towerPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            CanBuildTower = false;
            //투명색 머터리얼로 변경.
            triggerRenderer.material = invisibleMaterial;
            scrollView.CloseScrollView();

        }

        public void SellTower()
        {
            if (builtTower != null)
            {
                Destroy(builtTower);
                builtTower = null;
                CanBuildTower = true;

                //미설치 머터리얼로 변경.
                triggerRenderer.material = visibleMaterial;
            }

        }
        public void UpgradeTower()
        {
            if (builtTower != null)
            {
                TowerTest tower = builtTower.GetComponent<TowerTest>();

                //Todo: 레벨업 이벤트 호출
                tower.level += 1; //임시
            }
        }

        public bool CanBuild() => CanBuildTower;

    }
}