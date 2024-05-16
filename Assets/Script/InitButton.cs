using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InitButton : MonoBehaviour
{
    //让开始游戏菜单可以通过键盘选择按钮 并且鼠标点击了其他地方不会丢失选中
    private GameObject lastSelect;
    // Start is called before the first frame update
    void Start()
    {
        lastSelect = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {


        if (EventSystem.current.currentSelectedGameObject == null)
        {
            //Debug.Log(" lastSelect -null");
            EventSystem.current.SetSelectedGameObject(lastSelect);//   如果当前没有选中游戏对象就选中上一个对象
        }
        else
        {
            //Debug.Log(" lastSelect -null");
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
    }
}
