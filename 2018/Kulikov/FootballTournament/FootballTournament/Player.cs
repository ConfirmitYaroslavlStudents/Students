﻿using System;

namespace FootballTournament
{
    public class Player
    {
        public string Name { get; private set; }

        public Player(string name)
        {
            Name = name;
        }
    }
}
