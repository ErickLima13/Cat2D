using System;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public static event Action<int> OnOpenStore;

    private StoreController storeController;
    private SpriteRenderer spriteItem;

    [SerializeField] private int itemId;

    private void Start()
    {
        storeController = FindObjectOfType<StoreController>();
        spriteItem = GetComponent<SpriteRenderer>();

        spriteItem.sprite = storeController.sprites[itemId];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out SpecialMoves specialMoves))
        {
            OnOpenStore?.Invoke(itemId);
        }
    }
}
