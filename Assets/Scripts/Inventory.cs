using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject gridLayout;
    private Button[] _buttons;
    private List<Egg> _eggs;
    private Image[] _slots;

    private void Start()
    {
        _slots = gridLayout.GetComponentsInChildren<Image>();
        _buttons = gridLayout.GetComponentsInChildren<Button>();

        _eggs = SaveSystem.Instance.Load();
        if (_eggs != null)
            UpdateInventoryDisplay();
        else
            _eggs = new List<Egg>();
    }

    private void EggOnClick(int id)
    {
        GameManager.Instance.IncubateEgg(_eggs[id]);
        RemoveEgg(id);
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
            var i1 = i;
            ChangeSprite(_eggs[i].GetKeyList(), _slots[i]);
            _slots[i].color = Color.white;

            _buttons[i].onClick.AddListener(delegate { EggOnClick(i1); });
        }

        for (var i = _eggs.Count; i < _slots.Length; i++)
        {
            _slots[i].color = new Color(0, 0, 0, 0);
            _buttons[i].onClick.RemoveAllListeners();
        }
    }

    public void AddEgg(Egg e)
    {
        _eggs.Add(e);
        UpdateInventoryDisplay();
        SaveSystem.Instance.Save(_eggs);
    }

    private void RemoveEgg(int spot)
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