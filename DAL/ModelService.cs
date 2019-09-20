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
    public class ModelService
    {
        #region 通过ID查询模型储存地址
        public Model GetModelByID(string ModelID)
        {
            Model objModel = new Model();
            string sql1 = "select MODEL_ADDRESS from MODEL where MODEL_ID=@MODELID";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@MODELID",ModelID)
            };
            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql1, param, false);
                if (objReader.Read())
                {
                    objModel.MODELADDRESS = objReader["MODEL_ADDRESS"].ToString();
                    objReader.Close();
                    return objModel;
                }
                else
                {
                    objModel = null;
                    objReader.Close();
                    return objModel;
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 通过项目号查询所含模型
        public List<Model> GetModelListByProject(string PROJECTID)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@PROJECTID",PROJECTID)
            };
            SqlDataReader objReader = SQLHelper.GetReader("usp_QueryModel", param, true);
            List<Model> ModelList = new List<Model>();
            while (objReader.Read())
            {
                ModelList.Add(new Model()
                {
                    MODELID = objReader["MODEL_ID"].ToString(),
                    MODELADDRESS = objReader["MODEL_ADDRESS"].ToString(),
                    MODELFORMAT = objReader["MODEL_FORMAT"].ToString(),
                    MODELNAME = objReader["MODEL_NAME"].ToString(),
                    MODELSIZE = objReader["MODEL_SIZE"].ToString(),
                    MODELTYPE = Convert.ToInt32(objReader["MODEL_TYPE_NUMBER"]),
                    MODELUPDATETIME = objReader["MODEL_UPDATE_TIME"].ToString(),
                    POSITIONX = Convert.ToDouble(objReader["MODEL_POSITION_X"]),
                    POSITIONY = Convert.ToDouble(objReader["MODEL_POSITION_Y"]),
                    //POSITIONZ = Convert.ToDouble(objReader["MODEL_POSITION_Z"]),
                    UPDATERID = objReader["UPDATER_ID"].ToString(),
                    PROJECT = PROJECTID
                });
            }
            objReader.Close();
            return ModelList;
        }
        #endregion

        #region 添加模型
        public int AddModel(Model objModel)
        {
            SqlParameter[] param = new SqlParameter[]
            {
              new SqlParameter("@MODEL_ID",objModel.MODELID),
              new SqlParameter("@MODEL_NAME",objModel.MODELNAME),
              new SqlParameter("@MODEL_ADDRESS",objModel.MODELADDRESS),
              new SqlParameter("@MODEL_FORMAT",objModel.MODELFORMAT),
              new SqlParameter("@MODEL_SIZE",objModel.MODELSIZE),
              new SqlParameter("@MODEL_TYPE_NUMBER",objModel.MODELTYPE),
              new SqlParameter("@MODEL_POSITION_X",objModel.POSITIONX),
              new SqlParameter("@MODEL_POSITION_Y",objModel.POSITIONY),
              new SqlParameter("@MODEL_POSITION_Z",objModel.POSITIONZ),
              new SqlParameter("@UPDATER_ID",objModel.UPDATERID),
              new SqlParameter("@PROJECT",objModel.PROJECT),
              new SqlParameter("@MODEL_UPDATE_TIME",objModel.MODELUPDATETIME)
            };
            return Convert.ToChar(SQLHelper.Update("usp_AddModel", param, true));
        }
        #endregion

        #region 修改模型
        public int ModifyModel(Model objModel)
        {
            SqlParameter[] param = new SqlParameter[]
            {
              new SqlParameter("@MODELNAME",objModel.MODELNAME),
              new SqlParameter("@POSITIONX",objModel.POSITIONX),
              new SqlParameter("@POSITIONY",objModel.POSITIONY),
              new SqlParameter("@POSITIONZ",objModel.POSITIONZ),
              new SqlParameter("@UPDATERID",objModel.UPDATERID),
              new SqlParameter("@PROJECT",objModel.PROJECT),
            };
            return Convert.ToChar(SQLHelper.GetSingleResult("usp_ModifyModel", param, true));
        }
        #endregion

        #region 删除模型
        public int DeleteModelByID(string ModelID)
        {
            string sql1 = "delete from MODEL where MODEL_ID=@MODELID";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@MODELID",ModelID)
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

        #region 获取所有模型
        public List<Model> GetAllModel()
        {
            string sql = "select distinct MODEL_ID, MODEL_NAME FROM MODEL";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<Model> ModelList = new List<Model>();
            while (objReader.Read())
            {
                ModelList.Add(new Model()
                {
                    MODELID = objReader["MODEL_ID"].ToString(),
                    MODELNAME = objReader["MODEL_NAME"].ToString()
                });
            }
            objReader.Close();
            return ModelList;
        }
        #endregion

        #region 根据模型号查询模型加载位置信息
        public Model GetModelPositionByModelID(string ModelID)
        {
            Model objModel = new Model();
            string sql = "select MODEL_POSITION_X,MODEL_POTITION_Y,MODEL_POSOTION_Z,MODEL_ADDRESS FROM MODEL WHERE MODEL_ID=@MODELID";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@MODELID",ModelID)
            };
            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql, param, false);
                if (objReader.Read())
                {
                    objModel.MODELADDRESS = objReader["MODEL_ADDRESS"].ToString();
                    objModel.POSITIONX = Convert.ToDouble(objReader["MODEL_POSITION_X"]);
                    objModel.POSITIONY = Convert.ToDouble(objReader["MODEL_POSITION_Y"]);
                    objModel.POSITIONZ = Convert.ToDouble(objReader["MODEL_POSITION_Z"]);
                    objReader.Close();
                    return objModel;
                }
                else
                {
                    objModel = null;
                    objReader.Close();
                    return objModel;
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
