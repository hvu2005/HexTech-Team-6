using UnityEngine;

public class Key : MonoBehaviour
{

    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;
    public GameManager gm;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

   // public GameObject key;

    // Start is called before the first frame update
    void Awake()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    void Update()
    {
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
    
   