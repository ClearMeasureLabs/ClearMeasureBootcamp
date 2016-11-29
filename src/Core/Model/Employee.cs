using System;

namespace ClearMeasure.Bootcamp.Core.Model
{
    public class Employee : IComparable<Employee>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string EmailAddress { get; set; }

        public Employee()
        {
        }

        public Employee(string userName, string firstName, string lastName, string emailAddress)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }

        public virtual bool CanActOnBehalf(Employee currentUser)
        {
            return this == currentUser;
        }

        public int CompareTo(Employee other)
        {
            int compareResult = LastName.CompareTo(other.LastName);
            if (compareResult == 0)
            {
                compareResult = FirstName.CompareTo(other.FirstName);
            }

            return compareResult;
        }

        public string GetFullName()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }

        public override string ToString()
        {
            return GetFullName();
        }

        public string GetNotificationEmail(DayOfWeek day)
        {
            return EmailAddress;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Employee)) return false;
            return Equals((Employee) obj);
        }

        public bool Equals(Employee other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Id == Guid.Empty) return false;
            return other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            if (Id == Guid.Empty) return base.GetHashCode();
            return Id.GetHashCode();
        }

        public static bool operator ==(Employee a, Employee b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object) a == null) || ((object) b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Equals(b);
        }

        public static bool operator !=(Employee a, Employee b)
        {
            return !(a == b);
        }
    }
}