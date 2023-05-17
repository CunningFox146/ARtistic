using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArPaint.Services.Commands
{
    public class CommandBuffer : ICommandBuffer
    {
        private readonly Stack<ICommand> _commands = new();
        private readonly Stack<ICommand> _discardedCommands = new();

        public void AddCommand(ICommand command, bool noPreform = false)
        {
            if (!noPreform)
                command.Perform();
            
            _commands.Push(command);
        }
        
        public void UndoLastCommand()
        {
            if (!_commands.TryPop(out var command)) return;
            command.Undo();
            _discardedCommands.Push(command);
        }

        public void RedoLastCommand()
        {
            if (!_discardedCommands.TryPop(out var command)) return;
            AddCommand(command);
        }

        public string SerializeDrawCommands()
        {
            var commands = new List<SerializableDrawCommand>();

            foreach (var command in _commands)
            {
                if (command is DrawCommand drawCommand)
                    commands.Add(drawCommand);   
            }

            return JsonConvert.SerializeObject(commands);
        }
    }
}