using TMPro;
using UnityEngine;

public class StateMachine {

    public IState CurrentState { get; private set; }
    private Rigidbody rigidbody { get; set; }
    private TMP_Text stateText;
    private readonly string STATE_TEXT = "STATE TEXT";

    public StateMachine(IState defalutState, Rigidbody rigidbody) {
        stateText = null ?? GameObject.Find(STATE_TEXT).GetComponent<TMP_Text>();
        CurrentState = defalutState;
        this.rigidbody = rigidbody;
    }

    public void SetState(IState state) {
        CurrentState.OperateExit();

        CurrentState = state;
        Debug.Log($"{CurrentState} Set {state}");
        stateText.text = CurrentState.ToString();

        CurrentState.OperateEnter(rigidbody);
    }

    public void DoOperateUpdate() => CurrentState.OperateUpdate();
}
