using UnityEngine;
using DNK.Item;

public class Home : MonoBehaviour
{
    [SerializeField] private GameObject girlfriend;
    [SerializeField] private Vector2 girlOffset;
    void OnTriggerEnter2D(Collider2D other)
    {
        ItemBehaviour obj = other.GetComponent<ItemBehaviour>();
        if (obj == null) return;

        if (obj.itemName == ItemNames.Flower)
        {
            Debug.Log("彼女が出てきます");
            Destroy(other.transform.gameObject);
            Vector2 pos = this.transform.position;
            Instantiate(girlfriend, pos + girlOffset, Quaternion.identity);

        }
    }
}
