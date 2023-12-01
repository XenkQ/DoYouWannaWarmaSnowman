using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public Stat health;
    [SerializeField] public Stat energy;
    [SerializeField] public Stat damage;
    [SerializeField] public Stat speed;
    [SerializeField] public Stat score;

    [Header("Max Values")]
    [SerializeField] private int maxDamageValue = 40;
    public int MaxDamageValue { get { return maxDamageValue; } }
    [SerializeField] private int maxSpeedValue = 12;
    public int MaxSpeedValue { get { return maxSpeedValue; } }

    public bool CanUpgrade(int cost)
    {
        return score.value >= cost;
    }
}
