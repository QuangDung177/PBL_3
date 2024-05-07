using MyProjectName.DataAccess.Interface;
using MyProjectName.Manager.Interface;
using MyProjectName.Model;
using MyProjectName.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProjectName.Manager.Impl
{
    public class SPManager : ISPManager
    {
        private readonly IStoredProcedureDataAccess DataAccess = null;
        public SPManager(IStoredProcedureDataAccess dataAccess)
        {
            DataAccess = dataAccess;
        }
        public APIResponse Sp_Login(Sp_LoginInputModel input)
    {
    var result = DataAccess.Sp_Login(input);
if (result != null)
    {
        return new APIResponse(ResponseCode.SUCCESS, "SP Executed Successfully", result);
   }
    else
    {
        return new APIResponse(ResponseCode.ERROR, "Error in Executing SP");
    }
}
public APIResponse Sp_Reg(Sp_RegInputModel input)
    {
    var result = DataAccess.Sp_Reg(input);
if (result)
    {
        return new APIResponse(ResponseCode.SUCCESS, "SP Executed Successfully", result);
   }
    else
    {
        return new APIResponse(ResponseCode.ERROR, "Error in Executing SP");
    }
}
       

        
    }
}

