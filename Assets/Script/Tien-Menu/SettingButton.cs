using UnityEngine;
using System.Collections;

public class SettingButtonObject : MonoBehaviour
{
    public GameObject menuPanel;   
    public GameObject settingPanel; 

    public Color clickColor = Color.grey;
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

       
        settingPanel.SetActive(false);
        menuPanel.SetActive(true);
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
        StartCoroutine(SwitchPanels()); 
    }

    IEnumerator SwitchPanels()
    {
        yield return new WaitForSeconds(0.1f); 

        
        bool isMenuActive = menuPanel.activeSelf;
        menuPanel.SetActive(!isMenuActive);
        settingPanel.SetActive(isMenuActive);
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
