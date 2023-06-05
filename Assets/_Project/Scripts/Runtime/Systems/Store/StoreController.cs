using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    public static event Action<int> OnBuyItem;

    private PlayerController player;

    private int storeItem;

    [SerializeField] private Image iconItem;
    [SerializeField] private TextMeshProUGUI priceItem;

    public Sprite[] sprites;

    [SerializeField] private int[] prices;

    [SerializeField] private GameObject storePanel;

    [SerializeField] private List<int> items;


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
        player = FindObjectOfType<PlayerController>();

        CloseStore();
    }

    private void OpenStore(int value)
    {
        storeItem = value;
        iconItem.sprite = sprites[storeItem];
        priceItem.text = prices[storeItem].ToString();

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

        int price = int.Parse(priceItem.text);

        if (!items.Contains(value) && player.coins >= price)
        {
            items.Add(value);
            player.coins -= price;
            OnBuyItem?.Invoke(value);

        }

        CloseStore();
    }
}
