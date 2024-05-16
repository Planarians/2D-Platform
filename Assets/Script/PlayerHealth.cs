using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    public int blinks;
    public float time;
    public float dieTime;
    public float hitBoxCdTime;//多久显示多边形碰撞箱

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
            //rb2d.velocity=new Vector2(0,0);//把刚体速度为0 防止尸体掉下来
            //rb2d.gravityScale = 0.0f;//把刚体重力为0 防止尸体掉下来
            GameController.isGameAlive = false;
            anim.SetTrigger("Die");
            Invoke("KillPlayer", dieTime);//经过dietime之后再调用killplayer方法删除player不然马上删除不会播放死亡动画
        }
        BlinkPlayer(blinks, time);
        PolygonCollider2D.enabled = false;//过一段时间显示多边形碰撞箱 这样能掉到地刺每隔一段时间受到伤害
        StartCoroutine(ShowPlayerHitBox());
    }

    IEnumerator ShowPlayerHitBox()//过一段时间显示多边形碰撞箱 这样能掉到地刺每隔一段时间受到伤害
    {
        yield return new WaitForSeconds(hitBoxCdTime);
        PolygonCollider2D.enabled=true;
    }

    void KillPlayer()
    {
        Destroy(gameObject);

    }

    void BlinkPlayer(int numBlinks, float seconds)//受伤闪烁
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
