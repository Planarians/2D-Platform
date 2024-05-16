using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{

    public GameObject dialogBox;
    public Text dialogBoxText;
    public string signText;
    private bool isPlayerInSign;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&isPlayerInSign)
        {
            dialogBox.SetActive(true);//按下e 显示对话框
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("EnterSign");
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")//不加的话就会弹出三次 因为player有三个碰撞框
        {
            isPlayerInSign = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")//不加的话就会弹出三次 因为player有三个碰撞框
        {
            isPlayerInSign = false;
            dialogBox.SetActive(false);
        }
    }
}
