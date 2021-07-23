using System;

namespace ToDoApp
{
    public interface IExecute
    {
        Action Add();
        Action Edit();
        Action Complete();
        Action Delete();
        Action List();
        Action Error();
    }

}
