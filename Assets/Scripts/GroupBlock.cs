using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupBlock : MonoBehaviour
{
    [SerializeField] private GameObject[] blocks;
    [SerializeField] private GameObject blockTrue;
    public Block block;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject blockObject in blocks)
        {
            block = blockTrue.GetComponent<Block>();  
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(block.MathfCham == true)
        {
            foreach (GameObject blockObject in blocks)
            {
                Destroy(blockObject);
            }
        }
    }

}
