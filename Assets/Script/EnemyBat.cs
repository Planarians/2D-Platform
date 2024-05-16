using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    public float speed;
    public float startWaitTime;
    private float waitTime;//����ɵ�λ��֮��Ҫͣ�����
  

    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;



    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        waitTime = startWaitTime;
        movePos.position = GetRandomPos();//�տ�ʼ�ͻ�ȡһ�������Ҫ�ƶ���������
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();

        transform.position=Vector2.MoveTowards(transform.position, movePos.position, speed*Time.deltaTime);

        if(Vector2.Distance(transform.position, movePos.position)<0.1f)//  ����Ѿ��ܽӽ�Ҫȥ�ĵص� �ٸ���һ�����Ҫȥ�ĵص�
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
