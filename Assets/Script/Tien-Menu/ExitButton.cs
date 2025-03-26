using UnityEngine;
using System.Collections;

public class ExitButtonObject : MonoBehaviour
{
    public Color clickColor = Color.gray;
    public float delayBeforeExit = 0.001f;
    public float scaleFactor = 1.07f;
    public float scaleSpeed = 5f;

    private Renderer objectRenderer;
    private Color originalColor;
    private Vector3 originalScale;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
        originalScale = transform.localScale;
    }

    void OnMouseEnter()
    {
        StopAllCoroutines();
        StartCoroutine(ScaleObject(originalScale * scaleFactor));
    }

    void OnMouseExit()
    {
        StopAllCoroutines();
        StartCoroutine(ScaleObject(originalScale));
    }

    void OnMouseDown()
    {
        objectRenderer.material.color = clickColor;
        StartCoroutine(ExitGameAfterDelay());
    }

    IEnumerator ExitGameAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeExit);
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
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
