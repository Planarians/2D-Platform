using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public float speed;
    public float waitTime;
    public Transform[] movPos;//要移动的位置

    private int i;
    private Transform playerDefTransform;


    // Start is called before the first frame update
    void Start()
    {
        i = 1;
        playerDefTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movPos[i].position, speed*Time.deltaTime);//从当前位置移动到
        if(Vector2.Distance(transform.position, movPos[i].position)<0.1f)
        {
            if(waitTime<0.0f)//判断if waittime小于0
            {
                if (i == 0)
                {
                    i = 1;
                }
                else
                {
                    i = 0;
                }
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
           
        }
    }


    private void OnTriggerEnter2D(Collider2D other)//用于让player跟着平台移动
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")//j检测脚上的碰撞框
        {
            other.gameObject.transform.parent=gameObject.transform;//把player变成平台的子对象
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")//j检测脚上的碰撞框
        {
            other.gameObject.transform.parent = playerDefTransform;
        }
    }
}
