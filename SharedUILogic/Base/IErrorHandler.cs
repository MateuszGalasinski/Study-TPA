using System;

namespace SharedUILogic.Base
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
