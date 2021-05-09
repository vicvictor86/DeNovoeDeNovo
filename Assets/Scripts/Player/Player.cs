using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Rigidbody2D rig;
    public Transform groundCheck;
    public LayerMask isGround;
    public Animator animator;
    public Light2D lightPost;
    public GameObject lampPost;

    [SerializeField] private float speed = 4;
    [SerializeField] private float jumpForce = 270;

    [SerializeField]private bool isGrounded = false;
    private bool lookingRight = true;
  
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = gameObject.transform.Find("GroundCheck").GetComponent<Transform>();
        //isGround = LayerMask.GetMask("Ground");

        lampPost = GameObject.Find("LampPost");
        lightPost = lampPost.GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpPlayer();
        }
        CheckJumping();
        
    }

    private void CheckJumping()
    {
        if (!isGrounded)
        {
            animator.SetBool("Jumping", true);
            print(animator.GetBool("Jumping"));

            if (rig.velocity.y > 0)
            {
                animator.SetFloat("TimeJumping", 0);
            }
            else
            {
                animator.SetFloat("TimeJumping", 1);
            }
        }
        else
        {
            animator.SetBool("Jumping", false);
            print(animator.GetBool("Jumping"));
        }
    }

    private void JumpPlayer()
    {
        if (isGrounded)
        {
            rig.AddForce(new Vector2(0, jumpForce),ForceMode2D.Force);
        }    
    }

    private void MovePlayer()
    {
        float movimentoH = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movimentoH * speed, rig.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, isGround);
        
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
        if (collision.gameObject.CompareTag("damage"))
        {
            print("colidiu");
            TakeDamage();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("nextLevel"))
        {
            GameManager.instance.WinLevel((OndeEstou.instance.fase + 1));
        }

        if (collision.gameObject.CompareTag("button"))
        {
            lightPost.intensity = 1;
            lampPost.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, 0.5f);
    }
}
