using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    public TextMeshProUGUI coinText; // Assign in Inspector
    private PlayerController player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (player != null)
        {
            coinText.text = "Coins: " + player.CoinCount.ToString();
        }
    }
}
