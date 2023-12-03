using System.Collections.Generic;
using UnityEngine;

public class Player : G_Character
{
    [SerializeField] private DynamicJoystick joyStick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    private Vector3 moveVector;
    private void Start()
    {
    }
    private void Awake()
    {
        rb.GetComponent<Rigidbody>();
    }
    private void Update()
    {
         MovePlayer();
    }
    private void MovePlayer()
    {
        moveVector = Vector3.zero;
        moveVector.x = joyStick.Horizontal * moveSpeed * Time.deltaTime;
        moveVector.z = joyStick.Vertical * moveSpeed * Time.deltaTime;
        if(joyStick.Horizontal != 0 || joyStick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
            ChangeAnimation("run");
        }
        else if (joyStick.Horizontal == 0 || joyStick.Vertical == 0)
        {
            ChangeAnimation("idle");
        }
        rb.MovePosition(rb.position + moveVector);
    }
    
}