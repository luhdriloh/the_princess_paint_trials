using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public static SoundEffect _sfxPlayer;
    public AudioClip _sfx;
    private AudioSource _audiosource;

    private void Awake()
    {
        if (_sfxPlayer == null)
        {
            _sfxPlayer = this;
            _audiosource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        _audiosource.PlayOneShot(clip);
    }
}
