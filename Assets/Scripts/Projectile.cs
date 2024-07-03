using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
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
        //我们还增加了调试日志来了解飞弹触碰到的对象
        //Debug.Log("Projectile Collision with " + other.gameObject);
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
            GameObject rf = GameObject.FindGameObjectWithTag("RobotFactory");
            RobotFactory rf2 = rf.GetComponent<RobotFactory>();
            rf2.fixedRobots = rf2.fixedRobots + 1;

        }
        MachineController e2 = other.collider.GetComponent<MachineController>();
        if (e2 != null)
        {
            e2.Fix();
            GameObject rf3 = GameObject.FindGameObjectWithTag("MachineFactory");
            MachineFactory mf = rf3.GetComponent<MachineFactory>();
            mf.fixedRobots = mf.fixedRobots + 1;

        }
        Boss1Controller e3 = other.collider.GetComponent<Boss1Controller>();
        if (e3 != null)
        {
            e3.ChangeHealth(-1); 

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
