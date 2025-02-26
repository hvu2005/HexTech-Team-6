using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private AudioSource audioSource;

    public string[] allowedScenes; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            SceneManager.sceneLoaded += OnSceneLoaded;

            
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

         if (System.Array.Exists(allowedScenes, s => s == scene.name))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
