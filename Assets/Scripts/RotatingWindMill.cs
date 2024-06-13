using System.Collections.Generic;
using UnityEngine;

public class RotatingWindMill : MonoBehaviour
{
    public float rotationSpeed = 10f;

    private GameObject[] existingWindmills;

    void Start()
    {
        // Find game objects with the tag "Windmill"
        existingWindmills = GameObject.FindGameObjectsWithTag("Windmill");
    }

    void Update()
    {
        // Rotate all existing windmills
        foreach (GameObject windmill in existingWindmills)
        {
            windmill.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
