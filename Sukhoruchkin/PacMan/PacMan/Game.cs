using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;


namespace PacMan
{
    public class Game
    {
        private PacMan _pacMan;
        private Enemy _enemy;
        private Timer _enemyTimer;
        private Map _map;
        private int _currentLevel;
        private int _numberLevels;

        public Game(int currentLevel)
        {
            this._currentLevel = currentLevel;
            this._numberLevels = ConsiderNumberLevels();

            Wall levelWall = new Wall();
            levelWall.WallCordinate = ReadNewMapOnFile();
            Rectangle infoByPacManAndEnemy = levelWall.WallCordinate[levelWall.WallCordinate.Count - 2];
            Rectangle infoByColumnAndLine = levelWall.WallCordinate[levelWall.WallCordinate.Count - 1];
            levelWall.WallCordinate.Remove(infoByPacManAndEnemy);
            levelWall.WallCordinate.Remove(infoByColumnAndLine);

            this._map = new Map(levelWall, infoByColumnAndLine.X, infoByColumnAndLine.Y);
            this._pacMan = new PacMan(new Point(infoByPacManAndEnemy.X, infoByPacManAndEnemy.Y));
            EnemyArtificialIntelligence _enemyAI = new EnemyArtificialIntelligence(_map.LevelFood.FoodCoordinates);
            this._enemy = new Enemy(new Point(infoByPacManAndEnemy.Width, infoByPacManAndEnemy.Height),_enemyAI);
            _enemyTimer = new Timer(MakeMoveEnemy, null, 0, GameSettings.EnemySpeed);
        }
        public PacMan PacMan
        {
            get { return _pacMan; }
        }
        public Enemy Enemy
        {
            get { return _enemy; }
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
            return _pacMan.PacManCoordinate == _enemy.EnemyCoordinate;
        }
        public List<Rectangle> ReadNewMapOnFile()
        {
            var reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Rectangle>));
            var file = new System.IO.StreamReader("../../levels/" + _currentLevel.ToString() + ".xml");
            List<Rectangle> newMap = (List<Rectangle>)reader.Deserialize(file);
            return newMap;
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
            _enemy.MoveEnemy(_pacMan.PacManCoordinate);
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
            return new System.IO.DirectoryInfo("../../levels/").GetFiles("*.*", System.IO.SearchOption.AllDirectories).Length;
        }
        public void KeyDown(string keyCode)
        {
            if (keyCode=="A")
            {
                Point newPacManLocation = new Point(_pacMan.PacManCoordinate.X - _pacMan.PacManStepInPixels, _pacMan.PacManCoordinate.Y);
                MakeMovePacMan(newPacManLocation, _pacMan.eyeCoordinateLeft, _pacMan.mouthCoordinateLeft);
            }
            if (keyCode == "D")
            {
                Point newPacManLocation = new Point(_pacMan.PacManCoordinate.X + _pacMan.PacManStepInPixels, _pacMan.PacManCoordinate.Y);
                MakeMovePacMan(newPacManLocation, _pacMan.eyeCoordinateRight, _pacMan.mouthCoordinateRight);
            }
            if (keyCode == "W")
            {
                Point newPacManLocation = new Point(_pacMan.PacManCoordinate.X, _pacMan.PacManCoordinate.Y - _pacMan.PacManStepInPixels);
                MakeMovePacMan(newPacManLocation, _pacMan.eyeCoordinateTop, _pacMan.mouthCoordinateTop);
            }
            if (keyCode == "S")
            {
                Point newPacManLocation = new Point(_pacMan.PacManCoordinate.X, _pacMan.PacManCoordinate.Y + _pacMan.PacManStepInPixels);
                MakeMovePacMan(newPacManLocation, _pacMan.eyeCoordinateDown, _pacMan.mouthCoordinateDown);
            }
        }
    }
}
