using UnityEngine;
using UnityEngine.UI;

public class NpcComment : MonoBehaviour
{
    [Header("吹き出し設定")]
    public GameObject speechBubble;  // 吹き出しのGameObject
    public Text dialogueText;        // 吹き出し内のテキスト
    public string message = "こんにちは！";  // 表示するメッセージ
    
    [Header("距離設定")]
    public float triggerDistance = 2f;  // 反応する距離
    
    private Transform player;
    private bool isPlayerNear = false;
    
    void Start()
    {
        // プレイヤーを探す
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        // 最初は吹き出しを非表示
        if (speechBubble != null)
            speechBubble.SetActive(false);
    }
    
    void Update()
    {
        if (player == null) return;
        
        // プレイヤーとの距離を計算
        float distance = Vector2.Distance(transform.position, player.position);
        
        // 距離に応じて吹き出しの表示/非表示を切り替え
        if (distance <= triggerDistance && !isPlayerNear)
        {
            ShowSpeechBubble();
            isPlayerNear = true;
        }
        else if (distance > triggerDistance && isPlayerNear)
        {
            HideSpeechBubble();
            isPlayerNear = false;
        }
        
        // 吹き出しが表示されている場合、NPCの上に位置を更新
        if (isPlayerNear && speechBubble != null)
        {
            UpdateBubblePosition();
        }
    }
    
    void ShowSpeechBubble()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(true);
            if (dialogueText != null)
                dialogueText.text = message;
        }
    }
    
    void HideSpeechBubble()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(false);
        }
    }
    
    void UpdateBubblePosition()
    {
        // NPCの頭上に吹き出しを配置
        Vector3 worldPosition = transform.position + Vector3.up * 1.5f;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        speechBubble.transform.position = screenPosition;
    }
}
