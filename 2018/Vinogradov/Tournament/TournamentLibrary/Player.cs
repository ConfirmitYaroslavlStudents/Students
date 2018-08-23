using System;

namespace TournamentLibrary
{
    [Serializable]
    public class Player
    {
        public readonly string Name;
        public PlayerCoords Coords;

        public Player(string name, PlayerCoords coords)
        {
            Name = name;
            Coords = coords;
        }
    }
}
