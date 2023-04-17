using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject btnok;

    [SerializeField] private AudioSource _audioSourceMusic;
    [SerializeField] private AudioSource _audioSourceFx;

    public AudioClip title;
    public AudioClip start;
    public AudioClip grass;
    public AudioClip water;

    // Start is called before the first frame update
    void Start()
    {
        InitialsValues();
    }

    public void SetMusicvol(float value)
    {
        _slider.value = value;
        _audioSourceMusic.volume = _slider.value;
    }

    public void BtnOk()
    {
        PlayerPrefs.SetFloat("volumeMusica", _slider.value);
    }

    private void InitialsValues()
    {
        if (PlayerPrefs.GetInt("round") == 0)
        {
            PlayerPrefs.SetFloat("volumeMusica", 0.5f);
            PlayerPrefs.SetInt("round", 1);
        }

        float volume = PlayerPrefs.GetFloat("volumeMusica");

        _audioSourceMusic.volume = volume;
        _slider.value = volume;

        _audioSourceMusic.clip = title;
        _audioSourceMusic.Play();
        _audioSourceMusic.loop = true;

        btnok.SetActive(true);
    }

    public IEnumerator ChangeMusic(AudioClip newClip)
    {
        float currentVol = _audioSourceMusic.volume;

        for (float v = currentVol; v > 0; v -= 0.05f)
        {
            _audioSourceMusic.volume = v;
            yield return new WaitForEndOfFrame();
        }

        _audioSourceMusic.clip = newClip;
        _audioSourceMusic.Play();

        for (float v = 0; v < currentVol; v += 0.05f)
        {
            _audioSourceMusic.volume = v;
            yield return new WaitForEndOfFrame();
        }


    }

}
