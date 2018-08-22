using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Championship;

namespace TournamentsWpfForms.Drawers
{
    internal class HorisontalDrawer : IDrawer
    {
        private List<UIElement> _listOfItems;

        public List<UIElement> GetListOfTournamentitems(Tournament _tournament)
        {
            _listOfItems = new List<UIElement>();
            
            var tournamentRounds = _tournament.GetTournamentToPrint();
            var nextCursorPositionLeft = 0;
            var nextDistanceBeewinPalyers = 60;
            var nextCursorPositionTop = 50;

            foreach (var round in tournamentRounds[0])
            {
                var maxLengthName = 100; //GetMaxLengthNameInRound(round);

                if (tournamentRounds[0][0].Meetings.Count != tournamentRounds[0][1].Meetings.Count)
                {
                    var stage = new Label
                    {
                        Margin = new Thickness(nextCursorPositionLeft, 0, 0, 0),
                        Content = GetStageToPrint(round.Stage)
                    };
                    _listOfItems.Add(stage);
                }

                var distanceBetweenPlayers = nextDistanceBeewinPalyers;
                var positionCursorLeft = nextCursorPositionLeft;
                var positionCursorTop = nextCursorPositionTop;

                for (var i = 0; i < round.Meetings.Count; i++)
                {
                    var meeting = round.Meetings[i];
                    var isEmptyMeetingInFirstRound = meeting.FirstPlayer == null
                                                     && meeting.SecondPlayer == null
                                                     && round.Equals(tournamentRounds[0][0]);

                    var indexForDrowLine = (distanceBetweenPlayers / 2)+10;

                    if (!isEmptyMeetingInFirstRound)
                    {
                        PrintNamePlayer(positionCursorLeft, positionCursorTop, meeting, true);


                        var line = new Line
                        {
                            Stroke = Brushes.Black,
                            X1 = positionCursorLeft + maxLengthName,
                            Y1 = positionCursorTop + 10,
                            X2 = positionCursorLeft + maxLengthName,
                            Y2 = positionCursorTop + distanceBetweenPlayers + 20
                        };
                        _listOfItems.Add(line);

                        line = new Line
                        {
                            Stroke = Brushes.Black,
                            X1 = positionCursorLeft + maxLengthName,
                            Y1 = positionCursorTop + indexForDrowLine,
                            X2 = positionCursorLeft + maxLengthName + 150,
                            Y2 = positionCursorTop + indexForDrowLine
                        };
                        _listOfItems.Add(line);

                        if (i == 0)
                        {
                            nextCursorPositionLeft = positionCursorLeft + maxLengthName + 150;
                            nextDistanceBeewinPalyers *= 2;
                            nextCursorPositionTop = positionCursorTop + indexForDrowLine - 15;
                        }

                        positionCursorTop += distanceBetweenPlayers;
                        PrintNamePlayer(positionCursorLeft, positionCursorTop, meeting, false);
                    }

                    positionCursorTop += distanceBetweenPlayers;
                }
            }

            var finishMeeting = tournamentRounds[0][tournamentRounds[0].Count - 1].Meetings[0];

            PrintNamePlayer(nextCursorPositionLeft, nextCursorPositionTop, finishMeeting,
                finishMeeting.Winner == MeetingWinner.FirstPlayer);

            return _listOfItems;
        }

        private void PrintNamePlayer(int x, int y, Meeting meeting, bool isFirstPlayer)
        {
            var cloud = new Rectangle
            {
                RadiusX = 10,
                RadiusY = 10,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 100,
                Height = 30
            };

            switch (meeting.Winner)
            {
                case MeetingWinner.MatchDidNotTakePlace:
                    cloud.Stroke = Brushes.Aqua;
                    cloud.Fill = Brushes.Gray;
                    break;

                case MeetingWinner.FirstPlayer when isFirstPlayer:
                case MeetingWinner.SecondPlayer when !isFirstPlayer:
                    cloud.Stroke = Brushes.Aqua;
                    cloud.Fill = Brushes.GreenYellow;
                    break;

                case MeetingWinner.FirstPlayer:
                case MeetingWinner.SecondPlayer:
                    cloud.Stroke = Brushes.Aqua;
                    cloud.Fill = Brushes.Red;
                    break;
            }

            if (meeting.SecondPlayer == null)
            {
                cloud.Stroke = Brushes.Aqua;
                cloud.Fill = Brushes.GreenYellow;
            }

            cloud.Margin = new Thickness(x, y, 0, 0);

            var labelName = new Label
            {
                Content = isFirstPlayer ? meeting.FirstPlayer : meeting.SecondPlayer,
                Margin = new Thickness(x + 5, y + 2, 0, 0)
            };

            _listOfItems.Add(cloud);
            _listOfItems.Add(labelName);
        }

        private string GetStageToPrint(int stage)
        {
            if (stage == 1)
            {
                return "final";
            }

            return "1/" + stage;
        }
    }
}
