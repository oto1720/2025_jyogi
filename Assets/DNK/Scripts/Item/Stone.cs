using UnityEngine;

public class Stone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnterが起きたよ", gameObject);
    }
    
}