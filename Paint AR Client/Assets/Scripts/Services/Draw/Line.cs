using UnityEngine;
using Zenject;

namespace ArPaint.Services.Draw
{
    public class Line : MonoBehaviour
    {
        [field: SerializeField] public LineRenderer LineRenderer { get; private set; }

        public class Factory : PlaceholderFactory<Line>
        {
        }
    }
}