using UnityEngine;

public class ShrimpFollower : MonoBehaviour {
    public ShrimpManager shrimpManager;

    [Header("Leader Settings")]
    public Transform leader;
    public float swarmRadius = 3f;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float positionSmoothness = 0.2f;
    public float rotationSmoothness = 5f;

    [Header("Organic Motion")]
    public float noiseFrequency = 0.5f;
    public float noiseAmplitude = 0.5f;
    public float wobbleIntensity = 0.1f;
    public float wobbleSpeed = 2f;

    private Vector3 velocity;
    private Vector3 targetOffset;

    void Start() {
        targetOffset = GetRandomOffset();

        // Add a Capsule Collider if none exists
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        if (collider == null) {
            collider = gameObject.AddComponent<CapsuleCollider>();
        }

        // Configure the Capsule Collider
        collider.center = new Vector3(0f, -0.01f, 0f); // Center as specified
        collider.radius = 0.02f;                       // Radius as specified
        collider.height = 0.11f;                       // Height as specified
        collider.direction = 2;                        // Z-axis

        // Add and configure a Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null) {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Update() {
        if (leader == null) return;

        Vector3 noiseOffset = new Vector3(
            Mathf.PerlinNoise(Time.time * noiseFrequency, 0) - 0.5f,
            0,
            Mathf.PerlinNoise(0, Time.time * noiseFrequency) - 0.5f
        ) * noiseAmplitude;

        Vector3 targetPosition = leader.position + targetOffset + noiseOffset;

        velocity = Vector3.Lerp(velocity, (targetPosition - transform.position).normalized * moveSpeed, Time.deltaTime / positionSmoothness);
        transform.position += velocity * Time.deltaTime;

        Quaternion wobble = Quaternion.Euler(
            Mathf.Sin(Time.time * wobbleSpeed) * wobbleIntensity,
            Mathf.Sin(Time.time * wobbleSpeed * 1.5f) * wobbleIntensity,
            0
        );

        if (velocity.magnitude > 0.1f) {
            Quaternion targetRotation = Quaternion.LookRotation(velocity) * wobble;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);
        }

        if (Random.value < 0.01f) {
            targetOffset = GetRandomOffset();
        }
    }

    private Vector3 GetRandomOffset() {
        Vector2 randomCircle = Random.insideUnitCircle * swarmRadius;
        return new Vector3(randomCircle.x, 0, randomCircle.y);
    }
}
