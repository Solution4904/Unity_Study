using UnityEngine;

public class StateMachine {

    public IState CurrentState { get; private set; }
    private Rigidbody rigidbody { get; set; }

    public StateMachine(IState defalutState, Rigidbody rigidbody) {
        CurrentState = defalutState;
        this.rigidbody = rigidbody;
    }

    public void SetState(IState state) {
        CurrentState.OperateExit();

        CurrentState = state;
        Debug.Log($"{CurrentState} Set {state}");

        CurrentState.OperateEnter(rigidbody);
    }

    public void DoOperateUpdate() => CurrentState.OperateUpdate();
}
