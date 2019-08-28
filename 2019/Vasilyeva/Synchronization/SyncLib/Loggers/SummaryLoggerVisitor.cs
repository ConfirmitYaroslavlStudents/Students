using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncLib.Loggers
{
    public class SummaryLoggerVisitor : IVisitor
    {
        public int DeletedCount = 
        public void Visit(DifferentContentConflict conflict)
        {
            throw new NotImplementedException();
        }

        public void Visit(ExistDirectoryConflict conflict)
        {
            throw new NotImplementedException();
        }

        public void Visit(ExistFileConflict conflict)
        {
            throw new NotImplementedException();
        }

        public void Visit(NoExistDirectoryConflict conflict)
        {
            throw new NotImplementedException();
        }

        public void Visit(NoExistFileConflict conflict)
        {
            throw new NotImplementedException();
        }
    }
}
