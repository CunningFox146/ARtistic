namespace ArPaint.Services.Commands
{
    public interface ICommand
    {
        void Perform();
        void Undo();
    }
}