using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyProjectName.Model
{
    public class Sp_LoginOutputModel { 
public List<Sp_Login_ResultTable1> ResultTable1{get;set;}

public Sp_LoginOutputModel()
{
ResultTable1 = new List<Sp_Login_ResultTable1>();
}
}

public class Sp_Login_ResultTable1 { 

public String Result{get;set;}
}
}

