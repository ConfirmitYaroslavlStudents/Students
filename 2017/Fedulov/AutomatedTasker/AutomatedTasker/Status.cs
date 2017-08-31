using System.ComponentModel.DataAnnotations;

namespace AutomatedTasker
{
    public enum Status
    {
        [Display(Name="Command not started yet!")]
        NotStarted,

        [Display(Name = "Command in process now!")]
        Started,

        [Display(Name = "Command finish succesfully!")]
        Success,

        [Display(Name = "Command execution failed!")]
        Error
    }
}
