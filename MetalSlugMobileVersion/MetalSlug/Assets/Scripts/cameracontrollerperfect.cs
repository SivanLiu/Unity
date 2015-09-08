using UnityEngine;
using System.Collections;

public class cameracontrollerperfect : MonoBehaviour
{
    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset1, offset2;
    Vector3 targetPos;
    private float timer = 0;
    void Start()
    {
        targetPos = transform.position;
        this.offset1 = this.transform.position - targetPos;
    }

    void Update()
    {
    }
    void FixedUpdate()
    {
        if (target)
        {
            if (target.transform.position.x <= 13.7f && target.transform.position.x >= 0.71f)
            {
                this.transform.position = new Vector3(target.transform.position.x + this.offset1.x,
              0.02f, this.transform.position.z);
            }
            if (target.transform.position.x >= 13.7f && target.transform.position.x <= 21.4f)
            {
         
                Vector3 posNoZ = transform.position;
                posNoZ.z = target.transform.position.z;

                Vector3 targetDirection = (target.transform.position - posNoZ);

                interpVelocity = targetDirection.magnitude * 30f;

                targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

                transform.position = Vector3.Lerp(transform.position, targetPos + offset2, 0.8f);
            }
           
        }
    }

}
