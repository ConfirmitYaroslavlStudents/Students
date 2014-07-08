using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using Level;


namespace PacMan
{
    public class Game
    {
        private PacMan _pacMan;
        private Enemys _enemys;
        private Timer _enemyTimer;
        private Map _map;
        private int _currentLevel;
        private int _numberLevels;
        private enum _stepDirection
        {
            Down,
            Top,
            Right,
            Left
        }

        public Game(int currentLevel)
        {
            this._currentLevel = currentLevel;
            this._numberLevels = ConsiderNumberLevels();

            Level.Level LevelInfo = new Level.Level();
            LevelInfo = ReadNewLevelOnFile();

            _pacMan = new PacMan(LevelInfo.pacMan);
            Wall levelWall = new Wall();
            levelWall.WallCordinate = LevelInfo.wall;
            this._map = new Map(levelWall, LevelInfo.numberColumns, LevelInfo.numberLines);
            this._pacMan = new PacMan(LevelInfo.pacMan);
            EnemyArtificialIntelligence _enemyAI = new EnemyArtificialIntelligence(_map.LevelFood.FoodCoordinates);
            this._enemys = new Enemys(LevelInfo.enemys, _enemyAI);
            _enemyTimer = new Timer(MakeMoveEnemy, null, 0, GameSettings.EnemySpeed);
        }
        public PacMan PacMan
        {
            get { return _pacMan; }
        }
        public Enemys Enemys
        {
            get { return _enemys; }
        }
        public Map Map
        {
            get { return _map; }
        }
        public int CurrentLevel
        {
            get { return _currentLevel; }
            set { _currentLevel = value; }
        }

        public bool IsLevelComplete()
        {
            return _map.LevelFood.FoodCoordinates.Count == 0;
        }
        public bool IsWin()
        {
            return _currentLevel > _numberLevels;
        }
        public bool IsLoose()
        {
            foreach (Point enemyCoordinate in _enemys.EnemyCoordinates)
            {
                if (_pacMan.PacManCoordinate == enemyCoordinate)
                    return true;
            }
            return false;
        }
        public Level.Level ReadNewLevelOnFile()
        {
            var reader = new System.Xml.Serialization.XmlSerializer(typeof(Level.Level));
            var file = new System.IO.StreamReader("levels/" + _currentLevel.ToString() + ".xml");
            Level.Level newLevel = (Level.Level)reader.Deserialize(file);
            return newLevel;
        }
        public void MakeMovePacMan(Point newPacManLocation,Point eyeCoordinate, Point mouthCoordinate)
        {
            if (_map.IsOutOfWall(newPacManLocation) && IsNewPacManLocationInMap(newPacManLocation)) 
            {
                _pacMan.DoStep(newPacManLocation);
                _pacMan.EyeCoordinate = new Point(_pacMan.PacManCoordinate.X + eyeCoordinate.X, _pacMan.PacManCoordinate.Y + eyeCoordinate.Y);
                _pacMan.MouthCoordinate = mouthCoordinate;
                _pacMan.EatFood(_map.LevelFood);
            }
        }
        private void MakeMoveEnemy(object sender)
        {
            _enemys.MoveEnemy(_pacMan.PacManCoordinate);
        }
        private bool IsNewPacManLocationInMap(Point newLocation)
        {
            if (newLocation.X >= 0 && newLocation.X < _map.NumberColimns * GameSettings.WidthCell && newLocation.Y >= 0 && newLocation.Y < _map.NumberLines * GameSettings.HeightCell) 
            {
                return true;
            }
            return false;
        }
        private int ConsiderNumberLevels()
        {
            return new System.IO.DirectoryInfo("levels/").GetFiles("*.*", System.IO.SearchOption.AllDirectories).Length;
        }
        public void KeyDown(int directionNumber)
        {
            if (directionNumber == (int)_stepDirection.Left)
            {
                Point newPacManLocation = new Point(_pacMan.PacManCoordinate.X - _pacMan.PacManStepInPixels, _pacMan.PacManCoordinate.Y);
                MakeMovePacMan(newPacManLocation, _pacMan.eyeCoordinateLeft, _pacMan.mouthCoordinateLeft);
            }
            if (directionNumber == (int)_stepDirection.Right)
            {
                Point newPacManLocation = new Point(_pacMan.PacManCoordinate.X + _pacMan.PacManStepInPixels, _pacMan.PacManCoordinate.Y);
                MakeMovePacMan(newPacManLocation, _pacMan.eyeCoordinateRight, _pacMan.mouthCoordinateRight);
            }
            if (directionNumber == (int)_stepDirection.Top)
            {
                Point newPacManLocation = new Point(_pacMan.PacManCoordinate.X, _pacMan.PacManCoordinate.Y - _pacMan.PacManStepInPixels);
                MakeMovePacMan(newPacManLocation, _pacMan.eyeCoordinateTop, _pacMan.mouthCoordinateTop);
            }
            if (directionNumber == (int)_stepDirection.Down)
            {
                Point newPacManLocation = new Point(_pacMan.PacManCoordinate.X, _pacMan.PacManCoordinate.Y + _pacMan.PacManStepInPixels);
                MakeMovePacMan(newPacManLocation, _pacMan.eyeCoordinateDown, _pacMan.mouthCoordinateDown);
            }
        }
    }
}
