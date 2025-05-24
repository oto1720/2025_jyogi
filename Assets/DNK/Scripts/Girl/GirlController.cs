using UnityEngine;
using DG.Tweening;
using DNK.Girl;
using Cysharp.Threading.Tasks;
using DNK.Item;
using System.Xml.Serialization;

public class GirlController : MonoBehaviour
{
    private Collider2D col;
    [SerializeField] private Transform[] MovePoints;
    [SerializeField] private int MovePointIndex = 0;
    [SerializeField] private float MoveSpeed = 2f;
    [Header("GirlSprite")]
    [SerializeField] private SpriteRenderer girlRenderer;
    [SerializeField]private SpriteRenderer clothRenderer;
    [SerializeField] private SpriteRenderer ringRenderer;
    [SerializeField] private SpriteRenderer flowerRenderer;
    [SerializeField] private Sprite[] girlSprites;
    [SerializeField] private Sprite RingSprite;
    [SerializeField] private Sprite[] flowerSprites;
    [Header("GirlState")]
    [SerializeField] private GirlState girlState = GirlState.Idle;
    [SerializeField] private GirlGrabState girlGrabState = GirlGrabState.None;
    [SerializeField] private GirlClothState girlClothState = GirlClothState.Normal;
    [SerializeField] private GirlRingState girlRingState = GirlRingState.None;
    [Header("Item")]
    [SerializeField] private int beBouquetCount = 0;
    private int flowerCount = 0;

    private void Start()
    {
        GetFlower();
    }
    public async UniTask MoveToNextPoint()
    {
        if (MovePointIndex >= MovePoints.Length)
        {
            MovePointIndex = 0;
        }
        var targetPosition = MovePoints[MovePointIndex].position;
        await transform.DOMove(targetPosition, MoveSpeed).SetSpeedBased().SetEase(Ease.Linear).AsyncWaitForCompletion();
        MovePointIndex++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemBehaviour obj = other.GetComponent<ItemBehaviour>();
        if (obj == null) return;

        switch (obj.itemName)
        {
            case ItemNames.Flower:
                GetFlower();
                break;
            case ItemNames.Dress:
                GetDress();
                break;
            case ItemNames.Ring:
                GetRing();
                break;
            default:
                break;
        }
    }

    private void GetFlower()
    {
        flowerCount++;
        if (flowerCount >= beBouquetCount)
        {
            girlGrabState = GirlGrabState.Bouquet;
            flowerRenderer.sprite = girlSprites[2];
        }
        else
        {
            girlGrabState = GirlGrabState.Flowers;
            flowerRenderer.sprite = girlSprites[1];
        }
    }

    private void GetDress()
    {
        girlClothState = GirlClothState.Dress;
        girlRenderer.sprite = girlSprites[1];
    }

    private void GetRing()
    {
        girlRingState = GirlRingState.Ring;
        ringRenderer.sprite = RingSprite;
    }
}   
