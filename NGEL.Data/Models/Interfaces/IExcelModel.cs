using System;
using System.Text;
using System.Collections.Generic;
using MessagePack;
using NPOI.SS.UserModel;
using Lobby;

namespace NGEL.Data.Models
{
    public interface IExcelModel
    {
        abstract public List<string> GetExcelColumns();
        abstract public void SetExcelRows(ISheet sheet, int rowIndex);
    }
}
