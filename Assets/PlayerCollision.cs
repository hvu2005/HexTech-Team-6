using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void Awake()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            Destroy(collision.gameObject);

            GameManager.Instance.AddKeys(1);
            Debug.Log(GameManager.Instance.keys);
            if (GameManager.Instance.keys == 3)
            {
                GameManager.Instance.GameCompleted();
            }
        }
        else if (collision.CompareTag("Trap"))
        {
            GameManager.Instance.GameOver();
        }
    }
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
