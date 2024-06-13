using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TreeFinder : MonoBehaviour
{
    public List<GameObject> trees = new List<GameObject>();
    public List<GameObject> cuttedtrees = new List<GameObject>();
    public float updateInterval = 5.0f;

    private void Start()
    {
        // Start the coroutine to update the tree list every 5 seconds
        StartCoroutine(UpdateTreeList());
    }

    IEnumerator UpdateTreeList()
    {
        while (true)
        {
            //   Debug.Log("Updating tree list...");

            //------------------------------------------------------------------------
            GameObject[] foundTrees = GameObject.FindGameObjectsWithTag("GrownTree");
            // Filter out only the active GameObjects from foundTrees using LINQ
            IEnumerable<GameObject> activeTrees = foundTrees.Where(tree => tree.activeSelf);


            // Clear the trees list and add only the active GameObjects
            trees.Clear();
            trees.AddRange(activeTrees);

            //  Debug.Log("Number of trees found: " + trees.Count);


          //  Debug.Log("Updating cuttedtree list...");

            //------------------------------------------------------------------------
            GameObject[] foundcuttedtree = GameObject.FindGameObjectsWithTag("CuttedTree");
            cuttedtrees.Clear();
            cuttedtrees.AddRange(foundcuttedtree);

           // Debug.Log("Number of cuttedtree found: " + cuttedtrees.Count);

            yield return new WaitForSeconds(updateInterval);
        }
    }


    // Public method to access the list of trees
    public List<GameObject> GetTrees()
    {
        return trees;
    }
    public List<GameObject> GetCuttedTrees()
    {
        return cuttedtrees;
    }
}
