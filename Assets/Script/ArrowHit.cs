using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : MonoBehaviour
{

    public GameObject arrowPrefab;
    private PlayerInputActions controls;
    private Vector2 move;

    private void Awake()
    {
        controls = new PlayerInputActions();
        controls.GamePlay.Arrow.started += ctx => Shoot();
    }

    private void OnEnable()
    {
        controls.GamePlay.Enable();
    }
    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
     
        //if (input.getkeydown(keycode.t))
        //{
        //    instantiate(arrowprefab, transform.position, transform.rotation);
        //}
    }

    void Shoot()
    {
        //Debug.Log("arrow");
        Instantiate(arrowPrefab,transform.position,transform.rotation);
    }
}
