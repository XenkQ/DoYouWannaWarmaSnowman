using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHeatPoints = 100;
    private int currentHeat = 0;
    private EnemySpawning enemySpawning;
    private PlayerStats _playerStats;
    public int pooledIndex;
    private Animator animator;
    private bool isDieing = false;

    private void Awake()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        enemySpawning = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawning>();
        animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        isDieing = false;
        currentHeat = 0;
    }

    public void FixedUpdate()
    {
        if(isDieing == false)
        {
            if (currentHeat >= maxHeatPoints)
            {
                isDieing = true;
                StartDieingEvent();
            }
        }
    }

    public void ReturnEnemyToPull()
    {
        enemySpawning.currentUnusedIndexes.Add(pooledIndex);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (isDieing == false && currentHeat < maxHeatPoints && other.transform.CompareTag("HeatGun"))
        {
            currentHeat += _playerStats.damage.GetValue();
        }
    }

    public void StartDieingEvent()
    {
        BroadcastMessage("DieEvent");
    }

    public void SetRandomYRotationOnParent()
    {
        transform.parent.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
    }

    public void ResetRotationOnParent()
    {
        transform.parent.eulerAngles = Vector3.zero;
    }

    public void ApplyRootMotion()
    {
        animator.applyRootMotion = true;
    }

    public void DisapplyRootMotion()
    {
        animator.applyRootMotion = false;
    }
}
