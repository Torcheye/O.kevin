using DG.Tweening;
using UnityEngine;

public class UISettings : MonoBehaviour
{
    public const float PointSystemToggleTransitionTime = .4f;
    public const Ease PointSystemToggleTransitionEase = Ease.OutExpo;
    public const float SceneSwipeTransitionTime = .4f;
    public const Ease SceneSwipeTransitionEase = Ease.OutExpo;
    public const float InventoryToggleTransitionTime = .4f;
    public const Ease InventoryToggleTransitionEase = Ease.OutExpo;
    public static float ObjectsSwipeMoveScalerToUI;

    private void Awake()
    {
        var cam = Camera.main;
        if (cam != null)
        {
            var deltaPos = cam.ScreenToWorldPoint(new Vector2(Screen.width / 2f, 0)) -
                           cam.ScreenToWorldPoint(new Vector2(-Screen.width / 2f, 0));
            ObjectsSwipeMoveScalerToUI = deltaPos.x / Screen.width;
        }
    }
}