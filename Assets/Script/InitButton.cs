using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InitButton : MonoBehaviour
{
    //�ÿ�ʼ��Ϸ�˵�����ͨ������ѡ��ť ����������������ط����ᶪʧѡ��
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
            EventSystem.current.SetSelectedGameObject(lastSelect);//   �����ǰû��ѡ����Ϸ�����ѡ����һ������
        }
        else
        {
            //Debug.Log(" lastSelect -null");
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
    }
}
