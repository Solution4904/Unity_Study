using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour {
    private Slider slider;

    private void Awake() {
        slider = null ?? GetComponent<Slider>();
    }

    private void Start() {
        slider.maxValue = 100;
        slider.value = slider.maxValue;
    }

    public void SetValue(int value) {
        slider.value = value;
    }
}
