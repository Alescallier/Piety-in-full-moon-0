using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraLook : MonoBehaviour
{
    public CharacterController characterController;

    public float speed = 10f;

    public float gravity = -9.81f;

    public Transform groundCheck;
    public float sphereRadius = 0.3f;

    public LayerMask groundMask;

    bool isGrounded;

    public float jumpHeigth = 3;




    Vector3 velocity;


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;


        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            velocity.y = Mathf.Sqrt(jumpHeigth * -2 * gravity);

        }

        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;


        characterController.Move(velocity * Time.deltaTime);


    }
}
