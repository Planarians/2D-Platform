using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float doubleJumpSpeed;
    public float climbSpeed;
    public float restoreTime;//player掉下去并且恢复原本层的时间

    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private SpriteRenderer sr;
    private bool isGround;
    private bool canDoubleJump;
    private Rigidbody2D rb2d;
    private bool isOneWayPlatform;
    private bool isLadder;
    private bool isClimbing;
    private bool isJumping;
    private bool isFalling;
    private bool isDoubleJumping;
    private bool isDoubleFalling;

    private float playerGravity;
    private Vector2 move;

    private void Awake()
    {
        controls = new PlayerInputActions();
        controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += ctx => move = Vector2.zero;
        controls.GamePlay.Jump.started += ctx => Jump();//绑定到jump函数 一按下去就触发
      
    }

    private PlayerInputActions controls;

    // Start is called before the first frame update

    private void OnEnable()
    {
        controls.GamePlay.Enable();
    }  
    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        playerGravity = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.isGameAlive)
        {
            CheckAirStatus();
            Flip();
            Run();
            //Jump();
            Climb();
            //Attack();
            CheckGrounded();
            CheckLadder();
            SwitchAnimation();
            OneWayPlatformCheck();
           
        }

       
    }

    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))
            ||myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"))
            ||myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        isOneWayPlatform= myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        //Debug.Log(isGround);
    }

    void CheckLadder()
    {
        isLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }

    //翻转功能  不然跑起来怎么都是向右的动画
    void Flip()
    {
        bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;//Mathf.Epsilon is a very small v
        if (plyerHasXAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)//if use 0 可能由于很小的摩擦力不停左右翻转
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);//默认不翻转
            }

            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                // sr.flipX = true; //second way
            }
        }
    }


    void Run()
    {

        //原生操作方法 现在不用了
        //float moveDir = Input.GetAxis("Horizontal");
        //Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        //myRigidbody.velocity = playerVel;
        //bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;//Mathf.Epsilon is a very small v
        ////if has v in x then plyerHasXAxisSpeed = true
        //myAnim.SetBool("Run", plyerHasXAxisSpeed);
        ////if plyerHasXAxisSpeed = true then run =true

        Vector2 playerVel = new Vector2(move.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;//Mathf.Epsilon is a very small v
        //if has v in x then plyerHasXAxisSpeed = true
        myAnim.SetBool("Run", plyerHasXAxisSpeed);

    }

    void Jump()
    {
        //if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                myAnim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);//x轴速度为0
                myRigidbody.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {

                    myAnim.SetBool("DoubleJump", true);
                    Vector2 doubleJumpVel = new Vector2(0.0f, doubleJumpSpeed);//x轴速度为0
                    myRigidbody.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                }
            }
        }
    }

    void Climb()
    {
        if(isLadder)
        {
            float moveY = Input.GetAxis("Vertical");//获取垂直方向速度
            if (moveY > 0.5f || moveY < -0.5f)
            {
                myAnim.SetBool("Climbing", true);
                myRigidbody.gravityScale = 0.0f;//重力为0 不然不能爬梯子
                myRigidbody.velocity=new Vector2(myRigidbody.velocity.x, moveY*climbSpeed);
            }
            else
            {
                if (isJumping || isFalling || isDoubleJumping || isDoubleFalling)//只是路过梯子
                {
                    myAnim.SetBool("Climbing", false);
                }
                else//停在梯子上
                {
                    myAnim.SetBool("Climbing", false);
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0.0f);
                }
            }
        }
        else
        {
            myAnim.SetBool("Climbing", false);
            myRigidbody.gravityScale = playerGravity;
        }
    }

    //void Attack()
    //{
    //    if(Input.GetButtonDown("Attack"))
    //    {
    //        myAnim.SetTrigger("Attack");
    //    }
    //}

    void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);
        if (myAnim.GetBool("Jump"))//已经在跳跃过程中
        {
            if (myRigidbody.velocity.y < 0.0f)//达到最高点 y轴速度为负
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }

        if (myAnim.GetBool("DoubleJump"))//已经在跳跃过程中
        {
            if (myRigidbody.velocity.y < 0.0f)//达到最高点 y轴速度为负
            {
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("DoubleFall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Idle", true);
        }
    }

    void OneWayPlatformCheck()
    {

        if (isGround && gameObject.layer != LayerMask.NameToLayer("Player"))//不知道有啥用 对我没用
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }

        float moveY = Input.GetAxis("Vertical");//获得y轴上的速度
        if (isOneWayPlatform && moveY < -0.1f)
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");//把player变成和onewayplatform一个层就没有碰撞了就会掉下来
            Invoke("RestorePlayerLayer", restoreTime);
        }
    }

    void RestorePlayerLayer()//用OneWayPlatformCheck()让player掉下来之后恢复player的层 不然会一直掉下去
    {
        if (!isGround && gameObject.layer != LayerMask.NameToLayer("Player"))//不知道有啥用 对我没用
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    void CheckAirStatus()
    {
        isJumping = myAnim.GetBool("Jump");
        isFalling = myAnim.GetBool("Fall");
        isDoubleJumping = myAnim.GetBool("DoubleJump");
        isDoubleFalling = myAnim.GetBool("DoubleFall");
        isClimbing = myAnim.GetBool("Climbing");

    }
}

