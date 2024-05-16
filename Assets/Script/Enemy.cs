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

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();//��ͨ��tag�ҵ�player���� �ٻ�ȡ�������
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Instantiate(dropCoin,transform.position, Quaternion.identity);//������
            Destroy(gameObject);
        }
    }


    public void TakeDamage(int damage)//��player���õ�
    {
        GameObject gb= Instantiate(floatPoint,transform.position, Quaternion.identity)as GameObject;//����һ���˺����ָ�����Ч
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();//�������Ӷ����textֵ�ĳ��յ��Ĺ���ֵ
        health -= damage;
        FlashColor(flashTime);
        Instantiate(bloodEffect,transform.position, Quaternion.identity);//������Ѫ������Ч
        GameController.camShake.Shake();//�����
    }


    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);//�����������ɫ��ԭ����
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
