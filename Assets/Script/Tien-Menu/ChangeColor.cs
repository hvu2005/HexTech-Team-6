using UnityEngine;
using UnityEngine.UI;

public class RainbowText : MonoBehaviour
{
    public Text uiText;  // Kéo Text UI vào đây trong Inspector
    public float duration = 2f; // Thời gian đổi màu

    private Color[] rainbowColors = {
        Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta, Color.white
    };
    private int currentIndex = 0;
    private float timeElapsed = 0f;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        float t = Mathf.PingPong(timeElapsed / duration, 1);

        int nextIndex = (currentIndex + 1) % rainbowColors.Length;
        uiText.color = Color.Lerp(rainbowColors[currentIndex], rainbowColors[nextIndex], t);

        if (t >= 0.99f) // Khi đổi màu xong, chuyển sang cặp màu kế tiếp
        {
            currentIndex = nextIndex;
            timeElapsed = 0;
        }
    }
}
