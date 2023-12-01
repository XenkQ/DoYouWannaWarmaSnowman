using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour {
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    private Stat _health;
    
    //private void Awake() {
    //    _health = FindObjectOfType<PlayerStats>().health;
    //}

    //void Start() {
    //    UpdateSlider();
    //    _health.onValueChange += UpdateSlider;
    //}

    //private void UpdateSlider() {
    //    int healthDifference = (int)(_health.value - _healthSlider.maxValue);
    //    _healthSlider.maxValue += healthDifference;
    //}

    public void OnHealthSliderValueChange()
    {
        if(_healthSlider.value > 0)
        {
            healthText.text = _healthSlider.value.ToString();
        }
        else
        {
            _healthSlider.value = 1;
            healthText.text = "1";
        }

    }
}
