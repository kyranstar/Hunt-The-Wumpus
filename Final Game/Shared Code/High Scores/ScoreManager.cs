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
            SortScores();
        }

        public void Save()
        {
            SortScores();
            using (Stream Stream = FileUtils.GetFileStream(ScoreFile))
            {
                Serializer.Serialize(Stream, _Scores.ToArray());
            }
        }

        private void ValidateScores()
        {
            if (_Scores == null)
                throw new Exception("This operation cannot be performed until scores have been loaded.");
        }

        private void SortScores()
        {
            if(_Scores != null)
                _Scores.Sort((a, b) => b.Score.CompareTo(a.Score));
        }

        public void AddScore(ScoreEntry Score)
        {
            ValidateScores();

            _Scores.Add(Score);
            SortScores();
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
