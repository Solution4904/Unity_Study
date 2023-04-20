using UnityEngine;

public interface IState {
    void OperateEnter(Rigidbody rigidbody);
    void OperateUpdate();
    void OperateExit();
}
