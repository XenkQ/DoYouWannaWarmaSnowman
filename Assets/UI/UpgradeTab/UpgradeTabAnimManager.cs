using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UpgradeTabAnimManager : MonoBehaviour
{
    private Animator animator;
    private bool isClosed = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayCloseOpenAnim()
    {
        if (isClosed)
        {
            PlayOpenAnim();
        }
        else
        {
            PlayCloseAnim();
        }
    }

    private void PlayCloseAnim()
    {
        animator.SetTrigger("Close");
        isClosed = true;
    }

    private void PlayOpenAnim()
    {
        animator.SetTrigger("Open");
        isClosed = false;
    }
}
