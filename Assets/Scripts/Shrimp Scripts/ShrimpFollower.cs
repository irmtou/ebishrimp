using UnityEngine;

public class ShrimpFollower : MonoBehaviour {
    public ShrimpManager shrimpManager; // Reference to the ShrimpManager


    [Header("Leader Settings")]
    public Transform leader; // Reference to the leader
    public float swarmRadius = 3f; // Radius around the leader

    [Header("Movement Settings")]
    public float moveSpeed = 3f; // Base movement speed
    public float driftSpeed = .5f; // Extra drift speed for randomness
    public float positionSmoothness = 0.2f; // How quickly shrimp respond to new targets
    public float rotationSmoothness = 5f; // Speed of smooth rotation

    [Header("Organic Motion")]
    public float noiseFrequency = 0.5f; // How quickly noise changes direction
    public float noiseAmplitude = 0.5f; // Strength of random drifting
    public float wobbleIntensity = 0.1f; // Small rotation wobble for realism
    public float wobbleSpeed = 2f; // Speed of wobble oscillation

    private Vector3 velocity; // Current velocity
    private Vector3 targetOffset; // Target offset from leader

    void Start() {
        // Initialize with a random offset
        targetOffset = GetRandomOffset();

        // Add a Sphere Collider if none exists
        if (GetComponent<Collider>() == null) {
            SphereCollider collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = 0.5f; // Adjust to fit your shrimp size
            collider.center = Vector3.zero; // Center it on the GameObject
        }

    }
    // I applied Perlin noise because the normal movement was extremely stiff
    // I learned that there are flocking algorithms but that itself was a lot
    void Update() {
        if (leader == null) return;

        // Continuously update the target position with Perlin noise for smooth random drifting
        Vector3 noiseOffset = new Vector3(
            Mathf.PerlinNoise(Time.time * noiseFrequency, 0) - 0.5f,
            0,
            Mathf.PerlinNoise(0, Time.time * noiseFrequency) - 0.5f
        ) * noiseAmplitude;

        // Calculate target position
        Vector3 targetPosition = leader.position + targetOffset + noiseOffset;

        // Smoothly move towards the target position
        velocity = Vector3.Lerp(velocity, (targetPosition - transform.position).normalized * moveSpeed, Time.deltaTime / positionSmoothness);
        transform.position += velocity * Time.deltaTime;

        // Add rotational wobble for organic movement
        Quaternion wobble = Quaternion.Euler(
            Mathf.Sin(Time.time * wobbleSpeed) * wobbleIntensity,
            Mathf.Sin(Time.time * wobbleSpeed * 1.5f) * wobbleIntensity,
            0
        );

        // Smoothly rotate shrimp to face the movement direction
        if (velocity.magnitude > 0.1f) {
            Quaternion targetRotation = Quaternion.LookRotation(velocity) * wobble;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);
        }

        // Periodically recalculate the target offset for swarm dispersion
        if (Random.value < 0.01f) {
            targetOffset = GetRandomOffset();
        }
    }

    private Vector3 GetRandomOffset() {
        // Generate a random position within the swarm radius
        Vector2 randomCircle = Random.insideUnitCircle * swarmRadius;
        return new Vector3(randomCircle.x, 0, randomCircle.y);
    }
}
