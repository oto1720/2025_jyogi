using UnityEngine;
using DNK.Item;
public class ItemBehaviour : MonoBehaviour
{
    private Collider2D col;
    [Header("ItemBehaviour")]
    [SerializeField] public ItemNames itemName = ItemNames.None;
    [SerializeField] public ItemState itemState = ItemState.Idle;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    public void SetItemState(ItemState state)
    {
        itemState = state;
        switch (itemState)
        {
            case ItemState.Idle:
                col.enabled = true;
                break;
            case ItemState.Grab:
                col.enabled = false;
                break;
        }
    }

}
