using Unity.Netcode;
using UnityEngine;

public class ActiceButtonStart : NetworkBehaviour
{
    public GameObject button_Start;
    public int colliderCount = 0;
    public int countMax;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (colliderCount == countMax)
        {
            if (NetworkManager.Singleton.IsHost)
            {
                button_Start.SetActive(true);
            }
            else
            {
                button_Start.SetActive(false);
            }

        }
        else
        {
            button_Start.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (colliderCount == countMax) {
            if (NetworkManager.Singleton.IsHost)
            {
                button_Start.SetActive(true);
            }
            else
            {
                button_Start.SetActive(false);
            }

        }
        else
        {
            button_Start.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        colliderCount++;
        // Debug.Log(colliderCount);

    }

    private void OnTriggerExit2D(Collider2D other)
    {

        colliderCount--;
        // Debug.Log(colliderCount);
        //foreach (GameObject num in numList)
        //{
        //    if (num.GetComponent<NumberOfPlayerOnBlank>().numbers == colliderCount)
        //    {
        //        num.gameObject.SetActive(true);
        //    }
        //}
    }

}
