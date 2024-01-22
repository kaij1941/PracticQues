namespace Common.Model
{
    public class BaseReturn
    {
        /// <summary>
        /// 狀態碼
        /// </summary>
        /// <value>
        /// 0:正常
        /// >1:異常狀況
        /// 9999:輸入異常
        /// -1:過期
        /// </value>
        public int StatusCode { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }

        public static BaseReturn GoodReturn()
        {
            return new BaseReturn()
            {
                StatusCode = 0,
                Message = "Success"
            };
        }

        public static BaseReturn BadReturn()
        {
            return new BaseReturn()
            {
                StatusCode = -1,
                Message = "過期"
            };
        }
    }

    
}
