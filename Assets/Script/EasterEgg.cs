using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    public string easterEggPassword;
    public static string Password;

    public GameObject coin;
    public int coinQuantity;
    public float coinUpSpeed;
    public float intervalTime;//金币掉落间隔时间

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Password == easterEggPassword)
        {
            Debug.Log("egg");
        }
    }
}
