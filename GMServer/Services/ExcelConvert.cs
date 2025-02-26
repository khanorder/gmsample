using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NGEL.Data;
using NGEL.Data.Models;
using NGEL.Data.Helpers;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NGEL.Data.Settings;

namespace GMServer.Services
{
    public class ExcelConvertOptions
    {
        public Defines.TimeZone timeZone { get; set; } = Defines.TimeZone.UTC;
        public CultureInfo cultureInfo { get; set; } = CultureInfo.CreateSpecificCulture("ko-KR");
    }

    public static class ExcelConvertOptionsExteneds
    {
        public static TimeZoneInfo GetTimeZoneInfo(this ExcelConvertOptions options)
        {
            return ExtendedHelper.GetTimeZoneInfo(options.timeZone);
        }
    }

    public class ExcelConvert
    {
        private readonly CommonSettings _commonSettings;
        private readonly ILogger<ExcelConvert> _logger;
        public ExcelConvert(CommonSettings commonSettings, ILogger<ExcelConvert> logger)
        {
            _commonSettings = commonSettings;
            _logger = logger;
        }

        #region 운영툴 로그
        private List<string> GetExcelColumnNames(List<GMCombinedLog> sourceDatas, ExcelConvertOptions options)
        {
            var ret = new List<string>
            {
                "로그타입",
                "이름",
                "기능",
                "경로",
                $"사용일시({options.timeZone.GetTimeZoneString()})",
                "인자",
                "IP"
            };
            return ret;
        }

        private void SetExcelRowsValue(GMCombinedLog data, ISheet sheet, int rowIndex, ExcelConvertOptions options)
        {
            IRow row = sheet.CreateRow(rowIndex + 1);
            ICell cellLogType = row.CreateCell(0);
            cellLogType.SetCellValue(data.type.ToString());
            ICell cellName = row.CreateCell(1);
            cellName.SetCellValue(data.userName);
            ICell cellMethod = row.CreateCell(2);
            cellMethod.SetCellValue(data.methodName);
            ICell cellURL = row.CreateCell(3);
            cellURL.SetCellValue(data.urlName);
            ICell cellregTime = row.CreateCell(4);
            cellregTime.SetCellValue(data.regTime.ConvertTimeZone(options.timeZone).ToString("yyyy-MM-dd(ddd) HH:mm:ss", options.cultureInfo));
            ICell cellMessage = row.CreateCell(5);
            cellMessage.SetCellValue(data.message);
            ICell cellIP = row.CreateCell(6);
            cellIP.SetCellValue(data.remoteAddress);
        }

        private bool ConvertExcelSheet(List<GMCombinedLog> sourceDatas, string SheetName, ExcelConvertOptions options, ref IWorkbook wb)
        {
            if (sourceDatas.Count == 0)
            {
                return false;
            }
            var colurms = GetExcelColumnNames(sourceDatas, options);
            ISheet sheet = wb.CreateSheet(SheetName);
            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < colurms.Count; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                cell.SetCellValue(colurms[i]);
            }

            for (var i = 0; i < sourceDatas.Count; i++)
            {
                var sourceData = sourceDatas[i];
                SetExcelRowsValue(sourceData, sheet, i, options);
            }
            return true;
        }

        public bool ConvertExcelData(List<GMCombinedLog> sourceDatas, ExcelConvertOptions options, ref byte[] data, ref string contentsType)
        {
            if (sourceDatas.Count == 0)
            {
                return false;
            }

            IWorkbook wb = new XSSFWorkbook();
            if (!ConvertExcelSheet(sourceDatas, "Sheet1", options, ref wb))
            {
                return false;
            }
            using (var excelData = new MemoryStream())
            {
                wb.Write(excelData, true);
                data = excelData.ToArray();
            }

            contentsType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return true;
        }
        #endregion

        #region 게임 로그
        private List<string> GetExcelColumnNames(List<BiskitLog> sourceDatas, ExcelConvertOptions options)
        {
            var ret = new List<string>
            {
                "log_id",
                "event_group_id",
                "event_id",
                $"timestamp({options.timeZone.GetTimeZoneString()})",
                "sequence_number",
                "stove_member_no",
                "stove_nickname_no",
                "account_id",
                "account_level",
                "account_name",
                "character_id",
                "session_id",
                "market_code",
                "server_code",
                "channel_code",
                "ip_address",
                "device_id",
                "device_type",
                "device_model",
                "os",
                "v1",
                "v2",
                "v3",
                "v4",
                "v5",
                "v6",
                "v7",
                "v8",
                "v9",
                "v10",
                "v11",
                "v12",
                "v13",
                "v14",
                "v15",
                "v16",
                "v17",
                "v18",
                "v19",
                "v20",
                "v21",
                "v22",
                "v23",
                "v24",
                "v25",
                "v26",
                "v27",
                "v28",
                "v29",
                "v30"
            };
            return ret;
        }

