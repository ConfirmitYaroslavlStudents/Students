namespace Shared.Interfaces
{
    public interface IHospitalDataAccessLayer : IPersonDataAccessLayer, IAnalysisDataAccessLayer,
        ITemplateDataAccessLayer
    {
        void OpenConnection(string dataProvider, string connectionString);
        void CloseConnection();
    }
}