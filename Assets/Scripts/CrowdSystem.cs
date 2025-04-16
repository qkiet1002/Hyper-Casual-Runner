using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header("Elments")]
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private Transform runnerParent;
    [SerializeField] private GameObject[] runnerPrefabs;

    [Header("Setting")]
    [SerializeField] private float radius;
    [SerializeField] private float angel;
    void Start()
    {
       
    }

    void Update()
    {
        if (!GameManager.instance.IsGameState())
            return;

        PlaceRunner();
        if(runnerParent.childCount <=0 )
        {
            GameManager.instance.SetGameState(GameManager.GameState.GameOver);
        }
    }

    public int Runners()
    {
        //for(int i = 0; i < runnerParent.childCount; i++)
        //{
        //    return i;
        //}
        return runnerParent.childCount;
    }

    private void PlaceRunner()
    {
        for (int i = 0; i < runnerParent.childCount; i++)
        {
            Vector3 chlidLocalPosition = PlayerRunnerLocalPositions(i);
            runnerParent.GetChild(i).localPosition = chlidLocalPosition;
        }
    }
    private Vector3 PlayerRunnerLocalPositions(int index)
    {
        float x = radius* Mathf.Sqrt(index)*Mathf.Cos(Mathf.Deg2Rad*index*angel);
        float z = radius* Mathf.Sqrt(index)*Mathf.Sin(Mathf.Deg2Rad*index*angel);
        return new Vector3(x,0,z);
    }

    public float GetCrowdRadis()
    {
        return radius*Mathf.Sqrt(runnerParent.childCount);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        PlayerAudio playerAudio = GetComponent<PlayerAudio>();
        switch (bonusType)
        {
            // cộng
            case BonusType.Addition:
                AddRunners(bonusAmount);
                playerAudio.AddMore();
                break;
            // nhân
            case BonusType.Product:
                int runnerToAdd = (runnerParent.childCount * bonusAmount) - runnerParent.childCount;
                AddRunners(runnerToAdd);
                playerAudio.AddMore();
                break;
            // trừ
            case BonusType.Difference:
                RemoveRunners(bonusAmount);
                playerAudio.RemoveMore();
                break;
            // chia
            case BonusType.Division:
                int runnerRemove = runnerParent.childCount - (runnerParent.childCount / bonusAmount);
                RemoveRunners(runnerRemove);
                playerAudio.RemoveMore();
                break;

        }
    }

    private void AddRunners(int amount)
    {
        for(int i = 0; i<amount; i++)
        {
            int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
            GameObject prefab = runnerPrefabs[selectedCharacter];

            Instantiate(prefab, runnerParent);
            playerAnimator.Run();
        }
    }
    private void RemoveRunners(int amount)
    {
        if(amount > runnerParent.childCount)
            amount = runnerParent.childCount;
        int runnersAmount = runnerParent.childCount;

        for (int i = runnersAmount - 1; i>runnersAmount - amount; i--)
        {
            Transform runnerToDestroy = runnerParent.GetChild(i);
            runnerToDestroy.SetParent(null);
            Destroy(runnerToDestroy.gameObject);
            
        }
    }
}
