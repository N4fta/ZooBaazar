using Data_Access;
using Logic;
using Logic.Interfaces;
using Logic.ScheduleStuff;
using Logic.ScheduleStuff.Makers;
using System.Data;
using System.Linq.Expressions;

namespace ZooBaazar
{
    public partial class MainPageForm : Form
    {
        //For all of them
        private static LocationManager locationManager;
        private static EmployeeManager employeeManager;
        private static ContractManager contractManager;

        //For the Animal Page
        private static AnimalManager animalManager;
        private static RelationshipManager relationshipManager;

        private static FeedingPlanManager feedingPlanManager;
        private static MedicalRecordManager mediicalRecordManager;

        //For the Tasks Page
        private static TaskManager taskManager;

        //For the Location Page
        private Location existingLocation;

        //Schedule
        private ScheduleManager scheduleManager;
        private Schedule schedule;

        public MainPageForm(EmployeeManager em, ContractManager cm, LocationManager lm, AnimalManager am, TaskManager tskm, RelationshipManager rm, FeedingPlanManager fm, MedicalRecordManager mm, ScheduleManager sm)
        {
            InitializeComponent();
            #region Tags buttons
            //button tags to help organize the code
            btnSchedulePage.Tag = "Schedule";

            btnEmployeesPage.Tag = "Employees";

            btnAnimalsPage.Tag = "Animals";

            btnTasksPage.Tag = "Tasks";

            btnLocationsPage.Tag = "Locations";

            btnLogOutPage.Tag = "LogOut";
            #endregion

            //For the Employes Page
            employeeManager = em;
            contractManager = cm;
            SetupDataGridViewEmp();
            PopulateEmployees();

            //For the Animal Page
            animalManager = am;
            relationshipManager = rm;
            feedingPlanManager = fm;
            mediicalRecordManager = mm;
            SetupDataGridViewAm();
            PopulateAnimals();

            //For the Tasks Page
            taskManager = tskm;
            SetupDataGridViewTsk();
            PopulateTasks();

            //For the Locations Page
            locationManager = lm;
            SetupDataGridViewLoc();
            PopulateLocations();

            //For Schedule
            scheduleManager = sm;
            InitializeComboBoxes();
        }


        private void MainPage_Load(object sender, EventArgs e)
        {
            PopulateFilterCb();
            tcMainPageForm.ItemSize = new Size(0, 1);
            tcMainPageForm.SizeMode = TabSizeMode.Fixed;

            // This will wire up the event handler to all buttons
            foreach (Control control in Controls)
            {
                if (control is Button button)
                {
                    if (button.Tag.ToString() != "LogOut")
                    {
                        button.Click += Button_Click;
                    }
                    else
                    {
                        button.Click += ButtonLogOut_Click;
                    }
                }
            }
        }

        private void MainPage_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Visible = true;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // This will determine the target tab page index based on the button clicked
                int targetIndex = -1;
                switch (clickedButton.Tag.ToString())
                {
                    case "Schedule":
                        targetIndex = 0;
                        break;
                    case "Employees":
                        targetIndex = 1;
                        break;
                    case "Animals":
                        targetIndex = 2;
                        break;
                    case "Tasks":
                        targetIndex = 3;
                        break;
                    case "Locations":
                        targetIndex = 4;
                        break;

                }

