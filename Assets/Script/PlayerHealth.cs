using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    public int blinks;
    public float time;
    public float dieTime;
    public float hitBoxCdTime;//�����ʾ�������ײ��

    private Animator anim;
    private Renderer myRender;
    private ScreenFlash sf;
    private Rigidbody2D rb2d;
    private PolygonCollider2D PolygonCollider2D;


    // Start is called before the first frame update
    void Start()
    {
        HealthBar.HealthCurrent = health;
        HealthBar.HealthMax = health;
        myRender = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        sf = GetComponent<ScreenFlash>();
        rb2d = GetComponent<Rigidbody2D>();
        PolygonCollider2D = GetComponent<PolygonCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamahePlayer(int damage)
    {

        sf.FlashScreen();
        health-=(int)damage;
        
        if (health < 0)
        {
            health=0;
        }
        HealthBar.HealthCurrent = health;
        if(health <=0 )
        {
            //rb2d.velocity=new Vector2(0,0);//�Ѹ����ٶ�Ϊ0 ��ֹʬ�������
            //rb2d.gravityScale = 0.0f;//�Ѹ�������Ϊ0 ��ֹʬ�������
            GameController.isGameAlive = false;
            anim.SetTrigger("Die");
            Invoke("KillPlayer", dieTime);//����dietime֮���ٵ���killplayer����ɾ��player��Ȼ����ɾ�����Ქ����������
        }
        BlinkPlayer(blinks, time);
        PolygonCollider2D.enabled = false;//��һ��ʱ����ʾ�������ײ�� �����ܵ����ش�ÿ��һ��ʱ���ܵ��˺�
        StartCoroutine(ShowPlayerHitBox());
    }

    IEnumerator ShowPlayerHitBox()//��һ��ʱ����ʾ�������ײ�� �����ܵ����ش�ÿ��һ��ʱ���ܵ��˺�
    {
        yield return new WaitForSeconds(hitBoxCdTime);
        PolygonCollider2D.enabled=true;
    }

    void KillPlayer()
    {
        Destroy(gameObject);

    }

    void BlinkPlayer(int numBlinks, float seconds)//������˸
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }
    IEnumerator DoBlinks(int numBlinks,float seconds)
    {
        for(int i = 0; i < numBlinks*2; i++) {
            myRender.enabled=!myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }
}
