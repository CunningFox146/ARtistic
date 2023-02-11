namespace ArPaint.Services.Commands
{
    public interface ICommandBuffer
    {
        void AddCommand(ICommand command);
        void UndoLastCommand();
    }
}