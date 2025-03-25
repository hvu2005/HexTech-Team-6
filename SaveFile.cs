using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public class GameScore
{
    public List<LevelScore> levelScores;
}

[System.Serializable]
public class LevelScore
{
    public int level;
    public int score;
}

public class SaveFile : MonoBehaviour
{
    public GameScore gameScore;

    public int level;
    public int score;

    private void Start()
    {
        LoadData();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            //SaveData();
            Debug.Log("Save");
            Save(level, score);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Debug.Log("Load");
            Load(level);
        }
    }

    public void Save(int level, int score)
    {
        foreach (var levelScore in gameScore.levelScores)
        {
            if(levelScore.level == level)
            {
                if(score > levelScore.score)
                {
                    levelScore.score = score;
                    SaveData();
                }

                return;
            }
        }

        LevelScore lScore = new LevelScore();
        lScore.level = level;
        lScore.score = score;

        gameScore.levelScores.Add(lScore);

        SaveData();
    }

    public void Load(int level)
    {
        foreach (var levelScore in gameScore.levelScores)
        {
            if(levelScore.level == level)
            {
                Debug.Log("Load level score: " + level + " " + levelScore.score);
                    return;
            }
        }

        Debug.Log("Load level score: " + level + " " + 0);
    }

    public void LoadData()
    {
        string file = "save.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
        }

        gameScore = JsonUtility.FromJson<GameScore>(File.ReadAllText(filePath));
        Debug.Log("Load done!");
    }

    public void SaveData()
    {
        string file = "save.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);

        string json = JsonUtility.ToJson(gameScore, true);

        File.WriteAllText(filePath, json);
        Debug.Log("File saved, at path: " + filePath);
    }
}