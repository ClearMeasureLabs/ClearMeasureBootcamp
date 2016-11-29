namespace ClearMeasure.Bootcamp.Core.Model
{
    public class Manager : Employee
    {
        public Manager()
        { }
        public Manager(string userName, string firstName, string lastName, string email)
            : base(userName, firstName, lastName, email)
        { }

        public Employee AdminAssistant { get; set; }
        public override bool CanActOnBehalf(Employee currentUser)
        {
            if (currentUser == null) return false;

            return currentUser == this || currentUser == AdminAssistant;
        }
    }
}