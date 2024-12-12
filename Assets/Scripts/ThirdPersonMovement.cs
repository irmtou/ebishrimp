using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

using Cinemachine;

public class ThirdPersonMovement : MonoBehaviour {

    public CharacterController controller;

    public Transform cam;
    public AudioManager audioManager;
    public AudioClip sound;
    public float speed = 6f;
    public float turnSmoothTime = 0.5f;
    public float gravity = -9.81f;
    float turnSmoothVelocity;
    private Vector3 velocity; 
    public float groundCheckDistance = 0.3f;
    public float jumpHeight = 2f;

    private void Start() {
        controller = GetComponent<CharacterController>();
    }


    private void Update() {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f) {

            // From Brackey's Video
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(speed * Time.deltaTime * moveDir.normalized);
        }
        // Handle jumping
        if (controller.isGrounded && velocity.y < 0) {
            velocity.y = -2f; // Keep the character grounded (prevent floating)
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded) {
            audioManager.PlaySound(sound);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Apply the jump force
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply the movement with gravity to the character controller
        controller.Move(velocity * Time.deltaTime);
    }


}