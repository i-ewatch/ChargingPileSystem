using ChargingPileSystem.Configuration;
using ChargingPileSystem.EF_Module;
using ChargingPileSystem.Enums;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Serilog;
using ChargingPileSystem.EF_Modules;
using ChargingPileSystem.Protocols;
using ChargingPileSystem.Protocols.ElectricMeter;
using ChargingPileSystem.Protocols.Senser;

namespace ChargingPileSystem.Methods
{
    public class SqlMethod
    {
        /// <summary>
        /// 資料庫類型
        /// </summary>
        private SQLEnumType SQLEnumType { get; set; }
        /// <summary>
        /// 資料庫查詢類型
        /// </summary>
        private SearchEnumType SearchEnumType { get; set; }
        /// <summary>
        /// 電表相位角
        /// <para> 0 = R </para>
        /// <para> 1 = S </para>
        /// <para> 2 = T </para>
        /// </summary>
        private PhaseAngleEnumType PhaseAngleEnumType { get; set; }
        /// <summary>
        /// 銀行與系統密碼資訊
        /// </summary>
        public BankAccountSetting BankAccountSetting { get; set; }
        /// <summary>
        /// server資料庫連結資訊
        /// </summary>
        public SqlConnectionStringBuilder Serverscsb;
        /// <summary>
        /// sql資料庫連結資訊
        /// </summary>
        public SqlConnectionStringBuilder scsb;
        /// <summary>
        /// MariaDB資料庫連結資訊
        /// </summary>
        public MySqlConnectionStringBuilder myscbs;
        /// <summary>
        /// 資料庫JSON
        /// </summary>
        public SqlDBSetting setting { get; set; }

