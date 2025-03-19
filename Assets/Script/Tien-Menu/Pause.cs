using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Pause : MonoBehaviour
{
    public GameObject gridPanel;
    public GameObject settingPanel;
    public GameObject pauseImage; 

    public Color clickColor = Color.gray;
    public float delayBeforeSwitch = 0.1f;

    private Renderer objectRenderer;
    private Color originalColor;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }

        if (gridPanel != null) gridPanel.SetActive(true);
        if (settingPanel != null) settingPanel.SetActive(false);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnMouseDown()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = clickColor;
        }

        StartCoroutine(SwitchPanels());
    }

    IEnumerator SwitchPanels()
    {
        yield return new WaitForSeconds(delayBeforeSwitch);

        if (gridPanel != null && settingPanel != null)
        {
            bool isGridActive = gridPanel.activeSelf;
            gridPanel.SetActive(!isGridActive);
            settingPanel.SetActive(isGridActive);
        }

        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
        }
    }

    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (pauseImage != null)
        {
            pauseImage.SetActive(false);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
