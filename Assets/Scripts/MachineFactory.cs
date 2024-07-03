using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
//using static UnityEditor.PlayerSettings;
public class MachineFactory : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject robotprefab;
    public GameObject bossPrefab;
    public int number = 8;//动态生成的机器人数量
    public int fixedRobots = 0;//修好的机器人数量
    public int initRobots = 2; //初始已经有的机器人数量
    public float proInterval = 5.0f; //生成机器人间隔时间
    float timer;
    Transform target;
    float range;
    int total = 0;
    int bossnum=0;
    Tilemap tm;
    void Start()
    {
        //GameObject ob = GameObject.FindGameObjectWithTag("Player");
        GameObject tilemap = GameObject.FindGameObjectWithTag("Water");
        tm = tilemap.GetComponent<Tilemap>();
        GameObject ob = GameObject.FindGameObjectWithTag("Player");
        target = ob.transform;
        float ct = number;
        //int reversenum = 0;
        Vector2 pos = new Vector2();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 pos = new Vector2();
        int upmaxnum = 0;
        timer -= Time.deltaTime;
        Vector2 diff = target.position - this.transform.position;
        if (timer < 0)
        {
            if(this.transform.position.y + total <= tm.localBounds.extents.y - 2)
            {
                pos = new Vector2(this.transform.position.x - 3, this.transform.position.y + total);
            }
            else
            {
                if (upmaxnum == 0)
                    upmaxnum = total;
                pos = new Vector2(this.transform.position.x - 4, this.transform.position.y - 1-total+upmaxnum);
            }
                
            total += 1;
            if (total <= number)
            {
                Instantiate(robotprefab, pos, target.rotation);
            }
            timer = proInterval;

        }
        if ((fixedRobots >= number ) && (bossnum==0))
        {
            pos = new Vector2(this.transform.position.x - 2, this.transform.position.y - 3);
            
            Instantiate(bossPrefab, pos, Quaternion.identity);
            bossnum = 1;
        }


    }
}
