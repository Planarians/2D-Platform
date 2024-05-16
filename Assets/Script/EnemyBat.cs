using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    public float speed;
    public float startWaitTime;
    private float waitTime;//蝙蝠飞到位置之后要停留多久
  

    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;



    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        waitTime = startWaitTime;
        movePos.position = GetRandomPos();//刚开始就获取一个随机的要移动到的坐标
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();

        transform.position=Vector2.MoveTowards(transform.position, movePos.position, speed*Time.deltaTime);

        if(Vector2.Distance(transform.position, movePos.position)<0.1f)//  如果已经很接近要去的地点 再给下一个随机要去的地点
        {
            if(waitTime<=0)
            {
                movePos.position = GetRandomPos();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x),Random.Range(leftDownPos.position.y,rightUpPos.position.y));
        return rndPos;
    }

   

}
