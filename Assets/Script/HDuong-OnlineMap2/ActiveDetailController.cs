using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDetailController : MonoBehaviour
{
    private SetActiveMayBay setActiveMB;
    // Start is called before the first frame update
    void Start()
    {
        setActiveMB = FindObjectOfType<SetActiveMayBay>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SetActiveMayBay.Instance != null)
        {
            SetActiveMayBay.Instance.Disappear();
        }
    }
}
