using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public bool vertical;
    public float changeTime = 5.0f;

    Animator animator;
    Rigidbody2D rigidby2D;
    float timer;
    int direction = 1;
    bool broken = true;
    public Transform target;

    private Transform mytransform;
    private SpriteRenderer sp;
    private bool facingRight;
    private Vector2 direcT = Vector2.zero;
    float range = 4.0f;
    float distance;
    void Start()
    {
        rigidby2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        mytransform = this.transform;
        GameObject ob = GameObject.FindGameObjectWithTag("Player");
        target = ob.transform;

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

    Vector2 lastPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;
    private void FixedUpdate()
    {
        velocity = ((Vector2)(transform.position) - lastPosition).normalized;
        lastPosition = transform.position;
        if (!broken)
        {
            return;
        }
        Vector2 position = rigidby2D.position;

        rigidby2D.MovePosition((Vector2)mytransform.position + direcT * speed * Time.deltaTime * direction);

        //print($"{position.x}, {position.y}; {transform.position.x}, {transform.position.y}");
        //¸ú×ÙÍæ¼Ò ruby
        Vector2 diff = target.position - mytransform.position;
        distance = diff.magnitude;
        if (distance < range)
        {
            
            direcT = diff.normalized;
            rigidby2D.MovePosition((Vector2)mytransform.position + direcT * speed * Time.deltaTime * direction);
        }
        setAnim();


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
           // animator.SetFloat("MoveX", direction * direcT.x);
          //  animator.SetFloat("MoveY", direction * direcT.y);
            animator.SetFloat("MoveX", velocity.x);
            animator.SetFloat("MoveY", velocity.y);
            // animator.SetBool("isWalking", true);
        }
       // else
       // {
            // animator.SetBool("isWalking", false);
        //}
    }
    public void Fix()
    {
        broken = false;
        sp = GetComponent<SpriteRenderer>();
        //sp.color = new Color32(255, 255, 255, 255);
        rigidby2D.simulated = false;
        animator.SetTrigger("Fixed");


    }
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            Debug.Log("Health change");
            player.ChangeHealth(-1);
        }
        else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Water") || other.gameObject.CompareTag("Tree"))
        {
            direction = -direction;
            timer = changeTime;
        }
    }
}
