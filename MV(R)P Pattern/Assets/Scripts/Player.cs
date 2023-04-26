using UniRx;
using UnityEngine;

public class Player : MonoBehaviour {
    #region Variable
    private readonly IntReactiveProperty _hp = new IntReactiveProperty(100);
    public IReadOnlyReactiveProperty<int> hp => _hp;
    #endregion

    #region Life Cycle
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Enemy") {
            Debug.Log("Ãæµ¹");

            if (_hp.Value > 0) _hp.Value -= 10;
            else _hp.Dispose();
        }
    }
    #endregion
}
