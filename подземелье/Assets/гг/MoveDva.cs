using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDva : MonoBehaviour
{
    public bool zemla;
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    //бег
    public int stamina;
    int max;
    public Slider slider;
    public float sprint;
    public float otkat;
    float m;
    float resopspeed;

    //блок
    public bool block;
    float resopspeed2;

    void Start()
    {
        resopspeed = speed;
        resopspeed2 = 2;
        max = stamina;
    }
    void Update()
    {
        //slider.value = stamina;
        if (stamina >= max)
        {
            stamina = max;
        }
        if (stamina <= 0)
        {
            stamina = 0;
            block = false;
            resopspeed = speed;
        }
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            stamina = stamina - 1;
            resopspeed = sprint;
            m = 0f;
        }
        else
        {
            m = m + 0.1f;
            resopspeed = speed;
            if (m > otkat)
            {
                stamina = stamina + 3;
            }
        }
        if (block)
        {
            resopspeed = resopspeed2;
            stamina -= 4;
        }


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * resopspeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        controller.Move(velocity * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        if (controller.isGrounded)
        {
            zemla = true;
        }
        else { zemla = false; }
    }

}
