using CarCaseTest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCaseTest.Domain.Models
{
    public class ServiceResult<T>
    {
        public ServiceResult()
        {
            this.Status = ResultStatus.Success;
        }

        public ResultStatus Status { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
    }
}
