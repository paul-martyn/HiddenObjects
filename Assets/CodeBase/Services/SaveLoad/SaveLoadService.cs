using CodeBase.Data;
using CodeBase.Data.Progress;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.Progress;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        
        private readonly IProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgressWriter progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
            Debug.Log($"Saved progress JSON: {_progressService.Progress.ToJson()}");
        }
        
        public PersistentProgress LoadProgress()
        {
            Debug.Log($"LoadProgress...");
            return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PersistentProgress>();
        }
    }
}