using UnityEngine;

public interface IPlayerView
{
    void ApplyRotation(Transform player, Transform aim, Vector2 rotation, float minPitch, float maxPitch);
}
