using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MonsterAudio : MonoBehaviour
{
    [SerializeField] private AudioClip m_shotSound;
    [SerializeField] private AudioClip m_hurtSound;
    [SerializeField] private AudioClip m_slashSound;
    [SerializeField] private AudioClip m_deadSound;

    private void PlaySFX(AudioClip _clip)
    {
        if (_clip != null)
        {
            SFXController sfx = AudioManager.Instance.GetSFX();
            if (sfx != null)
            {
                sfx.Play(_clip);
            }

        }
    }
    public void ShootingSound() => PlaySFX(m_shotSound);
    public void HurtSound() => PlaySFX(m_hurtSound);
    public void SlashSound() => PlaySFX(m_slashSound);
    public void DeadSound() => PlaySFX(m_deadSound);
}
