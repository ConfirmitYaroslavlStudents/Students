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
    public class Enemys
    {
        private List<Point> _enemyCoordinates;
        private EnemyArtificialIntelligence _enemyAI;

        public List<Point> EnemyCoordinates
        {
            get { return _enemyCoordinates;}
            set { _enemyCoordinates = value; }
        }

        public Enemys(List<Point> enemyCoordinates, EnemyArtificialIntelligence enemyAI)
        {
            this._enemyCoordinates = enemyCoordinates;
            this._enemyAI = enemyAI;

        }
        public void MoveEnemy(Point pacManCoordinate)
        {
            for (int i = 0; i < _enemyCoordinates.Count; i++)
            {
                _enemyCoordinates[i] = _enemyAI.GetNextStep(_enemyCoordinates[i], pacManCoordinate);
            }
        }
    }
}
