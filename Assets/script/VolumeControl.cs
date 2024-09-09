using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider bgmSlider; // BGM用スライダー
    public Slider seSlider;  // SE用スライダー
    public AudioSource[] bgmSource; // BGM用AudioSource
    public AudioSource[] seSources; // SE用AudioSource（複数のSEを管理）

    void Start()
    {

        // スライダーの初期値をPlayerPrefsから読み込む、保存されていない場合は1.0fをデフォルトにする
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        seSlider.value = PlayerPrefs.GetFloat("SEVolume", 0.5f);

        foreach (AudioSource bgmSource in bgmSource)
        {
            bgmSource.volume = bgmSlider.value;
        }
        foreach (AudioSource seSource in seSources)
        {
            seSource.volume = seSlider.value;
        }
        // スライダーの値が変わったときに音量を変更するリスナーを追加
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        seSlider.onValueChanged.AddListener(SetSEVolume);
    }

    void SetBGMVolume(float value)
    {
        // BGMの音量をスライダーの値に合わせて変更
        foreach (AudioSource bgmSource in bgmSource)
        {
            bgmSource.volume = value;
        }
        // 音量をPlayerPrefsに保存
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    void SetSEVolume(float value)
    {
        // 各SEの音量をスライダーの値に合わせて変更
        foreach (AudioSource seSource in seSources)
        {
            seSource.volume = value;
        }

        // 音量をPlayerPrefsに保存
        PlayerPrefs.SetFloat("SEVolume", value);
    }
}
