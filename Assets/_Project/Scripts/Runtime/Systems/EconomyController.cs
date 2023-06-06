using System;
using UnityEngine;

public class EconomyController : MonoBehaviour
{
    public event Action<int> OnUpdateCoins;

    [SerializeField] private int coins;

    public void AddCoins(int value)
    {
        coins += value;
        OnUpdateCoins?.Invoke(coins);
    }

    public void RemoveCoins(int value)
    {
        coins -= value;
        OnUpdateCoins?.Invoke(coins);
    }

    public int GetCoins()
    {
        return coins;
    }

}
