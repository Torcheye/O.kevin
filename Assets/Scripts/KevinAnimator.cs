using UnityEngine;

public class KevinAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void BootUp()
    {
        _animator.SetTrigger("BootUp");
    }

    public void SetAttributingState(bool attributing)
    {
        _animator.SetBool("Attributing", attributing);
    }
}
