using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyDisplay : MonoBehaviour
{
    public TextMeshProUGUI KeyText; // Assign in Inspector
    private PlayerController player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (player != null)
        {
            KeyText.text = "Key: " + player.KeyCount.ToString();
        }
    }
}