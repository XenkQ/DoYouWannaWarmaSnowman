using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy), typeof(Rigidbody), typeof(Animator))]
public class ShootingSnowman : MonoBehaviour
{
    [Header("Die")]
    [SerializeField] private int pointsForDefeat = 25;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed = 4f;
    private Rigidbody rb;

    [Header("Behavior")]
    [SerializeField] private float runAwayFromTargetDistance = 10f;
    [SerializeField] private float canShootingDistance = 18f;
    [SerializeField] private float startDelayBetweenShoots = 1.3f;
    private bool isDieing = false;

    [Header("Shrinking")]
    [SerializeField] private float shrinkingSpeed = 0.01f;
    [SerializeField] private float shrinkingStrengthOverTime = 0.01f;

    [Header("Shooting")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject carrot;
    private float currentDelayBetweenShoots;
    private Transform SpawnedObjectsPoint;
    private Transform target;

    [Header("Animations")]
    private Animator animator;

    [Header("Other Components")]
    private Enemy enemy;
    private PlayerStats _playerStats;

    private void Awake()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        SpawnedObjectsPoint = GameObject.FindGameObjectWithTag("SpawnedObjectsPoint").transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.one;
        currentDelayBetweenShoots = startDelayBetweenShoots;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void OnDisable()
    {
        enemy.DisapplyRootMotion();
    }

    private void FixedUpdate()
    {
        if (isDieing == false)
        {
            RotateAtDirection(target.position);
            MoveEnemy();
        }
    }

    private void Update()
    {
        if (CanShoot())
        {
            StartShootingAnim();
            currentDelayBetweenShoots = startDelayBetweenShoots;
        }
        else
        {
            currentDelayBetweenShoots -= Time.deltaTime;
        }
    }

    private bool CanShoot()
    {
        return isDieing == false && currentDelayBetweenShoots <= 0 && Vector3.Distance(target.transform.position, transform.position) <= canShootingDistance;
    }

    private void RotateAtDirection(Vector3 direciton)
    {
        Vector3 rotateDirection = direciton - transform.position;
        rotateDirection.y = 0;
        var rotation = Quaternion.LookRotation(rotateDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    private void MoveEnemy()
    {
        if (Vector3.Distance(target.transform.position, transform.position) < runAwayFromTargetDistance)
        {
            rb.velocity = (-transform.forward * Time.deltaTime).normalized * speed;
        }
        else if (Vector3.Distance(target.transform.position, transform.position) > canShootingDistance)
        {
            rb.velocity = (transform.forward * Time.deltaTime).normalized * speed;
        }
    }

    private void StartShootingAnim()
    {
        animator.SetTrigger("StartShooting");
    }

    public void ShootProjectile()
    {
        Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation, SpawnedObjectsPoint);
        carrot.SetActive(false);
    }

    public void ReloadProjectile()
    {
        carrot.SetActive(true);
    }

    private void DieEvent()
    {
        if (isDieing == false)
        {
            _playerStats.score.IncreaseValue(pointsForDefeat);
            isDieing = true;
            enemy.enabled = false;
            StartCoroutine(ShrinkProcess());
        }
    }

    private IEnumerator ShrinkProcess()
    {
        while (transform.localScale.x > 0 && transform.localScale.y > 0 && transform.localScale.z > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x - shrinkingStrengthOverTime,
                transform.localScale.y - shrinkingStrengthOverTime,
                transform.localScale.z - shrinkingStrengthOverTime);
            yield return new WaitForSeconds(shrinkingSpeed);
        }

        enemy.ReturnEnemyToPull();
        isDieing = false;
        gameObject.SetActive(false);
    }
}
