using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioMixer audioMixer;  // Kéo Audio Mixer vào đây
    public Slider volumeSlider;    // Kéo Slider vào đây

    void Start()
    {
        float volume;
        audioMixer.GetFloat("MasterVolume", out volume);
        volumeSlider.value = Mathf.Pow(10, volume / 20); // Chuyển đổi từ dB sang Slider value
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20); // Chuyển đổi từ Slider value sang dB
    }
}