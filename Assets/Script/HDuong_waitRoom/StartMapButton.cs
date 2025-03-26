using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class StartMapButton : NetworkBehaviour
{
    public string sceneMapName;
    public Button buttonStartMap;
    private ChangeSceneNetcode changeSceneNetcode;
    // Start is called before the first frame update
    void Start()
    {
        changeSceneNetcode = FindAnyObjectByType<ChangeSceneNetcode>();
        buttonStartMap.onClick.AddListener(() =>
        {
            changeSceneNetcode.ChangeScene(sceneMapName);
        });
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
