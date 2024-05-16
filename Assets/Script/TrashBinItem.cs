using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinItem : MonoBehaviour
{

    private bool isPlayerInTrashBin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (isPlayerInTrashBin)
            {
                if(CoinUI.CurrentCoinQuantity > 0)
                {
                    SoundManager.PlayThrowCoinClip();
                    TrashBinCoin.coinCurrent++;
                    CoinUI.CurrentCoinQuantity--;
                }
            }
        }
    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("EnterSign");
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")//不加的话就会弹出三次 因为player有三个碰撞框
        {
            isPlayerInTrashBin = true;
            Debug.Log("isPlayerInTrashBin-"+isPlayerInTrashBin);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")//不加的话就会弹出三次 因为player有三个碰撞框
        {
            isPlayerInTrashBin = false;
            Debug.Log("isPlayerInTrashBin-" + isPlayerInTrashBin);

        }
    }
}
