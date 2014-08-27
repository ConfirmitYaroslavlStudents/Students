using System;
using HospitalLib.DatebaseModel;

namespace HospitalLib.Providers
{
    public class NewIdProvider : INewIdProvider
    {
        private readonly IDatabaseProvider _databaseProvider;

        public NewIdProvider(IDatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
                throw new NullReferenceException("databaseProvider");

            _databaseProvider = databaseProvider;
        }

        public int GetPersonId()
        {
            var query = "select Id From IdTable where Type='Person'";
            var count = _databaseProvider.GetDataScalar(query) + 1;

            query = string.Format("update IdTable set Id='{0}'where Type='Person'", count);
            _databaseProvider.PushData(query);

            return count;
        }

        public int GetAnalysisId()
        {
            var query = "select Id From IdTable where Type='Analysis'";
            var count = _databaseProvider.GetDataScalar(query) + 1;

            query = string.Format("update IdTable set Id='{0}'where Type='Analysis'", count);
            _databaseProvider.PushData(query);

            return count;
        }

        public int GetTemplateId()
        {
            var query = "select Id From IdTable where Type='Template'";
            var count = _databaseProvider.GetDataScalar(query) + 1;

            query = string.Format("update IdTable set Id='{0}'where Type='Template'", count);
            _databaseProvider.PushData(query);

            return count;
        }
    }
}
