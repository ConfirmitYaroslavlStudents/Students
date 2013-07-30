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
        public int _currentLevel;

        public Game(int currentLevel)
        {
            this._currentLevel = currentLevel;

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
            _enemyTimer = new Timer(MakeMoveEnemy, null, 500, 500);
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

        public bool IsWin()
        {
            if (_map.LevelFood.FoodCoordinates.Count == 0)
            {
                return true;
            }
            return false;
        }
        public bool IsLoose()
        {
            if (_pacMan.PacManCoordinate == _enemy.EnemyCoordinate)
                return true;
            return false;
        }
        public List<Rectangle> ReadNewMapOnFile()
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Rectangle>));
            System.IO.StreamReader file = new System.IO.StreamReader("levels/" + _currentLevel.ToString() + ".xml");
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
            if (newLocation.X >= 0 && newLocation.X < _map.NumberColimns * 50 && newLocation.Y >= 0 && newLocation.Y < _map.NumberLines * 50) 
            {
                return true;
            }
            return false;
        }
    }
}
