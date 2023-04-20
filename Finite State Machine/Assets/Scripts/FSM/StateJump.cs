using UnityEngine;

public class StateJump : IState {
    private Rigidbody rigidbody = null;

    public void OperateEnter(Rigidbody rigidbody) {
        this.rigidbody = null ?? rigidbody;

        this.rigidbody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
    }

    public void OperateExit() {

    }

    public void OperateUpdate() {

    }
}
