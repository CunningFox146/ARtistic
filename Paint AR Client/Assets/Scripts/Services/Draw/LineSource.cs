using UnityEngine.Pool;
using Zenject;

namespace ArPaint.Services.Draw
{
    public class LineSource : ILineSource
    {
        private readonly IFactory<Line> _lineFactory;
        private readonly IObjectPool<Line> _linePool;

        public LineSource(Line.Factory lineFactory)
        {
            _linePool = new ObjectPool<Line>(
                lineFactory.Create,
                line => line.gameObject.SetActive(true),
                line => line.gameObject.SetActive(false)
            );
        }

        public Line Get()
        {
            return _linePool.Get();
        }

        public void Release(Line line)
        {
            line.Clear();
            _linePool.Release(line);
        }
    }
}