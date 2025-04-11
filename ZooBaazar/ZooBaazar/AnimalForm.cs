using Data_Access;
using Logic;
using System.Data;


namespace ZooBaazar
{
    public partial class AnimalForm : Form
    {
        public Animal newAnimal { get; private set; }
        private Animal existingAnimal;
        private AnimalManager animalManager;
        private RelationshipManager relationshipManager;
        private FeedingPlanManager feedingPlanManager;
        private MedicalRecordManager mediicalRecordManager;
        private LocationManager locationManager;
        private Location existingLocation;


        public AnimalForm(AnimalManager am, RelationshipManager rm, FeedingPlanManager fm, MedicalRecordManager mm, LocationManager manager, Animal animal = null, bool view = false)
        {
            animalManager = am;
            relationshipManager = rm;
            feedingPlanManager = fm;
            mediicalRecordManager = mm;
            locationManager = manager;
            InitializeComponent();
            //for the datagridview
            SetupDataGridViewRelations();
            PopulateRelations();
            //For the Form
            PopulateComboBoxes();
            PopulateSpecies();
            PopulateCheckedListBoxes();
            PopulateLocationComboBox();

            existingAnimal = animal;

            if (existingAnimal != null)
            {
                // No time
                // plRelationship.Visible = true;
                existingLocation = locationManager.LoadLocationFromDatabse().Find(l => l.Animals.Find(animal => animal.AnimalID == existingAnimal.AnimalID) != null);
                FillFormWithAnimalDetails(existingAnimal, existingLocation);
            }
            else
            {
                existingLocation = null;
            }

            if (view)
            {
                //This is for the main animal manager
                tbNameAnimalForm.Enabled = false;
                dtpEntryDate.Enabled = false;
                dtpExitDate.Enabled = false;
                cbSpecies.Enabled = false;
                tbOrigin.Enabled = false;
                cbLocationAnimalForm.Enabled = false;
                cbBloodType.Enabled = false;
                btnSaveAnimalForm.Enabled = false;
                dtpBirthday.Enabled = false;
                btnNewLocation.Enabled = false;

                //This is for the FeedingPlan
                tbFavoriteFood.Enabled = false;
                tbDislikedFood.Enabled = false;
                cbDiet.Enabled = false;
                clbFoodAllergies.Enabled = false;
                tbNotesFeeding.Enabled = false;
                tbCaloriesIntake.Enabled = false;
                tbWeight.Enabled = false;
                tbIdealWeight.Enabled = false;

                //this is for medical records
                tbMedicalAllergies.Enabled = false;
                tbDiseases.Enabled = false;
                tbInjuries.Enabled = false;
                tbOperations.Enabled = false;
                tbNotesMedical.Enabled = false;
                tbMedication.Enabled = false;
                cbDangerlvl.Enabled = false;

                //This is for the relationships
                tbSearchRelations.Enabled = false;
                clbAnimals.Enabled = false;
                tbRelations.Enabled = false;
                btnAddRelation.Enabled = false;
                tbSearch.Enabled = false;
                btnDeleteRelation.Enabled = false;
                dgvAnimalRelations.Enabled = false;
            }
        }

        private void PopulateSpecies()
        {
            cbSpecies.Items.Clear();
            cbSpecies.DataSource = animalManager.GetAllSpecies();
        }

        private void btnBackAnimalForm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSaveAnimalForm_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                int locationID = ((Location)cbLocationAnimalForm.SelectedItem).Id;

