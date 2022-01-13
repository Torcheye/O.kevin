using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject GridLayout;

    private int currentSlot;
    private Image[] slots;

    private void Start()
    {
        currentSlot = -1;
        slots = GridLayout.GetComponentsInChildren<Image>();
        AddEgg("Anger", 3);
        AddEgg("Sadness", 5);
        AddEgg("Fear", 1);
    }

    private void ChangeSprite(IEnumerable<string> keyList, Image s)
    {
        Addressables.LoadAssetsAsync<Sprite>(keyList, addressable =>
            {
                s.sprite = addressable;
            },
            Addressables.MergeMode.Intersection, true);
    }

    public void AddEgg(string n, int level)
    {
        var keyList = new List<string> {n, level.ToString()};
        ChangeSprite(keyList, slots[++currentSlot].GetComponentInChildren<Image>());
    }
}