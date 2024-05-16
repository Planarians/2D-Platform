using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public RectTransform UI_Element;
    public RectTransform CanvasREct;
    public Transform trashBinPos;//����Ͱ����������
    public float xOffset;
    public float yOffset;
    public Text coinNumber;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 viewportPos=Camera.main.WorldToViewportPoint(trashBinPos.position);//��������ת�ӿ�����
        Vector2 worldObjectScreenPos = new Vector2((viewportPos.x * CanvasREct.sizeDelta.x) -
                           (CanvasREct.sizeDelta.x * 0.5f) + xOffset,
                           (viewportPos.y * CanvasREct.sizeDelta.y) -
                           (CanvasREct.sizeDelta.y * 0.5f) + yOffset);
        UI_Element.anchoredPosition = worldObjectScreenPos;

    }
}