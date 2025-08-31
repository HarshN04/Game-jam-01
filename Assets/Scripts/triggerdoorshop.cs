using UnityEngine;

public class TriggerDoorShop : MonoBehaviour
{
    private ShopUIManager shopManager;

    private void Start()
    {
        shopManager = FindObjectOfType<ShopUIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopManager.OpenShop();
        }
    }
}