        private void SetExcelRowsValue(BiskitLog data, ISheet sheet, int rowIndex, ExcelConvertOptions options)
        {
            var row = sheet.CreateRow(rowIndex + 1);
            var cell01 = row.CreateCell(0); cell01.SetCellValue(data.logID);
            var cell02 = row.CreateCell(1); cell02.SetCellValue(data.eventGroupID);
            var cell03 = row.CreateCell(2); cell03.SetCellValue(data.eventID);
            var cell04 = row.CreateCell(3); cell04.SetCellValue(data.timestamp.ConvertTimeZone(options.timeZone).ToString("yyyy-MM-dd(ddd) HH:mm:ss", options.cultureInfo));
            var cell05 = row.CreateCell(4); cell05.SetCellValue(data.sequenceNumber);
            var cell06 = row.CreateCell(5); cell06.SetCellValue(data.stoveMemberNO);
            var cell07 = row.CreateCell(6); cell07.SetCellValue(data.stoveNickNameNO);
            var cell08 = row.CreateCell(7); cell08.SetCellValue(data.accountID);
            var cell09 = row.CreateCell(8); cell09.SetCellValue(data.accountLevel);
            var cell10 = row.CreateCell(9); cell10.SetCellValue(data.accountName);
            var cell11 = row.CreateCell(10); cell11.SetCellValue(data.characterID);
            var cell12 = row.CreateCell(11); cell12.SetCellValue(data.characterLevel);
            var cell13 = row.CreateCell(12); cell13.SetCellValue(data.sessionID);
            var cell14 = row.CreateCell(13); cell14.SetCellValue(data.marketCode);
            var cell15 = row.CreateCell(14); cell15.SetCellValue(data.serverCode);
            var cell16 = row.CreateCell(15); cell16.SetCellValue(data.ipAddress);
            var cell17 = row.CreateCell(16); cell17.SetCellValue(data.deviceID);
            var cell18 = row.CreateCell(17); cell18.SetCellValue(data.deviceType);
            var cell19 = row.CreateCell(18); cell19.SetCellValue(data.deviceModel);
            var cell20 = row.CreateCell(19); cell20.SetCellValue(data.os);
            var cell21 = row.CreateCell(20); cell21.SetCellValue(data.v01);
            var cell22 = row.CreateCell(21); cell22.SetCellValue(data.v02);
            var cell23 = row.CreateCell(22); cell23.SetCellValue(data.v03);
            var cell24 = row.CreateCell(23); cell24.SetCellValue(data.v04);
            var cell25 = row.CreateCell(24); cell25.SetCellValue(data.v05);
            var cell26 = row.CreateCell(25); cell26.SetCellValue(data.v06);
            var cell27 = row.CreateCell(26); cell27.SetCellValue(data.v07);
            var cell28 = row.CreateCell(27); cell28.SetCellValue(data.v08);
            var cell29 = row.CreateCell(28); cell29.SetCellValue(data.v09);
            var cell30 = row.CreateCell(29); cell30.SetCellValue(data.v10);
            var cell31 = row.CreateCell(30); cell31.SetCellValue(data.v11);
            var cell32 = row.CreateCell(31); cell32.SetCellValue(data.v12);
            var cell33 = row.CreateCell(32); cell33.SetCellValue(data.v13);
            var cell34 = row.CreateCell(33); cell34.SetCellValue(data.v14);
            var cell35 = row.CreateCell(34); cell35.SetCellValue(data.v15);
            var cell36 = row.CreateCell(35); cell36.SetCellValue(data.v16);
            var cell37 = row.CreateCell(36); cell37.SetCellValue(data.v17);
            var cell38 = row.CreateCell(37); cell38.SetCellValue(data.v18);
            var cell39 = row.CreateCell(38); cell39.SetCellValue(data.v19);
            var cell40 = row.CreateCell(39); cell40.SetCellValue(data.v20);
            var cell41 = row.CreateCell(30); cell41.SetCellValue(data.v21);
            var cell42 = row.CreateCell(41); cell42.SetCellValue(data.v22);
            var cell43 = row.CreateCell(42); cell43.SetCellValue(data.v23);
            var cell44 = row.CreateCell(43); cell44.SetCellValue(data.v24);
            var cell45 = row.CreateCell(44); cell45.SetCellValue(data.v25);
            var cell46 = row.CreateCell(45); cell46.SetCellValue(data.v26);
            var cell47 = row.CreateCell(46); cell47.SetCellValue(data.v27);
            var cell48 = row.CreateCell(47); cell48.SetCellValue(data.v28);
            var cell49 = row.CreateCell(48); cell49.SetCellValue(data.v29);
            var cell50 = row.CreateCell(49); cell50.SetCellValue(data.v30);

            //cellregTime.SetCellValue(data.regTime.ToLocalTime().ToString("yyyy-MM-dd(ddd) HH:mm:ss", CultureInfo.CreateSpecificCulture("ko-KR")));
        }

        private bool ConvertExcelSheet(List<BiskitLog> sourceDatas, string SheetName, ExcelConvertOptions options, ref IWorkbook wb)
        {
            if (sourceDatas.Count == 0)
            {
                return false;
            }
            var colurms = GetExcelColumnNames(sourceDatas, options);
            ISheet sheet = wb.CreateSheet(SheetName);
            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < colurms.Count; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                cell.SetCellValue(colurms[i]);
            }

            for (var i = 0; i < sourceDatas.Count; i++)
            {
                var sourceData = sourceDatas[i];
                SetExcelRowsValue(sourceData, sheet, i, options);
            }
            return true;
        }

        public bool ConvertExcelData(List<BiskitLog> sourceDatas, ExcelConvertOptions options, ref byte[] data, ref string contentsType)
        {
            if (sourceDatas.Count == 0)
            {
                return false;
            }

            IWorkbook wb = new XSSFWorkbook();
            if (!ConvertExcelSheet(sourceDatas, "Sheet1", options, ref wb))
            {
                return false;
            }
            using (var excelData = new MemoryStream())
            {
                wb.Write(excelData, true);
                data = excelData.ToArray();
            }

            contentsType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return true;
        }
        #endregion

    }
}
