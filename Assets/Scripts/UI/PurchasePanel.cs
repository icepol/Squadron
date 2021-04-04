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

        EventManager.TriggerEvent(Events.PANEL_HIDE);
    }

    public void OnUnlockAllSkinsButtonClick()
    {
        purchaseInProgressPanel.SetActive(true);
        
        EventManager.TriggerEvent(Events.PANEL_SHOW);
    }

    public void OnRestorePurchasesButtonClick()
    {
        purchaseInProgressPanel.SetActive(true);
        
        EventManager.TriggerEvent(Events.PANEL_SHOW);
    }
}
