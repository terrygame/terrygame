using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public bool vertical;
    public float changeTime = 5.0f;
    public ParticleSystem smokeEffect;
    Animator animator;
    Rigidbody2D rigidby2D;
    float timer;
    int direction = 1;
    bool broken = true;
    public Transform target;
    //int rotateSpeed = 1;
    private Transform mytransform;
    private SpriteRenderer sp;
    private bool facingRight;
    private Vector2 direcT=Vector2.zero;
    float range = 3f;
    float distance;
    void Start()
    {
        rigidby2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        mytransform = this.transform;
        GameObject ob = GameObject.FindGameObjectWithTag("Player");
        
        target = ob.transform;
        //Debug.Log(target.gameObject.tag);
      //  Debug.Log(target.gameObject.gameObject.name);
        //GameObject.FindGameObjectWithTag
        direcT = getRandomVector();

    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
            
        }


    }
    private void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        /* 3.29×¢ÊÍ if (vertical)
         {
             position.y = position.y + speed * direction * Time.deltaTime;
             animator.SetFloat("MoveX", 0);
             animator.SetFloat("MoveY", direction);
         }
         else
         {

             //print($"{position.x},{position.y}; {position.x + Time.deltaTime * speed * direction}");
             position.x = position.x + speed * direction * Time.deltaTime;
             animator.SetFloat("MoveX", direction);
             animator.SetFloat("MoveY", 0);
         }*/
        Vector2 position = rigidby2D.position;
        setAnim();
        rigidby2D.MovePosition((Vector2)mytransform.position + direcT * speed * Time.deltaTime * direction);
        //rigidby2D.velocity = (position - (Vector2)transform.position).normalized*speed;
        //print(rigidby2D.velocity);
        //transform.position = position;
       

        //print($"{position.x}, {position.y}; {transform.position.x}, {transform.position.y}");
        //¸ú×ÙÍæ¼Ò ruby
        Vector2 diff = target.position - mytransform.position;
        distance = diff.magnitude;
        if (distance < range)
        {
            direction = 1;
            direcT =diff.normalized;
            rigidby2D.MovePosition((Vector2)mytransform.position + direcT * speed * Time.deltaTime * direction);
        }


    }
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
           // Debug.Log("Health change");
            player.ChangeHealth(-1);
        }
        else if(other.gameObject.CompareTag("Wall")|| other.gameObject.CompareTag("Water") || other.gameObject.CompareTag("Tree"))
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    public void Fix()
    {
        broken = false;
        sp = GetComponent<SpriteRenderer>();
        sp.color = new Color32(255, 255, 255, 255);
        rigidby2D.simulated = false;
        smokeEffect.Stop();
        animator.SetTrigger("Fixed");


    }
    void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    Vector2 getRandomVector()
    {
        Vector2 v2 = new Vector2(UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-10.0f, 10.0f));
        return v2.normalized;
    }
    void setAnim()
    {
        if (direcT.magnitude > 0f)
        {
            animator.SetFloat("MoveX", direction * direcT.x);
            animator.SetFloat("MoveY", direction * direcT.y);
           // animator.SetBool("isWalking", true);
        }
        else
        {
           // animator.SetBool("isWalking", false);
        }
    }

}
