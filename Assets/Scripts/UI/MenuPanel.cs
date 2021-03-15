using pixelook;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private Button soundsButton;
    [SerializeField] private Button musicButton;

    [SerializeField] private GameObject skinsPanel;
    [SerializeField] private Button unlockAllSkinsButton;
    [SerializeField] private Button restorePurchasesButton;

    private bool _isSettingsPanelVisible;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
    }

    private void Start()
    {
        UpdateButton(soundsButton, Settings.IsSfxEnabled);
        UpdateButton(musicButton, Settings.IsMusicEnabled);
        UpdateButton(unlockAllSkinsButton, !GameManager.Instance.GameSetup.AreUnlockedAll);
        UpdateButton(restorePurchasesButton, !GameManager.Instance.GameSetup.AreUnlockedAll);

        if (!GameManager.Instance.GameSetup.AreUnlockedAll) return;
        
        unlockAllSkinsButton.GetComponent<Button>().enabled = false;
        restorePurchasesButton.GetComponent<Button>().enabled = false;
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
    }

    private void OnGameStarted()
    {
        gameObject.SetActive(false);
    }

    public void OnPlayButtonClick()
    {
        EventManager.TriggerEvent(Events.GAME_STARTED);
    }

    public void OnSettingsButtonClick()
    {
        _animator.SetTrigger("ToggleSettings");
        _isSettingsPanelVisible = !_isSettingsPanelVisible;
        
        skinsPanel.SetActive(!_isSettingsPanelVisible);
    }
    
    public void OnSoundsButtonClick()
    {
        Settings.IsSfxEnabled = !Settings.IsSfxEnabled;
        UpdateButton(soundsButton, Settings.IsSfxEnabled);
    }
    
    public void OnMusicButtonClick()
    {
        Settings.IsMusicEnabled = !Settings.IsMusicEnabled;
        UpdateButton(musicButton, Settings.IsMusicEnabled);
        
        EventManager.TriggerEvent(Events.MUSIC_SETTINGS_CHANGED);
    }

    public void OnLeaderboardButtonClick()
    {
        GameServices.ShowLeaderBoard();
    }

    public void OnRatingButtonClick()
    {
        Application.OpenURL(Constants.AppStoreLink);
    }

    public void OnPrivacyPolicyButtonClick()
    {
        Application.OpenURL(Constants.PrivacyPolicyURL);
    }
    
    public void OnSkinLeftButtonClick()
    {
        EventManager.TriggerEvent(Events.SKIN_LEFT_BUTTON_CLICK);
    }
    
    public void OnSkinRightButtonClick()
    {
        EventManager.TriggerEvent(Events.SKIN_RIGHT_BUTTON_CLICK);
    }

    void UpdateButton(Button button, bool isEnabled) {
        foreach (Text text in button.GetComponentsInChildren<Text>()) {
            Color color = text.color;
            color.a = isEnabled ? 1f : 0.5f;
            text.color = color;
        }
    }
}
