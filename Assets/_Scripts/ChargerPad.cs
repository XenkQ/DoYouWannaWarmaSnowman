using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerPad : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player")) return;
        Transform player = other.transform;
        player.GetComponentInChildren<HeatGun>().enabled = false;
        StartCoroutine(player.GetComponentInChildren<PlayerHealth>().Heal(10));
        StartCoroutine(player.GetComponentInChildren<Battery>().IncreaseEnergyOverTime(10));
    }
    private void OnTriggerExit(Collider other) {
        if(!other.CompareTag("Player")) return;
        other.transform.GetComponentInChildren<HeatGun>().enabled = true;
        StopAllCoroutines();

        // StopCoroutine
    }
}
