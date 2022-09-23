using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator
{
    internal class CodeGenerator
    {
        private string ClassName { get; set; }
        private string NameSpace { get; set; }
        private DataTable Columns { get; set; }

        private StringBuilder Result { get; set; }

        private List<TableProperty> Properties { get; set; }

        public CodeGenerator(string ClassName, string NameSpace, DataTable Columns)
        {
            this.ClassName = ClassName;
            this.NameSpace = NameSpace;
            this.Columns = Columns;
            Result = new StringBuilder();
            Properties = GetProperties();
        }

        private void AppendLine(string Value)
        {
            Result.Append(Value);
            Result.Append(Environment.NewLine);
        }

        private List<TableProperty> GetProperties()
        {
            Properties = new List<TableProperty>();

            foreach (DataRow Property in Columns.Rows)
            {
                string PropertyName = Property["COLUMN_NAME"].ToString().Replace("_", "");
                string ColumnName = Property["COLUMN_NAME"].ToString();
                string DataType = "";

                switch (Property["DATA_TYPE"])
                {
                    case "bit":
                        DataType = "bool";
                        break;
                    case "nvarchar":
                    case "varchar":
                        DataType = "string";
                        break;
                    case "bigint":
                    case "int":
                        DataType = "int";
                        break;
                    case "datetime":
                        DataType = "DateTime";
                        break;
                    case "decimal":
                        DataType = "decimal";
                        break;
                    default:
                        DataType = "string";
                        break;
                }
                TableProperty property = new TableProperty(ColumnName, DataType, PropertyName);
                Properties.Add(property);
            }
            Properties.First().PropertyName = "Key"; //Rename ID Column to Key
            return Properties;
        }

        public string GenerateClass()
        {
            AppendLine(" namespace " + NameSpace + "{");
            AppendLine(" public class " + ClassName + " : Base {");
            GenerateProperties();
            GenerateConstructor();
            GenerateDeleteMethod();
            GenerateExists();
            GenerateReadMethod();
            GenerateDataToClassMethod();
            GenerateSave();
            GenerateMethodsSection();
            AppendLine(" }");
            AppendLine(" }");
            AppendLine(" }");

            return Result.ToString();
        }

        private void GenerateProperties()
        {
            foreach (TableProperty Property in Properties)
            {
                AppendLine(string.Format("public {0} {1} {{get;set;}}", Property.SqlDataType, Property.PropertyName));
            }
        }

        private void GenerateSave()
        {
            AppendLine(@"public bool Save()");
            AppendLine(@"{");
            AppendLine(@"try");
            AppendLine(@"{");
            AppendLine(@"if (string.IsNullOrEmpty(Key))");
            AppendLine(@"{");
            AppendLine(@"StringBuilder Sql = new StringBuilder();");
            AppendLine(@"Key = Guid.NewGuid().ToString();");

            AppendLine(string.Format(@"Sql.Append(""insert into [{0}] ("");", ClassName));
            foreach (TableProperty Property in Properties)
            {
                AppendLine(string.Format(@"Sql.Append("" {0}, "");", Property.ColumnName));
            }
            RemoveLastComma();
            AppendLine(@"Sql.Append("" ) values ("");");
            foreach (TableProperty Property in Properties)
            {
                switch (Property.ClassDataType)
                {
                    case "string":
                        AppendLine(string.Format(@"Sql.Append("" '"" + {0}.SanitizeInput() + ""', "");", Property.PropertyName));
                        break;
                    case "int":
                        AppendLine(string.Format(@"Sql.Append("" "" + {0} + "", "");", Property.PropertyName));
                        break;
                    case "decimal":
                    case "float":
                    case "double":
                        AppendLine(string.Format(@"Sql.Append("" "" + {0} + "", "");", Property.PropertyName));
                        break;
                    case "bool":
                        AppendLine(string.Format(@"Sql.Append("" Convert.ToInt32("" + {0} + ""), "");", Property.PropertyName));
                        break;
                    case "DateTime":
                        AppendLine(string.Format(@"Sql.Append("" '"" + {0}.ToDBDate() + ""', "");", Property.PropertyName));
                        break;
                    default:
                        throw new Exception("Unhandled data type encountered in GenerateSave");
                }
            }
            RemoveLastComma();
            AppendLine(@"Sql.Append("" )"");");
            AppendLine(@"return ExecuteNonQuery(Sql.ToString());");
            AppendLine(@"}");
            AppendLine(@"else");
            AppendLine(@"{");
            AppendLine(@"StringBuilder Sql = new StringBuilder();");
            AppendLine(string.Format(@"Sql.Append(""update [{0}] set"");", ClassName));
            foreach (TableProperty Property in Properties)
            {
                switch (Property.ClassDataType)
                {
                    case "string":
                        AppendLine(string.Format(@"Sql.Append("" {0} = '"" + {1}.SanitizeInput() + ""', "");", Property.ColumnName, Property.PropertyName));
                        break;
                    case "int":
                        AppendLine(string.Format(@"Sql.Append("" {0} = "" + {1} + "", "");", Property.ColumnName, Property.PropertyName));
                        break;
                    case "decimal":
                    case "float":
                    case "double":
                        AppendLine(string.Format(@"Sql.Append("" {0} = "" + {1} + "", "");", Property.ColumnName, Property.PropertyName));
                        break;
                    case "bool":
                        AppendLine(string.Format(@"Sql.Append("" {0} = Convert.ToInt32("" + {1} + ""), "");", Property.ColumnName, Property.PropertyName));
                        break;
                    case "DateTime":
                        AppendLine(string.Format(@"Sql.Append("" {0} = '"" + {1}.ToDBDate() + ""', "");", Property.ColumnName, Property.PropertyName));
                        break;
                    default:
                        throw new Exception("Unhandled data type encountered in GenerateSave");
                }
            }
            RemoveLastComma();
            AppendLine(string.Format(@"Sql.Append("" where {0} = '"" + Key.SanitizeInput() + ""'"");", Properties.First().ColumnName));
            AppendLine(@"return ExecuteNonQuery(Sql.ToString());");
            AppendLine(@"}");
            AppendLine(@"}");
            AppendLine(@"catch (Exception Ex)");
            AppendLine(@"{");
            AppendLine(string.Format(@"throw new Exception(""Models > {0} > Save "" + Ex.Message);", ClassName));
            AppendLine(@"}");
            AppendLine(@"}");
        }

        private void GenerateConstructor()
        {
            AppendLine(string.Format(@"public {0}(string key = """")", ClassName));
            AppendLine(@"{");
            AppendLine(@"try");
            AppendLine(@"{");
            AppendLine(@"if (string.IsNullOrEmpty(key))");
            AppendLine(@"{");

            foreach (TableProperty Property in Properties)
            {
                AppendLine(string.Format("{0} = {1};", Property.PropertyName, Property.DefaultValue));
            }

            AppendLine(@"}");
            AppendLine(@"else");
            AppendLine(@"{");
            AppendLine(@"DataRowToClass(Read(key));");
            AppendLine(@"}");
            AppendLine(@"}");
            AppendLine(@"catch (Exception Ex)");
            AppendLine(@"{");
            AppendLine(string.Format(@"throw new Exception(""Models > {0} > Constructor "" + Ex.Message);", ClassName));
            AppendLine(@"}");
            AppendLine(@"}");
        }

        private void GenerateDeleteMethod()
        {
            AppendLine(@"public void Delete()");
            AppendLine(@"{");
            AppendLine(@"try");
            AppendLine(@"{");
            AppendLine(@"if (!string.IsNullOrEmpty(Key))");
            AppendLine(@"{");
            AppendLine(@"StringBuilder Sql = new StringBuilder();");
            AppendLine(string.Format(@"Sql.Append("" delete from [{0}] "");", ClassName));
            AppendLine(string.Format(@"Sql.Append("" where {0} = '"" + Key.SanitizeInput() + ""'"");", Properties.First().ColumnName));
            AppendLine(@"ExecuteNonQuery(Sql.ToString());");
            AppendLine(@"}");
            AppendLine(@"}");
            AppendLine(@"catch (Exception Ex)");
            AppendLine(@"{");
            AppendLine(String.Format(@"throw new Exception(""Models > {0} > Delete "" + Ex.Message);", ClassName));
            AppendLine(@"}");
            AppendLine(@"}");
        }

        private void GenerateExists()
        {
            AppendLine(@"public bool Exists()");
            AppendLine(@"{");
            AppendLine(@"try");
            AppendLine(@"{");
            AppendLine(@"StringBuilder Sql = new StringBuilder();");
            AppendLine(string.Format(@"throw new NotImplementedException(""Models > {0} > Exists "");", ClassName));
            AppendLine(@"return Convert.ToInt32(ExecuteScalar(Sql.ToString())) > 0;");
            AppendLine(@"}");
            AppendLine(@"catch (Exception Ex)");
            AppendLine(@"{");
            AppendLine(string.Format(@"throw new Exception(""Models > {0} > Exists "" + Ex.Message);", ClassName));
            AppendLine(@"}");
            AppendLine(@"}");
        }

        private void GenerateReadMethod()
        {
            AppendLine(@"public static DataRow Read(string Key)");
            AppendLine(@"{");
            AppendLine(@"try");
            AppendLine(@"{");
            AppendLine(@"StringBuilder Sql = new StringBuilder();");
            AppendLine(@"  Sql.Append(Methods.GetReadSql());");
            AppendLine(string.Format(@"Sql.Append("" where {0} = '"" + Key.SanitizeInput() + ""'"");", Properties.First().ColumnName));
            AppendLine(@"return ReadDataRow(Sql.ToString());");
            AppendLine(@"}");
            AppendLine(@"catch (Exception Ex)");
            AppendLine(@"{");
            AppendLine(string.Format(@"throw new Exception(""Models > {0} > Read "" + Ex.Message);", ClassName));
            AppendLine(@"}");
            AppendLine(@"}");
        }

        private void GenerateDataToClassMethod()
        {
            AppendLine(@"public void DataRowToClass(DataRow Data)");
            AppendLine(@"{");
            AppendLine(@"try");
            AppendLine(@"{");
            foreach (TableProperty Property in Properties)
            {
                switch (Property.ClassDataType)
                {
                    case "string":
                        AppendLine(string.Format(@"{0} = Data[""{1}""].ToString();", Property.PropertyName, Property.ColumnName));
                        break;
                    case "int":
                        AppendLine(string.Format(@"{0} = Convert.ToInt32(Data[""{1}""].ToString());", Property.PropertyName, Property.ColumnName));
                        break;
                    case "decimal":
                        AppendLine(string.Format(@"{0} = Convert.ToDecimal(Data[""{1}""].ToString());", Property.PropertyName, Property.ColumnName));
                        break;
                    case "float":
                    case "double":
                        AppendLine(string.Format(@"{0} = Convert.toDouble(Data[""{1}""].ToString());", Property.PropertyName, Property.ColumnName));
                        break;
                    case "bool":
                        AppendLine(string.Format(@"{0} = Convert.toBoolean(Data[""{1}""].ToString());", Property.PropertyName, Property.ColumnName));
                        break;
                    case "DateTime":
                        AppendLine(string.Format(@"{0} = Convert.toDateTime(Data[""{1}""].ToString());", Property.PropertyName, Property.ColumnName));
                        break;
                    default:
                        throw new Exception("Unhandled data type encountered in GenerateDataToClass");
                }
            }
            AppendLine(@"}");
            AppendLine(@"catch (Exception Ex)");
            AppendLine(@"{");
            AppendLine(string.Format(@"throw new Exception(""Models > {0} > DataRowToClass "" + Ex.Message);", ClassName));
            AppendLine(@"}");
            AppendLine(@"}");
        }

        private void GenerateMethodsSection()
        {
            AppendLine(@"public class Methods");
            AppendLine(@"{");
            AppendLine(@"public static string GetReadSql()");
            AppendLine(@"{");
            AppendLine(@"StringBuilder Sql = new StringBuilder(); ");
            AppendLine(@"Sql.Append("" select"");");
            foreach (TableProperty Property in Properties)
            {
                AppendLine(string.Format(@"Sql.Append("" {0}, "");", Property.ColumnName));
            }
            RemoveLastComma();
            AppendLine(string.Format(@"Sql.Append("" from {0}"");", ClassName));
            AppendLine(@"return Sql.ToString();");
            AppendLine(@"}");
        }

        private void RemoveLastComma()
        {
            int CharPosition = Result.ToString().LastIndexOf(',');
            Result.Remove(CharPosition, 1);
        }
    }
}
