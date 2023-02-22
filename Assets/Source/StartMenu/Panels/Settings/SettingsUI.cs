using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _sliderSound;
    [SerializeField] private AudioSource _backGroundTrack;
    [SerializeField] private TMP_Dropdown _dropdownQuality;
    [SerializeField] private GameObject _blackScreen;

    string _quality = "Quality";
    string _volume = "Volume";
    int _valueDecrease = 20;
    string _start = "Start";

    private void Awake()
    {
        if (PlayerPrefs.GetFloat(_volume) == 0)
        {
            float startVolume = 0.5f;
            PlayerPrefs.SetFloat(_volume, startVolume);
        }

        _sliderSound.value = PlayerPrefs.GetFloat(_volume);
        _audioMixer.SetFloat(_volume, Mathf.Log10(0.0001f) * _valueDecrease);
        SoundFading(_start);
        SetQuality(PlayerPrefs.GetInt(_quality));
        _dropdownQuality.value = PlayerPrefs.GetInt(_quality);
    }

    public void SoundFading(string choice = "End")
    {
        if (choice == _start)
        {
            StartCoroutine(CoroutineSoundIncrease(PlayerPrefs.GetFloat(_volume)));
        }
        else
        {
            StartCoroutine(CoroutineSoundDecrease(PlayerPrefs.GetFloat(_volume)));
        }
    }

    public void SetVolume(float value)
    {
        _audioMixer.SetFloat(_volume, Mathf.Log10(value) * _valueDecrease);
        PlayerPrefs.SetFloat(_volume, value);
    }

    public void SetQuality(int qualitiIndex)
    {
        QualitySettings.SetQualityLevel(qualitiIndex);
        PlayerPrefs.SetInt(_quality, qualitiIndex);
    }

    public void Sound()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void EndSounds(AudioSource track)
    {
        track.Play();
        StartCoroutine(SoundOff(_backGroundTrack));
    }

    public void ReloadSceneLanguage()
    {
        StartCoroutine(RestartScene());
    }

    private IEnumerator CoroutineSoundIncrease(float value)
    {
        float expiredTime = 0f;

        while (expiredTime < 1)
        {
            expiredTime += Time.deltaTime;
            _audioMixer.SetFloat(_volume, Mathf.Log10(value * expiredTime) * _valueDecrease);
            yield return null;
        }
    }

    private IEnumerator CoroutineSoundDecrease(float value)
    {
        float expiredTime = 1f;

        while (expiredTime > 0)
        {
            Debug.Log(value);
            expiredTime -= Time.deltaTime;
            _audioMixer.SetFloat(_volume, Mathf.Log10(value * expiredTime) * _valueDecrease);
            yield return null;
        }
    }

    private IEnumerator SoundOff(AudioSource track)
    {
        float expiredTime = 1f;

        while (expiredTime > 0)
        {
            expiredTime -= Time.deltaTime;
            track.volume = Mathf.MoveTowards(track.volume, 0, 1 * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator RestartScene()
    {
        string off = "Off";
        float waiting = 2f;
        _blackScreen.GetComponent<Animator>().SetTrigger(off);
        yield return new WaitForSeconds(waiting); ;
        SceneManager.LoadScene(1);
    }
}