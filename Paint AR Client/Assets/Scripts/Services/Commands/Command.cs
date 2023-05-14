using System;

namespace ArPaint.Services.Commands
{
    public class Command : ICommand
    {
        public Action PerformAction { get; set; }
        public Action UndoAction { get; set; }

        public void Perform()
        {
            PerformAction?.Invoke();
        }

        public void Undo()
        {
            UndoAction?.Invoke();
        }
    }
}