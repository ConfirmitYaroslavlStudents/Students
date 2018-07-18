using System;
using System.Collections.Generic;

namespace Championship
{
    public class TournamentGrid
    {
        private Stack<Node> _orderofmatches;

        public Node Final { get; set; }
        public int MaxStage { get; set; }
        private int _stage;

        public TournamentGrid(Queue<string> teams, int stage)
        {
            Final = new Node(0);
            MaxStage = stage;
            _stage = MaxStage + 1;
            _orderofmatches = new Stack<Node>();
            CreateTournamentGrid(Final, teams);
            GetOrderOfMatches(new List<Node>() { Final });
        }

        private void CreateTournamentGrid(Node Root, Queue<string> teams)
        {
            if (Root.Stage != MaxStage)
            {
                Root.Left = new Node(Root.Stage + 1);
                Root.Right = new Node(Root.Stage + 1);
                CreateTournamentGrid(Root.Left, teams);
                CreateTournamentGrid(Root.Right, teams);
            }
            else
            {
                Root.Match.FirstTeam = new Team(teams.Dequeue());
                Root.Match.SecondTeam = new Team(teams.Dequeue());
            }
        }

        private void GetOrderOfMatches(List<Node> games)
        {
            if (games.Count == 0)
                return;

            List<Node> NextGames = new List<Node>();

            for (int i=0;i<games.Count;i++)
            {
                _orderofmatches.Push(games[i]);
            }

            for (int i = 0; i < games.Count; i++)
            {
                if (games[i].Left != null)
                    NextGames.Add(games[i].Left);
                if (games[i].Right != null)
                    NextGames.Add(games[i].Right);
            }

            GetOrderOfMatches(NextGames);
        }

        public void StartTournament()
        {
            while (_orderofmatches.Count != 0)
            {
                Node item = _orderofmatches.Pop();

                if (item.Stage < _stage)
                {
                    _stage = item.Stage;

                    switch (_stage)
                    {
                        case 0:
                            Console.WriteLine("Start Final game!\n");
                            break;

                        default:
                            Console.WriteLine("Start 1/{0} games!\n", Math.Pow(2, item.Stage));
                            break;
                    }
                }

                item.GetOpponents();
                item.Match.PrintOpponents();
                item.Match.SetWinner();
                item.Match.PrintResultOfGame();
            }

        }

        public void ResultOfTournament()
        {
            Console.WriteLine("\nThe winner of Championship is {0}!!!",Final.Match.Winner);
            Console.ReadKey();
        }
    }
}

