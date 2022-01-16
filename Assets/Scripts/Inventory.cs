using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject gridLayout;

    private int _currentSlot;
    private List<Image> _slots;
    private Button[] _buttons;
    private List<Egg> _eggs;

    private void Start()
    {
        _currentSlot = -1;
        _slots = gridLayout.GetComponentsInChildren<Image>().ToList();
        _buttons = gridLayout.GetComponentsInChildren<Button>();
        _eggs = new List<Egg>();

        // foreach (var button in _buttons)
        // {
        //     button.onClick.AddListener(delegate {  });
        // }
    }

    private void EggOnClick()
    {
        
    }

    private void ChangeSprite(IEnumerable<string> keyList, Image s)
    {
        Addressables.LoadAssetsAsync<Sprite>(keyList, addressable => { s.sprite = addressable; },
            Addressables.MergeMode.Intersection, true);
    }

    private void UpdateInventoryDisplay()
    {
        for (var i = 0; i < _eggs.Count; i++)
        {
            ChangeSprite(_eggs[i].GetKeyList(), _slots[i]);
            _slots[i].color = Color.white;
        }

        for (var i = _eggs.Count; i < _slots.Count; i++)
        {
            _slots[i].color = new Color(0, 0, 0, 0);
        }
    }

    public void AddEgg(Egg e)
    {
        _eggs.Add(e);
        UpdateInventoryDisplay();
        SaveSystem.Instance.Save(_eggs);
    }

    public void RemoveEgg(int spot)
    {
        if (_eggs.Count <= 0)
        {
            Debug.LogError("Trying to remove egg but nothing left!");
            return;
        }

        _eggs.RemoveAt(spot);
        UpdateInventoryDisplay();
        SaveSystem.Instance.Save(_eggs);
    }
}