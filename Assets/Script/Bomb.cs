using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public GameObject explosionRange;

    public Vector2 startSpeed;
    public float destroyBombTime;
    public float delayExplodeTime;
    public float hitBoxTime;//多久产生炸弹碰撞箱

    private Rigidbody2D rb2d;
    private Animator anim;
   

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb2d.velocity=transform.right*startSpeed.x+transform.up*startSpeed.y;//向右上方丢出去

        Invoke("Explode", delayExplodeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        anim.SetTrigger("Explode");
        Invoke("GenExplosionRange", hitBoxTime);
        Invoke("DestroyThisBomb", destroyBombTime);

    }

    void GenExplosionRange()
    {
        Instantiate(explosionRange,transform.position, Quaternion.identity);
    }
    void DestroyThisBomb()
    {
        Destroy(gameObject);
    }
}
