using UnityEngine;
using System.Collections;

public class ShopUIManager : MonoBehaviour
{
    public GameObject shopPanel;   // assign Shop Panel Canvas
    public SceneFader fader;       // assign FadeCanvas with SceneFader script

    // Public methods so other scripts (like the door) can call them
    public void OpenShop()
    {
        StartCoroutine(OpenShopCoroutine());
    }

    public void CloseShop()
    {
        StartCoroutine(CloseShopCoroutine());
    }

    // These remain private (only ShopUIManager uses them internally)
    private IEnumerator OpenShopCoroutine()
    {
        if (fader != null)
            yield return fader.FadeOutCoroutine();

        shopPanel.SetActive(true);

        if (fader != null)
            yield return fader.FadeInCoroutine();
    }

    private IEnumerator CloseShopCoroutine()
    {
        if (fader != null)
            yield return fader.FadeOutCoroutine();

        shopPanel.SetActive(false);

        if (fader != null)
            yield return fader.FadeInCoroutine();
    }
}
