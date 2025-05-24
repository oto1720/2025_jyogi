using Unity.VisualScripting;
using UnityEngine;
using DNK.Item;
public class lake : MonoBehaviour
{
    [SerializeField] private GameObject dress;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D other)
    {
        ItemBehaviour obj = other.GetComponent<ItemBehaviour>();
        if (obj == null) return;
        if (obj.itemName != ItemNames.Dress)
        {
            Destroy(other.transform.gameObject);
        }

        if (obj.itemName == ItemNames.Cloth)
        {
            Instantiate(dress, this.transform.position, Quaternion.identity);
        }
    }
}
