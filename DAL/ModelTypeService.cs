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
    public class ModelTypeService
    {
        #region 获取所有模型类型
        public List<ModelType>GetAllModelType()
        {
            string sql = "select MODEL_TYPE_NUMBER,MODEL_TYPE from MODEL_TYPE";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<ModelType> list = new List<ModelType>();
            while(objReader.Read())
            {
                list.Add(new ModelType()
                {
                    MODELTYPE = objReader["MODEL_TYPE"].ToString(),
                    MODELTYPENUMBER = Convert.ToInt32(objReader["MODEL_TYPE_NUMBER"])
                });
            }
            objReader.Close();
            return list;
        }
    }
    #endregion
}
