using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss1Controller : MonoBehaviour
{
    public GameObject bossprojPrefab;
    public int maxHealth = 10;
    public float speed = 3.0f;
    public float fireTime = 2.0f;
    public float range = 100.0f;
    public GameObject GamepassPrefab;
    public int health { get { return currentHealth; } }

    public Transform target;
    Vector2 lookDirection = new Vector2(1, 0);
    bool broken = true;
    int currentHealth;
    Animator animator;
    Rigidbody2D rigidbody2d;
    float timer;
    private Vector2 directV= Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        GameObject ob = GameObject.FindGameObjectWithTag("Player");
        target = ob.transform;
        directV = getRandomVector();
        timer = fireTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }
        Vector2 position = rigidbody2d.position;
        Vector2 move = new Vector2(position.x, position.y);

        rigidbody2d.MovePosition((Vector2)this.transform.position + directV * speed * Time.deltaTime);
        Vector2 diff = target.position - this.transform.position;
        if (diff.magnitude < range)
        {
            directV = diff.normalized;
            rigidbody2d.MovePosition((Vector2)this.transform.position + directV * speed * Time.deltaTime );
        }
        lookDirection.Set(diff.x, diff.y);
        lookDirection.Normalize();

        animator.SetFloat("MoveX", lookDirection.x);
        animator.SetFloat("MoveY", lookDirection.y);
        setAnim();

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Launch();
            timer = fireTime;

        }
    }
    void Launch()
    {
        GameObject pob = Instantiate(bossprojPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        BossProject bossproj = pob.GetComponent<BossProject>();
        bossproj.Launch(lookDirection, 300);
    }
    Vector2 getRandomVector()
    {
        Vector2 v2 = new Vector2(UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-10.0f, 10.0f));
        return v2.normalized;
    }
    void setAnim()
    {
        if (directV.magnitude > 0f)
        {
            animator.SetFloat("MoveX",  directV.x);
            animator.SetFloat("MoveY",  directV.y);
            // animator.SetBool("isWalking", true);
        }
        else
        {
            
        }
    }
    public void ChangeHealth(int amount)
    {
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("boss blood " + currentHealth + "/" + maxHealth);
        BossBloodBar.instance.SetValue(currentHealth / (float)maxHealth);
        if (currentHealth <= 0)
        {
            Fix();
        }
    }
    public void Fix()
    {
        broken = false;
       
        rigidbody2d.simulated = false;
       
        animator.SetTrigger("Fixed");
        GameObject overObject = Instantiate(GamepassPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        
        //rigidbody2d.simulated = false;
        

    }
}
