using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerDeathEffect playerDeathEffect;
    private int healingValue = 10;
    private Stat _health;
    public float healthRegenerationSpeed = 2.6f;
    public float maxHealthRegenerationSpeed { get { return 1.75f; } }
    private bool isHealing = false;

    private void Start()
    {
        _health = GetComponent<PlayerStats>().health;
        currentHealth = _health.value;
    }

    private void FixedUpdate()
    {
        if(isHealing == false)
        {
            StartCoroutine(Heal(healingValue));
        }
    }

    public void GetDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            _healthSlider.value = currentHealth;
        }
        else
        {
            playerDeathEffect.ActiveteDeathEffect();
            _healthSlider.gameObject.SetActive(false);
        }
    }

    public IEnumerator Heal(int hp)
    {
        isHealing = true;
        Debug.Log("Heal");
        while (_healthSlider.value < _health.value)
        {
            yield return new WaitForSeconds(healthRegenerationSpeed);
            currentHealth += hp;
            _healthSlider.value = currentHealth;
        }
        isHealing = false;
    }
}
