using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image buttonImage; // Ảnh nền của nút
    private Color originalColor;
    public Color hoverColor = new Color(0.8f, 0.8f, 0.8f, 1f); // Màu khi di chuột vào
    public Color clickColor = new Color(0.6f, 0.6f, 0.6f, 1f); // Màu khi click

    void Start()
    {
        originalColor = buttonImage.color; // Lưu màu gốc
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.color = hoverColor; // Đổi màu khi di chuột vào
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.color = originalColor; // Trả về màu gốc khi di chuột ra
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonImage.color = clickColor; // Đổi màu khi click
        Invoke("ResetColor", 0.2f); // Reset màu sau 0.2s
    }

    void ResetColor()
    {
        buttonImage.color = originalColor;
    }
}