using System;

namespace Championship
{
    class Paint
    {
        public void PaintTournamentStage(TournamentGrid grid)
        {
            Console.Clear();
            Console.WriteLine(grid.Tournament[0].Stage);
            Console.WriteLine();
            foreach (var meeting in grid.Tournament)
            {
                if (meeting.PlayerSecond == null)
                {
                    Console.WriteLine(meeting.PlayerFirst + " vs " + "----------");
                }
                else
                {
                    Console.WriteLine(meeting.PlayerFirst + " vs " + meeting.PlayerSecond);
                }
            }
            Console.WriteLine("Tap Enter ...");
            Console.ReadKey();
        }

        public void PaintTournamentRound(TournamentGrid grid)
        {
            Console.Clear();
            Console.WriteLine(grid.Tournament[0].Stage);
            Console.WriteLine();
            foreach (var meeting in grid.Tournament)
            {
                if (meeting.PlayerSecond == null)
                {
                    Console.WriteLine(meeting.PlayerFirst + " " + meeting.Score + " " + "----------");
                }
                else
                {
                    Console.WriteLine(meeting.PlayerFirst + " " + meeting.Score + " " + meeting.PlayerSecond);
                }
            }

            Console.WriteLine("Tap Enter ...");
            Console.ReadKey();
        }
    }
}
