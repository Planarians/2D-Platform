using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : MonoBehaviour
{

    public float speed;
    public int damage;
    public float rotateSpeed;
    public float tuning;//微调
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
        rb2d.velocity = transform.right * speed;//给一个初始往右的速度
        startSpeed = rb2d.velocity;
        playerTransform=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        stickleTransform =GetComponent<Transform>();
        camShake=GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

  
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed);//给回旋飘一个z轴旋转速度
        float y = Mathf.Lerp(transform.position.y, playerTransform.position.y, tuning);//让回旋镖坐标的y跟着player走
        transform.position = new Vector3(transform.position.x, y, 0.0f);
        rb2d.velocity = rb2d.velocity - startSpeed * Time.deltaTime;    //让速度一直减小 让回旋飘返回


        if (Mathf.Abs(transform.position.x - playerTransform.position.x) < 0.5f)//如果距离的绝对值小于0.5 让回旋飘消失
        {
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))//对enemy造成伤害
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
