using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAttack : MonoBehaviour
{


    public int damage;
    public float startTime;//��ú���ֹ�����ײ��
    public float time;//����󹥻���ײ�����ʧ

    private Animator anim;
    private PolygonCollider2D collider2D;

    private PlayerInputActions controls;
    private Vector2 move;

    private void Awake()
    {
        controls = new PlayerInputActions();
        controls.GamePlay.Attack.started += ctx => Attack();//�󶨵�jump���� һ����ȥ�ʹ���
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
        collider2D.enabled = true;//����ײ���Ϊtrue
        StartCoroutine(disableHitBox());
    }
    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        collider2D.enabled = false;//����ײ���Ϊtrue
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);//uʵ�����EnemyBatҪдenemy ��һ����������enemybat�̳�enemy
        }
    }
}
