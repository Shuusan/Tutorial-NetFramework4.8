using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using windows_form_app_tutorial.HelperViews;

namespace windows_form_app_tutorial.Views
{

    /// <summary>
    /// New : Shuusan - 2023/03/25
    /// </summary>
    public partial class frmBindingNavigator : Form
    {

        ///////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////

        #region INITIAL FUNCTION

        /// <summary>
        /// form binding
        /// </summary>
        public frmBindingNavigator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialization Firebase
        /// </summary>
        /// <returns></returns>
        private FirestoreDb InitializeFirestore()
        {
            string projectId = "shuusan-tutorial";

            string jsonFileName = "credential.json";
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, jsonFileName);

            GoogleCredential credential = GoogleCredential.FromFile(jsonPath);
            FirestoreDbBuilder builder = new FirestoreDbBuilder
            {
                ProjectId = projectId,
                Credential = credential
            };

            FirestoreDb firestore = builder.Build();

            return firestore;
        }


        /// <summary>
        /// Load Binding Navigator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BindingNavigator_Load(object sender, EventArgs e)
        {
            await FetchDataAsync();

            // Set the ID column to read-only
            dataGridView1.Columns["id"].ReadOnly = true;

            // Fill all
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set the HeaderText property for each column
            dataGridView1.Columns["id"].HeaderText = "ID";
            dataGridView1.Columns["first_name"].HeaderText = "FIRST NAME";
            dataGridView1.Columns["last_name"].HeaderText = "LAST NAME";

            dataGridView1.ColumnHeadersVisible = true;

        }

        #endregion

        ///////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////

        #region CRUD FUNCTION : Firebase Firestore


        /// <summary>
        /// CRUD function : READ FUNCTION
        /// Fetch data from firebase to tabel using binding source
        /// </summary>
        /// <returns></returns>
        private async Task FetchDataAsync()
        {

            // FIREBASE SECTION BEGIN
            FirestoreDb firestore = InitializeFirestore();
            CollectionReference collection = firestore.Collection("NetFramework").Document("BindingNavigator").Collection("Employees");

            QuerySnapshot snapshot = await collection.GetSnapshotAsync();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id");
            dataTable.Columns.Add("first_name");
            dataTable.Columns.Add("last_name");

            foreach (DocumentSnapshot document in snapshot)
            {
                if (document.Exists)
                {
                    Dictionary<string, object> data = document.ToDictionary();
                    DataRow row = dataTable.NewRow();
                    row["id"] = data["id"];
                    row["first_name"] = data["first_name"];
                    row["last_name"] = data["last_name"];

                    dataTable.Rows.Add(row);
                }
            }

            // FIREBASE SECTION END


            // Binding the data to bindingSource, then attach to dataGrid and Binding Navigator
            bindingSource1.DataSource = dataTable;
            dataGridView1.DataSource = bindingSource1;
            bindingNavigator1.BindingSource = bindingSource1;
        }

        /// <summary>
        /// CRUD function : ADD FUNCTION
        /// </summary>
        /// <param name="newRow"></param>
        /// <returns></returns>
        private async Task AddEmployeeToFirestoreAsync(DataRowView newRow)
        {
            FirestoreDb firestore = InitializeFirestore();
            CollectionReference collection = firestore.Collection("NetFramework").Document("BindingNavigator").Collection("Employees");


            // Handle null values in the first_name and last_name fields
            string firstName = newRow["first_name"] == DBNull.Value ? "" : newRow["first_name"].ToString();
            string lastName = newRow["last_name"] == DBNull.Value ? "" : newRow["last_name"].ToString();

            Dictionary<string, object> newEmployee = new Dictionary<string, object>
            {
                { "first_name", firstName },
                { "last_name", lastName }
            };

            DocumentReference addedDocRef = await collection.AddAsync(newEmployee);

            // Update the ID in the DataRowView with the auto-generated ID from Firestore
            newRow["id"] = addedDocRef.Id;

            // Update the ID field in Firestore
            Dictionary<string, object> updateData = new Dictionary<string, object>
            {
                { "id", addedDocRef.Id }
            };
            await addedDocRef.UpdateAsync(updateData);
        }

        /// <summary>
        /// CRUD function : DELETE FUNCTION
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        private async Task DeleteEmployeeFromFirestoreAsync(string employeeId)
        {
            FirestoreDb firestore = InitializeFirestore();
            CollectionReference collection = firestore.Collection("NetFramework").Document("BindingNavigator").Collection("Employees");

            if (string.IsNullOrEmpty(employeeId))
            {
                // Data not exist, delete the view only
                // Already handled in delete click
                return;
            }

            DocumentReference employeeDocRef = collection.Document(employeeId);
            await employeeDocRef.DeleteAsync();
        }

        /// <summary>
        /// CRUD function : UPDATE FUNCTION
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        private async Task UpdateEmployeeInFirestoreAsync(DataRow updatedRow)
        {
            FirestoreDb firestore = InitializeFirestore();
            CollectionReference collection = firestore.Collection("NetFramework").Document("BindingNavigator").Collection("Employees");

            string employeeId = updatedRow["id"].ToString();
            DocumentReference employeeDocRef = collection.Document(employeeId);

            Dictionary<string, object> updatedEmployee = new Dictionary<string, object>
            {
                { "first_name", updatedRow["first_name"] },
                { "last_name", updatedRow["last_name"] }
            };

            await employeeDocRef.UpdateAsync(updatedEmployee);
        }

        #endregion

        ///////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////

        #region UI INTERACTION EVENT; CALLBACK FUNCTION


        /// <summary>
        /// Event when delete button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void btnDeleteItem_Click(object sender, EventArgs e)
        {
            await LoadingPopup.ShowDialogAsync(this, async () =>
            {
                if (bindingSource1.Current != null)
                {
                    DataRowView selectedRow = (DataRowView)bindingSource1.Current;
                    string employeeId = selectedRow["id"].ToString();
                    await DeleteEmployeeFromFirestoreAsync(employeeId);

                    // Remove the item from the DataGridView
                    // Remove using binding source
                    bindingSource1.RemoveCurrent();
                }
            });
        }


        /// <summary>
        /// Event when save button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSave_Click(object sender, EventArgs e)
        {

            //remove focus
            bindingSource1.EndEdit();
            dataGridView1.EndEdit();


            await LoadingPopup.ShowDialogAsync(this, async () =>
            {
                // Loop through all rows in the DataGridView
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Do not include header and the last empty list, remove 2 rows
                    if (row.Index < dataGridView1.Rows.Count - 1)
                    {
                        DataRowView dataRowView = (DataRowView)row.DataBoundItem;
                        DataRow dataRow = dataRowView.Row;

                        // skip when first and last name is null
                        if (string.IsNullOrEmpty(dataRow["first_name"].ToString()) && string.IsNullOrEmpty(dataRow["last_name"].ToString()))
                        {
                            continue;
                        }

                       
                        string employeeId = dataRow["id"].ToString();

                        if (string.IsNullOrEmpty(employeeId))
                        {
                            // Add new employee to Firestore
                            await AddEmployeeToFirestoreAsync(dataRowView);
                        }
                        else
                        {
                            // Update the existing employee in Firestore
                            await UpdateEmployeeInFirestoreAsync(dataRow);
                        }
                    }
                }
            });

            // Show a popup message after saving the data
            MessageBox.Show("Data berhasil disimpan!", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

    }
}
