using UnityEngine;
using UnityEngine.UI;

public class RestartBtn : MonoBehaviour
{
    private Button btn;
    void Start()
    {
        btn = GetComponent<Button>();

        btn.onClick.AddListener(() => GameTimeManager.Instance.GameOnlyStart());

    }
}