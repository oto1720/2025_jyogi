using UnityEngine;
using DG.Tweening;
using DNK.Girl;
using Cysharp.Threading.Tasks;

public class GirlController : MonoBehaviour
{
    [SerializeField] private Transform[] MovePoints;
    [SerializeField] private int MovePointIndex = 0;
    [SerializeField] private float MoveSpeed = 2f;
    [Header("GirlSprite")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] girlSprites;
    [Header("GirlState")]
    [SerializeField] private GirlState girlState = GirlState.Idle;
    [SerializeField] private GirlGrabState girlGrabState = GirlGrabState.None;
    [SerializeField] private GirlClothState girlClothState = GirlClothState.Normal;
    [SerializeField] private GirlRingState girlRingState = GirlRingState.None;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
}   
