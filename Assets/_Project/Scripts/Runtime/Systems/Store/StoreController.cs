using System;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    public static event Action<int> OnBuyItem;

    public Sprite[] sprites;

    public int storeItem;

    public GameObject storePanel;

    private void OnEnable()
    {
        ItemData.OnOpenStore += OpenStore;
    }

    private void OnDisable()
    {
        ItemData.OnOpenStore -= OpenStore;
    }

    void Start()
    {
        CloseStore();
    }

    private void OpenStore(int value)
    {
        storeItem = value;
        Time.timeScale = 0;
        storePanel.SetActive(true);
    }

    public void CloseStore()
    {
        Time.timeScale = 1;
        storePanel.SetActive(false);
    }

    public void BuyItem(int value)
    {
        value = storeItem;
        OnBuyItem?.Invoke(value);
        CloseStore();
    }
}
