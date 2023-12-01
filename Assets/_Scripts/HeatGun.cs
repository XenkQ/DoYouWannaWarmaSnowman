using UnityEngine;
using System.Collections;

public class HeatGun : MonoBehaviour
{
    private ParticleSystem _heatGunPS;
    private PlayerInputActions _playerInputActions;
    private Battery _battery;
    [SerializeField] private float shootDelay = 0.2f;

    private void Awake()
    {
        _battery = GetComponentInParent<Battery>();
        _heatGunPS = GetComponent<ParticleSystem>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    void Start()
    {
        _battery.onDischarge += () =>
        {
            _heatGunPS.Stop();
            StopAllCoroutines();
        };
        _playerInputActions.Player.Attack.started += context =>
        {
            StartCoroutine(ShootAfterStartDelay());
        };
        _playerInputActions.Player.Attack.canceled += context =>
        {
            _heatGunPS.Stop();
            StopAllCoroutines();
        };
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Attack.Disable();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Attack.Enable();
    }

    private IEnumerator ShootAfterStartDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        _heatGunPS.Play();
        StartCoroutine(_battery.DecreaseEnergyOverTime(10));
    }
}
