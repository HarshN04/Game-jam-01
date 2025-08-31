using UnityEngine;

public class SellTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory.Instance.SellAllSugarcane();
        }
    }
}
