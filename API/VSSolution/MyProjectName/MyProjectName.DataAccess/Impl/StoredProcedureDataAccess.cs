using MyProjectName.DataAccess.Interface;
using MyProjectName.Model;
using MyProjectName.Utility;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace MyProjectName.DataAccess.Impl
{
    public class StoredProcedureDataAccess : IStoredProcedureDataAccess
    {
        private MSSqlDatabase MSSqlDatabase { get; set; }
        public StoredProcedureDataAccess(MSSqlDatabase msSqlDatabase)
        {
            MSSqlDatabase = msSqlDatabase;
            SetANSIWarning();
        }
        private void SetANSIWarning()
        {
            //You can add more warning ON/OFF at this place.
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = "SET ANSI_WARNINGS OFF";
            cmd.ExecuteNonQuery();

        }
       
       

public Sp_LoginOutputModel Sp_Login(Sp_LoginInputModel input)
{
bool outParam=false;
   var ret = new Sp_LoginOutputModel();
    var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
    cmd.CommandText = "sp_Login";
    cmd.CommandType = CommandType.StoredProcedure;
    cmd.Parameters.AddWithValue("username", input.Username);
    cmd.Parameters.AddWithValue("password", input.Password);
    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
    {
       DataSet ds = new DataSet();
       da.Fill(ds);
       
       
foreach (DataRow r in ds.Tables[0].Rows)
{
var z1=new Sp_Login_ResultTable1(){
Result =(String) r["Result"],
};
ret.ResultTable1.Add(z1);
};
  }
  return ret;
}

public bool Sp_Reg(Sp_RegInputModel input)
{
bool outParam=false;
   
    var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
    cmd.CommandText = "sp_Reg";
    cmd.CommandType = CommandType.StoredProcedure;

    cmd.Parameters.AddWithValue("userName", input.Username);
    cmd.Parameters.AddWithValue("password", input.Password);
    cmd.Parameters.AddWithValue("email", input.Email);

    cmd.Parameters.AddWithValue("fullName", input.Fullname);
    cmd.Parameters.AddWithValue("birthDate", input.Birthdate);
    cmd.Parameters.AddWithValue("gender", input.Gender);
    cmd.Parameters.AddWithValue("address", input.Address);
    cmd.Parameters.AddWithValue("phoneNumber", input.Phonenumber);
    cmd.Parameters.AddWithValue("accountStatus", input.Accountstatus);
    cmd.Parameters.AddWithValue("role", input.Role);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
    {
       DataSet ds = new DataSet();
       da.Fill(ds);
       
       
  }
  return true;
}
    }
}

