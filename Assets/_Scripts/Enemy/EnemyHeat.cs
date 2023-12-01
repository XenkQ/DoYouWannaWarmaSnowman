using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeat : MonoBehaviour {
    [SerializeField] private int maxHeatPoints = 100;
    [SerializeField] private int currentHeat = 0;
    private PlayerStats _playerStats;
    private void Awake() {
        _playerStats = FindObjectOfType<PlayerStats>();
    }
    private void OnParticleCollision(GameObject other) {
        if (other.transform.CompareTag("HeatGun")) {
            currentHeat += _playerStats.damage.GetValue();
        }
        if (currentHeat >= maxHeatPoints) {
            _playerStats.score.IncreaseValue(10);
            gameObject.SetActive(false);
        }
    }
}
