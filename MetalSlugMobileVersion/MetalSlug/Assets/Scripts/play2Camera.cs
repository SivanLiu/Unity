using UnityEngine;
using System.Collections;

public class play2Camera : MonoBehaviour {

    //玩家对象
    private GameObject player = null;
    public Vector3 offset;
    float t;
    public Camera mainCamera;
    void Start()
    {

        // 查找玩家地实例对象
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.offset = this.transform.position - this.player.transform.position;
    }
    void Update()
    {
        if (this.transform.position.y < 4.35f) {
            if (this.transform.position.x < 14)
            {
                this.transform.position = new Vector3(player.transform.position.x + this.offset.x,
                this.transform.position.y, this.transform.position.z);
            }
            else if (this.transform.position.x >= 14 && this.transform.position.x < 18.1f)
            {
                this.transform.position = new Vector3(player.transform.position.x + this.offset.x,
              player.transform.position.y + 1.1f, this.transform.position.z);
            }
            else if (this.transform.position.x >= 18.1 && this.transform.position.x <= 21.3f)
            {
                this.transform.position = new Vector3(player.transform.position.x + this.offset.x,
              player.transform.position.y + 1.2f, this.transform.position.z);

            }
        }
    }
}
