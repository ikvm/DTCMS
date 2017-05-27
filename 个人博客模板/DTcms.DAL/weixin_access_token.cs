using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using DTcms.DBUtility;

namespace DTcms.DAL
{
	/// <summary>
    /// ���ݷ�����:�˻��洢AccessToKenֵ
	/// </summary>
	public partial class weixin_access_token
	{
        private string databaseprefix; //���ݿ����ǰ׺
        public weixin_access_token(string _databaseprefix)
		{
            databaseprefix = _databaseprefix;
        }

        #region ��������================================
        /// <summary>
        /// �õ����ID
        /// </summary>
        private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
        {
            string strSql = "select top 1 id from " + databaseprefix + "weixin_access_token order by id desc";
            object obj = DbHelperOleDb.GetSingle(conn, trans, strSql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists()
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "weixin_access_token");
			return DbHelperOleDb.Exists(strSql.ToString());
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int Add(Model.weixin_access_token model)
		{
            int newId;
            using (OleDbConnection conn = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                conn.Open();
                using (OleDbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
			            StringBuilder strSql=new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "weixin_access_token(");
			            strSql.Append("account_id,access_token,expires_in,[count],add_time)");
			            strSql.Append(" values (");
			            strSql.Append("@account_id,@access_token,@expires_in,@count,@add_time)");
			            OleDbParameter[] parameters = {
					            new OleDbParameter("@account_id", OleDbType.Integer,4),
					            new OleDbParameter("@access_token", OleDbType.VarChar,1000),
					            new OleDbParameter("@expires_in", OleDbType.Integer,4),
					            new OleDbParameter("@count", OleDbType.Integer,4),
					            new OleDbParameter("@add_time", OleDbType.Date)};
			            parameters[0].Value = model.account_id;
			            parameters[1].Value = model.access_token;
			            parameters[2].Value = model.expires_in;
			            parameters[3].Value = model.count;
			            parameters[4].Value = model.add_time;
                        DbHelperOleDb.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        //ȡ���²����ID
                        newId = GetMaxId(conn, trans);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return -1;
                    }
                }
            }
            return newId;
		}

		/// <summary>
		/// ����һ������
		/// </summary>
        public bool Update(Model.weixin_access_token model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "weixin_access_token set ");
            strSql.Append("account_id=@account_id,");
            strSql.Append("access_token=@access_token,");
            strSql.Append("expires_in=@expires_in,");
            strSql.Append("[count]=@count,");
            strSql.Append("add_time=@add_time");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@account_id", OleDbType.Integer,4),
					new OleDbParameter("@access_token", OleDbType.VarChar,1000),
					new OleDbParameter("@expires_in", OleDbType.Integer,4),
					new OleDbParameter("@count", OleDbType.Integer,4),
					new OleDbParameter("@add_time", OleDbType.Date),
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = model.account_id;
            parameters[1].Value = model.access_token;
            parameters[2].Value = model.expires_in;
            parameters[3].Value = model.count;
            parameters[4].Value = model.add_time;
            parameters[5].Value = model.id;

            int rows = DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool Delete()
		{
            int rows = DbHelperOleDb.ExecuteSql("delete from " + databaseprefix + "weixin_access_token");
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public Model.weixin_access_token GetModel()
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select top 1 id,account_id,access_token,expires_in,[count],add_time from " + databaseprefix + "weixin_access_token");
			DataSet ds=DbHelperOleDb.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}
		#endregion

        #region ��չ����================================
        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.weixin_access_token DataRowToModel(DataRow row)
        {
            Model.weixin_access_token model = new Model.weixin_access_token();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["account_id"] != null && row["account_id"].ToString() != "")
                {
                    model.account_id = int.Parse(row["account_id"].ToString());
                }
                if (row["access_token"] != null)
                {
                    model.access_token = row["access_token"].ToString();
                }
                if (row["expires_in"] != null && row["expires_in"].ToString() != "")
                {
                    model.expires_in = int.Parse(row["expires_in"].ToString());
                }
                if (row["count"] != null && row["count"].ToString() != "")
                {
                    model.count = int.Parse(row["count"].ToString());
                }
                if (row["add_time"] != null && row["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(row["add_time"].ToString());
                }
            }
            return model;
        }
        #endregion
    }
}

