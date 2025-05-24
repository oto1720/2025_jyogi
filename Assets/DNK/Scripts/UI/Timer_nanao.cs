using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Timer_nanao : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider countdownSlider;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Image fillImage;
    
    [Header("Timer Settings")]
    [SerializeField] private float totalTime = 60f; // 総時間（秒）
    [SerializeField] private bool autoStart = true; // 自動開始
    
    [Header("Visual Settings")]
    [SerializeField] private Color normalColor = Color.green;
    [SerializeField] private Color warningColor = Color.yellow;
    [SerializeField] private Color dangerColor = Color.red;
    [SerializeField] private float warningThreshold = 20f; // 警告色になる秒数
    [SerializeField] private float dangerThreshold = 10f;  // 危険色になる秒数
    
    [Header("Text Display")]
    [SerializeField] private bool showText = true;
    [SerializeField] private string textFormat = "残り時間: {0:00}秒";
    
    // 内部変数
    private float currentTime;
    private bool isRunning = false;
    private bool isCompleted = false;
    
    void Start()
    {
        // 初期化
        InitializeSlider();
        
        if (autoStart)
        {
            StartCountdown();
        }
    }
    
    void Update()
    {
        if (isRunning && !isCompleted)
        {
            UpdateCountdown();
        }
    }
    
    void InitializeSlider()
    {
        currentTime = totalTime;
        
        // スライダー設定
        if (countdownSlider != null)
        {
            countdownSlider.maxValue = 1f;
            countdownSlider.minValue = 0f;
            countdownSlider.value = 1f;
        }
        
        // Fill Imageを取得（設定されていない場合）
        if (fillImage == null && countdownSlider != null)
        {
            fillImage = countdownSlider.fillRect.GetComponent<Image>();
        }
        
        UpdateDisplay();
    }
    
    void UpdateCountdown()
    {
        // 時間を減らす
        currentTime -= Time.deltaTime;
        
        // 完了チェック
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            CompleteCountdown();
        }
        
        UpdateDisplay();
    }
    
    void UpdateDisplay()
    {
        // スライダーの値を更新
        if (countdownSlider != null)
        {
            float progress = currentTime / totalTime;
            countdownSlider.value = progress;
        }
        
        // テキスト更新
        UpdateTimeText();
        
        // 色更新
        UpdateSliderColor();
    }
    
    void UpdateTimeText()
    {
        if (timeText != null && showText)
        {
            timeText.text = string.Format(textFormat, currentTime);
        }
    }
    
    void UpdateSliderColor()
    {
        if (fillImage == null) return;
        
        Color targetColor = normalColor;
        
        if (currentTime <= dangerThreshold)
            targetColor = dangerColor;
        else if (currentTime <= warningThreshold)
            targetColor = warningColor;
        
        fillImage.color = targetColor;
    }
    
    void CompleteCountdown()
    {
        isRunning = false;
        isCompleted = true;
        
        Debug.Log("カウントダウン完了！");
        
        // 完了時の処理
        OnCountdownFinished();
    }
    
    void OnCountdownFinished()
    {
        // ここに完了時の処理を追加
        // 例：サウンド再生、画面切り替え、ゲーム終了など
        
        if (fillImage != null)
        {
            fillImage.color = Color.white;
        }
    }
    
    // 公開メソッド
    public void StartCountdown()
    {
        if (!isRunning && !isCompleted)
        {
            isRunning = true;
            Debug.Log("カウントダウン開始");
        }
    }
    
    public void StopCountdown()
    {
        isRunning = false;
        Debug.Log("カウントダウン停止");
    }
    
    public void ResetCountdown()
    {
        currentTime = totalTime;
        isRunning = false;
        isCompleted = false;
        UpdateDisplay();
        Debug.Log("カウントダウンリセット");
    }
    
    public void RestartCountdown()
    {
        ResetCountdown();
        StartCountdown();
    }
    
    // 時間設定
    public void SetTotalTime(float newTotalTime)
    {
        totalTime = newTotalTime;
        if (!isRunning)
        {
            currentTime = totalTime;
            UpdateDisplay();
        }
    }
    
    // 現在の残り時間を取得
    public float GetRemainingTime()
    {
        return currentTime;
    }
    
    // 進行度を取得（0-1）
    public float GetProgress()
    {
        return currentTime / totalTime;
    }
    
    // 完了したかどうか
    public bool IsCompleted()
    {
        return isCompleted;
    }
    
    // 実行中かどうか
    public bool IsRunning()
    {
        return isRunning;
    }
    
    // 残り時間をパーセンテージで取得
    public float GetRemainingPercentage()
    {
        return (currentTime / totalTime) * 100f;
    }
}

