using AutoFixture;
using Common.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Implement;

namespace ServiceTests.Implement
{
    [TestClass()]
    public class ProductServiceTests
    {
        [TestMethod()] public async Task CheckProductDeadLine_輸入空字串_回傳訊息標籤異常無輸入訊息狀態9999()
        {
            //Arrange
            var target = new ProductService();
            var statusCode = "";
            var nowDateTime = DateTime.Now;

            var excepted = new BaseReturn()
            {
                StatusCode = 9999,
                Message = "標籤異常:無輸入訊息"
            };

            //act
            var result = await target.CheckProductDeadLine(statusCode, nowDateTime);

            //Assert
            Assert.AreEqual(excepted.StatusCode, result.StatusCode);
            Assert.AreEqual(excepted.Message, result.Message);
        }

        [TestMethod()]
        public async Task CheckProductDeadLine_輸入Null_回傳訊息標籤異常無輸入訊息狀態9999()
        {
            //Arrange
            var target = new ProductService();
            string statusCode = null;
            var nowDateTime = DateTime.Now;

            var excepted = new BaseReturn()
            {
                StatusCode = 9999,
                Message = "標籤異常:無輸入訊息"
            };

            //act
            var result = await target.CheckProductDeadLine(statusCode, nowDateTime);

            //Assert
            Assert.AreEqual(excepted.StatusCode, result.StatusCode);
            Assert.AreEqual(excepted.Message, result.Message);
        }

        [TestMethod()]
        public async Task CheckProductDeadLine_輸入16個字_回傳訊息標籤異常字數錯誤狀態9999()
        {
            //Arrange
            var target = new ProductService();
            var fixture = new Fixture();
            string statusCode = fixture.Create<string>().Substring(0, 16);
            var nowDateTime = DateTime.Now;

            var excepted = new BaseReturn()
            {
                StatusCode = 9999,
                Message = "標籤異常:字數錯誤"
            };

            //act
            var result = await target.CheckProductDeadLine(statusCode, nowDateTime);

            //Assert
            Assert.AreEqual(excepted.StatusCode, result.StatusCode);
            Assert.AreEqual(excepted.Message, result.Message);
        }

        [TestMethod()]
        public async Task CheckProductDeadLine_輸入14個字_回傳訊息標籤異常字數錯誤狀態9999()
        {
            //Arrange
            var target = new ProductService();
            var fixture = new Fixture();
            string statusCode = fixture.Create<string>().Substring(0, 14);
            var nowDateTime = DateTime.Now;

            var excepted = new BaseReturn()
            {
                StatusCode = 9999,
                Message = "標籤異常:字數錯誤"
            };

            //act
            var result = await target.CheckProductDeadLine(statusCode, nowDateTime);

            //Assert
            Assert.AreEqual(excepted.StatusCode, result.StatusCode);
            Assert.AreEqual(excepted.Message, result.Message);
        }


        [TestMethod()]
        public async Task CheckProductDeadLine_輸入任意5字及YYYYMMDD的文字及00_回傳訊息標籤日期格式錯誤狀態9999()
        {
            //Arrange
            var target = new ProductService();
            var fixture = new Fixture();
            string statusCode = fixture.Create<string>().Substring(0,5)+"YYYYMMDD00";
            var nowDateTime = DateTime.Now;

            var excepted = new BaseReturn()
            {
                StatusCode = 9999,
                Message = "標籤異常:日期格式錯誤"
            };

            //act
            var result = await target.CheckProductDeadLine(statusCode, nowDateTime);

            //Assert
            Assert.AreEqual(excepted.StatusCode, result.StatusCode);
            Assert.AreEqual(excepted.Message, result.Message);
        }

        [TestMethod()]
        public async Task CheckProductDeadLine_輸入任意5字及DateTimeDate格式的文字及AA_回傳訊息標籤日期格式錯誤狀態9999()
        {
            //Arrange
            var target = new ProductService();
            var fixture = new Fixture();
            string statusCode = fixture.Create<string>().Substring(0, 5) + fixture.Create<DateTime>().ToString("yyyyMMdd")+"AA";
            var nowDateTime = DateTime.Now;

            var excepted = new BaseReturn()
            {
                StatusCode = 9999,
                Message = "標籤異常:效期格式錯誤"
            };

            //act
            var result = await target.CheckProductDeadLine(statusCode, nowDateTime);

            //Assert
            Assert.AreEqual(excepted.StatusCode, result.StatusCode);
            Assert.AreEqual(excepted.Message, result.Message);
        }

        [TestMethod()]
        public async Task CheckProductDeadLine_輸入任意5字及10天前的DateTimeDate格式的文字及00_回傳訊息商品已過期狀態負1()
        {
            //Arrange
            var target = new ProductService();
            var fixture = new Fixture();
            string statusCode = fixture.Create<string>().Substring(0, 5) + DateTime.Now.AddDays(-10).ToString("yyyyMMdd")+"00";
            var nowDateTime = DateTime.Now;

            var excepted = new BaseReturn()
            {
                StatusCode = -1,
                Message = "商品已過期"
            };

            //act
            var result = await target.CheckProductDeadLine(statusCode, nowDateTime);

            //Assert
            Assert.AreEqual(excepted.StatusCode, result.StatusCode);
            Assert.AreEqual(excepted.Message, result.Message);
        }

