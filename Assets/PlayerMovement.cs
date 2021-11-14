using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public Animator anim;
    public float movementSpeed = 5f;

    private Vector2 _input;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this._input.x = Input.GetAxisRaw("Horizontal");
        this._input.y = Input.GetAxisRaw("Vertical");
        this._input.Normalize();

        anim.SetFloat("Horizontal", _input.x);
        anim.SetFloat("Vertical", _input.y);
        anim.SetFloat("Speed", _input.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + this._input * movementSpeed * Time.fixedDeltaTime);
        if (_input.x > 0) {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        } else {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
