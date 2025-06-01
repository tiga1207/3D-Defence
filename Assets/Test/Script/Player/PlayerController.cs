using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerModel model;
    private PlayerPresenter presenter;

    private void Awake()
    {
        model = GetComponent<PlayerModel>();
        presenter = new PlayerPresenter(model, GetComponent<IPlayerView>());
        Debug.Log("test");
    }

    private void FixedUpdate()
    {
        presenter.Move();
    }

    private void LateUpdate()
    {
        presenter.Rotate();
    }

    public void OnMove(InputValue value) => presenter.OnMove(value);
    public void OnRotate(InputValue value) => presenter.OnRotate(value);
}
