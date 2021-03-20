using System;
using pixelook;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private GameObject selectedSkin;

    private void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_SKIN_CHANGED, OnPlayerSkinChanged);
    }

    private void Start()
    {
        ChangePlayerSkin();
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PLAYER_SKIN_CHANGED, OnPlayerSkinChanged);
    }

    private void OnPlayerSkinChanged()
    {
        ChangePlayerSkin();
    }

    private void ChangePlayerSkin()
    {
        if (selectedSkin != null)
            Destroy(selectedSkin);

        selectedSkin =
            Instantiate(GameManager.Instance.GameSetup.skins[GameManager.Instance.GameSetup.SelectedSkinIndex].model,
                transform, false);
    }
}