                if (existingAnimal == null)
                {
                    FeedingPlan newfeedingPlan = CreateFeedingPlan();

                    MedicalRecord newmedicalRecord = CreateMedicalRecord();

                    Relationship newrelationship = CreateRelationship(null);

                    Animal newAnimal = existingAnimal == null ?
                     //comment

                     new Animal(
                           tbNameAnimalForm.Text,
                           cbSpecies.Text,
                           (BloodTypes)cbBloodType.SelectedItem,
                           tbOrigin.Text,
                           newrelationship,
                           dtpBirthday.Value,
                           dtpEntryDate.Value,
                           dtpExitDate.Value,
                           newfeedingPlan,
                           newmedicalRecord
                     ) :

                    new Animal(
                            existingAnimal.AnimalID,
                            tbNameAnimalForm.Text,
                            cbSpecies.Text,
                            (BloodTypes)cbBloodType.SelectedItem,
                            tbOrigin.Text,
                            newrelationship,
                            dtpBirthday.Value,
                            dtpEntryDate.Value,
                            dtpExitDate.Value,
                            newfeedingPlan,
                            newmedicalRecord
                    );

                    Result result = animalManager.Update(newAnimal, locationID, newfeedingPlan, newmedicalRecord);

                    if (result.Success)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        tsslblAnimalForm.Text = result.Message;
                    }

                }
                else if (existingAnimal != null)
                {
                    FeedingPlan feedingPlan = GetUpdatedFeedingPlan();

                    MedicalRecord medicalRecord = GetUpdatedMedicalRecord();

                    Relationship relationship = GetUpdatedRelationship();

                    Animal newAnimal = existingAnimal == null ?

                     new Animal(
                           tbNameAnimalForm.Text,
                           cbSpecies.Text,
                           (BloodTypes)cbBloodType.SelectedItem,
                           tbOrigin.Text,
                           relationship,
                           dtpBirthday.Value,
                           dtpEntryDate.Value,
                           dtpExitDate.Value,
                           feedingPlan,
                           medicalRecord
                     ) :

                    new Animal(
                            existingAnimal.AnimalID,
                            tbNameAnimalForm.Text,
                            cbSpecies.Text,
                            (BloodTypes)cbBloodType.SelectedItem,
                            tbOrigin.Text,
                            relationship,
                            dtpBirthday.Value,
                            dtpEntryDate.Value,
                            dtpExitDate.Value,
                            feedingPlan,
                            medicalRecord
                    );

                    Result result = animalManager.Update(newAnimal, existingLocation.Id, feedingPlan, medicalRecord);

                    if (result.Success)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        tsslblAnimalForm.Text = result.Message;
                    }
                }
            }

        }

        private void btnAddRelation_Click(object sender, EventArgs e)
        {
            if (ValidateRelations())
            {
                List<int> selectedAnimalIDs = new List<int>();

                foreach (var item in clbAnimals.CheckedItems)
                {
                    selectedAnimalIDs.Add(((Animal)item).AnimalID);
                }

                if (selectedAnimalIDs.Count > 0)
                {
                    foreach (int secondaryAnimalID in selectedAnimalIDs)
                    {
                        Relationship newRelationship = new Relationship(
                            existingAnimal.AnimalID,
                            tbRelations.Text,
                            secondaryAnimalID
                        );

                        Result result;

                        // Check if the relationship already exists to determine if we should add or update
                        if (newRelationship.RelationShipID == 0)
                        {
                            result = relationshipManager.Add(newRelationship);
                        }
                        else
                        {
                            result = relationshipManager.Update(newRelationship);
                        }

                        if (!result.Success)
                        {
                            tsslblAnimalForm.Text = result.Message;
                            return; // Exit the method if an error occurs
                        }
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    tsslblAnimalForm.Text = "Please select at least one animal for the relationship.";
                }
            }
        }


        private void btnDeleteRelation_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dgvAnimalRelations.SelectedRows.Count > 0)
            {
                // Get the selected Relation
                Relationship selectedRelationship = dgvAnimalRelations.SelectedRows[0].DataBoundItem as Relationship;

                DialogResult result = MessageBox.Show("Are you sure you want to delete this RelationShip?", "Delete RelationShip", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    // Remove the selected relationship from the RelationshipManager
                    relationshipManager.Remove(selectedRelationship);

                    PopulateRelations();
                }
            }
            else
            {
                tsslblAnimalForm.Text = "Please select an Relationship to delete.";
            }
        }


        public void FillFormWithAnimalDetails(Animal animal, Location location)
        {
            tbNameAnimalForm.Text = animal.Name;
            cbSpecies.Text = animal.Species;
            foreach (Location cbLocation in cbLocationAnimalForm.Items)
            {
                if (cbLocation.Name == location.Name)
                {
                    cbLocationAnimalForm.SelectedItem = cbLocation;
                }
            }
            cbBloodType.SelectedItem = animal.BloodType;
            tbOrigin.Text = animal.Origin;
            dtpEntryDate.Value = animal.EntryZoo;
            dtpExitDate.Value = animal.ExitZoo;


            FeedingPlan feedingPlan = animal.FeedingPlan;
            if (feedingPlan != null)
            {
                tbFavoriteFood.Text = feedingPlan.FavoriteFood;
                tbDislikedFood.Text = feedingPlan.DislikedFood;
                cbDiet.SelectedItem = feedingPlan.Diet;
                tbNotesFeeding.Text = feedingPlan.Notes;
                tbCaloriesIntake.Text = feedingPlan.CaloriesIntake.ToString();
                tbWeight.Text = feedingPlan.Weight.ToString();
                tbIdealWeight.Text = feedingPlan.IdealWeight.ToString();


                foreach (var foodAllergie in feedingPlan.FoodAllergies)
                {
                    int index = clbFoodAllergies.Items.IndexOf(foodAllergie.ToString());
                    if (index != -1)
                    {
                        clbFoodAllergies.SetItemChecked(index, true);
                    }
                }

            }

            MedicalRecord medicalRecord = animal.MedicalRecord;
            if (medicalRecord != null)
            {
                tbMedicalAllergies.Text = medicalRecord.Allergies;
                tbDiseases.Text = medicalRecord.Diseases;
                tbInjuries.Text = medicalRecord.Injuries;
                tbOperations.Text = medicalRecord.Operations;
                tbNotesMedical.Text = medicalRecord.Notes;
                tbMedication.Text = medicalRecord.Medication;
                cbDangerlvl.SelectedItem = medicalRecord.Dangerlevel;
            }

            Relationship relationship = animal.Relationship;
            if (relationship != null)
            {
                foreach (var secondaryAnimal in animalManager.LoadAnimalFromDataBase())
                {
                    int index = clbAnimals.Items.IndexOf(secondaryAnimal.ToString());
                    if (index != -1)
                    {
                        clbAnimals.SetItemChecked(index, true);
                    }
                }
            }
        }

        public Relationship CreateRelationship(Animal primaryAnimal)
        {
            if (!string.IsNullOrEmpty(tbNameAnimalForm.Text) && !string.IsNullOrEmpty(tbRelations.Text) && clbAnimals.SelectedItem != null)
            {
                try
                {
                    int relationShipID = 0;

                    // PrimaryAnimal should be the ID or some identifier of the animal being edited or added
                    int primaryAnimalId = primaryAnimal.AnimalID;

                    // Read the RelationType from tbRelations
                    string relationType = tbRelations.Text;

                    // SecondaryAnimal should be the ID of the animal selected in clbAnimals
                    int secondaryAnimalId = ((Animal)clbAnimals.SelectedItem).AnimalID;

                    // Create and return an instance of the Relationship class
                    return new Relationship(relationShipID, primaryAnimalId, relationType, secondaryAnimalId);
                }
                catch (Exception ex)
                {
                    tsslblAnimalForm.Text = "An error occurred: " + ex.Message;
                }
                return null;
            }
            else if (string.IsNullOrEmpty(tbNameAnimalForm.Text) && string.IsNullOrEmpty(tbRelations.Text) && clbAnimals.SelectedItem == null)
            {
                ValidateRelations();
            }
            return null;
        }


        public FeedingPlan CreateFeedingPlan()
        {
            try
            {
                // Gather feedingplan details from form controls
                int FeedingPlanid = 0;
                string FavoriteFood = tbFavoriteFood.Text;
                string DislikedFood = tbDislikedFood.Text;
                DietEnum Diet = (DietEnum)cbDiet.SelectedItem;
                string Notes = tbNotesFeeding.Text;
                double CaloriesIntake = double.Parse(tbCaloriesIntake.Text);
                double Weight = double.Parse(tbWeight.Text);
                double IdealWeight = double.Parse(tbIdealWeight.Text);

                // Convert selected items in CheckedListBox to FoodAllergies enum values
                List<FoodAllergies> foodAllergies = clbFoodAllergies.CheckedItems.Cast<string>()
                                                .Select(item => (FoodAllergies)Enum.Parse(typeof(FoodAllergies), item))
                                                .ToList();

                // Create and return an instance of the FeedingPlan class
                return new FeedingPlan(FavoriteFood, DislikedFood, Diet, foodAllergies, Notes, CaloriesIntake, Weight, IdealWeight);
            }
            catch (Exception ex)
            {
                tsslblAnimalForm.Text = "An error occurred: " + ex.Message;
            }
            return null;
        }

        public MedicalRecord CreateMedicalRecord()
        {
            try
            {
                // Gather feedingplan details from form controls
                int medicalRecordid = 0;
                DateTime recordDate = DateTime.Now;
                string Allergies = tbMedicalAllergies.Text;
                string Diseases = tbDiseases.Text;
                string Injuries = tbInjuries.Text;
                string Operations = tbOperations.Text;
                string Notes = tbNotesMedical.Text;
                string Medication = tbMedication.Text;
                DangerEnum Dangerlvl = (DangerEnum)cbDangerlvl.SelectedItem;

                // Create and return an instance of the MedicalRecord class
                return new MedicalRecord(recordDate, Notes, Allergies, Diseases, Injuries, Operations, Medication, Dangerlvl);
            }
            catch (Exception ex)
            {
                tsslblAnimalForm.Text = "An error occurred: " + ex.Message;
            }
            return null;
        }

        public FeedingPlan GetUpdatedFeedingPlan()
        {
            if (ValidateInput())
            {

                List<FoodAllergies> selectedAllergies = new List<FoodAllergies>();
                foreach (var item in clbFoodAllergies.CheckedItems)
                {
                    if (Enum.TryParse(item.ToString(), out FoodAllergies allergy))
                    {
                        selectedAllergies.Add(allergy);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Invalid food allergy: {item}");
                    }
                }

                feedingPlanManager.Update(
                      new FeedingPlan(
                          existingAnimal.FeedingPlan.FeedingPlanID,
                          existingAnimal.AnimalID,
                          tbFavoriteFood.Text,
                          tbDislikedFood.Text,
                          (DietEnum)cbDiet.SelectedItem,
                          selectedAllergies,
                          tbNotesFeeding.Text,
                          Double.Parse(tbCaloriesIntake.Text),
                          Double.Parse(tbWeight.Text),
                          Double.Parse(tbIdealWeight.Text)
                          )
                    );

                return new FeedingPlan(
                          existingAnimal.FeedingPlan.FeedingPlanID,
                          existingAnimal.AnimalID,
                          tbFavoriteFood.Text,
                          tbDislikedFood.Text,
                          (DietEnum)cbDiet.SelectedItem,
                          selectedAllergies,
                          tbNotesFeeding.Text,
                          Double.Parse(tbCaloriesIntake.Text),
                          Double.Parse(tbWeight.Text),
                          Double.Parse(tbIdealWeight.Text)
                          );
            }
            else
            {
                return null;
            }
        }

        public MedicalRecord GetUpdatedMedicalRecord()
        {
            if (ValidateInput())
            {
                mediicalRecordManager.Update(
                      new MedicalRecord(
                           existingAnimal.MedicalRecord.MedicalRecordID,
                           existingAnimal.AnimalID,
                           existingAnimal.MedicalRecord.RecordDate = DateTime.Now,
                           tbNotesMedical.Text,
                           tbMedicalAllergies.Text,
                           tbDiseases.Text,
                           tbInjuries.Text,
                           tbOperations.Text,
                           tbMedication.Text,
                           (DangerEnum)cbDangerlvl.SelectedItem
                          )
                    );

                return new MedicalRecord(
                           existingAnimal.MedicalRecord.MedicalRecordID,
                           existingAnimal.AnimalID,
                           existingAnimal.MedicalRecord.RecordDate = DateTime.Now,
                           tbNotesMedical.Text,
                           tbMedicalAllergies.Text,
                           tbDiseases.Text,
                           tbInjuries.Text,
                           tbOperations.Text,
                           tbMedication.Text,
                           (DangerEnum)cbDangerlvl.SelectedItem
                          );
            }
            else
            {
                return null;
            }
        }

        public Relationship GetUpdatedRelationship()
        {
            if (ValidateInput() && existingAnimal.Relationship != null)
            {
                // Initialize with a default value
                int secondaryAnimalID = 0;
                foreach (var item in clbAnimals.CheckedItems)
                {
                    secondaryAnimalID = ((Animal)item).AnimalID;
                }

                int selectedPrimaryAnimalID = existingAnimal.AnimalID;

                relationshipManager.Update(
                     new Relationship(
                         existingAnimal.Relationship.RelationShipID,
                         selectedPrimaryAnimalID,
                         tbRelations.Text,
                         secondaryAnimalID
                     )
                 );

                return new Relationship(
                    existingAnimal.Relationship.RelationShipID,
                    selectedPrimaryAnimalID,
                    tbRelations.Text,
                    secondaryAnimalID
                );
            }
            else
            {
                return null;
            }
        }



        public Animal GetNewAnimal()
        {
            if (ValidateInput())
            {
                FeedingPlan feedingPlan = CreateFeedingPlan();
                MedicalRecord medicalRecord = CreateMedicalRecord();

                Animal newAnimal = new Animal(
                    tbNameAnimalForm.Text,
                    cbSpecies.Text,
                    (BloodTypes)cbBloodType.SelectedItem,
                    tbOrigin.Text,
                    null, // Temporarily set to null
                    dtpBirthday.Value,
                    dtpEntryDate.Value,
                    dtpExitDate.Value,
                    feedingPlan,
                    medicalRecord
                );

                // Create the relationship after the animal is instantiated
                Relationship relationship = CreateRelationship(newAnimal);
                newAnimal.Relationship = relationship;

                return newAnimal;
            }
            else
            {
                // Return null if validation fails
                return null;
            }
        }

        public int GetLocationID()
        {
            int locationID = locationManager.GetLocationID(cbLocationAnimalForm.Text);
            // Implement this method to return the location ID from the form
            return locationID;
        }

        public Animal GetUpdatedAnimal()
        {
            if (ValidateInput())
            {
                Relationship relationship = GetUpdatedRelationship();

                FeedingPlan feedingPlan = GetUpdatedFeedingPlan();

                MedicalRecord medicalRecord = GetUpdatedMedicalRecord();

                animalManager.Update(
                     new Animal(
                       existingAnimal.AnimalID,
                       tbNameAnimalForm.Text,
                       cbSpecies.Text,
                       (BloodTypes)cbBloodType.SelectedItem,
                       tbOrigin.Text,
                       relationship,
                       dtpBirthday.Value,
                       dtpEntryDate.Value,
                       dtpExitDate.Value,
                       feedingPlan,
                       medicalRecord
                       ), existingLocation.Id,
                     feedingPlan,
                     medicalRecord
                    );

                return new Animal(
                           existingAnimal.AnimalID,
                           tbNameAnimalForm.Text,
                           cbSpecies.Text,
                           (BloodTypes)cbBloodType.SelectedItem,
                           tbOrigin.Text,
                           relationship,
                           dtpBirthday.Value,
                           dtpEntryDate.Value,
                           dtpExitDate.Value,
                           feedingPlan,
                           medicalRecord
                       );

            }
            else
            {
                return null;
            }
        }

        private bool ValidateRelations()
        {
            if (string.IsNullOrEmpty(tbNameAnimalForm.Text))
            {
                tsslblAnimalForm.Text = $"Please input a name of the Animal";
                return false;
            }

            if (string.IsNullOrEmpty(tbRelations.Text))
            {
                tsslblAnimalForm.Text = $"Please input a type of relation between these animals";
                return false;
            }

            if (clbAnimals.SelectedItem == null)
            {
                tsslblAnimalForm.Text = $"Please select an animal that is related with the Primary animal";
                return false;
            }
            // If all validations pass, return true
            return true;
        }

        private bool ValidateInput()
        {
            // Check if any of the required fields are empty
            if (string.IsNullOrEmpty(tbNameAnimalForm.Text))
            {
                tsslblAnimalForm.Text = "Please enter the name of the animal.";
                return false;
            }

            if (string.IsNullOrEmpty(cbSpecies.Text))
            {
                tsslblAnimalForm.Text = "Please enter the species of the animal.";
                return false;
            }

            if (cbDangerlvl.SelectedItem == null)
            {
                tsslblAnimalForm.Text = "Please select the danger level.";
                return false;
            }

            if (cbLocationAnimalForm.SelectedItem == null)
            {
                tsslblAnimalForm.Text = "Please select the location of the animal.";
                return false;
            }

            if (cbBloodType.SelectedItem == null)
            {
                tsslblAnimalForm.Text = "Please select the blood type of the animal.";
                return false;
            }

            if (dtpEntryDate.Value >= dtpExitDate.Value)
            {
                tsslblAnimalForm.Text = "Exit date must be after entry date.";
                return false;
            }
            if (cbDiet.SelectedItem.Equals(-1))
            {
                tsslblAnimalForm.Text = "The Diet cannot be negative. Please try again";
                return false;
            }

            if (double.Parse(tbCaloriesIntake.Text).Equals(0))
            {
                tsslblAnimalForm.Text = "The Calories intake cannot be negative. Please try again";
                return false;
            }

            if (double.Parse(tbWeight.Text).Equals(0))
            {
                tsslblAnimalForm.Text = "The Weight cannot be negative. Please try again";
                return false;
            }

            if (double.Parse(tbIdealWeight.Text).Equals(0))
            {
                tsslblAnimalForm.Text = "The Idael weight cannot be negative. Please try again";
                return false;
            }

            // If all validations pass, return true
            return true;
        }

        //for the datagrid view
        private void SetupDataGridViewRelations()
        {
            // Set DataGridView properties
            dgvAnimalRelations.AutoGenerateColumns = false;
            dgvAnimalRelations.AllowUserToAddRows = false;

            // Add DataGridView columns
            dgvAnimalRelations.Columns.Add("PrimaryAnimal", "PrimaryAnimal");
            dgvAnimalRelations.Columns["PrimaryAnimal"].DataPropertyName = "PrimaryAnimalName";

            dgvAnimalRelations.Columns.Add("RelationType", "RelationType");
            dgvAnimalRelations.Columns["RelationType"].DataPropertyName = "RelationshipType";

            dgvAnimalRelations.Columns.Add("SecondaryAnimal", "SecondaryAnimal");
            dgvAnimalRelations.Columns["SecondaryAnimal"].DataPropertyName = "SecondaryAnimalName";

            foreach (DataGridViewColumn column in dgvAnimalRelations.Columns)
            {
                column.Visible = true;
            }
            dgvAnimalRelations.DefaultCellStyle.ForeColor = Color.Black;
            dgvAnimalRelations.DefaultCellStyle.BackColor = Color.White;
            dgvAnimalRelations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAnimalRelations.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            PopulateRelations();
        }

        private void PopulateRelations()
        {
            try
            {
                List<Relationship> relationList = relationshipManager.LoadAnimalRelationshipsFromDatabase();
                List<AnimalRelationshipDetails> animalRelationDetailsList = new List<AnimalRelationshipDetails>();

                foreach (var relation in relationList)
                {
                    var primaryAnimal = animalManager.GetAnimalByAnimalID(relation.PrimaryAnimalID);
                    var secondaryAnimal = animalManager.GetAnimalByAnimalID(relation.SecondaryAnimalID);

                    animalRelationDetailsList.Add(new AnimalRelationshipDetails
                    {
                        PrimaryAnimalName = primaryAnimal.Name,
                        RelationshipType = relation.RelationType,
                        SecondaryAnimalName = secondaryAnimal.Name
                    });
                }

                dgvAnimalRelations.DataSource = animalRelationDetailsList;
            }
            catch (Exception ex)
            {
                tsslblAnimalForm.Text = ex.Message;
            }
        }

        private void PopulateComboBoxes()
        {
            // Populate cmbRole
            cbBloodType.DataSource = Enum.GetValues(typeof(BloodTypes));
            //Populate danger
            cbDangerlvl.DataSource = Enum.GetValues(typeof(DangerEnum));
            //Populate diet
            cbDiet.DataSource = Enum.GetValues(typeof(DietEnum));
        }

        private void PopulateLocationComboBox()
        {
            // Use the LocationManager instance to populate cmbLocations
            cbLocationAnimalForm.Items.Clear();
            cbLocationAnimalForm.Items.AddRange(locationManager.LoadLocationFromDatabse().ToArray());
            cbLocationAnimalForm.DisplayMember = "Name";

        }
        private void PopulateCheckedListBoxes()
        {
            // Convert FoodAlergies enum values to string representations
            var allergiesName = Enum.GetNames(typeof(FoodAllergies));

            // Add the string representations to the CheckedListBox
            clbFoodAllergies.Items.AddRange(allergiesName);

            foreach (var animal in animalManager.LoadAnimalFromDataBase())
            {
                clbAnimals.Items.Add(animal);
            }
        }

        private void AnimalManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Owner != null)
            {
                Owner.Visible = true;
            }
        }

        private void btnNewLocation_Click(object sender, EventArgs e)
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
                        // Add the new Location to the LocationManager
                        locationManager.Add(newLocation);

                        // Update the ListBox
                        cbLocationAnimalForm.DataSource = locationManager.LoadLocationFromDatabse().ToArray();
                    }
                }
            }

        }

    }
}
