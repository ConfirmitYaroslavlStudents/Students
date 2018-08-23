namespace ChampionshipLibrary
{
    public interface IChampionship
    {
        bool IsTeamInput { get; set; }
        IGrid Grid { get; set; }
        Team Champion { get; set; }

        void InputTeams();
        void PlayTour();
        void Accept(IVisitor visitor);
    }
}
