using MyProjectName.Model;
using MyProjectName.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProjectName.Manager.Interface
{
    public interface ISPManager
    {
       public APIResponse Sp_Login(Sp_LoginInputModel input);
public APIResponse Sp_Reg(Sp_RegInputModel input);
    }
}

