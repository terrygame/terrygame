using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProject : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2d;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            Debug.Log("Health change");
            player.ChangeHealth(-1);
        }

        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 100.0f)
        {
            Debug.Log("destroy");
            Destroy(gameObject);
        }
    }
}
