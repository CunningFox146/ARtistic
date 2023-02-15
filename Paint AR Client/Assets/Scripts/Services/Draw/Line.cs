using UnityEngine;
using Zenject;

namespace ArPaint.Services.Draw
{
    public class Line : MonoBehaviour
    {
        [field: SerializeField] public LineRenderer LineRenderer { get; private set; }

        public void Clear()
        {
            LineRenderer.positionCount = 0;
        }

        public class Factory : PlaceholderFactory<Line>
        {
        }
    }
}