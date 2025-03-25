using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour
{
    /*[Header("Objects")]
    public GameObject[] objectsToShow; // Các object cần hiện lại khi nhấn nút Exit
    public GameObject objectToHide;    // Object đang hiển thị, cần ẩn đi khi nhấn Exit

    [Header("Effects")]
    public Color clickColor = Color.red;
    public float scaleFactor = 1.07f;
    public float scaleSpeed = 5f;
    public float fadeDuration = 0.2f;

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
        StartCoroutine(FadeToColor(originalColor));
        StartCoroutine(ScaleObject(originalScale));
    }

    void OnMouseDown()
    {
        StopAllCoroutines();
        StartCoroutine(FadeToColor(clickColor));
        ToggleObjects();
    }

    void ToggleObjects()
    {
        // Ẩn object đang hiển thị
        if (objectToHide != null)
        {
            objectToHide.SetActive(false);
        }

        // Hiện lại tất cả object trong danh sách
        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    IEnumerator FadeToColor(Color targetColor)
    {
        float elapsedTime = 0f;
        Color startColor = objectRenderer.material.color;

        while (elapsedTime < fadeDuration)
        {
            objectRenderer.material.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        objectRenderer.material.color = targetColor;
    }

    IEnumerator ScaleObject(Vector3 targetScale)
    {
        while (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
            yield return null;
        }
        transform.localScale = targetScale;
    }*/
}
