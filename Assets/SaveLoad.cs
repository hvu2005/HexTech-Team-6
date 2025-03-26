using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public string Key_score = "Score_save";
    public int Level = 0;
    public int Score;
    public string Key_score_format = "Score_save";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Debug.Log("Save");
            Save(Level, Score);
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Debug.Log("Load");
            Load(Level);
        }

    }

    public void Save(int Level, int Score)
    {
        Debug.Log("Save level " + Level + " " + Score);
        PlayerPrefs.SetInt(Key_score_format + Level.ToString(), Score);
        PlayerPrefs.Save();
    }

    public void Load(int Level)
    {
        int scoreLoaded = PlayerPrefs.GetInt(Key_score_format+Level.ToString());
        Debug.Log("Load level score: " + " "+ scoreLoaded);
    }

}
