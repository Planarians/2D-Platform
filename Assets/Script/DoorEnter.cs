using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class DoorEnter : MonoBehaviour
{

    public Transform backDoor;
    private bool isDoor;
    private Transform PlayerTransform;

    private PlayerInputActions controls;
    private Vector2 move;
    private void Awake()
    {
        controls = new PlayerInputActions();
        controls.GamePlay.EnterDoor.started += ctx => EnterDoor();//绑定到jump函数 一按下去就触发
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
        PlayerTransform=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void EnterDoor()
    {
        if (isDoor)
        {
            PlayerTransform.position=backDoor.position;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isDoor = false;
        }
    }
}
