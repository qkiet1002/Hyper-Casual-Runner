using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [Header("Setting")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int roadWidth;
    [SerializeField] private bool canMove;
    [SerializeField] private PlayerAnimator playerAnimator;
    [Header("Control")]
    [SerializeField] private float slidSpeed;
    private Vector3 clickScreenPosition;
    private Vector3 clickPlayerPosition;
    [SerializeField] private CrowdSystem crowdSystem;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.onGameStateChanged += GameStateChangeCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangeCallback;
    }
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MoveSpeedForward();
            ManageControl();
        }

    }

    private void GameStateChangeCallback(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.Game)
        {
            StartMoving();
        }else if (gameState == GameManager.GameState.GameOver)
        {
            StopMoving();
        }
        else if (gameState == GameManager.GameState.LevelComplete)
        {
            StopMoving();
        }
    }
    public void StartMoving()
    {
        canMove = true;
        playerAnimator.Run();
    }

    public void StopMoving()
    {
        canMove = false;
        playerAnimator.Idle();
    }
    private void MoveSpeedForward()
    {
        // di chuyển player
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
    }
    private void ManageControl()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            // người chơi chạm vào màng hình 
            clickScreenPosition = Input.mousePosition;
            clickPlayerPosition = transform.position;
        }else if (Input.GetMouseButton(0))
        {
            // giữ chuột
            float xScreenDifference = Input.mousePosition.x - clickScreenPosition.x;
            //Debug.Log("xScreenDifference" + xScreenDifference);
            xScreenDifference /= Screen.width;
            //Debug.Log("Screen.width" + Screen.width);
            xScreenDifference *= slidSpeed;

            Vector3 positon = transform.position;
            positon.x = clickPlayerPosition.x + xScreenDifference;

            positon.x = Mathf.Clamp(positon.x, -roadWidth / 2 + crowdSystem.GetCrowdRadis(), roadWidth / 2 - crowdSystem.GetCrowdRadis());

            transform.position = positon;

            //transform.position = clickPlayerPosition + Vector3.right * xScreenDifference;
        }
    }
}
