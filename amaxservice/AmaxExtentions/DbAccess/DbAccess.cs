using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxExtentions.DbAccess
{
    public class DbAccess : IDisposable
    {
        #region variable, constructor, distructors

        private string _connectionString;

        public string ConnectionString { get { return _connectionString; } }

        public SqlConnection con;
        public SqlTransaction Transaction = null;
        public DbAccess(string SqlConnectionString)
        {
            this._connectionString = SqlConnectionString;
            if(con==null) con = new SqlConnection(this.ConnectionString);

            con.Open();
        }
        //public DbAccess()
        //{
        //    this._connectionString = ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString;
        //    if (con == null) con = new SqlConnection(this.ConnectionString);

        //    con.Open();
        //}
       
        public void Dispose()
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close(); 
            GC.Collect();
        }

        ~DbAccess()
        {
            Dispose();
        }

        public string getLibVersion()
        {
            return System.Reflection
                .Assembly
                .GetExecutingAssembly()
                .GetName()
                .Version
                .ToString();
        }

        #endregion


        #region Scaller methods

        public object ExecuteScalarAsString(string SqlQuery, Dictionary<string, object> sqlParameters) => ExecuteScalar(SqlQuery, sqlParameters).ToString();

        public object ExecuteScalarAsString(string SqlQuery) => ExecuteScalar(SqlQuery, null).ToString();

        public object ExecuteScalar(string SqlQuery,Dictionary<string,object> sqlParameters=null, bool isSp=false)
        {
            using (SqlCommand cmd = new SqlCommand(SqlQuery, this.con))
            {
                cmd.CommandType = isSp ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text;
                if(sqlParameters!=null)
                    sqlParameters.ToList().ForEach(p => cmd.Parameters.Add(new SqlParameter(p.Key,p.Value)));

                return cmd.ExecuteScalar();
            }
        }
        
        #endregion

        #region DataTable or DataSet Methods
        
        public DataTable GetDataTable(string SqlQuery, Dictionary<string, object> parameterDictionary, bool IsSp = false)
        {
            using (DataTable dt = new DataTable())
            {
                SqlDataAdapter da = new SqlDataAdapter(SqlQuery, this.con);
                da.SelectCommand.CommandType = IsSp ? CommandType.StoredProcedure : CommandType.Text;
                if (parameterDictionary != null) parameterDictionary.ToList().ForEach(p => da.SelectCommand.Parameters.Add(new SqlParameter(p.Key, p.Value)));
                da.Fill(dt);
                return dt;
            }
        }

        public DataSet GetDataSet(string SqlQuery, Dictionary<string,object> parameterDictionary,bool IsSp = false)
        {
            using (DataSet ds = new DataSet())
            {
                SqlDataAdapter da = new SqlDataAdapter(SqlQuery, this.con);
                da.SelectCommand.CommandType = IsSp ? CommandType.StoredProcedure : CommandType.Text;
                if (parameterDictionary != null) parameterDictionary.ToList().ForEach(p => da.SelectCommand.Parameters.Add(new SqlParameter(p.Key, p.Value)));
                da.Fill(ds);
                return ds;
            }
        }
        public int InsertData(string SqlQuery, Dictionary<string,object> parameterDictionary)
        {
            SqlCommand cmd=new SqlCommand(SqlQuery,this.con);
            //this.con.Open();
            //if (this.Transaction != null) {
            //    cmd.Transaction = Transaction;
            //}
                if (parameterDictionary != null) parameterDictionary.ToList().ForEach(p => cmd.Parameters.Add(new SqlParameter(p.Key, p.Value)));
                int g=cmd.ExecuteNonQuery();
            //this.con.Close();
                return g;
        }
        public int InsertData(string SqlQuery, Dictionary<string, object> parameterDictionary, SqlTransaction transaction)
        {
            //transaction = this.con.BeginTransaction()
            SqlCommand cmd = new SqlCommand(SqlQuery, this.con);
            cmd.Transaction = transaction;
            //this.con.Open();
            if (parameterDictionary != null) parameterDictionary.ToList().ForEach(p => cmd.Parameters.Add(new SqlParameter(p.Key, p.Value)));
            int g = cmd.ExecuteNonQuery();
            //this.con.Close();
            return g;
        }
        #endregion
    }
}
