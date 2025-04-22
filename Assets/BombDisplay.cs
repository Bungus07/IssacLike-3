using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BombDisplay : MonoBehaviour
{
    public TextMeshProUGUI BombText; // Assign in Inspector
    private PlayerController player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (player != null)
        {
            BombText.text = "Bombs: " + player.BombCount.ToString();
        }
    }
}