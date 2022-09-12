using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator
{
    internal class TableProperty
    {
        public string ColumnName { get; set; }
        public string PropertyName { get; set; }
        public string SqlDataType { get; set; }
        public string ClassDataType { get; set; }
        public string DefaultValue { get; set; }

        public TableProperty(string ColumnName, string DataType, string PropertyName)
        {
            this.ColumnName = ColumnName;
            this.SqlDataType = DataType;
            this.PropertyName = PropertyName;
            this.DefaultValue = GetDefaultValue();
            this.ClassDataType = ConvertSqlDataTypeToCSharpDataType();
        }

        private string GetDefaultValue()
        {
            switch (SqlDataType.ToLower())
            {
                case "char":
                case "varchar":
                case "text":
                case "string":
                    return @"""""";
                case "nchar":
                case "ntext":
                case "ncarchar":
                    return @"""""";
                case "int":
                case "decimal":
                case "bigint":
                case "smallint":
                case "tinyint":
                case "money":
                case "numeric":
                case "float":
                case "double":
                case "real":
                    return "0";
                case "bit":
                case "bool":
                case "boolean":
                    return "false";
                case "datetime":
                case "date":
                case "time":
                case "smalldatetime":
                    return "DateTime.Now";
                default:
                    throw new Exception("Unhandled SQL Data type encountered in TableProperty > GetDefaultValue");
            }
        }

        private string ConvertSqlDataTypeToCSharpDataType()
        {
            switch (SqlDataType.ToLower())
            {
                case "char":
                case "varchar":
                case "text":
                case "nchar":
                case "ntext":
                case "ncarchar":
                case "string":
                    return "string";
                case "bigint":
                case "smallint":
                case "tinyint":
                case "int":
                case "numeric":
                    return "int";
                case "decimal":
                case "money":
                    return "decimal";
                case "float":
                case "real":
                    return "float";
                case "double":
                    return "double";
                case "bit":
                case "bool":
                case "boolean":
                    return "bool";
                case "datetime":
                case "date":
                case "time":
                case "smalldatetime":
                    return "DateTime";
                default:
                    throw new Exception("Unhandled SQL Data type encountered in TableProperty > ConvertSqlDataTypeToCSharpDataType");
            }
        }
    }
}
