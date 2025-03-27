using UnityEngine;

public class SettingsToggle : MonoBehaviour
{
    public GameObject settingsObject; // Kéo GameObject Settings vào đây trong Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            settingsObject.SetActive(!settingsObject.activeSelf); // Bật/tắt object khi ấn M
        }
    }
}
