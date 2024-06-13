using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IronFinder : MonoBehaviour
{
    public List<GameObject> irons = new List<GameObject>();
    public float updateInterval = 5.0f;

    private void Start()
    {
        // Start the coroutine to update the tree list every 5 seconds
        StartCoroutine(UpdateIronList());
    }

    IEnumerator UpdateIronList()
    {
        while (true)
        {

            GameObject[] foundIrons = GameObject.FindGameObjectsWithTag("Iron");

            IEnumerable<GameObject> activeIrons = foundIrons.Where(iron => iron.activeSelf);



            irons.Clear();
            irons.AddRange(activeIrons);



            yield return new WaitForSeconds(updateInterval);
        }
    }


    public List<GameObject> GetIrons()
    {
        return irons;
    }
}
