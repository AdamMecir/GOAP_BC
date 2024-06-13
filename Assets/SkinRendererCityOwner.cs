using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinRendererCityOwner : MonoBehaviour
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

        

        // Apply the random tiling values
        rend.material.mainTextureScale = new Vector2(1.34f, 1.41f);
    }

    // Update is called once per frame
    void Update()
    {
        // You can add dynamic behavior here if needed
    }
}
