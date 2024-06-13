using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinRenderer : MonoBehaviour
{
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>(); // Get the Renderer component attached to this GameObject
        if (rend == null || rend.material == null) // Check if Renderer and material exist
        {
            Debug.LogError("Renderer or Material not found!");
            return;
        }

        // Change the tiling of the main texture maps randomly
        float randomX = Random.Range(1f, 5f); // Random float between 1 and 5
        float randomY = Random.Range(1f, 5f); // Random float between 1 and 5

        // Apply the random tiling values
        rend.material.mainTextureScale = new Vector2(randomX, randomY);
    }

    // Update is called once per frame
    void Update()
    {
        // You can add dynamic behavior here if needed
    }
}
