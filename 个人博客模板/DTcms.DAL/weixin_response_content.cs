using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// ���ݷ�����:����ƽ̨�ظ���Ϣ
    /// </summary>
    public partial class weixin_response_content
    {
        private string databaseprefix; //���ݿ����ǰ׺
        public weixin_response_content(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region ��������================================
        /// <summary>
        /// �õ����ID
        /// </summary>
        private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
        {
            string strSql = "select top 1 id from " + databaseprefix + "weixin_response_content order by id desc";
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
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "weixin_response_content");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.weixin_response_content model)
        {
            int newId;
            using (OleDbConnection conn = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                conn.Open();
                using (OleDbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "weixin_response_content(");
                        strSql.Append("account_id,openid,request_type,request_content,response_type,reponse_content,create_time,xml_content,add_time)");
                        strSql.Append(" values (");
                        strSql.Append("@account_id,@openid,@request_type,@request_content,@response_type,@reponse_content,@create_time,@xml_content,@add_time)");
                        OleDbParameter[] parameters = {
					            new OleDbParameter("@account_id", OleDbType.Integer,4),
					            new OleDbParameter("@openid", OleDbType.VarChar,100),
					            new OleDbParameter("@request_type", OleDbType.VarChar,50),
					            new OleDbParameter("@request_content", OleDbType.VarChar,2000),
					            new OleDbParameter("@response_type", OleDbType.VarChar,50),
					            new OleDbParameter("@reponse_content", OleDbType.VarChar,2000),
					            new OleDbParameter("@create_time", OleDbType.VarChar,50),
					            new OleDbParameter("@xml_content", OleDbType.VarChar,2000),
					            new OleDbParameter("@add_time", OleDbType.Date)};
                        parameters[0].Value = model.account_id;
                        parameters[1].Value = model.openid;
                        parameters[2].Value = model.request_type;
                        parameters[3].Value = model.request_content;
                        parameters[4].Value = model.response_type;
                        parameters[5].Value = model.reponse_content;
                        parameters[6].Value = model.create_time;
                        parameters[7].Value = model.xml_content;
                        parameters[8].Value = model.add_time;
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
        public bool Update(Model.weixin_response_content model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "weixin_response_content set ");
            strSql.Append("account_id=@account_id,");
            strSql.Append("openid=@openid,");
            strSql.Append("request_type=@request_type,");
            strSql.Append("request_content=@request_content,");
            strSql.Append("response_type=@response_type,");
            strSql.Append("reponse_content=@reponse_content,");
            strSql.Append("create_time=@create_time,");
            strSql.Append("xml_content=@xml_content,");
            strSql.Append("add_time=@add_time");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@account_id", OleDbType.Integer,4),
					new OleDbParameter("@openid", OleDbType.VarChar,100),
					new OleDbParameter("@request_type", OleDbType.VarChar,50),
					new OleDbParameter("@request_content", OleDbType.VarChar,2000),
					new OleDbParameter("@response_type", OleDbType.VarChar,50),
					new OleDbParameter("@reponse_content", OleDbType.VarChar,2000),
					new OleDbParameter("@create_time", OleDbType.VarChar,50),
					new OleDbParameter("@xml_content", OleDbType.VarChar,2000),
					new OleDbParameter("@add_time", OleDbType.Date),
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = model.account_id;
            parameters[1].Value = model.openid;
            parameters[2].Value = model.request_type;
            parameters[3].Value = model.request_content;
            parameters[4].Value = model.response_type;
            parameters[5].Value = model.reponse_content;
            parameters[6].Value = model.create_time;
            parameters[7].Value = model.xml_content;
            parameters[8].Value = model.add_time;
            parameters[9].Value = model.id;

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
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "weixin_response_content ");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;

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
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.weixin_response_content GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,account_id,openid,request_type,request_content,response_type,reponse_content,create_time,xml_content,add_time");
            strSql.Append(" from " + databaseprefix + "weixin_response_content ");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;

            DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,account_id,openid,request_type,request_content,response_type,reponse_content,create_time,xml_content,add_time ");
            strSql.Append(" FROM " + databaseprefix + "weixin_response_content");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperOleDb.Query(strSql.ToString());
        }

        /// <summary>
        /// ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "weixin_response_content");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        #endregion

        #region ��չ����================================
        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.weixin_response_content DataRowToModel(DataRow row)
        {
            DTcms.Model.weixin_response_content model = new Model.weixin_response_content();
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
                if (row["openid"] != null)
                {
                    model.openid = row["openid"].ToString();
                }
                if (row["request_type"] != null)
                {
                    model.request_type = row["request_type"].ToString();
                }
                if (row["request_content"] != null)
                {
                    model.request_content = row["request_content"].ToString();
                }
                if (row["response_type"] != null)
                {
                    model.response_type = row["response_type"].ToString();
                }
                if (row["reponse_content"] != null)
                {
                    model.reponse_content = row["reponse_content"].ToString();
                }
                if (row["create_time"] != null)
                {
                    model.create_time = row["create_time"].ToString();
                }
                if (row["xml_content"] != null)
                {
                    model.xml_content = row["xml_content"].ToString();
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