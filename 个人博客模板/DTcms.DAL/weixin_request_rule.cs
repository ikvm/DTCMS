using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// ���ݷ�����:����ظ����
    /// </summary>
    public partial class weixin_request_rule
    {
        private string databaseprefix; //���ݿ����ǰ׺
        public weixin_request_rule(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region ��������================================
        /// <summary>
        /// �õ����ID
        /// </summary>
        private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
        {
            string strSql = "select top 1 id from " + databaseprefix + "weixin_request_rule order by id desc";
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
            strSql.Append("select count(1) from " + databaseprefix + "weixin_request_rule");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.weixin_request_rule model)
        {
            using (OleDbConnection conn = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                conn.Open();
                using (OleDbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "weixin_request_rule(");
                        strSql.Append("is_default,sort_id,add_time,account_id,[name],keywords,request_type,response_type,is_like_query)");
                        strSql.Append(" values (");
                        strSql.Append("@is_default,@sort_id,@add_time,@account_id,@name,@keywords,@request_type,@response_type,@is_like_query)");
                        OleDbParameter[] parameters = {
					            new OleDbParameter("@is_default", OleDbType.Integer,4),
					            new OleDbParameter("@sort_id", OleDbType.Integer,4),
					            new OleDbParameter("@add_time", OleDbType.Date),
					            new OleDbParameter("@account_id", OleDbType.Integer,4),
					            new OleDbParameter("@name", OleDbType.VarChar,200),
					            new OleDbParameter("@keywords", OleDbType.VarChar,2000),
					            new OleDbParameter("@request_type", OleDbType.Integer,4),
					            new OleDbParameter("@response_type", OleDbType.Integer,4),
					            new OleDbParameter("@is_like_query", OleDbType.Integer,4)};
                        parameters[0].Value = model.is_default;
                        parameters[1].Value = model.sort_id;
                        parameters[2].Value = model.add_time;
                        parameters[3].Value = model.account_id;
                        parameters[4].Value = model.name;
                        parameters[5].Value = model.keywords;
                        parameters[6].Value = model.request_type;
                        parameters[7].Value = model.response_type;
                        parameters[8].Value = model.is_like_query;
                        DbHelperOleDb.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        //ȡ���²����ID
                        model.id = GetMaxId(conn, trans);

                        if (model.contents != null)
                        {
                            StringBuilder strSql2;
                            foreach (Model.weixin_request_content models in model.contents)
                            {
                                strSql2 = new StringBuilder();
                                strSql2.Append("insert into " + databaseprefix + "weixin_request_content(");
                                strSql2.Append("sort_id,add_time,account_id,rule_id,title,content,link_url,img_url,media_url,meida_hd_url)");
                                strSql2.Append(" values (");
                                strSql2.Append("@sort_id,@add_time,@account_id,@rule_id,@title,@content,@link_url,@img_url,@media_url,@meida_hd_url)");
                                OleDbParameter[] parameters2 = {
						                new OleDbParameter("@sort_id", OleDbType.Integer,4),
						                new OleDbParameter("@add_time", OleDbType.Date),
						                new OleDbParameter("@account_id", OleDbType.Integer,4),
						                new OleDbParameter("@rule_id", OleDbType.Integer,4),
						                new OleDbParameter("@title", OleDbType.VarChar,500),
						                new OleDbParameter("@content", OleDbType.VarChar),
						                new OleDbParameter("@link_url", OleDbType.VarChar,500),
						                new OleDbParameter("@img_url", OleDbType.VarChar,500),
						                new OleDbParameter("@media_url", OleDbType.VarChar,500),
						                new OleDbParameter("@meida_hd_url", OleDbType.VarChar,500)};
                                parameters2[0].Value = models.sort_id;
                                parameters2[1].Value = models.add_time;
                                parameters2[2].Value = models.account_id;
                                parameters2[3].Value = model.id; //�²���Ĺ���ID
                                parameters2[4].Value = models.title;
                                parameters2[5].Value = models.content;
                                parameters2[6].Value = models.link_url;
                                parameters2[7].Value = models.img_url;
                                parameters2[8].Value = models.media_url;
                                parameters2[9].Value = models.meida_hd_url;
                                DbHelperOleDb.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                            }
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return -1;
                    }
                }
            }
            return model.id;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(Model.weixin_request_rule model)
        {
            using (OleDbConnection conn = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                conn.Open();
                using (OleDbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        #region ������Ϣ============================
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update " + databaseprefix + "weixin_request_rule set ");
                        strSql.Append("account_id=@account_id,");
                        strSql.Append("[name]=@name,");
                        strSql.Append("keywords=@keywords,");
                        strSql.Append("request_type=@request_type,");
                        strSql.Append("response_type=@response_type,");
                        strSql.Append("is_like_query=@is_like_query,");
                        strSql.Append("is_default=@is_default,");
                        strSql.Append("sort_id=@sort_id,");
                        strSql.Append("add_time=@add_time");
                        strSql.Append(" where id=@id");
                        OleDbParameter[] parameters = {
					            new OleDbParameter("@account_id", OleDbType.Integer,4),
					            new OleDbParameter("@name", OleDbType.VarChar,200),
					            new OleDbParameter("@keywords", OleDbType.VarChar,2000),
					            new OleDbParameter("@request_type", OleDbType.Integer,4),
					            new OleDbParameter("@response_type", OleDbType.Integer,4),
					            new OleDbParameter("@is_like_query", OleDbType.Integer,4),
					            new OleDbParameter("@is_default", OleDbType.Integer,4),
					            new OleDbParameter("@sort_id", OleDbType.Integer,4),
					            new OleDbParameter("@add_time", OleDbType.Date),
					            new OleDbParameter("@id", OleDbType.Integer,4)};
                        parameters[0].Value = model.account_id;
                        parameters[1].Value = model.name;
                        parameters[2].Value = model.keywords;
                        parameters[3].Value = model.request_type;
                        parameters[4].Value = model.response_type;
                        parameters[5].Value = model.is_like_query;
                        parameters[6].Value = model.is_default;
                        parameters[7].Value = model.sort_id;
                        parameters[8].Value = model.add_time;
                        parameters[9].Value = model.id;
                        DbHelperOleDb.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        #endregion

                        #region ��ӹ�������========================
                        //��ɾ���Ĺ�������
                        DbHelperOleDb.ExecuteSql(conn, trans, "delete from " + databaseprefix + "weixin_request_content where rule_id=" + model.id);
                        //���/�޸Ĺ�������
                        if (model.contents != null)
                        {
                            StringBuilder strSql2;
                            foreach (Model.weixin_request_content modelt in model.contents)
                            {
                                strSql2 = new StringBuilder();
                                strSql2.Append("insert into " + databaseprefix + "weixin_request_content(");
                                strSql2.Append("account_id,rule_id,title,content,link_url,img_url,media_url,meida_hd_url,sort_id,add_time)");
                                strSql2.Append(" values (");
                                strSql2.Append("@account_id,@rule_id,@title,@content,@link_url,@img_url,@media_url,@meida_hd_url,@sort_id,@add_time)");
                                OleDbParameter[] parameters2 = {
					                    new OleDbParameter("@account_id", OleDbType.Integer,4),
					                    new OleDbParameter("@rule_id", OleDbType.Integer,4),
					                    new OleDbParameter("@title", OleDbType.VarChar,500),
					                    new OleDbParameter("@content", OleDbType.VarChar),
					                    new OleDbParameter("@link_url", OleDbType.VarChar,500),
					                    new OleDbParameter("@img_url", OleDbType.VarChar,500),
					                    new OleDbParameter("@media_url", OleDbType.VarChar,500),
					                    new OleDbParameter("@meida_hd_url", OleDbType.VarChar,500),
					                    new OleDbParameter("@sort_id", OleDbType.Integer,4),
					                    new OleDbParameter("@add_time", OleDbType.Date)};
                                parameters2[0].Value = modelt.account_id;
                                parameters2[1].Value = modelt.rule_id;
                                parameters2[2].Value = modelt.title;
                                parameters2[3].Value = modelt.content;
                                parameters2[4].Value = modelt.link_url;
                                parameters2[5].Value = modelt.img_url;
                                parameters2[6].Value = modelt.media_url;
                                parameters2[7].Value = modelt.meida_hd_url;
                                parameters2[8].Value = modelt.sort_id;
                                parameters2[9].Value = modelt.add_time;
                                DbHelperOleDb.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                            }
                        }
                        #endregion

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int id)
        {
            Hashtable sqllist = new Hashtable();
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from " + databaseprefix + "weixin_request_content ");
            strSql2.Append(" where rule_id=@rule_id ");
            OleDbParameter[] parameters2 = {
					new OleDbParameter("@rule_id", OleDbType.Integer,4)};
            parameters2[0].Value = id;
            sqllist.Add(strSql2.ToString(), parameters2);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "weixin_request_rule ");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;
            sqllist.Add(strSql.ToString(), parameters);

            bool result = DbHelperOleDb.ExecuteSqlTran(sqllist);
            if (result)
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
        public Model.weixin_request_rule GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,account_id,[name],keywords,request_type,response_type,is_like_query,is_default,sort_id,add_time");
            strSql.Append(" from " + databaseprefix + "weixin_request_rule ");
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
            strSql.Append(" id,account_id,[name],keywords,request_type,response_type,is_like_query,is_default,sort_id,add_time ");
            strSql.Append(" FROM " + databaseprefix + "weixin_request_rule ");
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
            strSql.Append("select * FROM " + databaseprefix + "weixin_request_rule");
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
        /// �޸�һ������
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "weixin_request_rule set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperOleDb.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.weixin_request_rule GetRequestTypeModel(int request_type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,account_id,[name],keywords,request_type,response_type,is_like_query,is_default,sort_id,add_time");
            strSql.Append(" from " + databaseprefix + "weixin_request_rule");
            strSql.Append(" where request_type=@request_type");
            OleDbParameter[] parameters = {
                    new OleDbParameter("@request_type", OleDbType.Integer,4)};
            parameters[0].Value = request_type;

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
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.weixin_request_rule DataRowToModel(DataRow row)
        {
            Model.weixin_request_rule model = new Model.weixin_request_rule();
            if (row != null)
            {
                #region ������Ϣ======================
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["account_id"] != null && row["account_id"].ToString() != "")
                {
                    model.account_id = int.Parse(row["account_id"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["keywords"] != null)
                {
                    model.keywords = row["keywords"].ToString();
                }
                if (row["request_type"] != null && row["request_type"].ToString() != "")
                {
                    model.request_type = int.Parse(row["request_type"].ToString());
                }
                if (row["response_type"] != null && row["response_type"].ToString() != "")
                {
                    model.response_type = int.Parse(row["response_type"].ToString());
                }
                if (row["is_like_query"] != null && row["is_like_query"].ToString() != "")
                {
                    model.is_like_query = int.Parse(row["is_like_query"].ToString());
                }
                if (row["is_default"] != null && row["is_default"].ToString() != "")
                {
                    model.is_default = int.Parse(row["is_default"].ToString());
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (row["add_time"] != null && row["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(row["add_time"].ToString());
                }
                #endregion

                #region �ӱ���Ϣ======================
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select id,account_id,rule_id,title,content,link_url,img_url,media_url,meida_hd_url,sort_id,add_time");
                strSql2.Append(" from " + databaseprefix + "weixin_request_content");
                strSql2.Append(" where rule_id=@rule_id");
                OleDbParameter[] parameters2 = {
					new OleDbParameter("@rule_id", OleDbType.Integer,4)};
                parameters2[0].Value = model.id;

                DataSet ds2 = DbHelperOleDb.Query(strSql2.ToString(), parameters2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    List<Model.weixin_request_content> ls = new List<Model.weixin_request_content>();
                    for (int n = 0; n < ds2.Tables[0].Rows.Count; n++)
                    {
                        Model.weixin_request_content modelt = new Model.weixin_request_content();
                        if (ds2.Tables[0].Rows[n]["id"] != null && ds2.Tables[0].Rows[n]["id"].ToString() != "")
                        {
                            modelt.id = int.Parse(ds2.Tables[0].Rows[n]["id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["account_id"] != null && ds2.Tables[0].Rows[n]["account_id"].ToString() != "")
                        {
                            modelt.account_id = int.Parse(ds2.Tables[0].Rows[n]["account_id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["rule_id"] != null && ds2.Tables[0].Rows[n]["rule_id"].ToString() != "")
                        {
                            modelt.rule_id = int.Parse(ds2.Tables[0].Rows[n]["rule_id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["title"] != null)
                        {
                            modelt.title = ds2.Tables[0].Rows[n]["title"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["content"] != null)
                        {
                            modelt.content = ds2.Tables[0].Rows[n]["content"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["link_url"] != null)
                        {
                            modelt.link_url = ds2.Tables[0].Rows[n]["link_url"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["img_url"] != null)
                        {
                            modelt.img_url = ds2.Tables[0].Rows[n]["img_url"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["media_url"] != null)
                        {
                            modelt.media_url = ds2.Tables[0].Rows[n]["media_url"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["meida_hd_url"] != null)
                        {
                            modelt.meida_hd_url = ds2.Tables[0].Rows[n]["meida_hd_url"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["sort_id"] != null && ds2.Tables[0].Rows[n]["sort_id"].ToString() != "")
                        {
                            modelt.sort_id = int.Parse(ds2.Tables[0].Rows[n]["sort_id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["add_time"] != null && ds2.Tables[0].Rows[n]["add_time"].ToString() != "")
                        {
                            modelt.add_time = DateTime.Parse(ds2.Tables[0].Rows[n]["add_time"].ToString());
                        }
                        ls.Add(modelt);
                    }
                    model.contents = ls;
                }
                #endregion
            }
            return model;
        }
        #endregion

        #region ΢��ͨѶ����============================
        /// <summary>
        /// �õ�����ID�Լ��ظ�����
        /// </summary>
        public int GetRuleIdAndResponseType(string strWhere, out int response_type)
        {
            int rule_id = 0;
            response_type = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,response_type from " + databaseprefix + "weixin_request_rule");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            OleDbDataReader sr = DbHelperOleDb.ExecuteReader(strSql.ToString());

            while (sr.Read())
            {
                rule_id = int.Parse(sr["id"].ToString());
                response_type = int.Parse(sr["response_type"].ToString());
            }
            sr.Close();

            return rule_id;
        }

        /// <summary>
        /// �õ��ؽ��ֲ�ѯ�Ĺ���ID���ظ�����(�������Ч�ʿ�ʹ�ô洢����)
        /// </summary>
        public int GetKeywordsRuleId(string keywords, out int response_type)
        {
            int rule_id = 0;
            //��ȷƥ��
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("select top 1 id,response_type from " + databaseprefix + "weixin_request_rule");
            strSql3.Append(" where request_type=1");
            strSql3.Append(" and(keywords like '" + keywords + "|%' or keywords='%|" + keywords + "' or keywords like '%|" + keywords + "|%' or keywords='" + keywords + "')");
            strSql3.Append(" order by sort_id asc,add_time desc");
            DataSet ds3 = DbHelperOleDb.Query(strSql3.ToString());
            if (ds3.Tables[0].Rows.Count > 0)
            {
                rule_id = int.Parse(ds3.Tables[0].Rows[0][0].ToString());
                response_type = int.Parse(ds3.Tables[0].Rows[0][1].ToString());
                return rule_id;
            }
            //ģ��ƥ��
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select top 1 id,response_type from " + databaseprefix + "weixin_request_rule");
            strSql2.Append(" where request_type=1 and keywords like '%" + keywords + "%'");
            strSql2.Append(" order by sort_id asc,add_time desc");
            DataSet ds2 = DbHelperOleDb.Query(strSql2.ToString());
            if (ds2.Tables[0].Rows.Count > 0)
            {
                rule_id = int.Parse(ds2.Tables[0].Rows[0][0].ToString());
                response_type = int.Parse(ds2.Tables[0].Rows[0][1].ToString());
                return rule_id;
            }
            //Ĭ�ϻظ�
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("select top 1 id,response_type from " + databaseprefix + "weixin_request_rule");
            strSql1.Append(" where request_type=0");
            strSql1.Append(" order by sort_id asc,add_time desc");
            DataSet ds1 = DbHelperOleDb.Query(strSql1.ToString());
            if (ds1.Tables[0].Rows.Count > 0)
            {
                rule_id = int.Parse(ds1.Tables[0].Rows[0][0].ToString());
                response_type = int.Parse(ds1.Tables[0].Rows[0][1].ToString());
                return rule_id;
            }
            response_type = 0;
            return rule_id;
        }
        #endregion
    }
}