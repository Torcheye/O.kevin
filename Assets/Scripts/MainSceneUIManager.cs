using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUIManager : MonoBehaviour
{
    [SerializeField] private Button shopButton, inventoryButton;
    [SerializeField] private GameObject inventory;

    void Start()
    {
        inventoryButton.onClick.AddListener(ToggleInventory);
        ToggleInventory();
    }

    private void ToggleInventory()
    {
        var isActive = inventory.activeSelf;
        const Ease ease = UISettings.InventoryToggleTransitionEase;
        const float timing = UISettings.InventoryToggleTransitionTime;
        
        var canvasGroup = inventory.GetComponent<CanvasGroup>();

        if (isActive)
        {
            canvasGroup.DOFade(0, timing).SetEase(ease);
            inventory.SetActive(false);
        }
        else
        {
            inventory.SetActive(true);
            canvasGroup.DOFade(1, timing).SetEase(ease);
        }
    }
}