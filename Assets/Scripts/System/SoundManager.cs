using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
     public AudioMixer audioMixer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
