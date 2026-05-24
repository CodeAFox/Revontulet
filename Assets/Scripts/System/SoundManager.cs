using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
     public AudioMixer audioMixer;

    public void OnChangeMasterSlider(float Value)
    {
        Value = Value == 0 ? 0.0000001f : Value;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Value) * 20);
    }

    public void OnChangeFootstepsSlider(float Value)
    {
        Value = Value == 0 ? 0.0000001f : Value;
        audioMixer.SetFloat("FootstepsVolume", Mathf.Log10(Value) * 20);
    }

    public void OnChangeBackgroundMusicSlider(float Value)
    {
        Value = Value == 0 ? 0.0000001f : Value;
        audioMixer.SetFloat("BackgroundMusicVolume", Mathf.Log10(Value) * 20);
    }
}
