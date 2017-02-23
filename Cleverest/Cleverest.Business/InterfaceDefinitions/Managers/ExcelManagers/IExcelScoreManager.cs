using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;

namespace Cleverest.Business.InterfaceDefinitions.Managers.ExcelManagers
{
    public interface IExcelScoreManager
    {
        IList<Score> GetScores(FileInfo info);
    }
}
