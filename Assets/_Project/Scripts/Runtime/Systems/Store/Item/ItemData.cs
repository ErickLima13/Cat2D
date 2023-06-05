using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public static event Action OnOpenStore;
    public static event Action<int> OnBuyItem;

    public int itemId;

    public SpecialMoves player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out SpecialMoves specialMoves))
        {
            player = specialMoves;
            OnOpenStore?.Invoke();
        }
    }


    public void BuyItem()
    {
       OnBuyItem?.Invoke(itemId);
    }
    
}
