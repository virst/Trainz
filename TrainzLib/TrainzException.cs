using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainzLib
{
    public class TrainzException : Exception
    { 
        public int ErrorCode { get; }

        public TrainzException(int errorCode, string errorMessage) :
            base(errorMessage)
        {
            ErrorCode = errorCode;
        }
    }
}
