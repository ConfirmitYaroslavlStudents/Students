using System.Collections.Generic;
using System.Windows;
using Championship;

namespace TournamentsWpfForms.Drawers
{
    interface IDrawer
    {
        List<UIElement> GetListOfTournamentitems(Tournament tournament);
    }
}
