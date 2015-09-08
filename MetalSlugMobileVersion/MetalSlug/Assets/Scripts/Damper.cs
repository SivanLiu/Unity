using UnityEngine;
using System.Collections;

public class Damper : MonoBehaviour {

    public float speed = 2.0f;  //旋转速度
    public float angle = 0;     //旋转角度
    public bool isRotate = false;	// 是否开始旋转
    public bool is2Rotate = false;  //挡板收缩
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isRotate) {
            if (angle <= 130)
            {
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                angle += speed * Time.deltaTime;    //记录旋转角度
            }
            else {
                isRotate = false;   //当角度大于90度时，停止旋转
            }
          
        }
        if (is2Rotate)
        {
            if (angle <=260)
            {
                transform.Rotate(-Vector3.forward * (speed +10.0f) * Time.deltaTime);
                angle += speed * Time.deltaTime;    //记录旋转角度
            }
            else
            {
                is2Rotate = false;   //当角度大于度时，停止旋转
            }

        }
	}
   
    public void startRotate() {
        isRotate = true;
    }
    
}
