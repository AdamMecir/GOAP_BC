using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StoneFinder : MonoBehaviour
{
    public List<GameObject> stones = new List<GameObject>();
    public float updateInterval = 5.0f;

    private void Start()
    {
        // Start the coroutine to update the tree list every 5 seconds
        StartCoroutine(UpdateStoneList());
    }

    IEnumerator UpdateStoneList()
    {
        while (true)
        {

            GameObject[] foundStones = GameObject.FindGameObjectsWithTag("Stone");

            IEnumerable<GameObject> activeStones = foundStones.Where(stone => stone.activeSelf);



            stones.Clear();
            stones.AddRange(activeStones);

 

            yield return new WaitForSeconds(updateInterval);
        }
    }


    public List<GameObject> GetStones()
    {
        return stones;
    }

}
