using UnityEngine;
using TMPro;

public class Upgrader : MonoBehaviour
{
    [Header("Upgredes Costs")]
    [SerializeField] private int healthStatUpdateStartCost = 100;
    private int currentHealthStatUpdateCost;
    [SerializeField] private int damageStatUpdateStartCost = 120;
    private int currentDamageStatUpdateCost;
    [SerializeField] private int speedStatUpdateStartCost = 80;
    private int currentSpeedStatUpdateCost;

    [Header("Upgrades Values")]
    [SerializeField] private float healthRegenerationSpeedIncreaser = 0.8f;
    [SerializeField] private int damageIncreaser = 5;
    [SerializeField] private int speedIncreaser = 1;

    [Header("Cost Texts")]
    [SerializeField] private TextMeshProUGUI healthCostText;
    [SerializeField] private TextMeshProUGUI damageCostText;
    [SerializeField] private TextMeshProUGUI speedCostText;

    [Header("Other Scripts")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerHealth playerHealth;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void Start()
    {
        SetStartUpdateCosts();
        SetCostTextsOnStart();
    }

    private void SetStartUpdateCosts()
    {
        currentHealthStatUpdateCost = healthStatUpdateStartCost;
        currentDamageStatUpdateCost = damageStatUpdateStartCost;
        currentSpeedStatUpdateCost = speedStatUpdateStartCost;
    }

    private void SetCostTextsOnStart()
    {
        healthCostText.text = "Cost: <color=#F77F00>" + healthStatUpdateStartCost.ToString() + "</color>";
        damageCostText.text = "Cost: <color=#F77F00>" + damageStatUpdateStartCost.ToString() + "</color>";
        speedCostText.text = "Cost: <color=#F77F00>" + speedStatUpdateStartCost.ToString() + "</color>";
    }

    public void UpdateHealthStat()
    {
        if (CanUpgradeHealthStat())
        {
            playerStats.score.value -= currentHealthStatUpdateCost;
            playerHealth.healthRegenerationSpeed -= healthRegenerationSpeedIncreaser;
        }
    }

    public bool CanUpgradeHealthStat()
    {
        return playerHealth.healthRegenerationSpeed > playerHealth.maxHealthRegenerationSpeed && playerStats.CanUpgrade(currentHealthStatUpdateCost);
    }

    public void UpdateDamageStat()
    {
        if (CanUpgradeDamageStat())
        {
            playerStats.score.value -= currentDamageStatUpdateCost;
            playerStats.damage.value += damageIncreaser;
        }
    }

    public bool CanUpgradeDamageStat()
    {
        return playerStats.damage.value <= playerStats.MaxDamageValue && playerStats.CanUpgrade(currentDamageStatUpdateCost);
    }

    public void UpdateSpeedStat()
    {
        if (CanUpgradeSpeedStat())
        {
            Debug.Log("SPEED");
            playerStats.score.value -= currentSpeedStatUpdateCost;
            playerStats.speed.IncreaseValue(speedIncreaser);
        }
    }

    public bool CanUpgradeSpeedStat()
    {
        return playerStats.speed.value <= playerStats.MaxSpeedValue && playerStats.CanUpgrade(currentSpeedStatUpdateCost);
    }
}
