using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankSCript : MonoBehaviour
{
    public List<GameObject> numList = new List<GameObject> ();
    private int colliderCount = 0;
    public int countMax;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject num in numList)
        {
            if (num.GetComponent<NumberOfPlayerOnBlank>().numbers == (countMax-colliderCount))
            {
                num.gameObject.SetActive(true);
            }
            else
            {
                num.gameObject.SetActive (false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    { 
        if (colliderCount > countMax)
        {
            this.gameObject.AddComponent<Rigidbody2D>();
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(DestroyObject());
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

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
