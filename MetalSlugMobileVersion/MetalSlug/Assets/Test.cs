using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
    private int groundLayerMask;
    private bool isGround = false;
	// Use this for initialization
	void Start () {

        groundLayerMask = LayerMask.GetMask("Ground");
        
	}
	
	// Update is called once per frame
	void Update () {
       
        RaycastHit hitInfo;
        //isGround = (Physics.Raycast(transform.position + Vector3.down * raySpeedRate+new Vector3(rayx,0,0),
        //   Vector3.down, out hitInfo, rayLength, groundLayerMask));
        Vector3 rayOrigin = GetComponent<Collider>().bounds.center;
        if (transform.localScale.x == 1)
        {
            rayOrigin.x -= 0.5f;
        }
        else if(transform.localScale.x==-1){
            rayOrigin.x += 0.5f;
        }
        
        
        float rayDistance = GetComponent<Collider>().bounds.extents.y + 0.2f;
        isGround = Physics.Raycast(rayOrigin, Vector3.down, out hitInfo, rayDistance, groundLayerMask);
        Debug.DrawLine(transform.position, hitInfo.point, Color.red, 1);
	}
}
