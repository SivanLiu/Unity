using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
   //玩家对象
    private GameObject player = null;
    public Vector3 offset;
    float t;
    public Camera mainCamera;
    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset1, offset2;

    Vector3 targetPos;
    void Start()
    {
        // 查找玩家地实例对象
        this.player = GameObject.FindGameObjectWithTag("Player");
        targetPos = player.transform.position;
        this.offset1 = this.transform.position - targetPos;
    }
    void FixedUpdate()
    {
        if (target)
        {
            if (this.transform.position.y >= 4.5f)
            {
                this.transform.position = new Vector3(transform.position.x, 4.5f, transform.position.z);
            }
            if (transform.position.x < 0.5f)
            {
                this.transform.position = new Vector3(0.5f, transform.position.y, transform.position.z);
            }
            if (transform.position.y <= 0.25f)
            {
                this.transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
            }
            
            if (transform.position.x <= 9.9f && transform.position.x >= 0.5f)
                {
                    Vector3 posNoZ = transform.position;
                    posNoZ.z = target.transform.position.z + 0.05f;

                    Vector3 targetDirection = (target.transform.position - posNoZ);

                    interpVelocity = targetDirection.magnitude * 30f;

                    targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

                    transform.position = Vector3.Lerp(transform.position, targetPos + offset1 + new Vector3(0, 0.05f, 0), 0.2f);
                }
            if (player.transform.position.x >= 9.9f && player.transform.position.x <= 50.1f)
            {
                this.transform.position = new Vector3(player.transform.position.x + 0.5f,
                this.transform.position.y-0.3f, this.transform.position.z);
            }

        }
    }
}