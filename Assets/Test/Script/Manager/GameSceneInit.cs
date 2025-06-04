using UnityEngine;

public class GameSceneInit : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterEvent();
            Debug.Log("GameInit 실행");
        }
    }
}