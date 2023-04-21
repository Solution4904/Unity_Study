using UniRx;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour {
    private Player player;
    private PlayerState playerState;

    private void Awake() {
        player = null ?? FindObjectOfType<Player>();
        playerState = null ?? FindObjectOfType<PlayerState>();
    }

    private void Start() {
        player.hp.Subscribe(x => {
            playerState.SetValue(x);
        }).AddTo(this);
    }
}
