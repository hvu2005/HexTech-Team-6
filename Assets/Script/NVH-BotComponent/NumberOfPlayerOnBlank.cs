using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class NumberOfPlayerOnBlank : MonoBehaviour
{
    public static NumberOfPlayerOnBlank Instance;
    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        Instance = null;
    }

    public int numbers;
    // Start is called before the first frame update
    void Awake()
    {
        numbers = Int32.Parse(this.gameObject.name);
    }
}
