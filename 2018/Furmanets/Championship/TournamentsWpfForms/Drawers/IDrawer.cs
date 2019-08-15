using System.Collections.Generic;
using System.Windows;
using Championship;

namespace TournamentsWpfForms.Drawers
{
    interface IDrawer
    {
        List<UIElement> GetListOfTournamentItems(List<Round> tournamentRounds);
    }
}
