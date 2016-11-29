using System;

namespace ClearMeasure.Bootcamp.Core.Model
{
	public class ExpenseReportStatus
	{
		public static readonly ExpenseReportStatus None = new ExpenseReportStatus("", "", " ", 0);
		public static readonly ExpenseReportStatus Draft = new ExpenseReportStatus("DFT", "Draft", "Drafting", 1);
		public static readonly ExpenseReportStatus Submitted = new ExpenseReportStatus("SBM", "Submitted", "Submitted", 2);
		public static readonly ExpenseReportStatus Approved = new ExpenseReportStatus("APV", "Approved", "Approved", 3);
		public static readonly ExpenseReportStatus Cancelled = new ExpenseReportStatus("CAN", "Cancelled", "Cancelled", 4);
		
	    private string _code;
		private string _key;

	    protected ExpenseReportStatus()
		{
		}

		protected ExpenseReportStatus(string code, string key, string friendlyName, byte sortBy)
		{
			_code = code;
			_key = key;
			FriendlyName = friendlyName;
			SortBy = sortBy;
		}

		public static ExpenseReportStatus[] GetAllItems()
		{
			return new []
				{
					Draft,
					Submitted,
					Approved,
                    Cancelled
				};
		}

		public string Code
		{
			get { return _code; }
		}

		public string Key
		{
			get { return _key; }
		}

	    public string FriendlyName { get; set; }

	    public byte SortBy { get; set; }

	    public override bool Equals(object obj)
		{
			var code = obj as ExpenseReportStatus;
			if (code == null) return false;

			if (!GetType().Equals(obj.GetType())) return false;

			return _code.Equals(code.Code);
		}

		public override string ToString()
		{
			return FriendlyName;
		}

		public override int GetHashCode()
		{
			return _code.GetHashCode();
		}

		public bool IsEmpty()
		{
			return Code == "";
		}

		public static ExpenseReportStatus FromCode(string code)
		{
			ExpenseReportStatus[] items = GetAllItems();
			ExpenseReportStatus match =
				Array.Find(items, delegate(ExpenseReportStatus instance) { return instance.Code == code; });

			if (match == null)
			{
				match = None;
			}
           
			return match;
		}

		public static ExpenseReportStatus FromKey(string key) 
		{
			if (key == null)
			{
				throw new NotSupportedException("Finding a ExpenseReportStatus for a null key is not supported");
			}

			ExpenseReportStatus[] items = GetAllItems();
			ExpenseReportStatus match = Array.Find(items, delegate(ExpenseReportStatus instance) { return (instance.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); });

			if (match == null)
			{
				throw new ArgumentOutOfRangeException(string.Format("Key '{0}' is not a valid key for {1}", key, typeof(ExpenseReportStatus).Name));
			}

			return match;
		}

		public static ExpenseReportStatus Parse(string name)
		{
			return FromKey(name);
		}
	}
}