using UnityEngine;

public class Stonecutter : MonoBehaviour
{
    [SerializeField] private Transform seat;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnterが起きたよ", gameObject);

        ItemBehaviour obj = other.GetComponent<ItemBehaviour>();
        if (obj == null) return;

        other.transform.SetParent(seat);
        other.transform.localPosition = new Vector2(0,0);
    }
    
}
