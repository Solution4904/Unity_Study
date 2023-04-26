using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Palate : MonoBehaviour
{
    public readonly ColorReactiveProperty _color = new ColorReactiveProperty();
    public IReadOnlyReactiveProperty<Color> color => _color;
}
