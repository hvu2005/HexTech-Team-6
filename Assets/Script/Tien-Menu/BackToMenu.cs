using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackToMenu : MonoBehaviour
{
    public string sceneName = "scenemenu";
    public Color clickColor = Color.gray;
    public float delayBeforeLoad = 0.001f;
    public float scaleFactor = 1.07f;
    public float scaleSpeed = 5f;

    private Renderer objectRenderer;
    private Color originalColor;
    private Vector3 originalScale;
    private bool isHovering = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
        originalScale = transform.localScale;
    }

    void OnMouseEnter()
    {
        isHovering = true;
        StopAllCoroutines();
        StartCoroutine(ScaleObject(originalScale * scaleFactor));
    }

    void OnMouseExit()
    {
        isHovering = false;
        StopAllCoroutines();
        StartCoroutine(ScaleObject(originalScale));
    }

    void OnMouseDown()
    {
        objectRenderer.material.color = clickColor;
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator ScaleObject(Vector3 targetScale)
    {
        while (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
            yield return null;
        }
        transform.localScale = targetScale;
    }
}
