using System.Collections.Generic;

namespace ArPaint.Services.Commands
{
    public class CommandBuffer : ICommandBuffer
    {
        private readonly Stack<ICommand> _commands;

        public CommandBuffer()
        {
            _commands = new Stack<ICommand>();
        }

        public void AddCommand(ICommand command)
        {
            command.Perform();
            _commands.Push(command);
        }

        public void UndoLastCommand()
        {
            if (!_commands.TryPop(out var command)) return;
            command.Undo();
        }
    }
}