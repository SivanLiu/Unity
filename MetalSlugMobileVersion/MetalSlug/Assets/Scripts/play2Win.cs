using UnityEngine;
using System.Collections;

public class play2Win : MonoBehaviour {
    private GameObject player;

    bool nextLevel = false;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

	}
	// Update is called once per frame
	void Update () {
        nextLevel = true;
        foreach (Collider co in Physics.OverlapSphere(player.transform.position, 100))
        {
            if (co.gameObject.tag == "Enemy1" || co.gameObject.tag == "Enemy2" ||
                co.gameObject.tag == "Enemy3" || co.gameObject.tag == "Jet" || co.gameObject.tag == "Tank")
            {
                nextLevel = false;
            }
        }
        if (nextLevel == true&&(player.transform.position.x >= this.transform.position.x))
        {
            Application.LoadLevel("wingame3");
        }
	}
}
