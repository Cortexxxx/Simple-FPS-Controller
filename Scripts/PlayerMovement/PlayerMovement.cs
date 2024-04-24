using System;
using UnityEngine;

namespace Scripts.PlayerMovement
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private float jumpHeight = 1;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groudDistance;
        [SerializeField] private LayerMask groundMask;
        
        private CharacterController controller;
        private Vector3 velocity;
        private bool isGrounded;
        
        private const float GravityAcceleration = -9.81f;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groudDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            
            Vector3 direction = transform.right * x + transform.forward * z;
            
            controller.Move(direction * (Time.deltaTime * speed));

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * GravityAcceleration);
            }

            velocity.y += GravityAcceleration * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }
}