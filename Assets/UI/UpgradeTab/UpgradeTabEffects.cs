using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTabEffects : MonoBehaviour
{
    [SerializeField] private GameObject healthOverlay;
    [SerializeField] private GameObject damageOverlay;
    [SerializeField] private GameObject speedOverlay;
    [SerializeField] private GameObject summonEntityOverlay;
    [SerializeField] private Upgrader upgrader;

    private void FixedUpdate()
    {
        ManageOverlays();
    }

    private void ManageOverlays()
    {
        ManageHealthOverlay();
        ManageDamageOverlay();
        ManageSpeedOverlay();
    }

    private void ManageHealthOverlay()
    {
        if (upgrader.CanUpgradeHealthStat())
        {
            healthOverlay.SetActive(false);
        }
        else
        {
            healthOverlay.SetActive(true);
        }
    }

    private void ManageDamageOverlay()
    {
        if (upgrader.CanUpgradeDamageStat())
        {
            damageOverlay.SetActive(false);
        }
        else
        {
            damageOverlay.SetActive(true);
        }
    }

    private void ManageSpeedOverlay()
    {
        if (upgrader.CanUpgradeSpeedStat())
        {
            speedOverlay.SetActive(false);
        }
        else
        {
            speedOverlay.SetActive(true);
        }
    }
}
