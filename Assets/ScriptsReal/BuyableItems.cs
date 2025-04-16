using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyableItems : MonoBehaviour
{
    public int Price;
    public Item Item;
    public TextMeshProUGUI PriceText;
    // Start is called before the first frame update
    void Start()
    {
        PriceText.text = Price.ToString();
        gameObject.GetComponent<SpriteRenderer>().sprite = Item.ItemSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
