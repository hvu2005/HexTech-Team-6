using UnityEngine;
using UnityEngine.UI;

public class musicManager : MonoBehaviour
{
    public Slider volumeSlider;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = FindObjectOfType<MusicManager>().GetComponent<AudioSource>();

        if (audioSource != null && volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f); 
            audioSource.volume = volumeSlider.value;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume); 
        }
    }
}
