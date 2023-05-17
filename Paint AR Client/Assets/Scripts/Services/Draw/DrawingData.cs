using System;
using System.Collections.Generic;
using ArPaint.Services.Commands;

namespace ArPaint.Services.Draw
{
    public class DrawingData
    {
        public event Action ItemUpdate;

        private string _name;
        private string _description;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                ItemUpdate?.Invoke();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                ItemUpdate?.Invoke();
            }
        }
        public List<SerializableDrawCommand> DrawCommands { get; set; }
    }
}