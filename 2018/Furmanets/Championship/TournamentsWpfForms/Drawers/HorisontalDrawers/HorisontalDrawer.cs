using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Championship;

namespace TournamentsWpfForms.Drawers.HorisontalDrawers
{
    internal class HorisontalDrawer : IDrawer
    {
        protected List<UIElement> ListOfItems;

        public List<UIElement> GetListOfTournamentItems(List<Round> tournamentRounds)
        {
            ListOfItems = new List<UIElement>();
            var nextCursorPositionLeft = 0;
            var nextDistanceBeewinPalyers = 40;
            var nextCursorPositionTop = 50;
            const int middleRectangleVertical = 15;
            const int minimalWidthCloud = 50;

            foreach (var round in tournamentRounds)
            {
                var maxWidthCloud = GetMaxLengthNameInRound(round) * 10 + minimalWidthCloud;
                var middleRectangHorisontal = maxWidthCloud / 2;

                var stage = new Label
                {
                    Margin = new Thickness(nextCursorPositionLeft, 0, 0, 0),
                    Content = GetStageToPrintSingleGrid(round.Stage)
                };
                ListOfItems.Add(stage);

                var distanceBetweenPlayers = nextDistanceBeewinPalyers;
                var positionCursorLeft = nextCursorPositionLeft;
                var positionCursorTop = nextCursorPositionTop;

                for (var i = 0; i < round.Meetings.Count; i++)
                {
                    var meeting = round.Meetings[i];

                    var isEmptyMeetingInFirstRound = meeting.FirstPlayer == null
                                                     && meeting.SecondPlayer == null
                                                     && round.Equals(tournamentRounds[0]);

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
                        ListOfItems.Add(lineVertical);

                        var lineHorisontal = new Line
                        {
                            Stroke = Brushes.Black,
                            X1 = positionCursorLeft + maxWidthCloud,
                            Y1 = positionCursorTop + indexForDrowLine,
                            X2 = positionCursorLeft + maxWidthCloud + middleRectangHorisontal,
                            Y2 = positionCursorTop + indexForDrowLine
                        };
                        ListOfItems.Add(lineHorisontal);

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

            var finalMeeting = tournamentRounds[tournamentRounds.Count - 1].Meetings[0];

            PrintNameWinner(nextCursorPositionLeft, nextCursorPositionTop, finalMeeting);
            return ListOfItems;
        }

        protected int GetMaxLengthNameInRound(Round round)
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

        protected void PrintNameWinner(int x, int y, Meeting meeting)
        {
            var minWidthCloud = 50;
            var namePlayer = "";

            var cloud = new Rectangle
            {
                RadiusX = 10,
                RadiusY = 10,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = minWidthCloud,
                Height = 30,
                Margin = new Thickness(x, y, 0, 0)
            };

            switch (meeting.Winner)
            {
                case MeetingWinner.FirstPlayer:
                    cloud.Width += meeting.FirstPlayer.Length;
                    namePlayer = meeting.FirstPlayer;
                    cloud.Stroke = Brushes.Aqua;
                    cloud.Fill = Brushes.GreenYellow;
                    break;
                case MeetingWinner.SecondPlayer:
                    cloud.Width += meeting.SecondPlayer.Length;
                    namePlayer = meeting.SecondPlayer;
                    cloud.Stroke = Brushes.Aqua;
                    cloud.Fill = Brushes.GreenYellow;
                    break;
                case MeetingWinner.MatchDidNotTakePlace:
                    cloud.Stroke = Brushes.Aqua;
                    cloud.Fill = Brushes.Gray;
                    break;
            }

            var labelName = new Label
            {
                Content = namePlayer,
                Margin = new Thickness(x + 5, y + 2, 0, 0)
            };

            ListOfItems.Add(cloud);
            ListOfItems.Add(labelName);
        }

        protected void PrintNamePlayer(int x, int y, Meeting meeting, bool isFirstPlayer, int maxWidthCloud)
        {
            var cloud = new Rectangle
            {
                RadiusX = 10,
                RadiusY = 10,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = maxWidthCloud,
                Height = 30,
                Margin = new Thickness(x, y, 0, 0)
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

            var labelName = new Label
            {
                Content = isFirstPlayer ? meeting.FirstPlayer : meeting.SecondPlayer,
                Margin = new Thickness(x + 5, y + 2, 0, 0)
            };

            ListOfItems.Add(cloud);
            ListOfItems.Add(labelName);
        }

        private string GetStageToPrintSingleGrid(int stage)
        {
            if (stage == 1)
            {
                return "final";
            }

            return "1/" + stage;
        }
    }
}
