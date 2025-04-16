using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDection : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private CrowdSystem crowdSystem;

    private bool hasFinished = false; // Biến cờ để kiểm tra nếu đã hoàn thành

    void Start()
    {

    }

    void Update()
    {
        DetectDoors();
    }

    private void DetectDoors()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, 1);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Doors doors))
            {
               // Debug.Log("Hit the Doors");
                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);
                doors.DisableDoorCollider();
                crowdSystem.ApplyBonus(bonusType, bonusAmount);

            }
            else if (detectedColliders[i].tag == "Finish" && !hasFinished)
            {
                // Đảm bảo chỉ xử lý một lần khi va chạm với Finish
                hasFinished = true;

              //  Debug.Log("Current Level: " + PlayerPrefs.GetInt("Level"));
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
                GameManager.instance.SetGameState(GameManager.GameState.LevelComplete);
                PlayerAudio playerAudio = GetComponent<PlayerAudio>();
                playerAudio.Win();
             //   Debug.Log("Next Level: " + PlayerPrefs.GetInt("Level"));

                // Bạn có thể thêm hiệu ứng ăn mừng tại đây nếu cần
            }
        }
    }
}
