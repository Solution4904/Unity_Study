using UniRx;
using UnityEngine;

public class Player : MonoBehaviour {
    private readonly IntReactiveProperty _hp = new IntReactiveProperty(100);
    public IReadOnlyReactiveProperty<int> hp => _hp;


    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Enemy") {
            Debug.Log("�浹");
            _hp.Value -= 10;
        }
    }

    private void OnDestroy() {
        _hp.Dispose();
    }
}
