using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCodeRoom : MonoBehaviour
{
    private IDRoomInstance roomInstance;
    public Text textIdRoom;

    // Start is called before the first frame update
    void Start()
    {
        roomInstance = FindAnyObjectByType<IDRoomInstance>();
        textIdRoom.text = "Code: " + roomInstance.IdRoom;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
