using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int keys = 0;
    public Text keyText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateKey();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddKeys(int key)
    {
        keys += key;
        UpdateKey();
    }
    private void UpdateKey()
    {
        keyText.text = keys.ToString();
    }
}
