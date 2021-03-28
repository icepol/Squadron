using System;
using pixelook;
using UnityEngine;

public class PlayerRocketEngine : MonoBehaviour
{
    [SerializeField] private ParticleSystem engineDust;
    [SerializeField] private ParticleSystem engineDustForsage;

    private void OnEnable()
    {
        EventManager.AddListener(Events.COMBO_MULTIPLIER_CHANGED, OnComboMultiplierChanged);
    }

    void Start()
    {
        ForsageOff();
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.COMBO_MULTIPLIER_CHANGED, OnComboMultiplierChanged);
    }

    private void OnComboMultiplierChanged()
    {
        if (GameState.ComboMultiplier >= 3)
            ForsageOn();
        else
            ForsageOff();
    }

    void ForsageOn()
    {
        engineDust.Stop();
        engineDustForsage.Play();
    }

    void ForsageOff()
    {
        engineDust.Play();
        engineDustForsage.Stop();
    } 
}
