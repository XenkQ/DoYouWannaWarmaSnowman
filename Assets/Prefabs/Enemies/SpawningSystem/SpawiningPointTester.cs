using UnityEngine;

public class SpawiningPointTester : MonoBehaviour
{
    [SerializeField] private float Yoffset;
    public bool CanSpawn = true;

    private void OnEnable()
    {
        transform.position = new Vector3(transform.position.x, Yoffset, transform.position.z);
        Destroy(gameObject, 0.12f);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Obstacle"))
        {
            CanSpawn = false;
        }
    }
}
