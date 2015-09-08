using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour {
    protected virtual void Initialize() { }
    protected virtual void FSMUpdate() { }
    protected virtual void FSMFixedUpdate() { }

    void Start()
    {
        //初始化
        Initialize();
    }

    void Update()
    {
        //每帧更新FSM
        FSMUpdate();
    }
    void FixedUpdate()
    {
        //以固定时间周期更新FSM
        FSMFixedUpdate();
    }
}
