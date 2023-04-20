using UnityEngine;

public class StateRun : IState {
    private Rigidbody rigidbody = null;
    private float horizontal = 0.0f;
    private Vector3 movement = Vector3.zero;

    public void OperateEnter(Rigidbody rigidbody) {
        this.rigidbody = null ?? rigidbody;

        if (Input.GetKey(KeyCode.LeftArrow)) horizontal = -1;
        else horizontal = 1;

        movement.Set(horizontal, 0, 0);
        movement = movement.normalized * 5 * Time.deltaTime;

        rigidbody.MovePosition(rigidbody.position + movement);
    }

    public void OperateExit() {

    }

    public void OperateUpdate() {

    }
}
