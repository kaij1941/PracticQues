using Common.Model;
using Service.Interface;

namespace Service.Implement;

public class ProductService : IProductService
{
    /// <summary>
    /// Checks the product dead line.
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="nowDateTime"></param>
    /// <returns></returns>
    public async Task<BaseReturn> CheckProductDeadLine(string statusCode,DateTime nowDateTime)
    {
        var result = BaseReturn.BadReturn();

        if (string.IsNullOrWhiteSpace(statusCode))
        {
            var extraMsg = "無輸入訊息";
            LabelErrorSetting(result, extraMsg);
            return result;
        }

        if (statusCode.Length != 15)
        {
            var extraMsg = "字數錯誤";
            LabelErrorSetting(result, extraMsg);
            return result;
        }

        var time = statusCode.Substring(5, 8);
        time= time.Insert(4, "/").Insert(7,"/");
        if ( ! DateTime.TryParse(time,out var deadLineDateTime))
        {
            var extraMsg = "日期格式錯誤";
            LabelErrorSetting(result, extraMsg);
            return result;
        }

        var  hourStr =  statusCode.Substring(13, 2);
        if ( ! int.TryParse(hourStr, out var deadLineHours)  || deadLineHours < 0 || deadLineHours > 24)
        {
            var extraMsg = "效期格式錯誤";
            LabelErrorSetting(result, extraMsg);
            return result;
        }


        var nowDate= nowDateTime.Date;
        if (deadLineDateTime < nowDate)
        {
            //日期就過期
            OverDeaLineSetting(result);
            return result;
        }

        var nowHour = nowDateTime.Hour;
        if (   deadLineDateTime == nowDate
            && deadLineHours <= nowHour)
        {
            //同一天但時間過期
            OverDeaLineSetting(result);
            return result;
        }

        var dayDiff = (deadLineDateTime - nowDate).Days;
        if (dayDiff>1)
        {
            //效期超過一天的狀況
            NormalSetting(result);
            return result;
        }

        var hourDiff= deadLineHours - nowHour;
        if (hourDiff > 1 || hourDiff<0)
        {
            //效期超過1小時的狀況
            NormalSetting(result);
            return result;
        }

        //效期剩下1小時以內的狀況
        if (nowDateTime.Minute > 55)
        {
            OverDeaLineSetting(result);
            return result;
        }
  
        DeadLineIsComingSetting(result);
        return result;

    }

    private static void DeadLineIsComingSetting(BaseReturn result)
    {
        result.StatusCode = 0;
        result.Message = "商品即將到期";
    }

    private static void NormalSetting(BaseReturn result)
    {
        result.StatusCode = 0;
        result.Message = "正常";
    }

    private static void LabelErrorSetting(BaseReturn result, string extraMsg)
    {
        result.StatusCode = 9999;
        result.Message = "標籤異常:" + extraMsg;
    }

    private static void OverDeaLineSetting(BaseReturn result)
    {
        result.StatusCode = -1;
        result.Message = "商品已過期";
    }
}