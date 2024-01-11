namespace interfaceEmployee{
    public interface IEmployee{
        public int Empid{get;set;}

        public string Empname{get;set;}

        public float Salary{get;set;}

        public DateTime Doj{get;set;}
        
        void AcceptDetails();

        void DisplayDetails();

        void CalculateSalary();
    }
}