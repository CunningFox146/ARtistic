using System;
using ArPaint.Services.Draw.Shapes;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Common.Interfaces;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace ArPaint.UI.ViewModels.Draw
{
    public class ShapeViewModel : ICollectionItem
    {
        [Observable(nameof(Name))] 
        private readonly IReadOnlyProperty<string> _name;
        
        [Observable(nameof(IsSelected))]
        private readonly IProperty<bool> _isSelected;
        
        [Observable(nameof(Icon))]
        private readonly IProperty<VectorImage> _icon;

        private readonly Action<Shape> _onSelect;

        public ICommand SelectShapeCommand { get; }

        public Shape Shape { get; }

        public string Name => _name.Value;
        
        public bool IsSelected
        {
            get => _isSelected.Value;
            set => _isSelected.Value = value;
        }
        
        public VectorImage Icon
        {
            get => _icon.Value;
            set => _icon.Value = value;
        }
        
        public int Id { get; }

        public ShapeViewModel(Shape shape, Action<Shape> onSelect)
        {
            _onSelect = onSelect;
            Id = Guid.NewGuid().GetHashCode();

            _isSelected = new Property<bool>();
            _icon = new Property<VectorImage>();
            _name = new ReadOnlyProperty<string>(shape.Name);
            
            Shape = shape;
            Icon = Shape.Icon;

            SelectShapeCommand = new Command(SelectShape);
        }

        private void SelectShape()
            => _onSelect?.Invoke(Shape);
    }
}