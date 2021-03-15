using System.Collections.Generic;
using UnityEngine;
using pixelook;

public class PigSkins : MonoBehaviour
{
    [SerializeField] private Skin skinPrefab;

    private List<Skin> _skinInstances;
    private int _selectedSkin;

    private void OnEnable()
    {
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.AddListener(Events.SKIN_LEFT_BUTTON_CLICK, OnSkinLeftButtonClick);
        EventManager.AddListener(Events.SKIN_RIGHT_BUTTON_CLICK, OnSkinRightButtonClick);
    }

    private void Start()
    {
        _skinInstances = new List<Skin>();
        _selectedSkin = GameManager.Instance.GameSetup.SelectedSkinIndex;
        
        ShowSkins();        
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.RemoveListener(Events.SKIN_LEFT_BUTTON_CLICK, OnSkinLeftButtonClick);
        EventManager.RemoveListener(Events.SKIN_RIGHT_BUTTON_CLICK, OnSkinRightButtonClick);
    }
    
    private void OnGameStarted()
    {
        gameObject.SetActive(false);
    }

    private void OnSkinLeftButtonClick()
    {
        // we are on the first position
        if (_selectedSkin == 0) return;

        _selectedSkin--;

        MoveSkinsToPositions();
    }

    private void OnSkinRightButtonClick()
    {
        // we are on the last position
        if (_selectedSkin == GameManager.Instance.GameSetup.skins.Length - 1) return;

        _selectedSkin++;

        MoveSkinsToPositions();
    }

    private void ShowSkins()
    {
        for (int i = 0; i < GameManager.Instance.GameSetup.skins.Length; i++)
        {
            Skin skinInstance = Instantiate(skinPrefab, transform, false);

            int skinRelativePosition = i - _selectedSkin;

            skinInstance.transform.localPosition = GetSkinPosition(skinRelativePosition);
            
            skinInstance.Setup(GameManager.Instance.GameSetup.skins[i], i / (float)GameManager.Instance.GameSetup.skins.Length);
            
            _skinInstances.Add(skinInstance);
        }
    }


    private void MoveSkinsToPositions()
    {
        if (GameManager.Instance.GameSetup.skins[_selectedSkin].IsUnlocked || GameManager.Instance.GameSetup.AreUnlockedAll)
        {
            GameManager.Instance.GameSetup.SelectedSkinIndex = _selectedSkin;
            EventManager.TriggerEvent(Events.PLAYER_SKIN_CHANGED);
        }
        
        for (int i = 0; i < GameManager.Instance.GameSetup.skins.Length; i++)
        {
            int skinRelativePosition = i - _selectedSkin;
            
            _skinInstances[i].MoveTo(GetSkinPosition(skinRelativePosition));
        }
    }

    private Vector3 GetSkinPosition(int skinRelativePosition)
    {
        return new Vector3(
            skinRelativePosition * 1.25f,
            0,
            Mathf.Abs(skinRelativePosition * 1.75f));
    }
}
