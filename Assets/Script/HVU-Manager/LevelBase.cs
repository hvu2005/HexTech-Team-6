using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract void onWin();
    protected abstract void onLose();
}
