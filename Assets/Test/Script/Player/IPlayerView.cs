using UnityEngine;

public interface IPlayerView
{
    void ApplyRotation(Transform player, Transform aim, Vector2 rotation, float minPitch, float maxPitch);
    void PlayAttackAnimation();
    void PlayHitAnimation();
    void PlayDeathAnimation();
    void PlayMoveAnimation(float speed);
}
