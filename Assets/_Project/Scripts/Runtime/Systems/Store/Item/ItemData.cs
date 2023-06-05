using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public static event Action<int> OnOpenStore;
   
    public int itemId;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out SpecialMoves specialMoves))
        {
            OnOpenStore?.Invoke(itemId);
        }
    }
}
