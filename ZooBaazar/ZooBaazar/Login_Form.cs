using Data_Access;
using Logic;
using Logic.ScheduleStuff;
using Microsoft.Data.SqlClient;
using System.CodeDom;
using System.Data.SqlTypes;

namespace ZooBaazar
{
    public partial class Login_Form : Form
    {
        private EmployeeManager employeeManager;
        private ContractManager contractManager;
        private AnimalManager animalManager;
        private TaskManager taskManager;
        private LocationManager locationManager;
        private FeedingPlanManager feedingPlanManager;
        private MedicalRecordManager medicalRecordManager;
        private RelationshipManager relationshipManager;
        private ScheduleManager scheduleManager;

        public Login_Form()
        {
            InitializeComponent();

            lblError.Text = string.Empty;

            employeeManager = new EmployeeManager(new EmployeeRepository(), new ContractRepository());

            contractManager = new ContractManager(new ContractRepository());

            animalManager = new AnimalManager(new AnimalRepository(), new RelationshipRepository(), new FeedingPlanRepository(), new MedicalRecordRepository());

            locationManager = new LocationManager(new LocationRepository(), new AnimalRepository(), new RelationshipRepository(), new FeedingPlanRepository(), new MedicalRecordRepository());

            taskManager = new TaskManager(new TaskRepository(), locationManager);

            relationshipManager = new RelationshipManager(new RelationshipRepository());

            feedingPlanManager = new FeedingPlanManager(new FeedingPlanRepository(), new AnimalRepository());

            medicalRecordManager = new MedicalRecordManager(new MedicalRecordRepository(), new AnimalRepository());

            scheduleManager = new ScheduleManager(new ScheduleRepository(), locationManager);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Resets Error message
            lblError.Text = string.Empty;

            List<Employee> employees = new();
            try
            {
                employees = employeeManager.LoadEmployeesFromDataBase();
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    lblError.Text = ex.Message;
                    // For testing
                    employees = new List<Employee>() { new(0, "PedroTheAdmin", "test1234", "Pedro", "", "", "", "", DateTime.Now, null, WorkType.Administrator) };
                }
                else throw ex;
            }


            if (employees.Where(e => e.Username == tbUsernameLoginForm.Text).Count() != 0)
            {
                Employee? User = employees.Where(e => e.Username == tbUsernameLoginForm.Text).First();

                if (User.Role != WorkType.Administrator)
                {
                    lblError.Text = "Access Denied";
                    return;
                }

                if (User.CheckPassword(tbPasswordLoginForm.Text))
                {
                    MainPageForm mainPageForm = new MainPageForm(employeeManager, contractManager, locationManager, animalManager, taskManager, relationshipManager, feedingPlanManager, medicalRecordManager, scheduleManager);
                    mainPageForm.Owner = this;
                    mainPageForm.Text = "Hello " + User.Name;
                    this.Visible = false;
                    mainPageForm.Show();
                }
                else
                {
                    lblError.Text = "Access Denied";
                }
            }
            else
            {
                lblError.Text = "Access Denied";
            }
        }
    }
}
