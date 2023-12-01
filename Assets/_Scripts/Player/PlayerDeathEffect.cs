using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerDeathEffect : MonoBehaviour
{
    [SerializeField] private DeadUI deadUI;
    [SerializeField] private HeatGun heatGun;
    private Rigidbody rb;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public void AfterDeathAnimActions()
    {
        deadUI.Invoke("EnableDeadUI", 1f);
    }

    public void ActiveteDeathEffect()
    {
        FreezePlayerAnim();
        heatGun.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void FreezePlayerAnim()
    {
        animator.SetTrigger("Die");
    }
}
