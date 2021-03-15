using pixelook;
using UnityEngine;

public class PurchasePanel : MonoBehaviour
{
    [SerializeField] private GameObject purchaseInProgressPanel;

    private void OnEnable()
    {
        EventManager.AddListener(Events.PURCHASE_FINISHED, OnPurchaseFinished);
    }

    private void Start()
    {
        purchaseInProgressPanel.SetActive(false);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PURCHASE_FINISHED, OnPurchaseFinished);
    }

    private void OnPurchaseFinished()
    {
        purchaseInProgressPanel.SetActive(false);        
    }

    public void OnUnlockAllSkinsButtonClick()
    {
        purchaseInProgressPanel.SetActive(true);
    }

    public void OnRestorePurchasesButtonClick()
    {
        purchaseInProgressPanel.SetActive(true);
    }
}
