using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;
using System.Data.SqlClient;
using DAL.Helper;

namespace DAL
{
    public class ProjectService
    {
        #region 查询项目
        public List<Project>GetProject()
        {
            string sql = "select distinct PROJECT_ID, PROJECT_NAME from PROJECT";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<Project> ProList = new List<Project>();
            while(objReader.Read())
            {
                ProList.Add(new Project()
                {
                    PROJECTID = objReader["PROJECT_ID"].ToString(),
                    PROJECTNAME = objReader["PROJECT_NAME"].ToString()
                });
            }
            objReader.Close();
            return ProList;
        }
        #endregion

        #region 根据用户ID查询可参与项目
        public List<Project>GetProjectByUserID(string UserID)
        {
            string sql = "select PROJECT.PROJECT_ID,PROJECT_NAME FROM PROJECT INNER JOIN USER_PROJECT ON PROJECT.PROJECT_ID=USER_PROJECT.PROJECT_ID WHERE USER_ID = @USER_ID";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@USER_ID",UserID)
            };
            SqlDataReader objReader = SQLHelper.GetReader(sql, param, false);
            List<Project> ProList = new List<Project>();
            while(objReader.Read())
            {
                ProList.Add(new Project()
                {
                    PROJECTID = objReader["PROJECT_ID"].ToString(),
                    PROJECTNAME=objReader["PROJECT_NAME"].ToString()
                });
            }
            objReader.Close();
            return ProList;
        }
        #endregion
    }
}
