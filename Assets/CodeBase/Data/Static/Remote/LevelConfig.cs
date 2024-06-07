using System;

namespace CodeBase.Data.Static.Remote
{
    [Serializable]
    public class LevelConfig
    {
        public int ID;
        public string Name;
        public string PreviewImageURL;
        public string LevelImageURL;
        public int TargetProgress;

        public void Log()
        {
            UnityEngine.Debug.Log(
                message: $"ID = {ID}" + "\n" +
                         $"Name = {Name}" + "\n" +
                         $"PreviewImageURL = {PreviewImageURL}" + "\n" +
                         $"LevelImageURL = {LevelImageURL}" + "\n" +
                         $"TargetProgress = {TargetProgress}" + "\n"
            );
        }
    }
}