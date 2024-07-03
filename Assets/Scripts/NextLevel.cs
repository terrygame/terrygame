using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            GameObject rf = GameObject.FindGameObjectWithTag("RobotFactory");
            RobotFactory rf2 = rf.GetComponent<RobotFactory>();
            if (rf2.fixedRobots >= rf2.number + rf2.initRobots)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
            

        }

    }
}
