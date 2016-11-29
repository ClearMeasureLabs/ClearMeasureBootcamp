using System;
using System.Data;
using ClearMeasure.Bootcamp.Core.Model;
using NHibernate.Dialect;
using NHibernate.SqlTypes;
using NHibernate.Type;

namespace ClearMeasure.Bootcamp.DataAccess.Mappings
{
	public class ExpenseReportStatusType : PrimitiveType
	{
		public ExpenseReportStatusType() : base(new StringFixedLengthSqlType(3))
		{
		}

		public override object Get(IDataReader rs, int index)
		{
			string value = (string)rs[index];
			return ExpenseReportStatus.FromCode(value);
		}

		public override object Get(IDataReader rs, string name)
		{
			int ordinal = rs.GetOrdinal(name);
			return Get(rs, ordinal);
		}
        
		public override Type ReturnedClass
		{
			get { return typeof(ExpenseReportStatus); }
		}

		public override object FromStringValue(string xml)
		{
			return byte.Parse(xml);
		}

		public override string Name
		{
			get { return "ExpenseReportStatus"; }
		}

		public override void Set(IDbCommand cmd, object value, int index)
		{
			var parameter = (IDataParameter)cmd.Parameters[index];

			var val = (ExpenseReportStatus)value;

			parameter.Value = val.Code;
		}
        
	    public override string ObjectToSQLString(object value, Dialect dialect)
	    {
	        return value.ToString();
	    }

	    public override Type PrimitiveClass
	    {
            get { return typeof (string); }
	    }

	    public override object DefaultValue
	    {
            get { return ""; }
	    }
	}
}