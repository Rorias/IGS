using UnityEngine;

public class InputHandler
{
    public float moveSpeed = 10f;

    private GameObject player;
    private Rigidbody2D rb;
    private Animator anim;

    private Command keyLeft, keyRight;

    private float moveX;

    public void Start(GameObject _player)
    {
        keyLeft = new MoveLeft();
        keyRight = new MoveRight();

        player = _player;
        rb = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponent<Animator>();
    }

    public void UpdatePlayerPos()
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
            keyLeft.Execute(anim);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            keyRight.Execute(anim);
        }
    }
}
