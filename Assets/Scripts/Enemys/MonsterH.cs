using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterH : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);
    }

    public void Flip()
    {
        speed *= -1;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        Flip();
    }
}
