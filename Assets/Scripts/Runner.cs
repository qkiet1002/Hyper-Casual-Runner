using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    [Header("Setting")]
    private bool isTaget;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTaget()
    {
        isTaget = true;
    }
    public bool IsTaget()
    {
        return isTaget;
    }
}
