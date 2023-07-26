//goldmanager.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoldManager : MonoBehaviour
{
    public int gold = 200; //initial gold

    public static GoldManager Instance { get; private set; } //singleton instance

    public UnityEvent<int> OnGoldChanged = new UnityEvent<int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
        OnGoldChanged.Invoke(gold);
    }

    public bool SubtractGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            OnGoldChanged.Invoke(gold);
            return true;
        }
        else
        {
            Debug.Log("Not enough gold");
            return false;
        }
    }
}