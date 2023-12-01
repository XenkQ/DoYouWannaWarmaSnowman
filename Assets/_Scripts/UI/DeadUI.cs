using UnityEngine;
using TMPro;

public class DeadUI : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject deathPlace;
    [SerializeField] private GameObject levelPlace;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private GameObject cinemaCamera;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private Vector3 deathPlaceCameraPosition;

    public void EnableDeadUI()
    {
        EnableDeathPlaceWithUI();
        SetCameraToDeathPlace();
        SetDisplayedScoreInDeathPlace();
        DisableObjectsFromNonDeathPlace();
    }

    private void SetDisplayedScoreInDeathPlace()
    {
        score.text = scoreUI.GetScoreStat().value.ToString();
    }

    private void EnableDeathPlaceWithUI()
    {
        content.SetActive(true);
        deathPlace.SetActive(true);
    }

    private void SetCameraToDeathPlace()
    {
        mainCamera.transform.position = deathPlaceCameraPosition;
        mainCamera.transform.eulerAngles = Vector3.zero;
    }

    private void DisableObjectsFromNonDeathPlace()
    {
        cinemaCamera.SetActive(false);
        enemySpawner.SetActive(false);
        levelPlace.SetActive(false);
        gameUI.SetActive(false);
    }
}
