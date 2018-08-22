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

        public List<UIElement> GetListOfTournamentitems(Tournament tournament)
        {
            _listOfItems = new List<UIElement>();
            var tournamentRounds = tournament.GetTournamentToPrint();
            var nextCursorPositionLeft = 0;
            var nextDistanceBeewinPalyers = 60;
            var nextCursorPositionTop = 50;
            var middleRectangleVertical = 15;
            var minimalWidthCloud = 50;

            foreach (var round in tournamentRounds[0])
            {
                var maxWidthCloud = GetMaxLengthNameInRound(round) * 10 + minimalWidthCloud;
                var middleRectangHorisontal = maxWidthCloud / 2;

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

                    var indexForDrowLine = (distanceBetweenPlayers / 2) + middleRectangleVertical;

                    if (!isEmptyMeetingInFirstRound)
                    {
                        PrintNamePlayer(positionCursorLeft, positionCursorTop, meeting, true, maxWidthCloud);

                        var lineVertical = new Line
                        {
                            Stroke = Brushes.Black,
                            X1 = positionCursorLeft + maxWidthCloud,
                            Y1 = positionCursorTop + middleRectangleVertical,
                            X2 = positionCursorLeft + maxWidthCloud,
                            Y2 = positionCursorTop + distanceBetweenPlayers + middleRectangleVertical
                        };
                        _listOfItems.Add(lineVertical);

                        var lineHorisontal = new Line
                        {
                            Stroke = Brushes.Black,
                            X1 = positionCursorLeft + maxWidthCloud,
                            Y1 = positionCursorTop + indexForDrowLine,
                            X2 = positionCursorLeft + maxWidthCloud + middleRectangHorisontal,
                            Y2 = positionCursorTop + indexForDrowLine
                        };
                        _listOfItems.Add(lineHorisontal);

                        if (i == 0)
                        {
                            nextCursorPositionLeft = positionCursorLeft + maxWidthCloud + middleRectangHorisontal;
                            nextDistanceBeewinPalyers *= 2;
                            nextCursorPositionTop = positionCursorTop + indexForDrowLine - middleRectangleVertical;
                        }

                        positionCursorTop += distanceBetweenPlayers;
                        PrintNamePlayer(positionCursorLeft, positionCursorTop, meeting, false, maxWidthCloud);
                    }

                    positionCursorTop += distanceBetweenPlayers;
                }
            }

            var finishMeeting = tournamentRounds[0][tournamentRounds[0].Count - 1].Meetings[0];

            PrintNamePlayer(nextCursorPositionLeft, nextCursorPositionTop, finishMeeting,
                finishMeeting.Winner == MeetingWinner.FirstPlayer, 3);

            return _listOfItems;
        }

        public int GetMaxLengthNameInRound(Round round)
        {
            var maxLengthName = 0;

            foreach (var meeting in round.Meetings)
            {
                if (meeting.FirstPlayer != null && meeting.FirstPlayer.Length > maxLengthName)
                {
                    maxLengthName = meeting.FirstPlayer.Length;
                }

                if (meeting.SecondPlayer != null && meeting.SecondPlayer.Length > maxLengthName)
                {
                    maxLengthName = meeting.SecondPlayer.Length;
                }
            }
            return maxLengthName;
        }

        private void PrintNamePlayer(int x, int y, Meeting meeting, bool isFirstPlayer, int maxWidthCloud)
        {
            var cloud = new Rectangle
            {
                RadiusX = 10,
                RadiusY = 10,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = maxWidthCloud,
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

            if (meeting.FirstPlayer!= null && meeting.SecondPlayer == null)
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
