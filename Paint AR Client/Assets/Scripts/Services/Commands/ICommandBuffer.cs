namespace ArPaint.Services.Commands
{
    public interface ICommandBuffer
    {
        void AddCommand(ICommand command, bool noPerform = false);
        void UndoLastCommand();
        void RedoLastCommand();
    }
}