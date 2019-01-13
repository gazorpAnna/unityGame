using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAmovement : MonoBehaviour
{
    public float speed;

    private Animator anim;

    private bool playerMoving;
    private Vector2 lastMove;

    Vector3 lastPos;
    float threshold = 0.0f;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerMoving = false;
        Vector3 offset = transform.position - lastPos;

        if (transform.hasChanged)
        {
            if (offset.x > threshold)
            {
                lastPos = transform.position;
                lastMove = new Vector2(1.0f, 0f);
                playerMoving = true;
                anim.SetFloat("moveX", 1.0f);
            }
            if (offset.x < threshold)
            {
                lastPos = transform.position;
                lastMove = new Vector2(-1.0f, 0f);
                playerMoving = true;
                anim.SetFloat("moveX", -1.0f);
            }

            if (offset.y > threshold)
            {
                lastPos = transform.position;
                playerMoving = true;
                lastMove = new Vector2(0f, 1.0f);
                anim.SetFloat("moveY", 1.0f);
            }
            if (offset.y < threshold)
            {
                lastPos = transform.position;
                playerMoving = true;
                lastMove = new Vector2(0f, -1.0f);
                anim.SetFloat("moveY", -1.0f);
            }

        }
        //anim.SetFloat("moveX", Input.GetAxisRaw("Horizontal"));
        //anim.SetFloat("moveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }
}
