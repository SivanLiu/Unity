using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour {

	public float moveSpeed;
	private Vector3 moveDirection;
	public float turnSpeed;
	// Use this for initialization
	void Start () {
		//指向朝右的方向
		moveDirection = Vector3.right;
	}
	
	// Update is called once per frame
	void Update () {
	
		//获取僵尸当前的位置
		Vector3 currentPosition = transform.position;
		if(Input.GetButton("Fire1"))
		{
			//转换鼠标当前位置为世界坐标系
			Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//移动的方向就是目标位置减去僵尸目前的位置
			moveDirection = moveToward-currentPosition;
			//因为是2D的，所以无需改变Z轴的位置，直接设为0
			moveDirection.z=0;
			//将moveDirection变为长度为1的单位长度
			moveDirection.Normalize();
			//让僵尸跟随鼠标走路的效果，从当前位置移动到目标位置
			Vector3 target = moveDirection * moveSpeed + currentPosition;
			//计算当前位置与目标位置之前路径上僵尸的新位置。
			transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
		}

		//x轴与moveDirection之间的角度
		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		//Quaternion.Slerp来转向所计算的目标角度,Quaternion.Euler方法可以从一个欧拉角获得四元数，其包含单独的x,y,z的旋转角度
		transform.rotation =
			Quaternion.Slerp( transform.rotation,
			                 Quaternion.Euler( 0, 0, targetAngle ),
			                 turnSpeed * Time.deltaTime );
	}
}
