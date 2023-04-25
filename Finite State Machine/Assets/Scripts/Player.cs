using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    #region Variable
    /// <summary>
    /// Player 상태 별 enum 선언
    /// </summary>
    public enum ePlayerState {
        Idle,
        Run,
        Jump,
        Dead
    }

    private StateMachine stateMachine;
    private Dictionary<ePlayerState, IState> playerStateDic = new Dictionary<ePlayerState, IState>();
    private bool isGrounded = false;
    #endregion

    #region Life Cycle

    private void Awake() {
        AddStateToDic();

        stateMachine = new StateMachine(playerStateDic[ePlayerState.Idle], GetComponent<Rigidbody>());
    }

    private void Update() {
        KeyboardInputReceiver();
    }

    private void FixedUpdate() {
        stateMachine.DoOperateUpdate();
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
    #endregion

    #region Essential Function
    /// <summary>
    /// 상태 별 State를 Dictinary에 추가시켜 둠.
    /// </summary>
    private void AddStateToDic() {
        playerStateDic.Add(ePlayerState.Idle, new StateIdle());
        playerStateDic.Add(ePlayerState.Run, new StateRun());
        playerStateDic.Add(ePlayerState.Jump, new StateJump());
        playerStateDic.Add(ePlayerState.Dead, new StateDead());
    }
    #endregion

    #region Definition Function
    /// <summary>
    /// 키보드 입력에 따른 State 제어
    /// </summary>
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
    #endregion

}
