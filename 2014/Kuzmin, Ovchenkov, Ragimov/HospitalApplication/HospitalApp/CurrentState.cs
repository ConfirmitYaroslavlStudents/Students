using HospitalLib.Data;

namespace HospitalApp
{
    public static class CurrentState
    {
        public static Person CurrentPerson { get; set; }
        public static Template CurrentTemplate { get; set; }
        public static Analysis CurrentAnalysis { get; set; }
    }
}
