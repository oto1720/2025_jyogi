using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    [SerializeField] private float inGameTimer;
    [SerializeField] private float inGameTimeLimit;
    [SerializeField] private CameraMover cameraMover;
    [SerializeField] private int nowStageIndex = 0;

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

        if (inGameTimer >= inGameTimeLimit)
        {
            // Game Over logic here
            Debug.Log("Game Over");

        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void NextStage(int nextStageIndex)
    {
        if (nowStageIndex == nextStageIndex)
        {
            Debug.LogWarning("Already at the next stage.");
            return;
        }

        nowStageIndex = nextStageIndex;
        cameraMover.MoveNextPoint(nowStageIndex-1);
    }
}
