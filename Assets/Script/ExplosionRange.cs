using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionRange : MonoBehaviour
{

    public int damage;
    public float destroyTime;

    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        Destroy(gameObject, destroyTime);//����һ˲��������ʧ ��Ȼը�����������˺�
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);//uʵ�����EnemyBatҪдenemy ��һ����������enemybat�̳�enemy
        }


        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.DamahePlayer(damage);
            }
        }
    }

}
