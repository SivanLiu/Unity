using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour {

    public float speed = 60;    //旋转速度
    public bool isRotate = true;
	
	// Update is called once per frame
	void Update () {
        if (isRotate) {
            transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
        }
       
	}
    public void Stop() {
        isRotate = false;
    }
}
