using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public enum ePlayerState {
        Idle,
        Run,
        Jump,
        Dead
    }

    private StateMachine stateMachine;
    private Rigidbody rigidbody = null;
    private Dictionary<ePlayerState, IState> playerStateDic = new Dictionary<ePlayerState, IState>();
    private bool isGrounded = false;

    private void Awake() {
        rigidbody = null ?? gameObject.GetComponent<Rigidbody>();

        playerStateDic.Add(ePlayerState.Idle, new StateIdle());
        playerStateDic.Add(ePlayerState.Run, new StateRun());
        playerStateDic.Add(ePlayerState.Jump, new StateJump());
        playerStateDic.Add(ePlayerState.Dead, new StateDead());

        stateMachine = new StateMachine(playerStateDic[ePlayerState.Idle], rigidbody);
    }

    private void Start() {

    }

    private void Update() {
        KeyboardInputReceiver();
    }

    private void FixedUpdate() {
        stateMachine.DoOperateUpdate();
    }


    private void KeyboardInputReceiver() {
        if (stateMachine.CurrentState == playerStateDic[ePlayerState.Dead]) return;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            stateMachine.SetState(playerStateDic[ePlayerState.Run]);

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (stateMachine.CurrentState == playerStateDic[ePlayerState.Jump]
                || !isGrounded) return;
            stateMachine.SetState(playerStateDic[ePlayerState.Jump]);
            isGrounded = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) return;
            stateMachine.SetState(playerStateDic[ePlayerState.Idle]);
        }
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision == null) return;

        switch (collision.gameObject.name) {
            case "Enemy":
                stateMachine.SetState(playerStateDic[ePlayerState.Dead]);
                break;
            case "Plane":
                stateMachine.SetState(playerStateDic[ePlayerState.Idle]);
                isGrounded = true;
                break;
            default: break;
        }
    }
}
