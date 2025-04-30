using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Doors;
    public List<GameObject> EnemiesOnFloor;
    public GameObject[] Rewards;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            if (EnemiesOnFloor.Count != 0)
            {
                LockedDoor();
            }
        }
    }
    public void RemoveEnemy(GameObject Enemy)
    {
        EnemiesOnFloor.Remove(Enemy);
        if (EnemiesOnFloor.Count == 0)
        {
            UnLockedDoor();
        }
    }
    public void LockedDoor()
    {
        foreach (GameObject Door in Doors)
        {
            Door.GetComponent<Collider2D>().isTrigger = false;
        }
    }
    public void UnLockedDoor()
    {
        foreach (GameObject Door in Doors)
        {
            Door.GetComponent<Collider2D>().isTrigger = true;
        }
        GiveReward();
    }
    public void GiveReward()
    {
        if (Rewards.Length != 0)
        {
            foreach (GameObject Reward in Rewards)
            {
                Instantiate(Reward, gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
