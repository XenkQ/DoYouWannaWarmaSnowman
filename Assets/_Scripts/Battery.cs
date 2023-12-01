using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    [SerializeField] private Slider _energySlider;
    [SerializeField] private int _currentEnergy;
    public int CurrentEnergy => _currentEnergy;
    private Stat _energy;
    public event Action onDischarge;
    private bool isRechargingEnergy = false;
    private int energyIncreaser = 10;
    [SerializeField] private float rechargingEnergySpeed = 4f;

    private void Start()
    {
        _energy = GetComponent<PlayerStats>().energy;
        _currentEnergy = _energy.value;
    }

    private void FixedUpdate()
    {
        if(isRechargingEnergy == false)
        {
            StartCoroutine(IncreaseEnergyOverTime(energyIncreaser));
        }
    }

    public IEnumerator DecreaseEnergyOverTime(int value)
    {
        while (_currentEnergy > 0)
        {
            InstantDecreaseEnergy(value);
            yield return new WaitForSeconds(1f);
        }
        if (onDischarge == null) yield break;
        onDischarge();
    }

    public IEnumerator IncreaseEnergyOverTime(int value)
    {
        isRechargingEnergy = true;
        while (_currentEnergy < _energy.value)
        {
            yield return new WaitForSeconds(rechargingEnergySpeed);
            Debug.Log("Charging");
            InstantIncreaseEnergy(value);
        }
        isRechargingEnergy = false;
    }

    public void InstantIncreaseEnergy(int value)
    {
        _currentEnergy += value;
        _energySlider.value = _currentEnergy;
    }

    public void InstantDecreaseEnergy(int value)
    {
        _currentEnergy -= value;
        _energySlider.value = _currentEnergy;
    }
}
