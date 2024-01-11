namespace oopseg{
    class EmployeeClient{
        public static void main(){
            Trainee t = new Trainee();
            t.GetTraineeDetails();
            t.CalculateSalary();
            t.ShowTraineeDetails();
            
            Permanent p = new Permanent();
            p.GetDetails();
            p.CalculateSalary();
            p.ShowDetails();
            
        }
    }
}