using UnityEngine;
using System.Collections;

public class SettingButtonObject : MonoBehaviour
{
    public GameObject targetObject; // Object duy nhất để ẩn/hiện

    public Color clickColor = Color.grey;
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

        // Đảm bảo object được gán và thiết lập trạng thái ban đầu
        if (targetObject != null) targetObject.SetActive(true);
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
        StartCoroutine(ToggleObject());
    }

    IEnumerator ToggleObject()
    {
        yield return new WaitForSeconds(0.1f);

        // Nếu targetObject tồn tại, đổi trạng thái hiện/ẩn
        if (targetObject != null)
        {
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }

    IEnumerator ScaleObject(Vector3 targetScale)
    {
        while (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
            yield return null;
        }
        transform.localScale = targetScale;
    }
}
