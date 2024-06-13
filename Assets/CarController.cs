using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform npc;  // Reference to the NPC's Transform
    public float followDistance = 2.0f;  // Distance the cart should stay behind the NPC
    public float followSpeed = 5.0f;  // Speed at which the cart catches up to the NPC

    void Update()
    {
        if (npc == null) return;

        // Calculate the target position the cart should move towards.
        // This position is 'followDistance' units behind the NPC along its forward vector.
        Vector3 targetPosition = npc.position - npc.forward * followDistance;

        // Optionally, adjust the target position vertically based on the terrain or other factors
        // targetPosition.y = Terrain elevation or other logic

        // Move the cart towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Optionally, make the cart face the direction it's moving or match the NPC's orientation
        // transform.rotation = Quaternion.LookRotation(npc.forward);
    }
}
