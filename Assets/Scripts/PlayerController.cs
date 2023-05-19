using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables

    public BoxCollider2D box;
    public Rigidbody2D rig;
    public Animator ani;

    public float horiInput;
    public float moveSpeed;
    public float jumpForce;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetFloat("horiInput", Mathf.Abs(horiInput));
        ani.SetBool("isGrounded", isGrounded);

        // Basic movement
        horiInput = Input.GetAxis("Horizontal");
        transform.Translate(horiInput * moveSpeed * Time.deltaTime * Vector2.right);
        if (horiInput > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (horiInput < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Bounce(Vector2.up, jumpForce);
            isGrounded = false;
            ani.SetTrigger("bounce");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset jump state upon hitting the ground
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Light Platform"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void Bounce(Vector2 direction, float power)
    {
        rig.AddForce(direction * power, ForceMode2D.Impulse);
    }
}
