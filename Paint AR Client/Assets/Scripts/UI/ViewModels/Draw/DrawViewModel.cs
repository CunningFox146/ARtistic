﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArPaint.Services.Commands;
using ArPaint.Services.Draw;
using ArPaint.Services.Draw.Shapes;
using ArPaint.UI.Systems.Stack;
using ArPaint.UI.Views.DrawOptions;
using Services.StaticData;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;
using Command = UnityMvvmToolkit.Core.Command;
using ICommand = UnityMvvmToolkit.Core.Interfaces.ICommand;

namespace ArPaint.UI.ViewModels.Draw
{
    public class DrawViewModel : ViewModel
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly DrawService _drawService;
        
        [Observable(nameof(Shapes))]
        private readonly IReadOnlyProperty<ObservableCollection<ShapeViewModel>> _shapes;
        
        [Observable(nameof(IsShapeSelectVisible))]
        private readonly IProperty<bool> _isShapeSelectVisible;

        public ICommand ToggleShapeSelectCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }
        public ICommand OpenOptionsCommand { get; }
        public ObservableCollection<ShapeViewModel> Shapes => _shapes.Value;

        public bool IsShapeSelectVisible
        {
            get => _isShapeSelectVisible.Value;
            set => _isShapeSelectVisible.Value = value;
        }

        public DrawViewModel(IStaticDataService staticData, ICommandBuffer commandBuffer, DrawService drawService)
        {
            _commandBuffer = commandBuffer;
            _drawService = drawService;

            ToggleShapeSelectCommand = new Command(ToggleShapeSelect);
            UndoCommand = new Command(Undo);
            RedoCommand = new Command(Redo);
            OpenOptionsCommand = new Command(OpenOptions);
            _isShapeSelectVisible = new Property<bool>(false);
            _shapes = new ReadOnlyProperty<ObservableCollection<ShapeViewModel>>(new());
            
            InitShapes(staticData.Shapes.ShapesList);
        }

        private void OpenOptions()
        {
            ViewStack.PushView<DrawOptionsView>();
        }

        private void InitShapes(List<Shape> shapes)
        {
            foreach (var shape in shapes)
                Shapes.Add(new ShapeViewModel(shape, OnShapeSelected));
        }

        private void Undo()
        {
            _commandBuffer.UndoLastCommand();
        }

        private void Redo()
        {
            _commandBuffer.RedoLastCommand();
        }

        private void ToggleShapeSelect()
        {
            IsShapeSelectVisible = !IsShapeSelectVisible;
        }

        private void OnShapeSelected(Shape shape)
        {
            foreach (var shapeViewModel in Shapes)
            {
                shapeViewModel.IsSelected = shapeViewModel.Shape == shape;
            }

            _drawService.Shape = shape;
            IsShapeSelectVisible = false;
        }
    }
}