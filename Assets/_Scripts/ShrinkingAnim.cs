using System.Collections;
using UnityEngine;

public class ShrinkingAnim : MonoBehaviour
{
    [SerializeField] private float maxScaleDecreasedBy;
    private Vector3 minScale;
    private Vector3 startScale;
    [SerializeField] private float shrinkingSpeed = 0.01f;
    [SerializeField] private float shrinkingStrengthOverTime = 0.01f;
    private bool isShrinking = false;

    private void Start()
    {
        SetStartScale();
        SetMinScale();
    }

    private void SetStartScale()
    {
        startScale = transform.localScale;
    }

    private void SetMinScale()
    {
        minScale = new Vector3(
                startScale.x - maxScaleDecreasedBy,
                startScale.y - maxScaleDecreasedBy,
                startScale.z - maxScaleDecreasedBy
            );
    }

    private void FixedUpdate()
    {
        if (isShrinking == false)
        {
            StartCoroutine(ShrinkProcess());
        }
    }

    private IEnumerator ShrinkProcess()
    {
        isShrinking = true;
        while (transform.localScale.x >= minScale.x && transform.localScale.y >= minScale.y && transform.localScale.z >= minScale.z)
        {
            transform.localScale = new Vector3(transform.localScale.x - shrinkingStrengthOverTime,
                transform.localScale.y - shrinkingStrengthOverTime,
                transform.localScale.z - shrinkingStrengthOverTime);
            yield return new WaitForSeconds(shrinkingSpeed);
        }

        while (transform.localScale.x < startScale.x && transform.localScale.y < startScale.y && transform.localScale.z < startScale.z)
        {
            transform.localScale = new Vector3(transform.localScale.x + shrinkingStrengthOverTime,
                transform.localScale.y + shrinkingStrengthOverTime,
                transform.localScale.z + shrinkingStrengthOverTime);
            yield return new WaitForSeconds(shrinkingSpeed);
        }
        isShrinking = false;
    }
}
