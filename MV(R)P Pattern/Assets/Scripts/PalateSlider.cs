using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PalateSlider : MonoBehaviour {
    // ������ private������ View <-> Model�� ���� public. ��ǻ� �̷��� �Ǹ�
    // IReadOnlyReactiveProperty�� ���� ����.
    public readonly ColorReactiveProperty _color = new ColorReactiveProperty();
    public IReadOnlyReactiveProperty<Color> color => _color;
    private Color colorContainer = new Color(1, 1, 1, 1);

    [SerializeField] private Slider redSlider;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private Slider blueSlider;

    private void Start() {
        _color.Subscribe(c => {
            redSlider.value = c.r;
            greenSlider.value = c.g;
            blueSlider.value = c.b;
        }).AddTo(this);

        Observable.Merge(
            redSlider.OnValueChangedAsObservable(),
            greenSlider.OnValueChangedAsObservable(),
            blueSlider.OnValueChangedAsObservable())
            .Subscribe(c => {
                colorContainer.r = redSlider.value;
                colorContainer.g = greenSlider.value;
                colorContainer.b = blueSlider.value;

                _color.Value = colorContainer;
            }).AddTo(this);
    }
}