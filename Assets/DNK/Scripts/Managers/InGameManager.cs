using Cysharp.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DNK.Player;

public class InGameManager : MonoBehaviour
{
    [Header("gameTime")]
    [SerializeField] private float inGameTimer;
    [SerializeField] private float inGameTimeLimit;
    [Header("camera")]
    [SerializeField] private CameraMover cameraMover;
    [SerializeField] private int nowStageIndex = 0;
    [Header("character")] 
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GirlController girlController;
    [Header("gameOver")]
    private bool isGameOver = false;
    [SerializeField] private SpriteRenderer gameOverSr;
    [SerializeField] private Sprite bombSprite;
    [SerializeField] private int gameOverTimeMilliseconds = 1000;

    private void Start()
    {
        inGameTimer = 0f;
    }
    private void Update()
    {
        inGameTimer += Time.deltaTime;
        float timeSpan = inGameTimeLimit / 4;

        if (inGameTimer >= timeSpan && inGameTimer < timeSpan * 2)
        {
            NextStage(1);
        }
        else if (inGameTimer >= timeSpan * 2 && inGameTimer < timeSpan * 3)
        {
            NextStage(2);
        }
        else if (inGameTimer >= timeSpan * 3 && inGameTimer < inGameTimeLimit)
        {
            NextStage(3);
        }

        if (inGameTimer >= inGameTimeLimit && !isGameOver)
        {
            GameOver();
        }
    }

    public async void GameOver()
    {
        isGameOver = true;
        gameOverSr.sprite = bombSprite;
        playerController.SetPlayerState(PlayerState.Dead);
        await UniTask.Delay(gameOverTimeMilliseconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void NextStage(int nextStageIndex)
    {
        if (nowStageIndex == nextStageIndex)
        {
            return;
        }

        nowStageIndex = nextStageIndex;
        cameraMover.MoveNextPoint(nowStageIndex-1);
    }
}
