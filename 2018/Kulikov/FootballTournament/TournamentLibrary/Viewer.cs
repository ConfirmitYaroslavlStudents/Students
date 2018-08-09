namespace TournamentLibrary
{
    public static class Viewer
    {
        public static IViewer GetViewer()
        {
            IViewer viewer = new ConsoleWorker();

            return viewer;
        }
    }
}
