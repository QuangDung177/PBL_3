using MyProjectName.Model;
using System;
using System.Collections.Generic;

namespace MyProjectName.DataAccess.Interface
{
    public interface IStoredProcedureDataAccess
    {
       public Sp_LoginOutputModel Sp_Login(Sp_LoginInputModel input);
public bool Sp_Reg(Sp_RegInputModel input);
    }
}