        #region 資料庫連結
        /// <summary>
        /// EF資料庫連結
        /// </summary>
        /// <param name="DataBaseType">資料庫類型</param>
        public void SQLConnect()
        {
            SQLEnumType = (SQLEnumType)setting.SQLEnumsType;
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        Serverscsb = new SqlConnectionStringBuilder()
                        {
                            DataSource = setting.DataSource,
                            InitialCatalog = "master",
                            UserID = setting.UserID,
                            Password = setting.Password,
                        };
                        scsb = new SqlConnectionStringBuilder()
                        {
                            DataSource = setting.DataSource,
                            InitialCatalog = setting.InitialCatalog,
                            UserID = setting.UserID,
                            Password = setting.Password,
                        };
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        Serverscsb = new SqlConnectionStringBuilder()
                        {
                            DataSource = setting.DataSource,
                            InitialCatalog = "mysql",
                            UserID = setting.UserID,
                            Password = setting.Password,
                        };
                        myscbs = new MySqlConnectionStringBuilder()
                        {
                            Database = setting.InitialCatalog,
                            Server = setting.DataSource,
                            UserID = setting.UserID,
                            Password = setting.Password,
                            CharacterSet = "utf8"
                        };
                    }
                    break;
            }
        }
        #endregion

        #region 查詢 Gatewayconfig
        /// <summary>
        /// 查詢 Gatewayconfig
        /// </summary>
        /// <returns></returns>
        public List<GatewayConfig> Search_GatewayConfig()
        {
            try
            {
                string sql = "SELECT  * FROM Gatewayconfig";
                List<GatewayConfig> gatewayConfigs = null;
                switch (SQLEnumType)
                {
                    case SQLEnumType.SqlDB:
                        {
                            using (var conn = new SqlConnection(scsb.ConnectionString))
                            {
                                gatewayConfigs = conn.Query<GatewayConfig>(sql).ToList();
                            }
                        }
                        break;
                    case SQLEnumType.MariaDB:
                        {
                            using (var conn = new MySqlConnection(myscbs.ConnectionString))
                            {
                                gatewayConfigs = conn.Query<GatewayConfig>(sql).ToList();
                            }
                        }
                        break;
                }
                return gatewayConfigs;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "查詢 Gatewayconfig 失敗");
                return null;
            }

        }
        #endregion

        #region 查詢 Electricconfig
        /// <summary>
        /// 查詢 Electricconfig
        /// </summary>
        /// <returns></returns>
        public List<ElectricConfig> Search_Electricconfig()
        {
            try
            {
                string sql = "SELECT  * FROM Electricconfig";
                List<ElectricConfig> gatewayConfigs = null;
                switch (SQLEnumType)
                {
                    case SQLEnumType.SqlDB:
                        {
                            using (var conn = new SqlConnection(scsb.ConnectionString))
                            {
                                gatewayConfigs = conn.Query<ElectricConfig>(sql).ToList();
                            }
                        }
                        break;
                    case SQLEnumType.MariaDB:
                        {
                            using (var conn = new MySqlConnection(myscbs.ConnectionString))
                            {
                                gatewayConfigs = conn.Query<ElectricConfig>(sql).ToList();
                            }
                        }
                        break;
                }
                return gatewayConfigs;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "查詢 Electricconfig 失敗");
                return null;
            }

        }
        #endregion

        #region 變更 Electricconfig設備名稱
        /// <summary>
        /// 變更 Electricconfig設備名稱
        /// </summary>
        /// <param name="DeviceName">設備名稱</param>
        /// <param name="GatewayIndex">通訊編號</param>
        /// <param name="DeviceIndex">設備編號</param>
        /// <returns></returns>
        public bool Updata_ElectricConfig(string DeviceName,int GatewayIndex, int DeviceIndex)
        {
            try
            {
                string sql = "UPDATE Electricconfig SET " +
                             $"DeviceName = @DeviceName WHERE GatewayIndex = @GatewayIndex AND DeviceIndex = @DeviceIndex";
                switch (SQLEnumType)
                {
                    case SQLEnumType.SqlDB:
                        {
                            using (var conn = new SqlConnection(scsb.ConnectionString))
                            {
                                conn.Execute(sql, new { DeviceName, GatewayIndex, DeviceIndex });
                            }
                        }
                        break;
                    case SQLEnumType.MariaDB:
                        {
                            using (var conn = new MySqlConnection(myscbs.ConnectionString))
                            {
                                conn.Execute(sql, new { DeviceName, GatewayIndex, DeviceIndex });
                            }
                        }
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "變更 Electricconfig設備名稱 失敗");
                return false;
            }
        }
        #endregion

        #region 查詢 累積用電度
        /// <summary>
        /// 查詢 累積用電度
        /// </summary>
        /// <param name="StartTime">起始時間</param>
        /// <param name="EndTime">結束時間</param>
        /// <param name="GatewayIndex">通道編號</param>
        /// <param name="DeviceIndex">設備編號</param>
        /// <returns></returns>
        public List<ElectricTotalPrice> Search_ElectricTotalPrice(string StartTime, string EndTime, int GatewayIndex, int DeviceIndex)
        {
            try
            {
                List<ElectricTotalPrice> electricTotalPrices = null;
                string sql = "SELECT * FROM ElectricTotalPrice WHERE ttime >= @StartTime AND ttime <= @EndTime AND GatewayIndex = @GatewayIndex AND DeviceIndex = @DeviceIndex ";
                switch (SQLEnumType)
                {
                    case SQLEnumType.SqlDB:
                        {
                            using (var conn = new SqlConnection(scsb.ConnectionString))
                            {
                                electricTotalPrices = conn.Query<ElectricTotalPrice>(sql, new { StartTime , EndTime , GatewayIndex, DeviceIndex }).ToList();
                            }
                        }
                        break;
                    case SQLEnumType.MariaDB:
                        {
                            using (var conn = new MySqlConnection(myscbs.ConnectionString))
                            {
                                electricTotalPrices = conn.Query<ElectricTotalPrice>(sql, new { StartTime, EndTime , GatewayIndex, DeviceIndex }).ToList();
                            }
                        }
                        break;
                }
                return electricTotalPrices;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "查詢 累積用電度 失敗");
                return null;
            }
        }
        #endregion

        #region 查詢 即時用電
        /// <summary>
        /// 查詢 即時用電
        /// </summary>
        /// <param name="StartTime">起始時間</param>
        /// <param name="EndTime">結束時間</param>
        /// <param name="GatewayIndex">通道編號</param>
        /// <param name="DeviceIndex">設備編號</param>
        /// <returns></returns>
        public List<ThreePhaseElectricMeter> Search_ThreePhaseElectricMeter_Log(string StartTime, string EndTime, int GatewayIndex, int DeviceIndex)
        {
            try
            {
                List<ThreePhaseElectricMeter> threePhaseElectricMeters = null;
                string sql = "SELECT * FROM ThreePhaseElectricMeter_Log WHERE ttime >= @StartTime AND ttime <= @EndTime AND GatewayIndex = @GatewayIndex AND DeviceIndex = @DeviceIndex ";
                switch (SQLEnumType)
                {
                    case SQLEnumType.SqlDB:
                        {
                            using (var conn = new SqlConnection(scsb.ConnectionString))
                            {
                                threePhaseElectricMeters = conn.Query<ThreePhaseElectricMeter>(sql, new { StartTime = StartTime + "000000", EndTime = EndTime + "999999", GatewayIndex, DeviceIndex }).ToList();
                            }
                        }
                        break;
                    case SQLEnumType.MariaDB:
                        {
                            using (var conn = new MySqlConnection(myscbs.ConnectionString))
                            {
                                threePhaseElectricMeters = conn.Query<ThreePhaseElectricMeter>(sql, new { StartTime = StartTime + "000000", EndTime = EndTime + "999999", GatewayIndex, DeviceIndex }).ToList();
                            }
                        }
                        break;
                }
                return threePhaseElectricMeters;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "查詢 即時用電 失敗");
                return null;
            }
        }
        #endregion

        #region 查詢 累積用電度(畫面顯示使用)
        /// <summary>
        /// 查詢 累積用電度(畫面顯示使用)
        /// </summary>
        /// <param name="searchEnumType">0 = 本日累積用電，1 = 昨日累積用電，2 = 本月累積用電，3 =  總累積用電</param>
        /// <param name="GatewayIndex">通道編號</param>
        /// <param name="DeviceIndex">設備編號</param>
        /// <returns></returns>
        public List<ElectricTotalPrice> Search_ElectricTotalPrice(int searchEnumType, int GatewayIndex, int DeviceIndex)
        {
            string searchenumtype = string.Empty;
            try
            {
                List<ElectricTotalPrice> electricTotalPrices = null;
                string sql = "SELECT * FROM ElectricTotalPrice WHERE ttime >= @StartTime AND ttime <= @EndTime AND GatewayIndex = @GatewayIndex AND DeviceIndex = @DeviceIndex ";
                string Monthsql = "SELECT MAX(ttime) AS ttime," +
                                    "MAX(ttimen) AS ttimen," +
                                    "MAX(GatewayIndex) AS GatewayIndex," +
                                    "MAX(DeviceIndex) AS DeviceIndex," +
                                    "MAX(KwhStart1) AS KwhStart1," +
                                    "MAX(KwhEnd1) AS KwhEnd1," +
                                    "MAX(KwhStart2) AS KwhStart2," +
                                    "MAX(KwhEnd2) AS KwhEnd2, " +
                                    "SUM(KwhTotal) AS KwhTotal," +
                                    "MAX(UnitPrice) AS UnitPrice, " +
                                    "SUM(Price) AS Pricek " +
                                    "FROM ElectricTotalPrice WHERE ttime LIKE @StartTime AND GatewayIndex = @GatewayIndex AND DeviceIndex = @DeviceIndex ";
                string totalsql = "SELECT MAX(ttime) AS ttime," +
                                    "MAX(ttimen) AS ttimen," +
                                    "MAX(GatewayIndex) AS GatewayIndex," +
                                    "MAX(DeviceIndex) AS DeviceIndex," +
                                    "MAX(KwhStart1) AS KwhStart1," +
                                    "MAX(KwhEnd1) AS KwhEnd1," +
                                    "MAX(KwhStart2) AS KwhStart2," +
                                    "MAX(KwhEnd2) AS KwhEnd2, " +
                                    "SUM(KwhTotal) AS KwhTotal," +
                                    "MAX(UnitPrice) AS UnitPrice, " +
                                    "SUM(Price) AS Pricek " +
                                    "FROM ElectricTotalPrice WHERE  GatewayIndex = @GatewayIndex AND DeviceIndex = @DeviceIndex ";
                SearchEnumType = (SearchEnumType)searchEnumType;
                switch (SQLEnumType)
                {
                    case SQLEnumType.SqlDB:
                        {
                            using (var conn = new SqlConnection(scsb.ConnectionString))
                            {
                                switch (SearchEnumType)
                                {
                                    case SearchEnumType.NowDay:
                                        {
                                            searchenumtype = "本日累積用電";
                                            electricTotalPrices = conn.Query<ElectricTotalPrice>(sql, new { StartTime = DateTime.Now.ToString("yyyyMMdd"), EndTime = DateTime.Now.ToString("yyyyMMdd"), GatewayIndex, DeviceIndex }).ToList();
                                        }
                                        break;
                                    case SearchEnumType.AfterDay:
                                        {
                                            searchenumtype = "昨日累積用電";
                                            electricTotalPrices = conn.Query<ElectricTotalPrice>(sql, new { StartTime = DateTime.Now.AddDays(-1).ToString("yyyyMMdd"), EndTime = DateTime.Now.AddDays(-1).ToString("yyyyMMdd"), GatewayIndex, DeviceIndex }).ToList();
                                        }
                                        break;
                                    case SearchEnumType.NowMonth:
                                        {
                                            searchenumtype = "本月累積用電";
                                            electricTotalPrices = conn.Query<ElectricTotalPrice>(Monthsql, new { StartTime = DateTime.Now.ToString("yyyyMM")+"%", GatewayIndex, DeviceIndex }).ToList();
                                        }
                                        break;
                                    case SearchEnumType.Total:
                                        {
                                            searchenumtype = "總累積用電";
                                            electricTotalPrices = conn.Query<ElectricTotalPrice>(totalsql, new { GatewayIndex, DeviceIndex }).ToList();
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    case SQLEnumType.MariaDB:
                        {
                            using (var conn = new MySqlConnection(myscbs.ConnectionString))
                            {
                                switch (SearchEnumType)
                                {
                                    case SearchEnumType.NowDay:
                                        {
                                            searchenumtype = "本日累積用電";
                                            electricTotalPrices = conn.Query<ElectricTotalPrice>(sql, new { StartTime = DateTime.Now.ToString("yyyyMMdd"), EndTime = DateTime.Now.ToString("yyyyMMdd"), GatewayIndex, DeviceIndex }).ToList();
                                        }
                                        break;
                                    case SearchEnumType.AfterDay:
                                        {
                                            searchenumtype = "昨日累積用電";
                                            electricTotalPrices = conn.Query<ElectricTotalPrice>(sql, new { StartTime = DateTime.Now.AddDays(-1).ToString("yyyyMMdd"), EndTime = DateTime.Now.AddDays(-1).ToString("yyyyMMdd"), GatewayIndex, DeviceIndex }).ToList();
                                        }
                                        break;
                                    case SearchEnumType.NowMonth:
                                        {
                                            searchenumtype = "本月累積用電";
                                            electricTotalPrices = conn.Query<ElectricTotalPrice>(Monthsql, new { StartTime = DateTime.Now.ToString("yyyyMM")+"%", GatewayIndex, DeviceIndex }).ToList();
                                        }
                                        break;
                                    case SearchEnumType.Total:
                                        {
                                            searchenumtype = "總累積用電";
                                            electricTotalPrices = conn.Query<ElectricTotalPrice>(totalsql, new { GatewayIndex, DeviceIndex }).ToList();
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                }
                return electricTotalPrices;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"查詢 累積用電度(畫面顯示使用): {searchenumtype} 失敗");
                return null;
            }
        }
        #endregion

        #region 查詢 累積用電度(報表)
        /// <summary>
        /// 查詢 累積用電度(報表)
        /// </summary>
        /// <param name="StartTime">起始時間</param>
        /// <param name="EndTime">結束時間</param>
        /// <param name="GatewayIndex">通道編號</param>
        /// <param name="DeviceIndex">設備編號</param>
        /// <returns></returns>
        public ElectricTotalPrice Search_ElectricTotalPrice_Billing(string StartTime, string EndTime, int GatewayIndex, int DeviceIndex)
        {
            try
            {
                List<ElectricTotalPrice> electricTotalPrices = null;
                string sql = "SELECT MAX(ttime) AS ttime," +
                             "MAX(ttimen) AS ttimen ," +
                             "MAX(GatewayIndex) AS GatewayIndex ," +
                             "MAX(DeviceIndex) AS DeviceIndex," +
                             "MAX(KwhStart1) AS KwhStart1," +
                             "MAX(KwhEnd1) AS KwhEnd1," +
                             "MAX(KwhStart2) AS KwhStart2," +
                             "MAX(KwhEnd2) AS KwhEnd2, " +
                             "SUM(KwhTotal) AS KwhTotal, " +
                             "MAX(UnitPrice) AS UnitPrice, " +
                             "SUM(Price) AS Price " +
                             "FROM ElectricTotalPrice WHERE ttime >= @StartTime AND ttime <= @EndTime AND GatewayIndex = @GatewayIndex AND DeviceIndex = @DeviceIndex ";
                switch (SQLEnumType)
                {
                    case SQLEnumType.SqlDB:
                        {
                            using (var conn = new SqlConnection(scsb.ConnectionString))
                            {
                                electricTotalPrices = conn.Query<ElectricTotalPrice>(sql, new {StartTime,EndTime, GatewayIndex, DeviceIndex }).ToList();
                            }
                        }
                        break;
                    case SQLEnumType.MariaDB:
                        {
                            using (var conn = new MySqlConnection(myscbs.ConnectionString))
                            {
                                electricTotalPrices = conn.Query<ElectricTotalPrice>(sql, new {StartTime,  EndTime, GatewayIndex, DeviceIndex }).ToList();
                            }
                        }
                        break;
                }
                return electricTotalPrices[0];
            }
            catch (Exception ex)
            {
                Log.Error(ex, "查詢 累積用電度(報表) 失敗");
                return null;
            }
        }
        #endregion

        #region 更新三相電表 ForWeb、Log與預存程序
        /// <summary>
        /// 更新三相電表 ForWeb與Log
        /// </summary>
        /// <param name="data"></param>
        public void Insert_ThreePhaseElectricMeter(ElectricMeterData data)
        {
            try
            {
                DateTime ttimen = DateTime.Now;
                string ttime = ttimen.ToString("yyyyMMddHHmmss");
                string Checksql = string.Empty;
                string UpdataForwebsql = string.Empty;
                string InsertForweb = string.Empty;
                string InsertLogsql = string.Empty;
                string Proceduresql = string.Empty;
                string Checksql_Log = string.Empty;
                Checksql = $"SELECT * FROM ThreePhaseElectricMeter_ForWeb WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
                Checksql_Log = $"SELECT * FROM ThreePhaseElectricMeter_Log WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex} AND ttime = '{ttimen:yyyyMMddHHmm00}'";
                UpdataForwebsql = $"UPDATE ThreePhaseElectricMeter_ForWeb SET " +
                                  $"ttime= '{ttime}'," +
                                  $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                                  $"rv = {data.Rv}," +
                                  $"sv={data.Sv}," +
                                  $"tv={data.Tv}," +
                                  $"rsv = {data.RSv}," +
                                  $"stv = {data.STv}," +
                                  $"trv = {data.TRv}," +
                                  $"ra={data.RA}," +
                                  $"sa={data.SA}," +
                                  $"ta={data.TA}," +
                                  $"kw = {data.kW}," +
                                  $"kwh = {data.kWh}," +
                                  $"kvar={data.kVAR}," +
                                  $"kvarh={data.kVARh}," +
                                  $"kva={data.kVA}," +
                                  $"kvah={data.kVAh}," +
                                  $"pfe = {data.PF}," +
                                  $"hz={data.HZ} " +
                                  $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex} ";
                InsertForweb = $"INSERT INTO {setting.InitialCatalog}.ThreePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,rv,sv,tv,rsv,stv,trv,ra,sa,ta,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                            $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv},{data.Sv},{data.Tv},{data.RSv},{data.STv},{data.TRv},{data.RA},{data.SA},{data.TA},{data.kW},{data.kWh},{data.kVAR},{data.kVARh},{data.PF},{data.kVA},{data.kVAh},{data.HZ})";
                InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.ThreePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,rv,sv,tv,rsv,stv,trv,ra,sa,ta,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                            $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv},{data.Sv},{data.Tv},{data.RSv},{data.STv},{data.TRv},{data.RA},{data.SA},{data.TA},{data.kW},{data.kWh},{data.kVAR},{data.kVARh},{data.PF},{data.kVA},{data.kVAh},{data.HZ})";
                switch (SQLEnumType)
                {
                    case SQLEnumType.SqlDB:
                        {
                            using (var conn = new SqlConnection(scsb.ConnectionString))
                            {
                                Proceduresql = $"EXEC ElectricdailykwhpriceProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.kWh},{BankAccountSetting.ElectricityCost}";
                                var Exist = conn.Query<ThreePhaseElectricMeter>(Checksql).ToList();
                                if (Exist.Count > 0)
                                {
                                    conn.Execute(UpdataForwebsql);
                                }
                                else
                                {
                                    conn.Execute(InsertForweb);
                                }
                                var Exist_Log = conn.Query<ThreePhaseElectricMeter>(Checksql_Log).ToList();
                                if (Exist_Log.Count == 0)
                                {
                                    conn.Execute(InsertLogsql);
                                }
                                conn.Execute(Proceduresql);
                            }
                        }
                        break;
                    case SQLEnumType.MariaDB:
                        {
                            using (var conn = new MySqlConnection(myscbs.ConnectionString))
                            {
                                Proceduresql = $"CALL ElectricdailykwhpriceProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.kWh},{BankAccountSetting.ElectricityCost})";

                                var Exist = conn.Query<ThreePhaseElectricMeter>(Checksql).ToList();
                                if (Exist.Count > 0)
                                {
                                    conn.Execute(UpdataForwebsql);
                                }
                                else
                                {
                                    conn.Execute(InsertForweb);
                                }
                                var Exist_Log = conn.Query<ThreePhaseElectricMeter>(Checksql_Log).ToList();
                                if (Exist_Log.Count == 0)
                                {
                                    conn.Execute(InsertLogsql);
                                }
                                conn.Execute(Proceduresql);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "寫入 更新三相電表ForWeb與Log 失敗");
            }
        }
        #endregion

        #region 更新三相電表(單相使用) ForWeb、Log與預存程序
        /// <summary>
        /// 更新三相電表(單相使用) ForWeb與Log
        /// </summary>
        /// <param name="data"></param>
        public void Insert_SinglePhaseElectricMeter(ElectricMeterData data)
        {
            DateTime ttimen = DateTime.Now;
            string ttime = ttimen.ToString("yyyyMMddHHmmss");
            string Checksql = string.Empty;
            string Checksql_Log = string.Empty;
            string UpdataForwebsql = string.Empty;
            string InsertForweb = string.Empty;
            string InsertLogsql = string.Empty;
            string Proceduresql = string.Empty;
            Checksql = $"SELECT * FROM SinglePhaseElectricMeter_ForWeb WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
            Checksql_Log = $"SELECT * FROM SinglePhaseElectricMeter_Log WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex} AND ttime = '{ttimen:yyyyMMddHHmm00}'";
            PhaseAngleEnumType = (PhaseAngleEnumType)data.PhaseAngleEnumType;
            switch (PhaseAngleEnumType)
            {
                case PhaseAngleEnumType.R:
                    {
                        UpdataForwebsql = $"UPDATE SinglePhaseElectricMeter_ForWeb SET " +
                             $"ttime= '{ttime}'," +
                             $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                             $"v = {data.Rv}," +
                             $"a={data.RA}," +
                             $"kw = {data.kWh_A}," +
                             $"kwh = {data.kWh_A}," +
                             $"kvar={data.kVAR_A}," +
                             $"kvarh={data.kVARh_A}," +
                             $"kva={data.kVA_A}," +
                             $"kvah={data.kVAh_A}," +
                             $"pfe = {data.PF_A}," +
                             $"hz={data.HZ} " +
                             $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
                        InsertForweb = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv},{data.RA},{data.kW_A},{data.kWh_A},{data.kVAR_A},{data.kVARh_A},{data.PF_A},{data.kVA_A},{data.kVAh_A},{data.HZ})";
                        InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv},{data.RA},{data.kW_A},{data.kWh_A},{data.kVAR_A},{data.kVARh_A},{data.PF_A},{data.kVA_A},{data.kVAh_A},{data.HZ})";
                    }
                    break;
                case PhaseAngleEnumType.S:
                    {
                        UpdataForwebsql = $"UPDATE SinglePhaseElectricMeter_ForWeb SET " +
                             $"ttime= '{ttime}'," +
                             $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                             $"v = {data.Sv}," +
                             $"a={data.SA}," +
                             $"kw = {data.kWh_B}," +
                             $"kwh = {data.kWh_B}," +
                             $"kvar={data.kVAR_B}," +
                             $"kvarh={data.kVARh_B}," +
                             $"kva={data.kVA_B}," +
                             $"kvah={data.kVAh_B}," +
                             $"pfe = {data.PF_B}," +
                             $"hz={data.HZ} " +
                             $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
                        InsertForweb = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Sv},{data.SA},{data.kW_B},{data.kWh_B},{data.kVAR_B},{data.kVARh_B},{data.PF_B},{data.kVA_B},{data.kVAh_B},{data.HZ})";
                        InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Sv},{data.SA},{data.kW_B},{data.kWh_B},{data.kVAR_B},{data.kVARh_B},{data.PF_B},{data.kVA_B},{data.kVAh_B},{data.HZ})";
                    }
                    break;
                case PhaseAngleEnumType.T:
                    {
                        UpdataForwebsql = $"UPDATE SinglePhaseElectricMeter_ForWeb SET " +
                             $"ttime= '{ttime}'," +
                             $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                             $"v = {data.Tv}," +
                             $"a={data.TA}," +
                             $"kw = {data.kWh_C}," +
                             $"kwh = {data.kWh_C}," +
                             $"kvar={data.kVAR_C}," +
                             $"kvarh={data.kVARh_C}," +
                             $"kva={data.kVA_C}," +
                             $"kvah={data.kVAh_C}," +
                             $"pfe = {data.PF_C}," +
                             $"hz={data.HZ} " +
                             $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
                        InsertForweb = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Tv},{data.TA},{data.kW_C},{data.kWh_C},{data.kVAR_C},{data.kVARh_C},{data.PF_C},{data.kVA_C},{data.kVAh_C},{data.HZ})";
                        InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Tv},{data.TA},{data.kW_C},{data.kWh_C},{data.kVAR_C},{data.kVARh_C},{data.PF_C},{data.kVA_C},{data.kVAh_C},{data.HZ})";
                    }
                    break;
            }
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        switch (PhaseAngleEnumType)
                        {
                            case PhaseAngleEnumType.R:
                                {
                                    Proceduresql = $"EXEC ElectricdailykwhpriceProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.kWh_A},{BankAccountSetting.ElectricityCost}";
                                }
                                break;
                            case PhaseAngleEnumType.S:
                                {
                                    Proceduresql = $"EXEC ElectricdailykwhpriceProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.kWh_B},{BankAccountSetting.ElectricityCost}";
                                }
                                break;
                            case PhaseAngleEnumType.T:
                                {
                                    Proceduresql = $"EXEC ElectricdailykwhpriceProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.kWh_C},{BankAccountSetting.ElectricityCost}";
                                }
                                break;
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        switch (PhaseAngleEnumType)
                        {
                            case PhaseAngleEnumType.R:
                                {
                                    Proceduresql = $"CALL ElectricdailykwhpriceProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.kWh_A},{BankAccountSetting.ElectricityCost})";
                                }
                                break;
                            case PhaseAngleEnumType.S:
                                {
                                    Proceduresql = $"CALL ElectricdailykwhpriceProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.kWh_B},{BankAccountSetting.ElectricityCost})";
                                }
                                break;
                            case PhaseAngleEnumType.T:
                                {
                                    Proceduresql = $"CALL ElectricdailykwhpriceProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.kWh_C},{BankAccountSetting.ElectricityCost})";
                                }
                                break;
                        }
                    }
                    break;
            }
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        using (var conn = new SqlConnection(scsb.ConnectionString))
                        {
                            var Exist = conn.Query<SinglePhaseElectricMeterLog>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var Exist_Log = conn.Query<SinglePhaseElectricMeterLog>(Checksql_Log).ToList();
                            if (Exist_Log.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                            conn.Execute(Proceduresql);
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        using (var conn = new MySqlConnection(myscbs.ConnectionString))
                        {
                            var Exist = conn.Query<SinglePhaseElectricMeterLog>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var Exist_Log = conn.Query<SinglePhaseElectricMeterLog>(Checksql_Log).ToList();
                            if (Exist_Log.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                            conn.Execute(Proceduresql);
                        }
                    }
                    break;
            }

        }
        #endregion

        #region 更新多迴路電表(三相使用) ForWeb、Log與預存程序
        /// <summary>
        /// 更新多迴路電表(三相使用) ForWeb與Log
        /// </summary>
        /// <param name="data"></param>
        public void Insert_ThreePhaseElectricMeter(MultiCircuitElectricMeterData data)
        {
            DateTime ttimen = DateTime.Now;
            string ttime = ttimen.ToString("yyyyMMddHHmmss");
            string Checksql = string.Empty;
            string UpdataForwebsql = string.Empty;
            string InsertForweb = string.Empty;
            string InsertLogsql = string.Empty;
            string Proceduresql = string.Empty;
            string Checksql_Log = string.Empty;
            Checksql = $"SELECT * FROM ThreePhaseElectricMeter_ForWeb WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
            Checksql_Log = $"SELECT * FROM ThreePhaseElectricMeter_Log WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex} AND ttime = '{ttimen:yyyyMMddHHmm00}'";
            UpdataForwebsql = $"UPDATE ThreePhaseElectricMeter_ForWeb SET " +
                              $"ttime= '{ttime}'," +
                              $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                              $"rv = {data.Rv[data.LoopEnumType]}," +
                              $"sv={data.Sv[data.LoopEnumType]}," +
                              $"tv={data.Tv[data.LoopEnumType]}," +
                              $"rsv = {data.RSv[data.LoopEnumType]}," +
                              $"stv = {data.STv[data.LoopEnumType]}," +
                              $"trv = {data.TRv[data.LoopEnumType]}," +
                              $"ra={data.RA[data.LoopEnumType]}," +
                              $"sa={data.SA[data.LoopEnumType]}," +
                              $"ta={data.TA[data.LoopEnumType]}," +
                              $"kw = {data.kW[data.LoopEnumType]}," +
                              $"kwh = {data.kWh[data.LoopEnumType]}," +
                              $"kvar={data.kVAR[data.LoopEnumType]}," +
                              $"kvarh={data.kVARh[data.LoopEnumType]}," +
                              $"kva={data.kVA[data.LoopEnumType]}," +
                              $"kvah={data.kVAh[data.LoopEnumType]}," +
                              $"pfe = {data.PF[data.LoopEnumType]}," +
                              $"hz={data.HZ[data.LoopEnumType]} " +
                              $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
            InsertForweb = $"INSERT INTO {setting.InitialCatalog}.ThreePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,rv,sv,tv,rsv,stv,trv,ra,sa,ta,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                        $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv[data.LoopEnumType]},{data.Sv[data.LoopEnumType]},{data.Tv[data.LoopEnumType]},{data.RSv[data.LoopEnumType]},{data.STv[data.LoopEnumType]},{data.TRv[data.LoopEnumType]},{data.RA[data.LoopEnumType]},{data.SA[data.LoopEnumType]},{data.TA[data.LoopEnumType]},{data.kW[data.LoopEnumType]},{data.kWh[data.LoopEnumType]},{data.kVAR[data.LoopEnumType]},{data.kVARh[data.LoopEnumType]},{data.PF[data.LoopEnumType]},{data.kVA[data.LoopEnumType]},{data.kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
            InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.ThreePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,rv,sv,tv,rsv,stv,trv,ra,sa,ta,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                        $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv[data.LoopEnumType]},{data.Sv[data.LoopEnumType]},{data.Tv[data.LoopEnumType]},{data.RSv[data.LoopEnumType]},{data.STv[data.LoopEnumType]},{data.TRv[data.LoopEnumType]},{data.RA[data.LoopEnumType]},{data.SA[data.LoopEnumType]},{data.TA[data.LoopEnumType]},{data.kW[data.LoopEnumType]},{data.kWh[data.LoopEnumType]},{data.kVAR[data.LoopEnumType]},{data.kVARh[data.LoopEnumType]},{data.PF[data.LoopEnumType]},{data.kVA[data.LoopEnumType]},{data.kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        using (var conn = new SqlConnection(scsb.ConnectionString))
                        {
                            Proceduresql = $"EXEC ElectricdailykwhpriceProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.kWh[data.LoopEnumType]},{BankAccountSetting.ElectricityCost}";
                            var Exist = conn.Query<ThreePhaseElectricMeter>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var Exist_Log = conn.Query<ThreePhaseElectricMeter>(Checksql_Log).ToList();
                            if (Exist_Log.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                            conn.Execute(Proceduresql);
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        using (var conn = new MySqlConnection(myscbs.ConnectionString))
                        {
                            Proceduresql = $"CALL ElectricdailykwhpriceProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.kWh[data.LoopEnumType]},{BankAccountSetting.ElectricityCost})";

                            var Exist = conn.Query<ThreePhaseElectricMeter>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var Exist_Log = conn.Query<ThreePhaseElectricMeter>(Checksql_Log).ToList();
                            if (Exist_Log.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                            conn.Execute(Proceduresql);
                        }
                    }
                    break;
            }

        }
        #endregion

        #region 更新多迴路電表(單相使用) ForWeb、Log與預存程序
        /// <summary>
        /// 更新三相電表(單相使用) ForWeb與Log
        /// </summary>
        /// <param name="data"></param>
        public void Insert_SinglePhaseElectricMeter(MultiCircuitElectricMeterData data)
        {
            DateTime ttimen = DateTime.Now;
            string ttime = ttimen.ToString("yyyyMMddHHmmss");
            string Checksql = string.Empty;
            string Checksql_Log = string.Empty;
            string UpdataForwebsql = string.Empty;
            string InsertForweb = string.Empty;
            string InsertLogsql = string.Empty;
            string Proceduresql = string.Empty;
            Checksql = $"SELECT * FROM SinglePhaseElectricMeter_ForWeb WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
            Checksql_Log = $"SELECT * FROM SinglePhaseElectricMeter_Log WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex} AND ttime = '{ttimen:yyyyMMddHHmm00}'";
            switch (PhaseAngleEnumType)
            {
                case PhaseAngleEnumType.R:
                    {
                        UpdataForwebsql = $"UPDATE SinglePhaseElectricMeter_ForWeb SET " +
                             $"ttime= '{ttime}'," +
                             $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                             $"v = {data.Rv[data.LoopEnumType]}," +
                             $"a={data.RA[data.LoopEnumType]}," +
                             $"kw = {data.R_kW[data.LoopEnumType]}," +
                             $"kwh = {data.R_kWh[data.LoopEnumType]}," +
                             $"kvar={data.R_kVAR[data.LoopEnumType]}," +
                             $"kvarh={data.R_kVARh[data.LoopEnumType]}," +
                             $"kva={data.R_kVA[data.LoopEnumType]}," +
                             $"kvah={data.R_kVAh[data.LoopEnumType]}," +
                             $"pfe = {data.R_PF[data.LoopEnumType]}," +
                             $"hz={data.HZ[data.LoopEnumType]} " +
                             $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
                        InsertForweb = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv},{data.RA},{data.R_kW[data.LoopEnumType]},{data.R_kWh[data.LoopEnumType]},{data.R_kVAR[data.LoopEnumType]},{data.R_kVARh[data.LoopEnumType]},{data.R_PF[data.LoopEnumType]},{data.R_kVA[data.LoopEnumType]},{data.R_kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
                        InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Rv},{data.RA},{data.R_kW[data.LoopEnumType]},{data.R_kWh[data.LoopEnumType]},{data.R_kVAR[data.LoopEnumType]},{data.R_kVARh[data.LoopEnumType]},{data.R_PF[data.LoopEnumType]},{data.R_kVA[data.LoopEnumType]},{data.R_kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
                    }
                    break;
                case PhaseAngleEnumType.S:
                    {
                        UpdataForwebsql = $"UPDATE SinglePhaseElectricMeter_ForWeb SET " +
                             $"ttime= '{ttime}'," +
                             $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                             $"v = {data.Sv[data.LoopEnumType]}," +
                             $"a={data.SA[data.LoopEnumType]}," +
                             $"kw = {data.S_kW[data.LoopEnumType]}," +
                             $"kwh = {data.S_kWh[data.LoopEnumType]}," +
                             $"kvar={data.S_kVAR[data.LoopEnumType]}," +
                             $"kvarh={data.S_kVARh[data.LoopEnumType]}," +
                             $"kva={data.S_kVA[data.LoopEnumType]}," +
                             $"kvah={data.S_kVAh[data.LoopEnumType]}," +
                             $"pfe = {data.S_PF[data.LoopEnumType]}," +
                             $"hz={data.HZ[data.LoopEnumType]} " +
                             $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
                        InsertForweb = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Sv},{data.SA},{data.S_kW[data.LoopEnumType]},{data.S_kWh[data.LoopEnumType]},{data.S_kVAR[data.LoopEnumType]},{data.S_kVARh[data.LoopEnumType]},{data.S_PF[data.LoopEnumType]},{data.S_kVA[data.LoopEnumType]},{data.S_kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
                        InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Sv},{data.SA},{data.S_kW[data.LoopEnumType]},{data.S_kWh[data.LoopEnumType]},{data.S_kVAR[data.LoopEnumType]},{data.S_kVARh[data.LoopEnumType]},{data.S_PF[data.LoopEnumType]},{data.S_kVA[data.LoopEnumType]},{data.S_kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
                    }
                    break;
                case PhaseAngleEnumType.T:
                    {
                        UpdataForwebsql = $"UPDATE SinglePhaseElectricMeter_ForWeb SET " +
                             $"ttime= '{ttime}'," +
                             $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                             $"v = {data.Tv[data.LoopEnumType]}," +
                             $"a={data.TA[data.LoopEnumType]}," +
                             $"kw = {data.T_kW[data.LoopEnumType]}," +
                             $"kwh = {data.T_kWh[data.LoopEnumType]}," +
                             $"kvar={data.T_kVAR[data.LoopEnumType]}," +
                             $"kvarh={data.T_kVARh[data.LoopEnumType]}," +
                             $"kva={data.T_kVA[data.LoopEnumType]}," +
                             $"kvah={data.T_kVAh[data.LoopEnumType]}," +
                             $"pfe = {data.T_PF[data.LoopEnumType]}," +
                             $"hz={data.HZ[data.LoopEnumType]} " +
                             $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
                        InsertForweb = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Tv[data.LoopEnumType]},{data.TA[data.LoopEnumType]},{data.T_kW[data.LoopEnumType]},{data.T_kWh[data.LoopEnumType]},{data.T_kVAR[data.LoopEnumType]},{data.T_kVARh[data.LoopEnumType]},{data.T_PF[data.LoopEnumType]},{data.T_kVA},{data.T_kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
                        InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.SinglePhaseElectricMeter_Log (ttime,ttimen,GatewayIndex,DeviceIndex,v,a,kw,kwh,kvar,kvarh,pfe,kva,kvah,hz)VALUES(" +
                                    $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Tv[data.LoopEnumType]},{data.TA[data.LoopEnumType]},{data.T_kW[data.LoopEnumType]},{data.T_kWh[data.LoopEnumType]},{data.T_kVAR[data.LoopEnumType]},{data.T_kVARh[data.LoopEnumType]},{data.T_PF[data.LoopEnumType]},{data.T_kVA},{data.T_kVAh[data.LoopEnumType]},{data.HZ[data.LoopEnumType]})";
                    }
                    break;
            }
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        switch (PhaseAngleEnumType)
                        {
                            case PhaseAngleEnumType.R:
                                {
                                    Proceduresql = $"EXEC ElectricdailykwhpriceProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.R_kWh[data.LoopEnumType]},{BankAccountSetting.ElectricityCost}";
                                }
                                break;
                            case PhaseAngleEnumType.S:
                                {
                                    Proceduresql = $"EXEC ElectricdailykwhpriceProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.S_kWh[data.LoopEnumType]},{BankAccountSetting.ElectricityCost}";
                                }
                                break;
                            case PhaseAngleEnumType.T:
                                {
                                    Proceduresql = $"EXEC ElectricdailykwhpriceProcedure '{ttime}',{data.GatewayIndex},{data.DeviceIndex},{data.T_kWh[data.LoopEnumType]},{BankAccountSetting.ElectricityCost}";
                                }
                                break;
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        switch (PhaseAngleEnumType)
                        {
                            case PhaseAngleEnumType.R:
                                {
                                    Proceduresql = $"CALL ElectricdailykwhpriceProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.R_kWh[data.LoopEnumType]},{BankAccountSetting.ElectricityCost})";
                                }
                                break;
                            case PhaseAngleEnumType.S:
                                {
                                    Proceduresql = $"CALL ElectricdailykwhpriceProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.S_kWh[data.LoopEnumType]},{BankAccountSetting.ElectricityCost})";
                                }
                                break;
                            case PhaseAngleEnumType.T:
                                {
                                    Proceduresql = $"CALL ElectricdailykwhpriceProcedure({ttime},{data.GatewayIndex},{data.DeviceIndex},{data.T_kWh[data.LoopEnumType]},{BankAccountSetting.ElectricityCost})";
                                }
                                break;
                        }
                    }
                    break;
            }
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        using (var conn = new SqlConnection(scsb.ConnectionString))
                        {
                            PhaseAngleEnumType = (PhaseAngleEnumType)data.PhaseAngleEnumType;

                            var Exist = conn.Query<SinglePhaseElectricMeterLog>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var Exist_Log = conn.Query<SinglePhaseElectricMeterLog>(Checksql_Log).ToList();
                            if (Exist_Log.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                            conn.Execute(Proceduresql);
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        using (var conn = new MySqlConnection(myscbs.ConnectionString))
                        {
                            PhaseAngleEnumType = (PhaseAngleEnumType)data.PhaseAngleEnumType;

                            var Exist = conn.Query<SinglePhaseElectricMeterLog>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var Exist_Log = conn.Query<SinglePhaseElectricMeterLog>(Checksql_Log).ToList();
                            if (Exist_Log.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                            conn.Execute(Proceduresql);
                        }
                    }
                    break;
            }

        }
        #endregion

        #region 更新溫溼度感測器 ForWeb與Log
        /// <summary>
        /// 更新溫溼度感測器 ForWeb與Log
        /// </summary>
        /// <param name="data"></param>
        public void Insert_Senser(SenserData data)
        {
            DateTime ttimen = DateTime.Now;
            string ttime = ttimen.ToString("yyyyMMddHHmmss");
            string Checksql = string.Empty;
            string Checksql_log = string.Empty;
            string UpdataForwebsql = string.Empty;
            string InsertForweb = string.Empty;
            string InsertLogsql = string.Empty;
            Checksql = $"SELECT * FROM Senser_ForWeb WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
            Checksql_log = $"SELECT * FROM Senser_Log WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex} AND ttime = '{ttimen:yyyyMMddHHmm00}'";
            UpdataForwebsql = $"UPDATE Senser_ForWeb SET " +
                              $"ttime= '{ttime}'," +
                              $"ttimen = '{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}'," +
                              $"Temperature = {data.Temperature}," +
                              $"Humidity={data.Humidity}," +
                              $"PM1={data.PM1}," +
                              $"PM25 = {data.PM25}," +
                              $"PM10 = {data.PM10}," +
                              $"CO2 = {data.CO2}," +
                              $"TVOC={data.TVOC}," +
                              $"HCHO={data.HCHO}," +
                              $"O3={data.O3}," +
                              $"CO = {data.CO}," +
                              $"Mold = {data.Mold}," +
                              $"IAQ={data.IAQ} " +
                              $"WHERE GatewayIndex = {data.GatewayIndex} AND DeviceIndex = {data.DeviceIndex}";
            InsertForweb = $"INSERT INTO {setting.InitialCatalog}.Senser_ForWeb (ttime,ttimen,GatewayIndex,DeviceIndex,Temperature,Humidity,PM1,PM25,PM10,CO2,TVOC,HCHO,O3,CO,Mold,IAQ)VALUES(" +
                        $"'{ttime}','{ttimen.ToString("yyyy/MM/dd HH:mm:ss")}',{data.GatewayIndex},{data.DeviceIndex},{data.Temperature},{data.Humidity },{data.PM1},{data.PM25},{data.PM10},{data.CO2},{data.TVOC},{data.HCHO},{data.O3},{data.CO},{data.Mold},{data.IAQ})";
            InsertLogsql = $"INSERT INTO {setting.InitialCatalog}.Senser_Log (ttime,ttimen,GatewayIndex,DeviceIndex,Temperature,Humidity,PM1,PM25,PM10,CO2,TVOC,HCHO,O3,CO,Mold,IAQ)VALUES(" +
                          $"'{ttimen:yyyyMMddHHmm00}','{ttimen.ToString("yyyy/MM/dd HH:mm:00")}',{data.GatewayIndex},{data.DeviceIndex},{data.Temperature},{data.Humidity },{data.PM1},{data.PM25},{data.PM10},{data.CO2},{data.TVOC},{data.HCHO},{data.O3},{data.CO},{data.Mold},{data.IAQ})";
            switch (SQLEnumType)
            {
                case SQLEnumType.SqlDB:
                    {
                        using (var conn = new SqlConnection(scsb.ConnectionString))
                        {
                            var Exist = conn.Query<SenserLog>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var LogExist = conn.Query<SenserLog>(Checksql_log).ToList();
                            if (LogExist.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                        }
                    }
                    break;
                case SQLEnumType.MariaDB:
                    {
                        using (var conn = new MySqlConnection(myscbs.ConnectionString))
                        {
                            var Exist = conn.Query<SenserLog>(Checksql).ToList();
                            if (Exist.Count > 0)
                            {
                                conn.Execute(UpdataForwebsql);
                            }
                            else
                            {
                                conn.Execute(InsertForweb);
                            }
                            var LogExist = conn.Query<SenserLog>(Checksql_log).ToList();
                            if (LogExist.Count == 0)
                            {
                                conn.Execute(InsertLogsql);
                            }
                        }
                    }
                    break;
            }

        }
        #endregion
    }
}
