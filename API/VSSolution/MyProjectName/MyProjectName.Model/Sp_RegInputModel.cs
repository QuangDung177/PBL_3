using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyProjectName.Model
{
    public class Sp_RegInputModel
    {

public string Username{get;set;}
public string Password{get;set;}
public string Email{get;set;}

public string Fullname{get;set;}
public DateTime Birthdate{get;set;}
public bool Gender{get;set;}
public string Address{get;set;}
public string Phonenumber{get;set;}
public string Accountstatus{get;set;}
        public string Role { get;set;}
    }
}

