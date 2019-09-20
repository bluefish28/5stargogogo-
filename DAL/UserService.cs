using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using Models;
using DAL.Helper;
using Models.EX;

namespace DAL
{
    public class UserService
    {
        #region 用户登录
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="objUser">用户对象</param>
        /// <returns></returns>
        public User UserLogin(User objUser)
        {
            string sql = "select USER_NAME, USER_TYPE_NUMBER from [USER] WHERE USER_ID=@LoginID and PASSWORD=@LoginPWD";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ( "@LoginID", objUser.LoginID ),
                new SqlParameter ( "@LoginPWD", objUser.LoginPWD)
            };
            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql, param, false);
                if (objReader.Read())
                {
                    objUser.UserName = objReader["USER_NAME"].ToString();
                    objUser.UserTypeNumber = objReader["USER_TYPE_NUMBER"].ToString();
                }
                else
                {
                    objUser = null;
                }
                objReader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("用户登录数据访问出现异常:" + ex.Message);
            }
            return objUser;
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="objUser">用户对象</param>
        /// <returns></returns>
        public int ModifyPWD(User objUser)
        {
            string sql = "update [USER] set PASSWORD=@LoginPWD where USER_ID = @LoginID";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ( "@LoginPWD",objUser.LoginPWD),
                new SqlParameter("@LoginID",objUser.LoginID)
            };
            return SQLHelper.Update(sql, param, false);

        }
        #endregion

        #region 添加用户
        public int AddUser(User objUser)
        {
            SqlParameter[] param = new SqlParameter[]
            {
              new SqlParameter("USER_ID",objUser.LoginID),
              new SqlParameter("PASSWORD",objUser.LoginPWD),
              new SqlParameter("TELEPHONE",objUser.Telephone),
              new SqlParameter("USER_NAME",objUser.UserName),
              new SqlParameter("USER_TYPE_NUMBER",objUser.UserTypeNumber),
              new SqlParameter("EMAIL",objUser.Email),
            };
            return Convert.ToChar(SQLHelper.Update("usp_AddUser", param, true));
        }
        #endregion

        #region 修改用户

        #endregion

        #region 删除用户
        public int DeleteUserByID(string UserID)
        {
            string sql1 = "delete from [USER] where USER_ID=@USERID";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@USERID",UserID)
            };
            try
            {
                return SQLHelper.Update(sql1, param, false);

            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new Exception("该模型编号被其他实体引用！");
                }
                else
                {
                    throw new Exception("数据库操作异常！");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("数据库操作异常！" + ex.Message);
            }
        }
        #endregion

        #region 获取所有用户
        public List<UserEX>GetUser()
        {
            string sql = "select USER_ID,USER_NAME,TELEPHONE,EMAIL,USER_TYPE.USER_TYPE from [USER] INNER JOIN USER_TYPE ON [USER].USER_TYPE_NUMBER=USER_TYPE.USER_TYPE_NUMBER";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<UserEX> UserList = new List<UserEX>();
            while (objReader.Read())
            {
                UserList.Add(new UserEX()
                {
                    LoginID=objReader["USER_ID"].ToString(),
                    UserName=objReader["USER_NAME"].ToString(),
                    Telephone=objReader["TELEPHONE"].ToString(),
                    Email=objReader["EMAIL"].ToString(),
                    UserType=objReader["USER_TYPE"].ToString()
                });
            }
            objReader.Close();
            return UserList;
        }
        #endregion

        #region 获取所有用户类型
        public List<UserEX>GetAllUserType()
        {
            string sql = "select distinct USER_TYPE, USER_TYPE_NUMBER FROM USER_TYPE";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<UserEX> UserTypeList = new List<UserEX>();
            while (objReader.Read())
            {
                UserTypeList.Add(new UserEX()
                {
                    UserType=objReader["USER_TYPE"].ToString(),
                    UserTypeNumber=objReader["USER_TYPE_NUMBER"].ToString()
                });
            }
            objReader.Close();
            return UserTypeList;
        }
        #endregion

        #region 判断是否存在用户ID
        public bool IsExitUserID(string UserID)
        {
            string sql = "select USER_NAME from [USER] where USER_ID=@USERID";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@USERID",UserID)
            };
            SqlDataReader objReader = SQLHelper.GetReader(sql,param,false);
            String result=null;
            if (objReader.Read())
            {
                result = objReader["USER_NAME"].ToString();
                objReader.Close();
            }
            if (result!=null)
                return true;
            else
                return false;
        }
        #endregion
    }
}
