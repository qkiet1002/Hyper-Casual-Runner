using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;
    [Header("Elements")]
    [SerializeField] private LevelSO[] levels;
    private GameObject finishLine;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        //CreateOderdedLevels();
        GenerateLevels();
        finishLine = GameObject.FindWithTag("Finish");
    }

    private void CreateLevels(Chunk[] levelChunks)
    {
        Vector3 chunkPositon = Vector3.zero;
        for (int i = 0; i < levelChunks.Length; i++)
        {
            Chunk chunkToCreate = levelChunks[i];
            if (i > 0)
            {
                chunkPositon.z += chunkToCreate.GetLength() / 2;

            }
            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPositon, Quaternion.identity, transform);
            chunkPositon.z += chunkInstance.GetLength() / 2;
        }
    }

    //private void CreateRandomLevels()
    //{
    //    Vector3 chunkPositon = Vector3.zero;
    //    for (int i = 0; i < 5; i++)
    //    {
    //        Chunk chunkToCreate = chunkPrefab[Random.Range(0, chunkPrefab.Length)];
    //        if (i > 0)
    //        {
    //            chunkPositon.z += chunkToCreate.GetLength() / 2;

    //        }
    //        Chunk chunkInstance = Instantiate(chunkToCreate, chunkPositon, Quaternion.identity, transform);
    //        chunkPositon.z += chunkInstance.GetLength() / 2;
    //    }
    //}


    private void GenerateLevels()
    {
        int currentsLevel = GetLevels();
        currentsLevel = currentsLevel % levels.Length;
        LevelSO level = levels[currentsLevel];
        CreateLevels(level.chunks);
    }
    public float GetFinshZ()
    {
        return finishLine.transform.position.z;
    }

    public int GetLevels()
    {
        return PlayerPrefs.GetInt("Level",0);
    }
}
