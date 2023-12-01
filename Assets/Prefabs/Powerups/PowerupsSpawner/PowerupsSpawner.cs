using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsSpawner : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private GameObject plane;
    [SerializeField] private SpawiningPointTester spawiningPointTester;
    private bool isChecking = false;

    [Header("Delays")]
    [SerializeField] private float startPowerupSpawningDelay = 2f;
    [SerializeField] private float currentPowerupSpawningDelay;

    [Header("Powerups prefabs")]
    [SerializeField] private GameObject[] powerups;

    private void Start()
    {
        currentPowerupSpawningDelay = startPowerupSpawningDelay;
    }

    private void Update()
    {
        if (currentPowerupSpawningDelay <= 0)
        {
            currentPowerupSpawningDelay = 0;
            if (isChecking == false)
            {
                StartCoroutine(ActivateRandomPowerupProcess());
            }
        }
        else
        {
            currentPowerupSpawningDelay -= Time.deltaTime;
        }
    }


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

    private IEnumerator ActivateRandomPowerupProcess()
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

        SpawnRandomUnusedEnemyAtPosition(randomPos);
        ResetDelay();
        isChecking = false;
    }

    private void SpawnRandomUnusedEnemyAtPosition(Vector3 randomPos)
    {
        GameObject powerup = GetRandomPowerup();
        Instantiate(powerup, randomPos, Quaternion.identity, transform);
    }

    private GameObject GetRandomPowerup()
    {
        return powerups[Random.Range(0, powerups.Length)];
    }


    private void ResetDelay()
    {
        currentPowerupSpawningDelay = startPowerupSpawningDelay;
    }
}
