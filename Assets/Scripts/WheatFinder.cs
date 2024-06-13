using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WheatFinder : MonoBehaviour
{
    public List<GameObject> wheats = new List<GameObject>();
    public List<GameObject> seedplaces = new List<GameObject>();
    public GameObject FarmParent;
    public float updateInterval;

    private void Start()
    {
        GameObject wheatParent = GameObject.FindGameObjectWithTag("WheatParent");
        if (wheatParent != null)
        {
         
            wheatParent.tag = "WheatParentOwned";
            FarmParent = wheatParent;  // Update FarmParent reference
      
        }
        else
        {
            Debug.Log("WheatParent not found");
        }
        StartCoroutine(UpdateWheatList());
    }

    IEnumerator UpdateWheatList()
    {
        while (true)
        {
            //------------------------------------------------------------------------
            // Find and update WheatParent
            

            //------------------------------------------------------------------------
            // Find grown wheats within FarmParent
            GameObject[] foundWheats = FarmParent.transform
                .GetComponentsInChildren<Transform>(true)
                .Where(t => t.gameObject.CompareTag("GrownWheat"))
                .Select(t => t.gameObject)
                .ToArray();

            IEnumerable<GameObject> activeWheats = foundWheats.Where(Wheat => Wheat.activeSelf);

            wheats.Clear();
            wheats.AddRange(activeWheats);

            //------------------------------------------------------------------------
            // Find seed places within FarmParent
            GameObject[] foundWheatPlaces = FarmParent.transform
                .GetComponentsInChildren<Transform>(true)
                .Where(t => t.gameObject.CompareTag("SeedPlace"))
                .Select(t => t.gameObject)
                .ToArray();

            seedplaces.Clear();
            seedplaces.AddRange(foundWheatPlaces);

            yield return new WaitForSeconds(updateInterval);
        }
    }

    public List<GameObject> GetWheats()
    {
        return wheats;
    }

    public List<GameObject> GetWheatPlaces()
    {
        return seedplaces;
    }
}
