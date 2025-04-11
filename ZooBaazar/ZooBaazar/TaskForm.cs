using Data_Access;
using Logic;
using Logic.ScheduleStuff;
using System.Data;

namespace ZooBaazar
{
    public partial class TasksForm : Form
    {
        public Logic.ScheduleStuff.Task NewTask { get; private set; }
        private TaskManager _taskManager;
        private Logic.ScheduleStuff.Task? existingTask;
        private LocationManager locationManager;
        private EmployeeManager employeeManager;
        private ScheduleManager? scheduleManager;
        private Schedule? schedule = null;



        public TasksForm(TaskManager taskManager, LocationManager manager, EmployeeManager empManager, Logic.ScheduleStuff.Task? task = null, bool view = false, bool update = false, ScheduleManager? sm = null, Schedule? schedule = null)
        {
            InitializeComponent();
            _taskManager = taskManager;
            locationManager = manager;
            employeeManager = empManager;
            InitializeComboBoxes();
            InitializeLocationsComboBox();
            existingTask = task;

            if (existingTask != null)
            {
                FillFormWithTaskDetails(existingTask, employeeManager);
            }

            if (view)
            {
                tbTitle.Enabled = false;
                tbDescriptionTasksForm.Enabled = false;
                cbFunction.Enabled = false;
                cbRepeatTasksForm.Enabled = false;
                nudNumberOfRepeatsTasks.Enabled = false;
                dpStartTasksEmployee.Enabled = false;
                nudDurationTasks.Enabled = false;
                cbLocationTasksForm.Enabled = false;
                cbDayTask.Enabled = false;
                cbNightTask.Enabled = false;
                btnAddTasksForm.Enabled = false;
            }
            else if (update)
            {
                lblEmployeeTasks.Visible = true;
                cbEmployee.Visible = true;
                cbLocationTasksForm.Enabled = false;
                cbFunction.Enabled = false;

                lblRepeatTasksForm.Visible = false;
                nudNumberOfRepeatsTasks.Visible = false;
                cbRepeatTasksForm.Visible = false;
                cbDayTask.Visible = false;
                cbNightTask.Visible = false;
            }

            this.scheduleManager = sm;
            this.schedule = schedule;
        }

        private void FillFormWithTaskDetails(Logic.ScheduleStuff.Task task, EmployeeManager em)
        {
            tbTitle.Text = task.Name;
            tbDescriptionTasksForm.Text = task.Description;
            dpStartTasksEmployee.Value = task.StartDate;
            if (task.EndDate.Day != task.StartDate.Day)
            {
                nudDurationTasks.Value = 24 - task.StartDate.Hour;
                nudDurationTasks.Value += task.EndDate.Hour;
            }
            else
            {
                nudDurationTasks.Value = task.EndDate.Hour - task.StartDate.Hour;
            }
            cbRepeatTasksForm.SelectedItem = task.RepeatType;
            nudNumberOfRepeatsTasks.Value = task.NumberOfRepeats;
            cbFunction.SelectedItem = task.WorkType;

            if (existingTask.Employees.Count() > 0)
            {
                cbEmployee.Items.AddRange(employeeManager.LoadEmployeesFromDataBase().ToArray());
                foreach (Employee employee in cbEmployee.Items)
                {
                    if (employee.EmployeeID == existingTask.Employees.First().EmployeeID)
                    {
                        cbEmployee.SelectedItem = employee;
                    }
                }
            }

            if (!task.RepresentsShift)
            {
                foreach (Location location in cbLocationTasksForm.Items)
                {
                    if (location.Name == task.Location.Name)
                    {
                        cbLocationTasksForm.SelectedItem = location;
                    }
                }
            }

            if (existingTask.DayTask) cbDayTask.Checked = true;
            if (existingTask.NightTask) cbNightTask.Checked = true;

        }

        private void BtnAddTask_Click(object sender, EventArgs e)
        {
            // Update
            if (existingTask != null)
            {
                // This will validate input data
                if (ValidateTaskInput())
                {
                    Logic.ScheduleStuff.Task newTask = new Logic.ScheduleStuff.Task(
                        existingTask.Id,
                        tbTitle.Text,
                        tbDescriptionTasksForm.Text,
                        (WorkType)cbFunction.SelectedItem,
                        (RepeatEnum)cbRepeatTasksForm.SelectedItem,
                        (int)nudNumberOfRepeatsTasks.Value,
                        dpStartTasksEmployee.Value,
                        dpStartTasksEmployee.Value.AddHours((int)nudDurationTasks.Value),
                        cbDayTask.Checked,
                        cbNightTask.Checked,
                        (Location)cbLocationTasksForm.SelectedItem
                    );

                    // Update Task
                    if (schedule != null)
                    {
                        // Rushed solution
                        if (cbEmployee.SelectedItem == null)
                        {
                            tsslblTaskForm.Text = "Task needs an Employee";
                            return;
                        }
                        newTask.Employees.Add(cbEmployee.SelectedItem as Employee);
                        newTask.RepresentsShift = existingTask.RepresentsShift;
                        newTask.ScheduleTask = existingTask.ScheduleTask;

                        scheduleManager.UpdateTask(newTask);

                    }
                    else
                    {
                        _taskManager.Update(newTask);
                    }

                    // Close the form and return DialogResult.OK
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            // Create
            else
            {
                if (ValidateTaskInput())
                {
                    Logic.ScheduleStuff.Task newTask = new Logic.ScheduleStuff.Task(
                        tbTitle.Text,
                        tbDescriptionTasksForm.Text,
                        (WorkType)cbFunction.SelectedItem,
                        (RepeatEnum)cbRepeatTasksForm.SelectedItem,
                        (int)nudNumberOfRepeatsTasks.Value,
                        dpStartTasksEmployee.Value,
                        dpStartTasksEmployee.Value.AddHours((int)nudDurationTasks.Value),
                        cbDayTask.Checked,
                        cbNightTask.Checked,
                        (Location)cbLocationTasksForm.SelectedItem
                    );

                    // Add Task
                    _taskManager.Add(newTask);

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void InitializeLocationsComboBox()
        {
            // Uses the LocationManager instance to populate CbLocations
            cbLocationTasksForm.Items.Clear();
            cbLocationTasksForm.Items.AddRange(locationManager.LoadLocationFromDatabse().ToArray());
            cbLocationTasksForm.DisplayMember = "Name";
        }

        private bool ValidateTaskInput()
        {
            // This will check if the title is provided
            if (string.IsNullOrEmpty(tbTitle.Text))
            {
                tsslblTaskForm.Text = "Every Task needs a title. Please create one. Validation Error";
                return false;
            }

            // This will check if a function is selected
            if (cbFunction.SelectedItem == null)
            {
                tsslblTaskForm.Text = "Every Task needs a Function. Please select one. Validation Error";
                return false;
            }

            // This will check if a location is selected
            if (cbLocationTasksForm.SelectedItem == null)
            {
                tsslblTaskForm.Text = "Every Task needs a Location. Please select one. Validation Error";
                return false;
            }

            // This will check if at least one checkbox between Day and Night Task is checked
            if (!cbDayTask.Checked && !cbNightTask.Checked)
            {
                tsslblTaskForm.Text = "At least one most be selected between Day and Night Task";
                return false;
            }

            return true;
        }

        private void InitializeComboBoxes()
        {
            cbFunction.DataSource = Enum.GetValues(typeof(WorkType));

            cbRepeatTasksForm.DataSource = Enum.GetValues(typeof(RepeatEnum));
        }

        private void TasksForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Owner != null)
            {
                Owner.Visible = true;
            }
        }
    }

}
