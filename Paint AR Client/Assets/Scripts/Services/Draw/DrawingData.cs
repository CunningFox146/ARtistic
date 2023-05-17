using System.Collections.Generic;
using ArPaint.Services.Commands;

namespace ArPaint.Services.Draw
{
    public class DrawingData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SerializableDrawCommand> DrawCommands { get; set; }
    }
}