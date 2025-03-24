using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChildForBox : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            this.transform.SetParent(other.transform);
        }
    }

}
