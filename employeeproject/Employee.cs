namespace oopseg{
    class Employee{
        public int Empid{get;set;}

        public string Empname{get;set;}

        public float Salary{get;set;}

        public DateTime Doj{get;set;}

        public void AcceptDetails(){
            System.Console.WriteLine("Enter Emp ID: ");
            Empid = Convert.ToInt32(System.Console.ReadLine());
            System.Console.WriteLine("Enter Emp Name: ");
            Empname = System.Console.ReadLine();
            System.Console.WriteLine("Enter DOJ: ");
            Doj = Convert.ToDateTime(System.Console.ReadLine());
        }

        public void DisplayDetails(){
            System.Console.WriteLine("Your Employee ID is: "+Empid);
            System.Console.WriteLine("Your Name is: "+Empname);
            System.Console.WriteLine("Your DOJ is: {0}",Doj.ToShortDateString());
        }

        public virtual void CalculateSalary(){}
    }

    class Permanent : Employee{
        public float Basicpay{get;set;}

        public float HRA{get;set;}

        public float DA{get;set;}

        public float PF{get;set;}

        public void GetDetails(){
            base.AcceptDetails();
            System.Console.WriteLine("Enter BasicPay: ");
            Basicpay = float.Parse(System.Console.ReadLine());
            System.Console.WriteLine("Enter HRA: ");
            HRA = float.Parse(System.Console.ReadLine());
            System.Console.WriteLine("Enter DA: ");
            DA = float.Parse(System.Console.ReadLine());
            System.Console.WriteLine("Enter PF: ");
            PF = float.Parse(System.Console.ReadLine());
        }

        public void ShowDetails(){
            base.DisplayDetails();
            System.Console.WriteLine("Your BasicPay is: "+Basicpay);
            System.Console.WriteLine("Your HRA is: "+HRA);
            System.Console.WriteLine("Your DA is: "+DA);
            System.Console.WriteLine("Your PF is: "+PF);
            System.Console.WriteLine("Your Salary is: "+Salary);
        }

        public override void CalculateSalary()
        {
            Salary=Basicpay+HRA+DA-PF;
        }
    }

    class Trainee : Employee{
        public float Bonus{get;set;}

        public string Projectname{get;set;}

        public void GetTraineeDetails(){
            base.AcceptDetails();
            System.Console.WriteLine("Enter your Basic Salary: ");
            Salary = Convert.ToInt32(System.Console.ReadLine());
            System.Console.WriteLine("Enter your Project Name: ");
            Projectname = (System.Console.ReadLine());
        }

        public void ShowTraineeDetails(){
            base.DisplayDetails();
            System.Console.WriteLine("The project assigned to the trainee is: "+Projectname);
            // if(Projectname == "Banking"){
            //     Bonus = 0.05F*Salary;
            // }
            // else if(Projectname == "Insurance"){
            //     Bonus = 0.1F*Salary;
            // }
            // else{
            //     Bonus = 0;
            // }
            System.Console.WriteLine("The bonus for this trainee is: "+Bonus);
            System.Console.WriteLine("The total salary for this trainee is: "+Salary);
        }

        public override void CalculateSalary(){
            if(Projectname == "Banking"){
                Bonus = 0.05F*Salary;
            }
            else if(Projectname == "Insurance"){
                Bonus = 0.1F*Salary;
            }
            else{
                Bonus = 0;
            }
            Salary = Salary + Bonus;
        }
    }
}