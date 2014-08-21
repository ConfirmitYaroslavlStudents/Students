using System.IO;
using HospitalLib.Interfaces;

namespace HospitalLib.Parser
{
    public class Parser : ITemplateProvider
    {
        private string _type;
        private ITemplateProvider _parser;

        public Template.Template Load(string path)
        {
            _type = Path.GetExtension(path);
            if (_type == null) throw new InvalidDataException("Invalid file extension");
            switch (_type.ToLower())
            {
                case ".txt":
                    _parser = new TxtParser();
                    return _parser.Load(path);
                default:
                    throw new InvalidDataException("Invalid file extension");

            }
        }
    }
}
