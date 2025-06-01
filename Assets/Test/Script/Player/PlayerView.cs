using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView
{
    public void ApplyRotation(Transform player, Transform aim, Vector2 rotation, float minPitch, float maxPitch)
    {
        player.rotation = Quaternion.Euler(0f, rotation.x, 0f);
        if (aim != null)
        {
            aim.localRotation = Quaternion.Euler(Mathf.Clamp(rotation.y, minPitch, maxPitch), 0f, 0f);
        }
    }
}
