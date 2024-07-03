using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RobotFactory : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject robotprefab;
    public int number = 5;//动态生成的机器人数量
    public int fixedRobots=0;//修好的机器人数量
    public int initRobots = 2; //初始已经有的机器人数量
    Transform target;
    float range;
    void Start()
    {
        GameObject ob = GameObject.FindGameObjectWithTag("Player");
        GameObject tilemap= GameObject.FindGameObjectWithTag("Water");
        Tilemap tm = tilemap.GetComponent<Tilemap>();

        target = ob.transform;
        float ct = number;
        int reversenum = 0;
        Vector2 pos=new Vector2();
        for (int i = 1; i <= number; i++)
        {
            if (target.position.y + i * 2 <= tm.localBounds.extents.y)
            { 
                pos = new Vector2(target.position.x, target.position.y + i * 2);
                Instantiate(robotprefab, pos, target.rotation);
                reversenum = i;
            }
            else if(target.position.y - (i - reversenum) * 2>-1*tm.localBounds.extents.y)
            {               
                pos = new Vector2(target.position.x, target.position.y - (i-reversenum) * 2);
                Instantiate(robotprefab, pos, target.rotation);
            }
        }


    }

    // Update is called once per frame

    void Update()
    {
        
    }
}
