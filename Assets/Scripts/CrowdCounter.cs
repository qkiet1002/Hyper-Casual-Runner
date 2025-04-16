using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrowdCounter : MonoBehaviour
{
    [Header("Elements")]
    // thông báo số con
    [SerializeField] private TextMeshPro crowdCounterText;
    [SerializeField] private Transform runnerParent;

    void Start()
    {
        
    }

    void Update()
    {
        crowdCounterText.text = runnerParent.childCount.ToString();
        
        
        if(runnerParent.childCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
