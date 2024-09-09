using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider bgmSlider; // BGM�p�X���C�_�[
    public Slider seSlider;  // SE�p�X���C�_�[
    public AudioSource[] bgmSource; // BGM�pAudioSource
    public AudioSource[] seSources; // SE�pAudioSource�i������SE���Ǘ��j

    void Start()
    {

        // �X���C�_�[�̏����l��PlayerPrefs����ǂݍ��ށA�ۑ�����Ă��Ȃ��ꍇ��1.0f���f�t�H���g�ɂ���
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
        // �X���C�_�[�̒l���ς�����Ƃ��ɉ��ʂ�ύX���郊�X�i�[��ǉ�
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        seSlider.onValueChanged.AddListener(SetSEVolume);
    }

    void SetBGMVolume(float value)
    {
        // BGM�̉��ʂ��X���C�_�[�̒l�ɍ��킹�ĕύX
        foreach (AudioSource bgmSource in bgmSource)
        {
            bgmSource.volume = value;
        }
        // ���ʂ�PlayerPrefs�ɕۑ�
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    void SetSEVolume(float value)
    {
        // �eSE�̉��ʂ��X���C�_�[�̒l�ɍ��킹�ĕύX
        foreach (AudioSource seSource in seSources)
        {
            seSource.volume = value;
        }

        // ���ʂ�PlayerPrefs�ɕۑ�
        PlayerPrefs.SetFloat("SEVolume", value);
    }
}
