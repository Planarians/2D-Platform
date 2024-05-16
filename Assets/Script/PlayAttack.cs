using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAttack : MonoBehaviour
{


    public int damage;
    public float startTime;//多久后出现攻击碰撞箱
    public float time;//几秒后攻击碰撞箱会消失

    private Animator anim;
    private PolygonCollider2D collider2D;

    private PlayerInputActions controls;
    private Vector2 move;

    private void Awake()
    {
        controls = new PlayerInputActions();
        controls.GamePlay.Attack.started += ctx => Attack();//绑定到jump函数 一按下去就触发
    }

    private void OnEnable()
    {
        controls.GamePlay.Enable();
    }
    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }



    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        collider2D=GetComponent<PolygonCollider2D>();
            
     }

    // Update is called once per frame
    void Update()
    {
        //Attack();
    }

    void Attack()
    {
        //if(Input.GetButtonDown("Attack"))
        {
            anim.SetTrigger("Attack");
            StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        collider2D.enabled = true;//把碰撞框改为true
        StartCoroutine(disableHitBox());
    }
    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        collider2D.enabled = false;//把碰撞框改为true
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);//u实际情况EnemyBat要写enemy 做一个抽象类让enemybat继承enemy
        }
    }
}