        [TestMethod()]
        public async Task CheckProductDeadLine_輸入任意5字及與現在的DateTimeDate格式的文字及時數現在的前3個小時的數字_回傳訊息商品已過期狀態負1()
        {
            //Arrange
            var target = new ProductService();
            var fixture = new Fixture();
            string statusCode = fixture.Create<string>().Substring(0, 5) + DateTime.Now.ToString("yyyyMMdd") + (DateTime.Now.Hour-3).ToString();
            var nowDateTime = DateTime.Now;

            var excepted = new BaseReturn()
            {
                StatusCode = -1,
                Message = "商品已過期"
            };

            //act
            var result = await target.CheckProductDeadLine(statusCode, nowDateTime);

            //Assert
            Assert.AreEqual(excepted.StatusCode, result.StatusCode);
            Assert.AreEqual(excepted.Message, result.Message);
        }

        [TestMethod()]
        public async Task CheckProductDeadLine_輸入任意5字及與大於現在一天的DateTimeDate格式的文字及時數現在的前3個小時的數字_回傳訊息正常狀態0()
        {
            //Arrange
            var target = new ProductService();
            var fixture = new Fixture();
            string statusCode = fixture.Create<string>().Substring(0, 5) + DateTime.Now.AddDays(1).ToString("yyyyMMdd") + (DateTime.Now.AddHours(-3).Hour).ToString();
            var nowDateTime = DateTime.Now;

            var excepted = new BaseReturn()
            {
                StatusCode = 0,
                Message = "正常"
            };

            //act
            var result = await target.CheckProductDeadLine(statusCode, nowDateTime);

            //Assert
            Assert.AreEqual(excepted.StatusCode, result.StatusCode);
            Assert.AreEqual(excepted.Message, result.Message);
        }

        [TestMethod()]
        public async Task CheckProductDeadLine_輸入任意5字及與現在的DateTimeDate格式的文字及時數現在的後3個小時的數字並假設現在是晚上8點_回傳訊息正常狀態0()
        {
            //Arrange
            var target = new ProductService();
            var fixture = new Fixture();
            string statusCode = fixture.Create<string>().Substring(0, 5) + DateTime.Now.ToString("yyyyMMdd") + (DateTime.Today.AddHours(20).AddHours(3).Hour).ToString();
            var nowDateTime = DateTime.Today.AddHours(20);

            var excepted = new BaseReturn()
            {
                StatusCode = 0,
                Message = "正常"
            };

            //act
            var result = await target.CheckProductDeadLine(statusCode, nowDateTime);

            //Assert
            Assert.AreEqual(excepted.StatusCode, result.StatusCode);
            Assert.AreEqual(excepted.Message, result.Message);
        }

        [TestMethod()]
        public async Task CheckProductDeadLine_輸入任意5字及與現在的DateTimeDate格式的文字及時數為21點並假設現在是20點54分_回傳訊息正常狀態0()
        {
            //Arrange
            var target = new ProductService();
            var fixture = new Fixture();
            string statusCode = fixture.Create<string>().Substring(0, 5) + DateTime.Now.ToString("yyyyMMdd") + (DateTime.Today.AddHours(21).Hour).ToString();
            var nowDateTime = DateTime.Today.AddHours(20).AddMinutes(54);

            var excepted = new BaseReturn()
            {
                StatusCode = 0,
                Message = "商品即將到期"
            };

            //act
            var result = await target.CheckProductDeadLine(statusCode, nowDateTime);

            //Assert
            Assert.AreEqual(excepted.StatusCode, result.StatusCode);
            Assert.AreEqual(excepted.Message, result.Message);
        }

        [TestMethod()]
        public async Task CheckProductDeadLine_輸入任意5字及與現在的DateTimeDate格式的文字及時數為21點並假設現在是20點56分_回傳訊息正常狀態0()
        {
            //Arrange
            var target = new ProductService();
            var fixture = new Fixture();
            string statusCode = fixture.Create<string>().Substring(0, 5) + DateTime.Now.ToString("yyyyMMdd") + (DateTime.Today.AddHours(21).Hour).ToString();
            var nowDateTime = DateTime.Today.AddHours(20).AddMinutes(56);

            var excepted = new BaseReturn()
            {
                StatusCode = -1,
                Message = "商品已過期"
            };

            //act
            var result = await target.CheckProductDeadLine(statusCode, nowDateTime);

            //Assert
            Assert.AreEqual(excepted.StatusCode, result.StatusCode);
            Assert.AreEqual(excepted.Message, result.Message);
        }
    }
}