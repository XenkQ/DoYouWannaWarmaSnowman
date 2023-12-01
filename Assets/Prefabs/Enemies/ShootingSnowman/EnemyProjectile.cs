using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private int damage = 4;
    [SerializeField] private float projectileSpeed = 2f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float destroyAfter = 4f;

    private void OnEnable()
    {
        rb.AddForce((transform.forward * Time.deltaTime).normalized * projectileSpeed);
        Destroy(gameObject, destroyAfter);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().GetDamage(damage);
            Destroy(gameObject);
        }

        if (collision.transform.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
