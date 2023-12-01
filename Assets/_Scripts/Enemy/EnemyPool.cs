using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyPool : MonoBehaviour {
    [SerializeField] private GameObject _enemyPrefab;
    private PlayerInputActions playerInputActions;
    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    void Update()
    {
        if (playerInputActions.Player.Spawn.triggered) {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy() {
        Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
    }

}
