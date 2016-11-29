namespace ClearMeasure.Bootcamp.Core.Services
{
	public class EmployeeSpecification
	{
		public static readonly EmployeeSpecification All = new EmployeeSpecification();

	    private bool _canFulfill = false;

	    public EmployeeSpecification()
	    {
	    }

	    public EmployeeSpecification(bool canFulfill)
	    {
	        _canFulfill = canFulfill;
	    }

	    public bool CanFulfill
	    {
	        get { return _canFulfill; }
	        set { _canFulfill = value; }
	    }
	}
}