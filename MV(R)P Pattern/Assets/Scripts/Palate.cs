using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Palate : MonoBehaviour
{
    // 원래는 private이지만 View <-> Model을 위해 public. 사실상 이렇게 되면
    // IReadOnlyReactiveProperty는 쓸모가 없음.
    public readonly ColorReactiveProperty _color = new ColorReactiveProperty();
    public IReadOnlyReactiveProperty<Color> color => _color;
}
