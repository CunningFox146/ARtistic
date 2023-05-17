using System.Collections.Generic;

namespace ArPaint.Services.Commands
{
    public interface ICommandBuffer
    {
        void AddCommand(ICommand command, bool noPerform = false);
        void UndoLastCommand();
        void RedoLastCommand();
        List<SerializableDrawCommand> SerializeDrawCommands();
    }
}