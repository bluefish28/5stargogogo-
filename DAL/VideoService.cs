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
    public class VideoService
    {
        #region 通过模型号查询相关视频
        public List<Video> GetVideoListByModel(string MODELID)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@MODELID",MODELID)
            };
            SqlDataReader objReader = SQLHelper.GetReader("usp_QueryVideo", param, true);
            List<Video> VideoList = new List<Video>();
            while (objReader.Read())
            {
                VideoList.Add(new Video()
                {
                    VIDEONAME=objReader["VIDEO_NAME"].ToString(),
                    VIDEOID=objReader["VIDEO_NUMBER"].ToString(),
                    VIDEOSIZE=objReader["VIDEO_SIZE"].ToString(),
                    MODELID=MODELID,
                    VIDEOFORMAT=objReader["VIDEO_FORMAT"].ToString(),
                    VIDEOSHOTTINGTIME=objReader["VIDEO_SHOTTING_TIME"].ToString(),
                    VIDEOUPDATETIME=objReader["VIDEO_UPDATE_TIME"].ToString(),
                    UPDATER=objReader["VIDEO_UPDATER"].ToString(),                  
                });
            }
            objReader.Close();
            return VideoList;
        }
        #endregion

        #region 添加视频
        public int AddVideo(Video objVideo)
        {
            SqlParameter[] param = new SqlParameter[]
            {
              new SqlParameter("@VIDEO_NUMBER",objVideo.VIDEOID),
              new SqlParameter("@VIDEO_NAME",objVideo.VIDEONAME),
              new SqlParameter("@VIDEO_ADDRESS",objVideo.VIDEOADDRESS),
              new SqlParameter("@VIDEO_FORMAT",objVideo.VIDEOFORMAT),
              new SqlParameter("@VIDEO_SIZE",objVideo.VIDEOSIZE),
              new SqlParameter("@VIDEO_UPDATER",objVideo.UPDATER),
              new SqlParameter("@MODEL_ID",objVideo.MODELID),
              new SqlParameter("@VIDEO_UPDATE_TIME",objVideo.VIDEOUPDATETIME),
              new SqlParameter("VIDEO_SHOTTING_TIME",objVideo.VIDEOSHOTTINGTIME)
            };
            return Convert.ToChar(SQLHelper.Update("usp_AddVideo", param, true));
        }
        #endregion

        #region 通过ID查询视频
        public Video GetVideoByID(string VIDEOID)
        {
            Video objVideo = new Video();
            string sql1 = "select VIDEO_ADDRESS from VIDEO where VIDEO_NUMBER=@VIDEOID";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@VIDEOID",VIDEOID)
            };
            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql1, param, false);
                if (objReader.Read())
                {
                    objVideo.VIDEOADDRESS = objReader["VIDEO_ADDRESS"].ToString();
                    objReader.Close();
                    return objVideo;
                }
                else
                {
                    objVideo = null;
                    objReader.Close();
                    return objVideo;
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 删除视频
        public int DeleteVideoByID(string VIDEOID)
        {
            string sql1 = "delete from VIDEO where VIDEO_NUMBER=@VIDEOID";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@VIDEOID",VIDEOID)
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
    }
}
