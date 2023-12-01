using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _player;
    private void Awake() {
        _player = FindObjectOfType<PlayerMovement>().transform;
    }
    void OnEnable() {
        _navMeshAgent.SetDestination(_player.position);
        _navMeshAgent.Move(Vector3.forward);
    }
    void Update()
    {
        if (_navMeshAgent.destination != _player.position) {
            _navMeshAgent.SetDestination(_player.position);
        }
    }
}
