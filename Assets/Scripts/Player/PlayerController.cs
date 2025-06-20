using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerPresenter presenter;

    private void Start()
    {
        var model = GetComponent<PlayerModel>();
        var view = GetComponent<IPlayerView>();
        presenter = new PlayerPresenter(model, view, this);
        presenter.Init();
        
    }
    private void Update()
    {
        presenter.Update();
    }
    private void FixedUpdate()
    {
        presenter.Move();
    }

    private void LateUpdate()
    {
        presenter.Rotate();
    }

    public void TakeDamage(int dmg)
    {
        presenter.TakeDamage(dmg);
    }

    public void OnMove(InputValue value) { presenter.OnMove(value); }
    public void OnRotate(InputValue value) => presenter.OnRotate(value);
    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            presenter.TryAttack();
            // presenter.Test();
            // Debug.Log("공격 키 눌림");
        }
    }
    // void OnEnable()
    // {
    //     Debug.Log(presenter.model.HP.Value);
    //     presenter?.OnEnable();
    // }

    // void OnDisable()
    // {
    //     presenter?.OnDisable();
    // }
}
