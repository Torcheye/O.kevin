using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject gridLayout;

    private int _currentSlot;
    private List<Image> _slots;

    private void Start()
    {
        _currentSlot = -1;
        _slots = gridLayout.GetComponentsInChildren<Image>().ToList();
    }

    private void ChangeSprite(IEnumerable<string> keyList, Image s)
    {
        Addressables.LoadAssetsAsync<Sprite>(keyList, addressable => { s.sprite = addressable; },
            Addressables.MergeMode.Intersection, true);
    }

    public void AddEgg(string n, int level)
    {
        var keyList = new List<string> {n, level.ToString()};
        _slots[++_currentSlot].color = Color.white;
        ChangeSprite(keyList, _slots[_currentSlot]);
    }

    public void RemoveEgg(int spot)
    {
        if (_currentSlot == -1)
        {
            Debug.LogError("Trying to remove egg but nothing left!");
            return;
        }
        var temp = new List<Image>(_slots);
        temp.RemoveAt(spot);

        for (var i = 0; i < temp.Count; i++)
        {
            _slots[i].sprite = temp[i].sprite;
        }

        _slots[_currentSlot--].color = new Color(0, 0, 0, 0);
    }
}