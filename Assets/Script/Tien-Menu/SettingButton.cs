using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SettingButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("SettingScene");
    }
}
