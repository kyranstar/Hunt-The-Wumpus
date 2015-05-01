using HuntTheWumpus.SharedCode.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace HuntTheWumpus.SharedCode.Scores
{
    public class ScoreManager
    {
        private const string ScoreFileName = "HighScores.xml";
        private readonly string ScoreFile = FileUtils.GetConfigPath(ScoreFileName);
        private List<ScoreEntry> _Scores = null;

        private readonly XmlSerializer Serializer = new XmlSerializer(typeof(ScoreEntry[]));

        public ScoreManager()
        {

        }

        public void Load()
        {
            using (Stream Stream = FileUtils.GetFileStream(ScoreFile))
            {
                if (Stream.Length > 0)
                    _Scores = ((ScoreEntry[])Serializer.Deserialize(Stream)).ToList();
                else _Scores = new List<ScoreEntry>();
            }
        }

        public void Save()
        {
            Serializer.Serialize(FileUtils.GetFileStream(ScoreFile), _Scores.ToArray());
        }

        private void ValidateScores()
        {
            if (_Scores == null)
                throw new Exception("This operation cannot be performed until scores have been loaded.");
        }

        public void AddScore(ScoreEntry Score)
        {
            ValidateScores();

            _Scores.Add(Score);
            _Scores.Sort((a, b) => a.Score.CompareTo(b.Score));
        }

        public ScoreEntry[] GetAllScores()
        {
            ValidateScores();
            return _Scores.ToArray();
        }

        public ScoreEntry[] Scores
        {
            get
            {
                return GetAllScores();
            }
        }
    }
}
