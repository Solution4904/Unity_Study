using TMPro;
using UniRx;
using UnityEngine;

public class PalatePrecenter : MonoBehaviour {
    #region Variable
    private Palate palate;
    private PalateSlider palateSlider;
    private TextMeshProUGUI palateText;
    #endregion

    #region Life Cycle
    private void Awake() {
        palate = null ?? FindObjectOfType<Palate>();
        palateSlider = null ?? FindObjectOfType<PalateSlider>();
        palateText = null ?? FindObjectOfType<TextMeshProUGUI>();
    }

    private void Start() {
        // # View -> Model
        palateSlider.color.Subscribe(x => {
            palate._color.Value = x;
        }).AddTo(this);

        // # Model -> View
        palate.color.Subscribe(x => {
            palateText.text = x.ToString();
            palateText.color = x;

            palateSlider._color.Value = x;
        }).AddTo(this);
    }
    #endregion
}
