using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class StartMapButtonMap2 : NetworkBehaviour
{
    public string sceneMapName;
    public Button buttonStartMap;
    private SceneTransitionManager changeSceneNew;
    // Start is called before the first frame update
    void Start()
    {
        changeSceneNew = FindAnyObjectByType<SceneTransitionManager>();
        buttonStartMap.onClick.AddListener(() =>
        {
            Debug.Log("HaiDuong");
            changeSceneNew.ChangeScene(sceneMapName);
            Debug.Log("HaiDuong2");
        });

    }

    // Update is called once per frame
    void Update()
    {

    }
}
