using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform _mouseWorldPosition;

    void Update()
    {
        Quaternion lookRotation = Quaternion.LookRotation(_mouseWorldPosition.position - transform.position);
        lookRotation.Set(transform.rotation.eulerAngles.x, lookRotation.y, transform.rotation.eulerAngles.z, lookRotation.w);
        transform.localRotation = lookRotation;
    }
}
