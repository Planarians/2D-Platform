using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;




public class ThrowBomb : MonoBehaviour
{

    public GameObject bomb;

    private PlayerInputActions controls;
    private Vector2 move;

    private void Awake()
    {
        controls = new PlayerInputActions();
        controls.GamePlay.Bomb.started += ctx => Bomb();
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
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.O))
        //{
          
        //}
    }

   void Bomb()
    {
        Instantiate(bomb, transform.position, transform.rotation);
    }
}
