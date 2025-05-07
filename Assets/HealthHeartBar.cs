using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeartBar : MonoBehaviour
{
    public Sprite FullHeart, HalfHearts, EmptyHeart;
    private Image HeartImage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        HeartImage = GetComponent<Image>();
    }
    public void SetHeartImage(HeartStats Stats)
    {
        switch (Stats)
        {
            case HeartStats.Empty:
            HeartImage.sprite = EmptyHeart;
            break;
            case HeartStats.half:
            HeartImage.sprite = HalfHearts;
            break;
            case HeartStats.full:
            HeartImage.sprite = FullHeart;
            break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum HeartStats
{
    Empty = 0,
    half = 1,
    full = 2
}