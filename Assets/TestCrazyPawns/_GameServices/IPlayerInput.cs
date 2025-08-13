using System;

namespace TestCrazyPawns._GameServices
{
    public interface IPlayerInput
    {
        public event Action onStartDrag;
        public event Action onEndDrag;
        public event Action onDrag;
        public event Action onClick;
        public event Action<float> onScroll;
    
        public bool IsEnabled { get; set; }
    }
}
