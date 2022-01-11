using DG.Tweening;
using UnityEngine;

public class KevinController : MonoBehaviour
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

    public void TogglePosition(bool down, float timing, Ease ease)
    {
        var tween = transform.DOMoveY(down ? 0 : 2.1f, timing).SetEase(ease);
    }
}
