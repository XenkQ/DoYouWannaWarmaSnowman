using UnityEngine;

public class EnergyPowerUp : MonoBehaviour
{
    [SerializeField] private int energyIncreaser = 30;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().GetComponent<Battery>().InstantIncreaseEnergy(energyIncreaser);
            Destroy(gameObject);
        }
    }
}
