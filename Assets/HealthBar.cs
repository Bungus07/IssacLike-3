using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealhBar : MonoBehaviour
{
    public GameObject HeartPrefab;
    public int MaxHealth, CurrentHealth;
    public PlayerController Player;
    List<HealthHeartBar> Hearts = new List<HealthHeartBar>();
    // Start is called before the first frame update
    void Start()
    {
         Invoke("UpdatePlayerHealth", 0.1f);
        Debug.Log("PlayerScript is " + Player.gameObject.name);
    }
    public void UpdatePlayerHealth()
    {
        MaxHealth = Player.PlayerMaxHealth;
        CurrentHealth = Player.PlayerHealth;
        DrawHearts();
    }
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(HeartPrefab);
        newHeart.transform.SetParent(gameObject.transform);
        HealthHeartBar HeartComponent = newHeart.GetComponent<HealthHeartBar>();
        HeartComponent.SetHeartImage(HeartStats.Empty);
        Hearts.Add(HeartComponent);
    }
    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        Hearts = new List<HealthHeartBar>();
    }
    public void DrawHearts()
    {
        ClearHearts();
        float MaxHealthRemander = MaxHealth % 2;
        int HeartsToMake = (int)((MaxHealth / 2) + MaxHealthRemander);
        for (int i = 0; i < HeartsToMake; i++)
        {
            CreateEmptyHeart();
        }
        for (int i = 0; i < Hearts.Count; i++)
        {
            int HeartStatsRemander = (int) Mathf.Clamp(CurrentHealth - (i * 2), 0, 2);
            Hearts[i].SetHeartImage((HeartStats)HeartStatsRemander);
        }
    }
    void Update()
    {

    }
}
