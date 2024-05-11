using System;
using System.Collections.Generic;
using System.Text;

namespace MyProjectName.Utility
{
    public class FilterModel
    {
        public string ColumnName { get; set; }
        public FilterConditionEnum ColumnCondition { get; set; }
        public string ColumnValue { get; set; }
    }
    public enum FilterConditionEnum
    {
        Contains = 1,
        NotContains = 2,
        Equals = 3,
        GreaterThan = 4,
        GreaterThanEqual = 5,
        LessThan = 6,
        LessThanEqual = 7,
    }
}

