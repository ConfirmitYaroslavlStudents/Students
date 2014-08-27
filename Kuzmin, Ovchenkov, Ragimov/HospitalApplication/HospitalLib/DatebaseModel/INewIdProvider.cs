namespace HospitalLib.DatebaseModel
{
    public interface INewIdProvider
    {
        int GetPersonId();
        int GetAnalysisId();
        int GetTemplateId();
    }
}
