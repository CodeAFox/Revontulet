using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SlimeLogic
{
    private GameObject player;
    private GameObject slime;

    public SlimeLogic(GameObject player, GameObject slime)
    {
        this.player = player;
        this.slime = slime;
    }
    public Vector2 GetDistanceFromPlayer()
    {
        return new Vector2(player.transform.position.x - slime.transform.position.x, player.transform.position.y - slime.transform.position.y);
    }

    public Vector2 GetClosestChestDistance()
    {
        List<GameObject> chests = GameObject.FindGameObjectsWithTag("Chest").ToList();

        float minDistance = GetDistanceFromPlayer().magnitude;
        Vector2 closestChest = GetDistanceFromPlayer();

        for (int i = 0; i < chests.Count; i++)
        {
            Vector2 chest = new Vector2(chests[i].transform.position.x - slime.transform.position.x, chests[i].transform.position.y - slime.transform.position.y);
            
            if(minDistance > chest.magnitude)
            {
                closestChest = chest;
                minDistance = closestChest.magnitude;
            }
        }
        return closestChest;
    }

    public void SpawnAwayFromPlayer(int magnitude)
    {
        Vector2 randVector = UnityEngine.Random.insideUnitCircle.normalized * magnitude;
        slime.transform.position = new Vector2(player.transform.position.x + randVector.x, player.transform.position.y + randVector.y);
    }
}