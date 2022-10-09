using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    public float moveSpeed = 10f;

    private GameObject player;
    private Rigidbody2D rb;
    private Animator anim;

    Command keyLeft, keyRight;

    private float moveX;

    public void Start(GameObject _player)
    {
        keyLeft = new MoveLeft();
        keyRight = new MoveRight();

        player = _player;
        rb = player.GetComponent<Rigidbody2D>();
        anim = _player.GetComponent<Animator>();
    }


    public void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = moveX;
        rb.velocity = velocity;
    }

    public void HandleInput()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.rotation = Quaternion.Euler(0, 180, 0);
            keyLeft.Execute(anim);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            keyRight.Execute(anim);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("ghello");
        }
    }
}
