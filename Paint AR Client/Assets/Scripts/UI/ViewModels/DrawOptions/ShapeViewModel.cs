using System;
using ArPaint.Services.Draw.Shapes;
using UnityMvvmToolkit.Common.Interfaces;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.ViewModels.DrawOptions
{
    public class ShapeViewModel : ICollectionItem
    {
        private readonly Shape _shape;
        
        [Observable(nameof(Name))] 
        private readonly IReadOnlyProperty<string> _name;
        
        [Observable(nameof(IsSelected))]
        private readonly IProperty<bool> _isSelected = new Property<bool>();

        private Action<Shape> _onSelect;

        public ICommand SelectShapeCommand { get; }

        public Shape Shape => _shape;
        public string Name => _name.Value;
        
        public bool IsSelected
        {
            get => _isSelected.Value;
            set => _isSelected.Value = value;
        }
        
        public int Id { get; }

        public ShapeViewModel(Shape shape, Action<Shape> onSelect)
        {
            _onSelect = onSelect;
            Id = Guid.NewGuid().GetHashCode();
            
            _name = new ReadOnlyProperty<string>(shape.Name);
            _shape = shape;

            SelectShapeCommand = new Command(SelectShape);
        }

        private void SelectShape()
            => _onSelect?.Invoke(_shape);
    }
}