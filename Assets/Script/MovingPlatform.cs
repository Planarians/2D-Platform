using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public float speed;
    public float waitTime;
    public Transform[] movPos;//Ҫ�ƶ���λ��

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
        transform.position = Vector2.MoveTowards(transform.position, movPos[i].position, speed*Time.deltaTime);//�ӵ�ǰλ���ƶ���
        if(Vector2.Distance(transform.position, movPos[i].position)<0.1f)
        {
            if(waitTime<0.0f)//�ж�if waittimeС��0
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


    private void OnTriggerEnter2D(Collider2D other)//������player����ƽ̨�ƶ�
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")//j�����ϵ���ײ��
        {
            other.gameObject.transform.parent=gameObject.transform;//��player���ƽ̨���Ӷ���
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")//j�����ϵ���ײ��
        {
            other.gameObject.transform.parent = playerDefTransform;
        }
    }
}
