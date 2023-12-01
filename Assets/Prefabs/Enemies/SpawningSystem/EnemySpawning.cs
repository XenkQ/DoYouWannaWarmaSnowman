using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private GameObject plane;
    [SerializeField] private SpawiningPointTester spawiningPointTester;
    private bool isChecking = false;

    [Header("Delays")]
    [SerializeField] private float startEnemySpawningDelay = 2f;
    [SerializeField] private float currentEnemySpawningDelay;
    [SerializeField] private float delayDecreaser = 0.02f;

    [Header("Enemies perfabs")]
    [SerializeField] private GameObject spiderSnowman;
    [SerializeField] private GameObject shooterSnowman;


    [Header("Spawning Ammount")]
    const int MAX_SPIDER_SNOWMANS_NUMBER = 8; //8
    const int MAX_SHOOTER_SNOWMANS_NUMBER = 30; //30
    private Enemy[] pooledEnemies = new Enemy[MAX_SPIDER_SNOWMANS_NUMBER + MAX_SHOOTER_SNOWMANS_NUMBER];
    public List<int> currentUnusedIndexes = new List<int>();

    private void Awake()
    {
        poolEnemies();
        FillUnusedIndexesOnStart();
    }

    private void Start()
    {
        currentEnemySpawningDelay = 0;
    }

    private void Update()
    {
        if (currentEnemySpawningDelay <= 0 && currentUnusedIndexes.Count > 0)
        {
            currentEnemySpawningDelay = 0;
            if (isChecking == false)
            {
                StartCoroutine(ActivateRandomEnemyProcess());
            }
        }
        else
        {
            currentEnemySpawningDelay -= Time.deltaTime;
        }
    }

    private void poolEnemies()
    {
        PoolSpiderEnemies();
        PoolShooterEnemies();
    }

    private void PoolSpiderEnemies()
    {
        for (int i = 0; i < MAX_SPIDER_SNOWMANS_NUMBER; i++)
        {
            AddEnemyToPool(spiderSnowman, i);
        }
    }

    private void PoolShooterEnemies()
    {
        for (int i = MAX_SPIDER_SNOWMANS_NUMBER; i < MAX_SPIDER_SNOWMANS_NUMBER + MAX_SHOOTER_SNOWMANS_NUMBER; i++)
        {
            AddEnemyToPool(shooterSnowman, i);
        }
    }

    private void AddEnemyToPool(GameObject enemyPrefab, int index)
    {
        GameObject enemyParent = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity, transform);
        Enemy enemy = enemyParent.transform.GetChild(0).GetComponent<Enemy>();
        enemy.pooledIndex = index;
        enemy.gameObject.SetActive(false);
        pooledEnemies[index] = enemy;
    }

    private void FillUnusedIndexesOnStart()
    {
        for (int i = 0; i < pooledEnemies.Length; i++)
        {
            currentUnusedIndexes.Add(i);
        }
    }

    //private Vector3 GetRandomSpawningPositionOutOfCircle()
    //{
    //    var vector2 = Random.insideUnitCircle.normalized * spawningCircleRadius;
    //    return new Vector3(vector2.x, 0, vector2.y);
    //}

    private Vector3 GetRandomSpawningPositionOnPlane()
    {
        List<Vector3> VerticeList = new List<Vector3>(plane.GetComponent<MeshFilter>().sharedMesh.vertices);
        Vector3 leftTop = plane.transform.TransformPoint(VerticeList[0]);
        Vector3 rightTop = plane.transform.TransformPoint(VerticeList[10]);
        Vector3 leftBottom = plane.transform.TransformPoint(VerticeList[110]);
        Vector3 XAxis = rightTop - leftTop;
        Vector3 ZAxis = leftBottom - leftTop;

        Vector3 RndPointonPlane = leftTop + XAxis * Random.value + ZAxis * Random.value;
        return RndPointonPlane;
    }

    private IEnumerator ActivateRandomEnemyProcess()
    {
        isChecking = true;
        bool canSpawn = false;
        Vector3 randomPos;
        do
        {
            randomPos = GetRandomSpawningPositionOnPlane();
            SpawiningPointTester spt = Instantiate(spawiningPointTester, randomPos, Quaternion.identity, transform);
            yield return new WaitForSeconds(0.1f);
            canSpawn = spt.CanSpawn;
        } while (canSpawn == false);

        ActivateRandomUnusedEnemyAtPosition(randomPos);
        ResetDelay();
        DecreaseMaxDelay();
        isChecking = false;
    }

    private void ActivateRandomUnusedEnemyAtPosition(Vector3 randomPos)
    {
        Enemy enemy = pooledEnemies[GetRandomUnusedPooledEnemiesIndex()];
        enemy.transform.parent.position = randomPos;
        enemy.gameObject.SetActive(true);
    }

    private void ResetDelay()
    {
        currentEnemySpawningDelay = startEnemySpawningDelay;
    }

    private void DecreaseMaxDelay()
    {
        if (startEnemySpawningDelay > 0)
        {
            startEnemySpawningDelay -= delayDecreaser;
        }
    }

    private int GetRandomUnusedPooledEnemiesIndex()
    {
        int index = Random.Range(0, currentUnusedIndexes.Count);
        int number = currentUnusedIndexes[index];
        currentUnusedIndexes.RemoveAt(index);
        return number;
    }
}