                // Switch to the target tab page
                if (targetIndex != -1)
                {
                    tcMainPageForm.SelectedIndex = targetIndex;
                }
            }
        }


        private void ButtonLogOut_Click(object sender, EventArgs e)
        {
            if (Owner != null)
            {
                Owner.Visible = true;
            }
            this.Close();
        }

        private void PopulateFilterCb()
        {
            // Employee Page Filter
            cbFilterEmployeeMainPageForm.Items.Clear();
            cbFilterEmployeeMainPageForm.Items.Add("All");
            foreach (var workType in Enum.GetValues(typeof(WorkType)))
            {
                cbFilterEmployeeMainPageForm.Items.Add(workType);
            }
            cbFilterEmployeeMainPageForm.SelectedIndex = 0;

            // Animal Page Filter
            cbFilterAnimalsMainPageForm.Items.Clear();
            cbFilterAnimalsMainPageForm.Items.Add("All");
            try
            {
                foreach (var species in animalManager.GetAllSpecies())
                {
                    cbFilterAnimalsMainPageForm.Items.Add(species);
                }
                cbFilterAnimalsMainPageForm.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = ex.Message;
            }

            // Task Page Filter
            cbFilterTasksMainPageForm.Items.Clear();
            cbFilterTasksMainPageForm.Items.Add("All");
            foreach (var workType in Enum.GetValues(typeof(WorkType)))
            {
                cbFilterTasksMainPageForm.Items.Add(workType);
            }
            cbFilterTasksMainPageForm.SelectedIndex = 0;

            // Location Filter
            cbFilterLts.Items.Clear();
            cbFilterLts.Items.Add("All");
            foreach (var danger in Enum.GetValues(typeof(DangerEnum)))
            {
                cbFilterLts.Items.Add(danger);
            }
            cbFilterLts.SelectedIndex = 0;
            cbFilterAnimalsMainPageForm.SelectedIndex = 0;
            cbFilterEmployeeMainPageForm.SelectedIndex = 0;
        }

        #region Employee Page
        //For the Employes Page

        private void btnViewEmp_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                // Get the selected employee
                Employee selectedEmployee = dgvEmployees.SelectedRows[0].DataBoundItem as Employee;

                // Open the AddEmployeeForm with the selected employee's information
                using (AddEmployeeForm addEmployeeForm = new AddEmployeeForm(locationManager, employeeManager, contractManager, selectedEmployee, true))
                {
                    if (addEmployeeForm.ShowDialog() == DialogResult.OK)
                    {
                        // Update the employee with the modified information
                        Employee updatedEmployee = addEmployeeForm.GetUpdatedEmployee();

                        if (updatedEmployee != null)
                        {
                            employeeManager.Update(updatedEmployee, updatedEmployee.Contract);
                            PopulateEmployees();
                        }
                    }
                }
            }
            else
            {
                tsslblMainPageForm.Text = "Please select an employee to view.";
            }
        }

        private void tbSearchEmployeeMainPageForm_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = tbSearchEmployeeMainPageForm.Text.Trim();
            string selectedRoleStr = cbFilterEmployeeMainPageForm.SelectedItem.ToString();

            List<Employee> filteredEmployees;

            try
            {
                if (selectedRoleStr == "All")
                {
                    filteredEmployees = employeeManager.LoadEmployeesFromDataBase()
                        .Where(emp => emp.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                      emp.Email.ToLower().Contains(searchTerm.ToLower()) ||
                                      emp.PhoneNumber.Contains(searchTerm)).ToList();
                }
                else if (Enum.TryParse<WorkType>(selectedRoleStr, out WorkType selectedRole))
                {
                    filteredEmployees = employeeManager.LoadEmployeesFromDataBase()
                        .Where(emp => emp.Role == selectedRole &&
                                      (emp.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                       emp.Email.ToLower().Contains(searchTerm.ToLower()) ||
                                       emp.PhoneNumber.Contains(searchTerm))).ToList();
                }
                else
                {
                    filteredEmployees = employeeManager.LoadEmployeesFromDataBase()
                        .Where(emp => emp.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                      emp.Email.ToLower().Contains(searchTerm.ToLower()) ||
                                      emp.PhoneNumber.Contains(searchTerm)).ToList();
                }

                dgvEmployees.DataSource = filteredEmployees;
            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = ex.Message;
            }
        }


        private void cbFilterEmployeeMainPageForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRoleStr = cbFilterEmployeeMainPageForm.SelectedItem.ToString();

            try
            {
                if (selectedRoleStr == "All")
                {
                    PopulateEmployees();
                }
                else if (Enum.TryParse<WorkType>(selectedRoleStr, out WorkType selectedRole))
                {
                    List<Employee> filteredEmployees = employeeManager.LoadEmployeesFromDataBase()
                        .Where(emp => emp.Role == selectedRole).ToList();
                    dgvEmployees.DataSource = filteredEmployees;
                }
                else
                {
                    PopulateEmployees();
                }
            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = ex.Message;
            }
        }

        private void SetupDataGridViewEmp()
        {
            // Set DataGridView properties
            dgvEmployees.AutoGenerateColumns = false;
            dgvEmployees.AllowUserToAddRows = false;

            // Add DataGridView columns
            dgvEmployees.Columns.Add("Name", "Name");
            dgvEmployees.Columns["Name"].DataPropertyName = "Name";

            dgvEmployees.Columns.Add("Role", "Role");
            dgvEmployees.Columns["Role"].DataPropertyName = "Role";

            dgvEmployees.Columns.Add("Email", "Email");
            dgvEmployees.Columns["Email"].DataPropertyName = "Email";

            dgvEmployees.Columns.Add("PhoneNumber", "Phone Number");
            dgvEmployees.Columns["PhoneNumber"].DataPropertyName = "PhoneNumber";

        }

        private void PopulateEmployees()
        {
            // Populate the form with existing employees
            try
            {
                dgvEmployees.DataSource = employeeManager.LoadEmployeesFromDataBase();
            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = ex.Message;
            }
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            // Open the AddEmployeeForm
            using (AddEmployeeForm addEmployeeForm = new AddEmployeeForm(locationManager, employeeManager, contractManager))
            {
                // Show the AddEmployeeForm
                if (addEmployeeForm.ShowDialog() == DialogResult.OK)
                {
                    // Get the newly created employee from the AddEmployeeForm
                    Employee newEmployee = addEmployeeForm.GetNewEmployee();

                    // Check if a new employee was created
                    if (newEmployee != null)
                    {
                        // Add the new employee to the EmployeeManager
                        employeeManager.Add(newEmployee, newEmployee.Contract);

                        // Update the DataGridView
                        PopulateEmployees();
                    }
                }
            }
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                // Get the selected employee
                Employee selectedEmployee = dgvEmployees.SelectedRows[0].DataBoundItem as Employee;

                DialogResult result = MessageBox.Show("Are you sure you want to delete this employee?", "Delete Employee", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                // Check if the user clicked OK
                if (result == DialogResult.OK)
                {
                    // Remove the selected employee from the EmployeeManager
                    employeeManager.Remove(selectedEmployee);

                    // Update the DataGridView
                    PopulateEmployees();
                }
            }
            else
            {
                tsslblMainPageForm.Text = "Please select an employee to delete.";
            }
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                // Get the selected employee
                Employee selectedEmployee = dgvEmployees.SelectedRows[0].DataBoundItem as Employee;

                // Open the AddEmployeeForm with the selected employee's information
                using (AddEmployeeForm addEmployeeForm = new AddEmployeeForm(locationManager, employeeManager, contractManager, selectedEmployee))
                {
                    if (addEmployeeForm.ShowDialog() == DialogResult.OK)
                    {
                        // Update the employee with the modified information
                        Employee updatedEmployee = addEmployeeForm.GetUpdatedEmployee();

                        if (updatedEmployee != null)
                        {
                            employeeManager.Update(updatedEmployee, updatedEmployee.Contract);
                            PopulateEmployees();
                        }
                    }
                }
            }
            else
            {
                tsslblMainPageForm.Text = "Please select an employee to update.";
            }
        }

        #endregion

        #region Animal Page
        //For the Animals Page
        private void btnViewAni_Click(object sender, EventArgs e)
        {
            if (dgvAnimals.SelectedRows.Count > 0)
            {
                // Get the selected Animal
                Animal selectedAnimal = dgvAnimals.SelectedRows[0].DataBoundItem as Animal;

                // Open the AnimalForm with the selected animal's information
                using (AnimalForm addAnimalForm = new AnimalForm(animalManager, relationshipManager, feedingPlanManager, mediicalRecordManager, locationManager, selectedAnimal, true))
                {
                    if (addAnimalForm.ShowDialog() == DialogResult.OK)
                    {
                        // Update the animal with the modified information
                        Animal updatedAnimal = addAnimalForm.GetUpdatedAnimal();
                        if (updatedAnimal != null)
                        {
                            existingLocation = locationManager.LoadLocationFromDatabse().Find(l => l.Animals.Contains(selectedAnimal));
                            animalManager.Update(updatedAnimal, existingLocation.Id, updatedAnimal.FeedingPlan, updatedAnimal.MedicalRecord);
                            PopulateAnimals();
                        }
                    }
                }
            }
            else
            {
                tsslblMainPageForm.Text = "Please select an animal to view.";
            }
        }

        private void tbSearchAni_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = tbSearchAnimalsMainPageForm.Text.Trim();
            string selectedSpecies = cbFilterAnimalsMainPageForm.SelectedItem.ToString();

            List<Animal> filteredAnimals;

            try
            {
                if (selectedSpecies == "All")
                {
                    filteredAnimals = animalManager.LoadAnimalFromDataBase()
                        .Where(anm => anm.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                      anm.Species.ToLower().Contains(searchTerm.ToLower()) ||
                                      anm.Origin.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                else
                {
                    filteredAnimals = animalManager.LoadAnimalFromDataBase()
                        .Where(anm => anm.Species == selectedSpecies &&
                                      (anm.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                       anm.Origin.ToLower().Contains(searchTerm.ToLower()))).ToList();
                }

                dgvAnimals.DataSource = filteredAnimals;
            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = ex.Message;
            }
        }

        private void cbFilterAnimalsMainPageForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSpecies = cbFilterAnimalsMainPageForm.SelectedItem.ToString();

            try
            {
                if (selectedSpecies == "All")
                {
                    PopulateAnimals();
                }
                else
                {
                    List<Animal> filteredAnimals = animalManager.LoadAnimalFromDataBase()
                        .Where(anm => anm.Species == selectedSpecies).ToList();
                    dgvAnimals.DataSource = filteredAnimals;
                }
            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = ex.Message;
            }
        }


        private void SetupDataGridViewAm()
        {
            // Set DataGridView properties
            dgvAnimals.AutoGenerateColumns = false;
            dgvAnimals.AllowUserToAddRows = false;

            // Add DataGridView columns
            dgvAnimals.Columns.Add("Name", "Name");
            dgvAnimals.Columns["Name"].DataPropertyName = "Name";

            dgvAnimals.Columns.Add("Species", "Species");
            dgvAnimals.Columns["Species"].DataPropertyName = "Species";

            dgvAnimals.Columns.Add("Origin", "Origin");
            dgvAnimals.Columns["Origin"].DataPropertyName = "Origin";

        }

        private void PopulateAnimals()
        {
            // Populate the form with existing animals
            try
            {
                dgvAnimals.DataSource = null;
                dgvAnimals.DataSource = animalManager.LoadAnimalFromDataBase();
            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = ex.Message;
            }
        }

        private void btnAddAnimal_Click(object sender, EventArgs e)
        {
            // Open the AnimalForm
            using (AnimalForm addAnimalForm = new AnimalForm(animalManager, relationshipManager, feedingPlanManager, mediicalRecordManager, locationManager))
            {
                // Show the AnimalForm
                if (addAnimalForm.ShowDialog() == DialogResult.OK)
                {
                    PopulateAnimals();
                }
            }
        }



        private void btnDeleteAnimal_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dgvAnimals.SelectedRows.Count > 0)
            {
                // Get the selected Animal
                Animal selectedAnimal = dgvAnimals.SelectedRows[0].DataBoundItem as Animal;

                DialogResult result = MessageBox.Show("Are you sure you want to delete this Animal?", "Delete Animal", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    // Remove the selected animal from the AnimalManager
                    animalManager.Remove(selectedAnimal);

                    // Update the DataGridView
                    PopulateAnimals();
                }
            }
            else
            {
                tsslblMainPageForm.Text = "Please select an Animal to delete.";
            }
        }

        private void btnUpdateAnimal_Click(object sender, EventArgs e)
        {
            if (dgvAnimals.SelectedRows.Count > 0)
            {
                // Get the selected animal
                Animal selectedAnimal = dgvAnimals.SelectedRows[0].DataBoundItem as Animal;

                // Open the AnimalForm with the selected animal's information
                using (AnimalForm addAnimalForm = new AnimalForm(animalManager, relationshipManager, feedingPlanManager, mediicalRecordManager, locationManager, selectedAnimal))
                {
                    addAnimalForm.ShowDialog();
                    PopulateAnimals();
                }
            }
            else
            {
                tsslblMainPageForm.Text = "Please select an animal to update.";
            }
        }
        #endregion

        #region Tasks Page
        //For the Tasks Page
        private void btnViewTsk_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count > 0)
            {
                // Get the selected task
                Logic.ScheduleStuff.Task selectedTask = dgvTasks.SelectedRows[0].DataBoundItem as Logic.ScheduleStuff.Task;

                // Open the TasksForm with the selected task's information
                using TasksForm addTaskForm = new TasksForm(taskManager, locationManager, employeeManager, selectedTask, true);
                addTaskForm.ShowDialog();
                // Update the DataGridView
                dgvTasks.DataSource = taskManager.LoadTaskFromDataBase();

            }
            else
            {
                tsslblMainPageForm.Text = "Please select a Task to view.";
            }
        }

        private void tbSearchTsk_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = tbSearchTasksMainPageForm.Text.Trim();
            string selectedWorkTypeStr = cbFilterTasksMainPageForm.SelectedItem.ToString();

            List<Logic.ScheduleStuff.Task> filteredTasks;

            try
            {
                if (selectedWorkTypeStr == "All")
                {
                    filteredTasks = taskManager.LoadTaskFromDataBase()
                        .Where(tsk => tsk.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                      tsk.Description.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                else if (Enum.TryParse<WorkType>(selectedWorkTypeStr, out WorkType selectedWorkType))
                {
                    filteredTasks = taskManager.LoadTaskFromDataBase()
                        .Where(tsk => tsk.WorkType == selectedWorkType &&
                                      (tsk.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                       tsk.Description.ToLower().Contains(searchTerm.ToLower()))).ToList();
                }
                else
                {
                    filteredTasks = taskManager.LoadTaskFromDataBase()
                        .Where(tsk => tsk.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                      tsk.Description.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                dgvTasks.DataSource = filteredTasks;
            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = ex.Message;
            }
        }

        private void cbFilterTasksMainPageForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedWorkTypeStr = cbFilterTasksMainPageForm.SelectedItem.ToString();

            try
            {
                if (selectedWorkTypeStr == "All")
                {
                    PopulateTasks();
                }
                else if (Enum.TryParse<WorkType>(selectedWorkTypeStr, out WorkType selectedWorkType))
                {
                    List<Logic.ScheduleStuff.Task> filteredTasks = taskManager.LoadTaskFromDataBase()
                        .Where(tsk => tsk.WorkType == selectedWorkType).ToList();
                    dgvTasks.DataSource = filteredTasks;
                }
                else
                {
                    PopulateTasks();
                }
            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = ex.Message;
            }
        }

        private void SetupDataGridViewTsk()
        {
            // Set DataGridView properties
            dgvTasks.AutoGenerateColumns = false;
            dgvTasks.AllowUserToAddRows = false;

            // Add DataGridView columns
            dgvTasks.Columns.Add("Name", "Name");
            dgvTasks.Columns["Name"].DataPropertyName = "Name";

            dgvTasks.Columns.Add("Description", "Description");
            dgvTasks.Columns["Description"].DataPropertyName = "Description";

            dgvTasks.Columns.Add("StartDate", "Start Date");
            dgvTasks.Columns["StartDate"].DataPropertyName = "StartDate";

            dgvTasks.Columns.Add("RepeatType", "Repeats");
            dgvTasks.Columns["RepeatType"].DataPropertyName = "RepeatType";

            dgvTasks.Columns.Add("NumberOfRepeats", "Amount");
            dgvTasks.Columns["NumberOfRepeats"].DataPropertyName = "NumberOfRepeats";

            dgvTasks.Columns.Add("WorkType", "Work Type");
            dgvTasks.Columns["WorkType"].DataPropertyName = "WorkType";

            dgvTasks.Columns.Add("DayTask", "Day Task");
            dgvTasks.Columns["DayTask"].DataPropertyName = "DayTask";

            dgvTasks.Columns.Add("NightTask", "Night Task");
            dgvTasks.Columns["NightTask"].DataPropertyName = "NightTask";

            dgvTasks.Columns.Add("Location", "Location");
            dgvTasks.Columns["Location"].DataPropertyName = "Location";

        }

        private void PopulateTasks()
        {
            // Populate the form with existing tasks
            try
            {
                dgvTasks.DataSource = taskManager.LoadTaskFromDataBase();
            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = ex.Message;
            }
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            // Open the TasksForm
            using TasksForm addTaskForm = new TasksForm(taskManager, locationManager, employeeManager);
            addTaskForm.ShowDialog();
            // Update the DataGridView
            dgvTasks.DataSource = taskManager.LoadTaskFromDataBase();
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dgvTasks.SelectedRows.Count > 0)
            {
                // Get the selected task
                Logic.ScheduleStuff.Task selectedTask = dgvTasks.SelectedRows[0].DataBoundItem as Logic.ScheduleStuff.Task;

                DialogResult result = MessageBox.Show("Are you sure you want to delete this task?", "Delete Task", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    // Delete Task
                    taskManager.Remove(selectedTask);

                    // Update the DataGridView
                    dgvTasks.DataSource = taskManager.LoadTaskFromDataBase();
                }
            }
            else
            {
                tsslblMainPageForm.Text = "Please select an task to delete.";
            }

        }

        private void btnUpdateTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count > 0)
            {
                // Get the selected task
                Logic.ScheduleStuff.Task selectedTask = dgvTasks.SelectedRows[0].DataBoundItem as Logic.ScheduleStuff.Task;

                // Open the TasksForm with the selected task's information
                using TasksForm addTaskForm = new TasksForm(taskManager, locationManager, employeeManager, selectedTask);
                addTaskForm.ShowDialog();
                // Update the DataGridView
                dgvTasks.DataSource = taskManager.LoadTaskFromDataBase();
            }
            else
            {
                tsslblMainPageForm.Text = "Please select an Task to update.";
            }
        }
        #endregion

        #region Locations Page
        //For the Locations Page
        private void btnViewLts_Click(object sender, EventArgs e)
        {
            if (dgvLocations.SelectedRows.Count > 0)
            {
                // Get the selected location
                Location selectedLocation = dgvLocations.SelectedRows[0].DataBoundItem as Location;

                // Open the TasksForm with the selected task's information
                using (LocationForm addlocationForm = new LocationForm(locationManager, animalManager, selectedLocation, true))
                {
                    if (addlocationForm.ShowDialog() == DialogResult.OK)
                    {
                        // Update the Location with the modified information
                        Location updatedLocation = addlocationForm.GetUpdatedLocation();
                        if (updatedLocation != null)
                        {
                            locationManager.Update(updatedLocation);
                            PopulateLocations();
                        }
                    }
                }
            }
            else
            {
                tsslblMainPageForm.Text = "Please select an Location to update.";
            }
        }

        private void tbSearchLts_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = tbSearchLts.Text.Trim();
            string selectedDangerStr = cbFilterLts.SelectedItem.ToString();

            List<Location> filteredLocations;

            try
            {
                if (selectedDangerStr == "All")
                {
                    // If "All" is selected, searches among all locations
                    filteredLocations = locationManager.LoadLocationFromDatabse()
                        .Where(lts => lts.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                else if (Enum.TryParse<DangerEnum>(selectedDangerStr, out DangerEnum selectedDanger))
                {
                    // Filters the list of locations based on the selected danger
                    filteredLocations = locationManager.LoadLocationFromDatabse()
                        .Where(lts => lts.Danger == selectedDanger &&
                                       lts.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                else
                {
                    // If no valid danger selected, return all matching locations
                    filteredLocations = locationManager.LoadLocationFromDatabse()
                        .Where(lts => lts.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                // Updates the DataGridView with the filtered list of locations
                dgvLocations.DataSource = filteredLocations;

            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = ex.Message;
            }
        }

        private void cbFilterLts_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDangerStr = cbFilterLts.SelectedItem.ToString();

            try
            {
                if (selectedDangerStr == "All")
                {
                    // If "All" is selected, show all locations
                    PopulateLocations();
                }
                else if (Enum.TryParse<DangerEnum>(selectedDangerStr, out DangerEnum selectedDanger))
                {
                    // Filter the list of locations based on the selected danger
                    List<Location> filteredLocations = locationManager.LoadLocationFromDatabse().Where(ltn => ltn.Danger == selectedDanger).ToList();

                    // Updates the DataGridView with the filtered list of locations
                    dgvLocations.DataSource = filteredLocations;
                }
            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = ex.Message;
            }
        }

        private void SetupDataGridViewLoc()
        {
            // Set DataGridView properties
            dgvLocations.AutoGenerateColumns = false;
            dgvLocations.AllowUserToAddRows = false;

            // Add DataGridView columns
            dgvLocations.Columns.Add("Name", "Name");
            dgvLocations.Columns["Name"].DataPropertyName = "Name";

            dgvLocations.Columns.Add("Capacity", "Capacity");
            dgvLocations.Columns["Capacity"].DataPropertyName = "Capacity";

            dgvLocations.Columns.Add("AnimalCount", "AnimalCount");
            dgvLocations.Columns["AnimalCount"].DataPropertyName = "AnimalCount";

            dgvLocations.Columns.Add("Danger", "Danger");
            dgvLocations.Columns["Danger"].DataPropertyName = "Danger";

            dgvLocations.Columns.Add("Description", "Description");
            dgvLocations.Columns["Description"].DataPropertyName = "Description";

        }

        private void PopulateLocations()
        {
            // Populate the form with existing locations
            try
            {
                dgvLocations.DataSource = locationManager.LoadLocationFromDatabse();
            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = ex.Message;
            }
        }

        private void btnAddLocation_Click(object sender, EventArgs e)
        {
            // Open the LocationForm
            using (LocationForm addLocationForm = new LocationForm(locationManager, animalManager))
            {
                // Show the LocationForm
                if (addLocationForm.ShowDialog() == DialogResult.OK)
                {
                    // Get the newly created location from the LocationForm
                    Location newLocation = addLocationForm.GetNewLocation();

                    // Check if a new location was created
                    if (newLocation != null)
                    {
                        //// Add the new Location to the LocationManager
                        //locationManager.Add(newLocation);

                        // Update the DataGridView
                        PopulateLocations();
                    }
                }
            }
        }

        private void btnDeleteLocation_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dgvLocations.SelectedRows.Count > 0)
            {
                // Get the selected location
                Location selectedLocation = dgvLocations.SelectedRows[0].DataBoundItem as Location;

                if (selectedLocation.AnimalCount > 0)
                {
                    // Display an error message if there are animals in the location
                    tsslblMainPageForm.Text = "Cannot delete location with animals. Please move the animals first.";
                }
                else
                {
                    // Confirm deletion
                    DialogResult result = MessageBox.Show($"Are you sure you want to delete this Location? ", "Delete Location", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (result == DialogResult.OK)
                    {
                        // Remove the selected location from the locationManager
                        locationManager.Remove(selectedLocation);

                        // Update the DataGridView
                        PopulateLocations();
                    }
                }
            }
            else
            {
                tsslblMainPageForm.Text = "Please select a Location to delete.";
            }
        }


        private void btnUpdateLoaction_Click(object sender, EventArgs e)
        {
            if (dgvLocations.SelectedRows.Count > 0)
            {
                // Get the selected location
                Location selectedLocation = dgvLocations.SelectedRows[0].DataBoundItem as Location;

                // Open the TasksForm with the selected task's information
                using (LocationForm addlocationForm = new LocationForm(locationManager, animalManager, selectedLocation, false))
                {
                    if (addlocationForm.ShowDialog() == DialogResult.OK)
                    {
                        // Update the Location with the modified information
                        Location updatedLocation = addlocationForm.GetUpdatedLocation();
                        if (updatedLocation != null)
                        {
                            //locationManager.Update(updatedLocation);
                            PopulateLocations();
                        }
                    }
                }
            }
            else
            {
                tsslblMainPageForm.Text = "Please select an Location to update.";
            }
        }

        #endregion

        #region Schedule
        private void btnCreateSchedule_Click(object sender, EventArgs e)
        {
            ShiftMakerDefault shiftMaker;
            ITaskMaker taskMaker;
            try
            {
                shiftMaker = new ShiftMakerDefault(employeeManager.LoadEmployeesFromDataBase(), Convert.ToInt32(nudShiftLenght.Value));
                taskMaker = new TaskMakerFromDB(taskManager.LoadTaskFromDataBase());
            }
            // Fake data for offline testing (when DB went down)
            catch (Exception ex)
            {
                shiftMaker = new ShiftMakerDefault(new() {
                    // Cleaner
                    new(1, "Employee A", "password", "A", "", "", "", "", DateTime.Now,
                        new(1, DateTime.Now, DateTime.Now, 200, WorkType.Cleaner, 10, 10, 60, false, true, true, false,
                            new() { DayOfWeek.Monday,DayOfWeek.Tuesday,DayOfWeek.Wednesday,DayOfWeek.Thursday,DayOfWeek.Friday},
                            20, 20, "", ContractType.FixedTerm)
                    , WorkType.Cleaner),
                    new(1, "Employee B", "password", "B", "", "", "", "", DateTime.Now,
                        new(1, DateTime.Now, DateTime.Now, 200, WorkType.Cleaner, 10, 10, 60, false, true, false, true,
                            new() { DayOfWeek.Monday,DayOfWeek.Tuesday,DayOfWeek.Wednesday,DayOfWeek.Thursday,DayOfWeek.Friday},
                            20, 20, "", ContractType.FixedTerm)
                    , WorkType.Cleaner),

                    // Security Guard
                    new(1, "Employee C", "password", "C", "", "", "", "", DateTime.Now,
                        new(1, DateTime.Now, DateTime.Now, 200, WorkType.Cleaner, 10, 10, 60, false, true, true, true,
                            new() { DayOfWeek.Monday,DayOfWeek.Tuesday,DayOfWeek.Wednesday,DayOfWeek.Thursday,DayOfWeek.Friday},
                            20, 20, "", ContractType.FixedTerm)
                    , WorkType.Security),
                    new(1, "Employee D", "password", "D", "", "", "", "", DateTime.Now,
                        new(1, DateTime.Now, DateTime.Now, 200, WorkType.Cleaner, 10, 10, 60, false, true, false, true,
                            new() { DayOfWeek.Monday,DayOfWeek.Tuesday,DayOfWeek.Wednesday,DayOfWeek.Thursday,DayOfWeek.Friday},
                            20, 20, "", ContractType.FixedTerm)
                    , WorkType.Security),

                    // Others
                    new(1, "Employee E", "password", "E", "", "", "", "", DateTime.Now,
                        new(1, DateTime.Now, DateTime.Now, 200, WorkType.Cleaner, 10, 10, 60, false, true, true, true,
                            new() { DayOfWeek.Monday,DayOfWeek.Tuesday,DayOfWeek.Wednesday,DayOfWeek.Thursday,DayOfWeek.Friday},
                            20, 20, "", ContractType.FixedTerm)
                    , WorkType.Retail),
                    new(1, "Employee F", "password", "F", "", "", "", "", DateTime.Now,
                        new(1, DateTime.Now, DateTime.Now, 200, WorkType.Cleaner, 10, 10, 60, false, true, true, true,
                            new() { DayOfWeek.Monday,DayOfWeek.Tuesday,DayOfWeek.Wednesday,DayOfWeek.Thursday,DayOfWeek.Friday},
                            20, 20, "", ContractType.FixedTerm)
                    , WorkType.Administrator),


                }, Convert.ToInt32(nudShiftLenght.Value));
                taskMaker = new TaskMakerFromDB(new(){
                    new Logic.ScheduleStuff.Task(
                        1,
                        "Feeding Lions",
                        "",
                        WorkType.Caretaker,
                        RepeatEnum.Daily,
                        3,
                        DateTime.Now,
                        DateTime.Now.AddHours(2),
                        true,
                        false,
                        new Location("Lions Den", 5, 4, "", DangerEnum.HighDanger, "Lion", new())
                        )
                });
            }

            ScheduleMaker scheduleMaker = null;
            try
            {
                // Takes Lists, not single objects
                // Throws an error if inputs invalid
                scheduleMaker = new ScheduleMaker(new() { taskMaker }, shiftMaker, Convert.ToInt32(nudStartOfDay.Value), Convert.ToInt32(nudEndOfDay.Value));
            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = ex.Message;
                return;
            }
            try
            {
                schedule = scheduleMaker.GenerateSchedule(DateTime.Now.AddDays(14));
            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = ex.Message;
                return;
            }
            scheduleManager.AddSchedule(schedule);

            // Update List Boxes
            // Clear List boxes
            lbScheduleWeeks.SelectedItem = null;
            lbScheduleWeeks.Items.Clear();
            lbScheduleWeekDays.Items.Clear();
            lbScheduleTasks.Items.Clear();
            lbScheduleShifts.Items.Clear();
            lbScheduleWeeks_SelectedIndexChanged(sender, e);
        }

        private void lbScheduleWeeks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbScheduleWeeks.SelectedItem != null)
            {
                lbScheduleWeekDays.Items.Clear();
                foreach (var weekDay in ((KeyValuePair<DateTime, Dictionary<DayOfWeek, List<Logic.ScheduleStuff.Task>>>)lbScheduleWeeks.SelectedItem).Value)
                {
                    lbScheduleWeekDays.Items.Add(weekDay);
                    lbScheduleWeekDays.DisplayMember = "Key";
                }
            }
            else
            {
                lbScheduleWeeks.Items.Clear();
                foreach (var week in schedule.TasksPerWeek)
                {
                    lbScheduleWeeks.Items.Add(week);
                    lbScheduleWeeks.DisplayMember = "Key";
                }
            }
        }

        private void lbScheduleWeekDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbScheduleWeekDays.SelectedItem != null && lbScheduleWeeks.SelectedItem != null)
            {

                lbScheduleTasks.Items.Clear();
                var sortedTasks = new List<Logic.ScheduleStuff.Task>();
                foreach (var task in ((KeyValuePair<DayOfWeek, List<Logic.ScheduleStuff.Task>>)lbScheduleWeekDays.SelectedItem).Value)
                {
                    sortedTasks.Add(task);
                }
                sortedTasks.Sort((task1, task2) => task1.StartDate.CompareTo(task2.StartDate));

                // Filter by WorkType, Location, and Employee
                var selectedWorkType = cbScheduleWorkType.SelectedItem.ToString();
                var selectedLocation = cbScheduleLocation.SelectedItem.ToString();

                sortedTasks = sortedTasks.Where(task =>
                    (selectedWorkType == "All" || task.WorkType.ToString() == selectedWorkType) &&
                    (selectedLocation == "All" || task.Location.Name.Contains(selectedLocation)) &&
                    task.Employees.Any(employee => employee.Name.Contains(tbScheduleEmployee.Text))
                ).ToList();

                sortedTasks.ForEach(task => lbScheduleTasks.Items.Add(task));

                lbScheduleShifts.Items.Clear();
                var sortedShifts = new List<Logic.ScheduleStuff.Task>();

                var weekKey = ((KeyValuePair<DateTime, Dictionary<DayOfWeek, List<Logic.ScheduleStuff.Task>>>)lbScheduleWeeks.SelectedItem).Key;
                var weekDayKey = ((KeyValuePair<DayOfWeek, List<Logic.ScheduleStuff.Task>>)lbScheduleWeekDays.SelectedItem).Key;
                foreach (var shift in schedule.ShiftsPerWeek.Where(pair => pair.Key.Date == weekKey.Date).First().Value[weekDayKey])
                {
                    sortedShifts.Add(shift);
                }
                sortedShifts.Sort((shift1, shift2) => shift1.StartDate.CompareTo(shift2.StartDate));

                // Filter by Employee
                sortedShifts = sortedShifts.Where(shift =>
                    shift.Employees.Any(employee => employee.Name.Contains(tbScheduleEmployee.Text))
                ).ToList();

                sortedShifts.ForEach(shift => lbScheduleShifts.Items.Add(shift));
            }
            else if (schedule != null)
            {
                lbScheduleWeekDays.Items.Clear();
                foreach (var weekDay in schedule.TasksPerWeek.First().Value)
                {
                    lbScheduleWeekDays.Items.Add(weekDay);
                    lbScheduleWeekDays.DisplayMember = "Key";
                }
            }
        }


        private void InitializeComboBoxes()
        {
            cbScheduleWorkType.Items.Clear();
            cbScheduleWorkType.Items.Add("All");
            foreach (var workType in Enum.GetValues(typeof(WorkType)))
            {
                cbScheduleWorkType.Items.Add(workType);
            }
            cbScheduleWorkType.SelectedIndex = 0;


            try
            {
                var locations = locationManager.LoadLocationFromDatabse();

                cbScheduleLocation.Items.Clear();
                cbScheduleLocation.Items.Add("All");
                foreach (var location in locations)
                {
                    cbScheduleLocation.Items.Add(location.Name);
                }
                cbScheduleLocation.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                tsslblMainPageForm.Text = "Error loading locations: " + ex.Message;
            }
        }


        private void lbScheduleTasks_DoubleClick(object sender, EventArgs e)
        {
            if (lbScheduleTasks.SelectedItem != null)
            {
                Logic.ScheduleStuff.Task selectedTask = lbScheduleTasks.SelectedItem as Logic.ScheduleStuff.Task;

                // Open the TasksForm with the selected task's information
                using TasksForm addTaskForm = new TasksForm(taskManager, locationManager, employeeManager, selectedTask, false, true, scheduleManager, schedule);
                if (addTaskForm.ShowDialog() == DialogResult.OK)
                {
                    // Update displayed Schedule 
                    schedule = scheduleManager.GetSchedule();
                    lbScheduleWeeks.SelectedItem = null;
                    lbScheduleWeekDays.SelectedItem = null;
                    lbScheduleTasks.SelectedItem = null;
                    lbScheduleShifts.SelectedItem = null;
                    lbScheduleWeekDays.Items.Clear();
                    lbScheduleTasks.Items.Clear();
                    lbScheduleShifts.Items.Clear();
                }
            }
        }

        private void lbScheduleShifts_DoubleClick(object sender, EventArgs e)
        {
            // Ran out of time, more important things to fix

            //if (lbScheduleShifts.SelectedItem != null)
            //{
            //    Logic.ScheduleStuff.Task selectedShift = lbScheduleShifts.SelectedItem as Logic.ScheduleStuff.Task;

            //    // Open the TasksForm with the selected task's information
            //    using TasksForm addTaskForm = new TasksForm(taskManager, locationManager, employeeManager, selectedShift, false, true, scheduleManager, schedule);
            //    addTaskForm.ShowDialog();

            //    // Update displayed Schedule 
            //    schedule = scheduleManager.GetSchedule();
            //    lbScheduleWeeks.SelectedItem = null;
            //    lbScheduleWeekDays.SelectedItem = null;
            //    lbScheduleTasks.SelectedItem = null;
            //    lbScheduleShifts.SelectedItem = null;
            //    lbScheduleWeekDays.Items.Clear();
            //    lbScheduleTasks.Items.Clear();
            //    lbScheduleShifts.Items.Clear();
            //}
        }

        private void btnSaveSchedule_Click(object sender, EventArgs e)
        {
            var result = scheduleManager.SaveScheduleTasks();
            tsslblMainPageForm.Text = result.Message;
        }
        #endregion

        private void TimerErrors_Tick(object sender, EventArgs e)
        {
            tsslblMainPageForm.Text = string.Empty;
            timerErrors.Stop();
        }

        private void tsslblMainPageForm_TextChanged(object sender, EventArgs e)
        {
            timerErrors.Start();
        }
    }
}
