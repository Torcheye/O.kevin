using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; set; }
    [SerializeField] private RectTransform screen;
    [SerializeField] private Transform gameObjects;
    [SerializeField] private float swipeSpeedThreshold;
    private float _xStartUI, _xStartObjects;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        Touch.onFingerDown += OnFingerDown;
        Touch.onFingerUp += OnFingerUp;
        Touch.onFingerMove += OnFingerMove;
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        Touch.onFingerDown -= OnFingerDown;
        Touch.onFingerUp -= OnFingerUp;
        Touch.onFingerMove -= OnFingerMove;
    }

    private void OnFingerDown(Finger finger)
    {
        _xStartUI = screen.anchoredPosition.x;
        _xStartObjects = gameObjects.position.x;
    }

    private void OnFingerUp(Finger finger)
    {
        var touch = finger.currentTouch;
        if (touch.delta.magnitude >= swipeSpeedThreshold)
        {
            GameManager.Instance.scene += touch.screenPosition.x > touch.startScreenPosition.x ? 1 : -1;
        }

        var xDiff = GameManager.Instance.scene * Screen.width;
        screen.DOAnchorPosX(xDiff, UISettings.SceneSwipeTransitionTime).SetEase(UISettings.SceneSwipeTransitionEase);
        gameObjects.DOMoveX(xDiff * UISettings.objectsSwipeMoveScalerToUI, UISettings.SceneSwipeTransitionTime)
            .SetEase(UISettings.SceneSwipeTransitionEase);
    }

    private void OnFingerMove(Finger finger)
    {
        var touch = finger.currentTouch;
        var xDiffUI = touch.screenPosition.x - touch.startScreenPosition.x;
        screen.anchoredPosition = new Vector2(_xStartUI + xDiffUI, 0);
        gameObjects.position = new Vector3(_xStartObjects + xDiffUI * UISettings.objectsSwipeMoveScalerToUI, 0, 0);
    }
}