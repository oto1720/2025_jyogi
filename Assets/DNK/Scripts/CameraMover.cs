using UnityEngine;
using DG.Tweening;
public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float moveSpeed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void MoveNextPoint(int movePointIndex)
    {
        if (movePointIndex < 0 || movePointIndex >= movePoints.Length)
        {
            Debug.LogError("Invalid move point index.");
            return;
        }

        // Move the camera to the next point
        Vector3 targetPosition = movePoints[movePointIndex].position;
        transform.DOMove(targetPosition, moveSpeed).SetSpeedBased().SetEase(Ease.Linear);
    }
}
