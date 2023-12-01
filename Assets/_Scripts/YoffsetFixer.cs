using UnityEngine;

public class YoffsetFixer : MonoBehaviour
{
    [SerializeField] private float YOffset;

    private void OnEnable()
    {
        transform.position = new Vector3(transform.position.x, YOffset, transform.position.z);
    }
}
