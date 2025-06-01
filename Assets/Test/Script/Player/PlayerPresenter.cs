using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPresenter
{
    private PlayerModel model;
    private IPlayerView view;

    public PlayerPresenter(PlayerModel _model, IPlayerView _view)
    {
        model = _model;
        view = _view;
    }

    public void OnMove(InputValue value)
    {
        model.inputDir = value.Get<Vector2>();
    }

    public void OnRotate(InputValue value)
    {
        model.mouseDelta = value.Get<Vector2>() * model.mouseSensitivity;
        model.mouseDelta.y *= -1f;
    }

    public void Move()
    {
        Vector3 move = model.transform.right * model.inputDir.x + model.transform.forward * model.inputDir.y;
        Vector3 velocity = move * model.moveSpeed;
        velocity.y = model.Rb.velocity.y;
        model.Rb.velocity = velocity;
    }

    public void Rotate()
    {
        if (GameTimeManager.Instance.IsPaused) return;

        model.currentRotation.x += model.mouseDelta.x;
        model.currentRotation.y += model.mouseDelta.y;

        view.ApplyRotation(model.transform, model.Aim, model.currentRotation, model.minPitch, model.maxPitch);
    }
}
