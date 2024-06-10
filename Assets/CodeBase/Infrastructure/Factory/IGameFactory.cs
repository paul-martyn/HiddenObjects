using System.Collections.Generic;
using CodeBase.Services;
using CodeBase.Services.Progress;
using CodeBase.UI.Buttons.LevelButton;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        public LevelButton CreateLevelButton(RectTransform parent);
        public Level CreateLevel();
    }
}