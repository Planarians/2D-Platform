using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damage;

    public float flashTime;
    public GameObject bloodEffect;
    public GameObject dropCoin;
    public GameObject floatPoint;

    private SpriteRenderer sr;
    private Color originalColor;
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    public void Start()
    {

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();//先通过tag找到player对象 再获取他的组件
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Instantiate(dropCoin,transform.position, Quaternion.identity);//掉落金币
            Destroy(gameObject);
        }
    }


    public void TakeDamage(int damage)//给player调用的
    {
        GameObject gb= Instantiate(floatPoint,transform.position, Quaternion.identity)as GameObject;//生成一个伤害数字浮动特效
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();//把他的子对象的text值改成收到的攻击值
        health -= damage;
        FlashColor(flashTime);
        Instantiate(bloodEffect,transform.position, Quaternion.identity);//生成流血粒子特效
        GameController.camShake.Shake();//相机震动
    }


    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);//调用下面的颜色还原方法
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Player")&& other.GetType().ToString()== "UnityEngine.CapsuleCollider2D")
        {

            if(playerHealth!=null)
            {
                playerHealth.DamahePlayer(damage);
            }
        }
    }
}
