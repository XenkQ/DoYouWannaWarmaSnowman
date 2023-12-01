using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy), typeof(Rigidbody), typeof(Animator))]
public class SpiderSnowman : MonoBehaviour
{
    [Header("Die")]
    [SerializeField] private int pointsForDefeat = 10;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed = 4f;
    private Rigidbody rb;

    [Header("Attack")]
    [SerializeField] private int damage = 20;
    [SerializeField] private float startAttackDelay = 2f;
    private float currentAttackDelay;
    [SerializeField] [Range(0, 10f)] private float rangeForAttack = 4f;
    [SerializeField] [Range(-10f, 10f)] private float centreYOffset = 3f;
    private GameObject target;
    private bool isAttacking = false;

    [Header("Behaviour")]
    private bool isDieing = false;

    [Header("Shrinking")]
    [SerializeField] private float shrinkingSpeed = 0.01f;
    [SerializeField] private float shrinkingStrengthOverTime = 0.01f;

    [Header("Animations")]
    private Animator animator;

    [Header("Other Components")]
    private Enemy enemy;
    private PlayerStats _playerStats;

    private void Awake()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.one;
        transform.localPosition = new Vector3(0, 0, 0);
        currentAttackDelay = startAttackDelay;
    }

    private void OnDisable()
    {
        enemy.DisapplyRootMotion();
        GetComponent<Enemy>().enabled = true;
        animator.enabled = true;
        isDieing = false;
    }

    private void FixedUpdate()
    {
        if (isDieing == false)
        {
            RotateAtDirection(target.transform.position);
            if (!isAttacking) { Move(); }
            else { rb.velocity = Vector3.zero; }
        }
        else
        {
            rb.velocity = Vector3.zero;
            animator.enabled = false;
        }
    }

    private void Update()
    {
        if (CanStartAttackProcess())
        {
            AttackProcess();
        }
        else
        {
            currentAttackDelay -= Time.deltaTime;
        }
    }

    private bool CanStartAttackProcess()
    {
        return isDieing == false && currentAttackDelay <= 0;
    }

    private void RotateAtDirection(Vector3 direciton)
    {
        Vector3 rotateDirection = direciton - transform.position;
        rotateDirection.y = 0;
        var rotation = Quaternion.LookRotation(rotateDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    private void Move()
    {
        rb.velocity = (transform.forward * Time.deltaTime).normalized * speed;
    }

    private void AttackProcess()
    {
        if (CanAttack())
        {
            StartAttacking();
        }
    }

    private bool CanAttack()
    {
        return isAttacking == false && targetInAttackRange();
    }

    private bool targetInAttackRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) <= rangeForAttack;
    }

    private void StartAttacking()
    {
        animator.SetBool("IsAttacking", true);
        isAttacking = true;
    }

    public void StopAttacking()
    {
        animator.SetBool("IsAttacking", false);
        isAttacking = false;
    }

    public void Attack()
    {
        if(targetInAttackRange())
        {
            target.GetComponent<PlayerHealth>().GetDamage(damage);
            currentAttackDelay = startAttackDelay;
        }
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
        gameObject.SetActive(false);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(
            transform.position.x,
            transform.position.y + centreYOffset,
            transform.position.z),
            rangeForAttack
        );
    }
}
