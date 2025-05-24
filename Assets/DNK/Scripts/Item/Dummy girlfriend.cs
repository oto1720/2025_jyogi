using UnityEngine;
using DNK.Item;
using System.Collections.Generic;
public class Dummygirlfriend : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private List<ItemNames> sampleLunchList;
    [SerializeField] private List<ItemNames> lunchList;
    void OnTriggerEnter2D(Collider2D other)
    {
        ItemBehaviour obj = other.GetComponent<ItemBehaviour>();
        if (obj == null) return;

        if (obj.itemName == ItemNames.Strawberry || obj.itemName == ItemNames.banana || obj.itemName == ItemNames.Blueberry || obj.itemName == ItemNames.kiwi || obj.itemName == ItemNames.Orange || obj.itemName == ItemNames.mango || obj.itemName == ItemNames.Lychee)
        {
            lunchList.Add(obj.itemName);

            for (int i = 0; i < lunchList.Count; i++)
            {
                Debug.Log(lunchList +"and"+ sampleLunchList);
                if (lunchList[i] != sampleLunchList[i]) break;
                Debug.Log("checklist");
                if (i == sampleLunchList.Count -1 )
                {
                    Debug.Log("付き合えました");
                }
            }
            Destroy(other.transform.gameObject);
        }
    }


}
