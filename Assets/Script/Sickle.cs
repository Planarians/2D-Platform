using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : MonoBehaviour
{

    public float speed;
    public int damage;
    public float rotateSpeed;
    public float tuning;//΢��
    public float waitTime;

    private Rigidbody2D rb2d;
    private Transform playerTransform;
    private Transform stickleTransform;
    private Vector2 startSpeed;

    private CameraShake camShake;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;//��һ����ʼ���ҵ��ٶ�
        startSpeed = rb2d.velocity;
        playerTransform=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        stickleTransform =GetComponent<Transform>();
        camShake=GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

  
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed);//������Ʈһ��z����ת�ٶ�
        float y = Mathf.Lerp(transform.position.y, playerTransform.position.y, tuning);//�û����������y����player��
        transform.position = new Vector3(transform.position.x, y, 0.0f);
        rb2d.velocity = rb2d.velocity - startSpeed * Time.deltaTime;    //���ٶ�һֱ��С �û���Ʈ����


        if (Mathf.Abs(transform.position.x - playerTransform.position.x) < 0.5f)//�������ľ���ֵС��0.5 �û���Ʈ��ʧ
        {
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))//��enemy����˺�
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
