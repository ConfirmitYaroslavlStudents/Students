namespace Championship
{
    public class GridRendererVisitor : IVisitor
    {
        public GridRendererType GridType = GridRendererType.StandardGrid;

        public void VisitDoubleChampionship(DoubleChampionship doubleChampionship)
        {
            var winnersTour = (doubleChampionship.Grid as DoubleEliminationGrid).WinnersTour;
            var losersTour = (doubleChampionship.Grid as DoubleEliminationGrid).LosersTour;

            if (GridType == GridRendererType.StandardGrid)
            {
                var simpleWinnersTourRenderer = new OneSideGridRenderer(winnersTour, 0);
                simpleWinnersTourRenderer.StartRenderer();

                if (losersTour != null)
                {
                    var simpleLosersTourRenderer = new OneSideGridRenderer(losersTour, simpleWinnersTourRenderer.CursorTop);
                    simpleLosersTourRenderer.StartRenderer();
                }
            }
            else
            {
                var doubleWinnersTourRenderer = new DoubleSideGridRenderer(winnersTour, 0);
                doubleWinnersTourRenderer.StartRenderer();

                if (losersTour != null)
                {
                    var doubleLosersTourRenderer = new DoubleSideGridRenderer(losersTour, doubleWinnersTourRenderer.CursorTop);
                    doubleLosersTourRenderer.StartRenderer();
                }
            }
        }

        public void VisitSingleChampionship(SingleChampionship singleChampionship)
        {
            if (GridType == GridRendererType.StandardGrid)
            {
                var simpleRenderer = new OneSideGridRenderer(singleChampionship.Grid as SingleEliminationGrid, 0);
                simpleRenderer.StartRenderer();
            }
            else
            {
                var doubleRenderer = new DoubleSideGridRenderer(singleChampionship.Grid as SingleEliminationGrid, 0);
                doubleRenderer.StartRenderer();
            }
        }
    }
}
