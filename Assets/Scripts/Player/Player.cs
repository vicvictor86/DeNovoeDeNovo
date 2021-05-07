using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Rigidbody2D rig;
    public Transform groundCheck;
    public LayerMask isGround;
    public Animator animator;

    [SerializeField] private float speed = 4;
    [SerializeField] private float jumpForce = 270;

    private bool isGrounded = false;
    private bool lookingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = gameObject.transform.Find("GroundCheck").GetComponent<Transform>();
        //isGround = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        JumpPlayer();
    }

    private void JumpPlayer()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rig.AddForce(new Vector2(0, jumpForce),ForceMode2D.Force);
        }
    }

    private void MovePlayer()
    {
        float movimentoH = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movimentoH * speed, rig.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, isGround);
        
        if(movimentoH != 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        if((movimentoH > 0 && !lookingRight) || (movimentoH < 0 && lookingRight))
        {
            Flip();
        }    
    }

    private void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void TakeDamage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("thorns"))
        {
            print("colidiu");
            TakeDamage();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("nextLevel"))
        {
            LevelManager.instance.LoadLevel((OndeEstou.instance.fase + 1).ToString());
        }
    }
}
