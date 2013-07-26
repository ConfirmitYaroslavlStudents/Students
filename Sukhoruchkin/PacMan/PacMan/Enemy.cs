using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace PacMan
{
    public class Enemy
    {
        private Point _enemyCoordinate;
        private List<Point> _enemyRoute;
        private int _enemyStep = 0;
        private static Timer _enemyTimer;

        public Point EnemyCoordinate
        {
            get { return _enemyCoordinate;}
            set { _enemyCoordinate = value; }
        }

        public Enemy(Point enemyCoordinate)
        {
            this._enemyCoordinate = enemyCoordinate;
            this._enemyRoute = new List<Point>();
            _enemyTimer = new Timer(moveEnemy, null, 500, 500);
        }
        public void AddPointToRoute(Point newPoint)
        {
            _enemyRoute.Add(newPoint);
        }
        public void moveEnemy(object sender)
        {
            _enemyCoordinate = _enemyRoute[_enemyStep];
            _enemyStep++;
            if (_enemyStep == _enemyRoute.Count)
                _enemyStep = 0;
            
        }
    }
}
