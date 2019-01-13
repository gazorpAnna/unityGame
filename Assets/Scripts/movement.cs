using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    public float speed;

    private Animator anim;

    private bool playerMoving;
    private Vector2 lastMove;

    Vector3 lastPos;
    float threshold = 0.0f;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        lastPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        playerMoving = false;
        Vector3 offset = transform.position - lastPos;

        if (transform.hasChanged)
        {
            if(offset.x > threshold)
            {
                lastPos = transform.position;
                playerMoving = true;
                anim.SetFloat("moveX", 0.5f);
                anim.SetFloat("LastMoveX", lastPos.x);
            }
        }
        
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0f, 0f));
            playerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0f));
            playerMoving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        } 
        anim.SetFloat("moveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("moveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }
}
