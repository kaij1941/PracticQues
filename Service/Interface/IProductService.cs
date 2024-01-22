using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;


namespace Service.Interface
{
    public interface IProductService
    {
        /// <summary>
        /// Checks the product dead line.
        /// </summary>
        /// <param name="statusCode">The status code list.</param>
        /// <param name="nowDateTime"></param>
        /// <returns></returns>
        public Task<BaseReturn> CheckProductDeadLine(string statusCode, DateTime nowDateTime);
    }
}
