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
        private EnemyArtificialIntelligence _enemyAI;

        public Point EnemyCoordinate
        {
            get { return _enemyCoordinate;}
            set { _enemyCoordinate = value; }
        }

        public Enemy(Point enemyCoordinate,EnemyArtificialIntelligence enemyAI)
        {
            this._enemyCoordinate = enemyCoordinate;
            this._enemyAI = enemyAI;

        }
        public void MoveEnemy(Point pacManCoordinate)
        {
            _enemyCoordinate = _enemyAI.GetNextStep(_enemyCoordinate,pacManCoordinate);
        }
    }
}
