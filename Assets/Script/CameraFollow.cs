using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;//相机跟随对象（player
    public float smoothing;//相机移动的平滑因子 让相机移动更平滑

    public Vector2 minPosition;
    public Vector2 maxPosition;

    // Start is called before the first frame update
    void Start()
    {
        GameController.camShake=GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }

    private void LateUpdate()
    {
        if(target != null)
        {
            if(transform.position != target.position)
            {
                Vector3 targetPos= target.position; 
                targetPos.x=Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x); 
                targetPos.y=Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);//线性插值函数 起始位置 目标位置 到达
            }
        }
    }
   
    public void SetCamPosLimit(Vector2 minPos, Vector2 maxPos)
    {
        minPosition = minPos; 
        maxPosition = maxPos;
    }
}
