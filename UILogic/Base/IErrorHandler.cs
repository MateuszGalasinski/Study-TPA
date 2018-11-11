using System;

namespace UILogic.Base
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
