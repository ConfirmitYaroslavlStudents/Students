using Championship;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TournamentsWpfForms.Drawers.HorisontalDrawers
{
    class HorisontalLowerGridPainter : HorisontalDrawer
    {
        public List<UIElement> GetListOfLowreGridItems(List<Round> tournamentRounds, int cursorPositionTop)
        {
            ListOfItems = new List<UIElement>();
            var nextCursorPositionLeft = 0;
            var nextDistanceBeewinPalyers = 35;
            var nextCursorPositionTop = 50 + cursorPositionTop;
            const int middleRectangleVertical = 15;
            const int minimalWidthCloud = 50;
            var bottomPoint = 1000;
            var maximumDistanceBetweenLowerPointsOfStage = 100;

            for (var i = 0; i < tournamentRounds.Count; i++)
            {
                var round = tournamentRounds[i];
                var maxWidthCloud = GetMaxLengthNameInRound(round) * 10 + minimalWidthCloud;
                var middleRectangHorisontal = maxWidthCloud / 2;

                var stage = new Label
                {
                    Margin = new Thickness(nextCursorPositionLeft, 0, 0, 0),
                    Content = GetStageToPrintLowerGrid(round.Stage)
                };
                ListOfItems.Add(stage);

                var distanceBetweenPlayers = nextDistanceBeewinPalyers;
                var positionCursorLeft = nextCursorPositionLeft;
                var positionCursorTop = nextCursorPositionTop;
                bool isNotSkip = true;

                for (var j = 0; j < round.Meetings.Count; j++)
                {
                    var meeting = round.Meetings[j];
                    var isNotSkippingPlace = isNotSkip || i % 2 == 1;


                    var indexForDrowLine = (distanceBetweenPlayers / 2) + middleRectangleVertical;
                    if (isNotSkippingPlace)
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

                        if (j == round.Meetings.Count - 1 && 
                            bottomPoint + maximumDistanceBetweenLowerPointsOfStage < lineVertical.Y2)
                        {
                            distanceBetweenPlayers /= 2;
                            lineVertical.Y2 = positionCursorTop + distanceBetweenPlayers + middleRectangleVertical;
                            bottomPoint = (int)lineVertical.Y2;
                            indexForDrowLine = (distanceBetweenPlayers / 2) + middleRectangleVertical;
                        }

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
                    }

                    if (j == 0)
                    {
                        nextCursorPositionLeft = positionCursorLeft + maxWidthCloud + middleRectangHorisontal;
                        nextDistanceBeewinPalyers *= 2;
                        nextCursorPositionTop = positionCursorTop + indexForDrowLine - middleRectangleVertical;
                    }

                    positionCursorTop += distanceBetweenPlayers;

                    if (isNotSkippingPlace)
                    {
                        if (j == round.Meetings.Count - 1)
                        {
                            bottomPoint = positionCursorTop;
                        }
                        PrintNamePlayer(positionCursorLeft, positionCursorTop, meeting, false, maxWidthCloud);
                    }
                    else
                    {
                        j--;
                    }

                    isNotSkip = !isNotSkip;
                    positionCursorTop += distanceBetweenPlayers;
                }
            }

            var finalMeeting = tournamentRounds[tournamentRounds.Count - 1].Meetings[0];
            PrintNameWinner(nextCursorPositionLeft, nextCursorPositionTop, finalMeeting);

            return ListOfItems;
        }

        private string GetStageToPrintLowerGrid(int stage)
        {
            return "L.Round " + stage;
        }
    }
}
