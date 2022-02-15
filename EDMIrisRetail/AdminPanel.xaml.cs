using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Employees;
using EDMIrisRetail.Controller;
using EDMIrisRetail.Model;
using EDMIrisRetail.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EDMIrisRetail
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        List<Contractor> contractors = new List<Contractor>();

        List<Employee> employees = new List<Employee>();

        List<Employee> employeesTwo = new List<Employee>();

        List<Diadoc.Api.Proto.Departments.Department> departments = new List<Diadoc.Api.Proto.Departments.Department>();

        List<Diadoc.Api.Proto.Departments.Department> depIris = new List<Diadoc.Api.Proto.Departments.Department>();

        List<Counteragent> CounteragentList = new List<Counteragent>();

        List<UserIris> Irises = new List<UserIris>();

        List<DepartmentIris> GetDepartments = new List<DepartmentIris>();

        List<DocumentIrisIn> GetDocumentInIrises = new List<DocumentIrisIn>();

        List<DocumentIrisIn> GetDocumentInSFIrises = new List<DocumentIrisIn>();

        List<DocumentIrisIn> GetDocumentInSFCorrIrises = new List<DocumentIrisIn>();

        List<DocumentIrisOut> GetDocumentIrisOuts = new List<DocumentIrisOut>();

        //List<UserIris> userIrises = new List<UserIris>();
        //Employee employee = new Employee();

        ContractorController contractorController = new ContractorController();

        DepartmentIris departmentIris = new DepartmentIris();

        DepartmentIrisController departmentIrisController = new DepartmentIrisController();

        public string GetCurrTag = "";

        //UserIris userIris = new UserIris();

        UserIrisController userIrisController = new UserIrisController();

        //DocumentsFilterIris documentsFilterIris = new DocumentsFilterIris();

        DocumentIrisController documentIrisController = new DocumentIrisController();

        DocumentIrisController documentIrisControllerSF = new DocumentIrisController();

        DocumentIrisController documentIrisControllerSFCorrect = new DocumentIrisController();

        // DocumentFilterIrisController documentFilterIrisController = new DocumentFilterIrisController();
        ParcedDocument parced = new ParcedDocument();

        List<ParcedDocument> parcedDocuments = new List<ParcedDocument>();

        ParcedDocumentController documentController = new ParcedDocumentController();

        public AdminPanel()
        {
            InitializeComponent();


            //parcedDocuments = documentController.FillAllDoc();

            contractors = contractorController.GetContractors();

            CounteragentList = contractorController.GetCounteragentLists();

            employees = userIrisController.GetuserIris(employeesTwo);

            GetDepartments = departmentIrisController.GetDeptsIris();

           // depIris = departmentIrisController.GetDepartmentsIris();

            //Irises = userIrisController.UserIrises(employees, depIris);

            GetDocumentInIrises = documentIrisController.GetDocumentInbIris(contractors, GetDepartments);

            GetDocumentInSFIrises = documentIrisControllerSF.GetDocumentInSFbIris(CounteragentList, GetDepartments);

            GetDocumentInSFCorrIrises = documentIrisControllerSFCorrect.GetDocumentInSFCorIris(CounteragentList, GetDepartments);

            //gridContractors.ItemsSource = contractors;

            //gridEmployee.ItemsSource = Irises;

            //gridDepartments.ItemsSource = GetDepartments;


            itemOutDoc.ItemsSource = parcedDocuments;

            DataContext = this;
        }

        private void BtnOrganization_Click(object sender, RoutedEventArgs e)
        {
            gridContractors.Visibility = Visibility.Visible;
            panelContragents.Visibility = Visibility.Visible;
            panelDepart.Visibility = Visibility.Hidden;

            gridDepartments.Visibility = Visibility.Hidden;
            gridEmployee.Visibility = Visibility.Hidden;
            //BtnSearch.Visibility = Visibility.Hidden;
            //BtnSearchCancell.Visibility = Visibility.Hidden;
            tabDocuments.Visibility = Visibility.Hidden;
            tabDocumentsDB.Visibility = Visibility.Hidden;
            panelEmp.Visibility = Visibility.Hidden;

            DataContext = contractors;
        }

        private void BtnEmployee_Click(object sender, RoutedEventArgs e)
        {
            gridEmployee.Visibility = Visibility.Visible;
            panelEmp.Visibility = Visibility.Visible;
            panelDepart.Visibility = Visibility.Hidden;

            gridDepartments.Visibility = Visibility.Hidden;
            gridContractors.Visibility = Visibility.Hidden;
            //BtnSearch.Visibility = Visibility.Hidden;
            //BtnSearchCancell.Visibility = Visibility.Hidden;
            tabDocuments.Visibility = Visibility.Hidden;
            tabDocumentsDB.Visibility = Visibility.Hidden;
            panelContragents.Visibility = Visibility.Hidden;
            DataContext = Irises;
        
        }

        private void BtnDepartments_Click(object sender, RoutedEventArgs e)
        {
            gridDepartments.Visibility = Visibility.Visible;
            panelDepart.Visibility = Visibility.Visible;

            gridEmployee.Visibility = Visibility.Hidden;
            gridContractors.Visibility = Visibility.Hidden;
            //BtnSearch.Visibility = Visibility.Hidden;
            //BtnSearchCancell.Visibility = Visibility.Hidden;
            tabDocuments.Visibility = Visibility.Hidden;
            tabDocumentsDB.Visibility = Visibility.Hidden;
            panelContragents.Visibility = Visibility.Hidden;
            panelEmp.Visibility = Visibility.Hidden;

            DataContext = GetDepartments;

        }

        private void BtnDoc_Click(object sender, RoutedEventArgs e)
        {
            //BtnSearch.Visibility = Visibility.Visible;
            //BtnSearchCancell.Visibility = Visibility.Visible;
            tabDocuments.Visibility = Visibility.Visible;
            tabDocumentsDB.Visibility = Visibility.Hidden;
            panelDepart.Visibility = Visibility.Hidden;

            gridDepartments.Visibility = Visibility.Hidden;
            gridEmployee.Visibility = Visibility.Hidden;
            gridContractors.Visibility = Visibility.Hidden;
            panelContragents.Visibility = Visibility.Hidden;
            panelEmp.Visibility = Visibility.Hidden;

            itemInDoc.ItemsSource = GetDocumentInIrises.OrderByDescending(c => c.CreateDate);

            itemSFDoc.ItemsSource = GetDocumentInSFIrises.OrderByDescending(c => c.CreateDate);

            itemSFCorDoc.ItemsSource = GetDocumentInSFCorrIrises.OrderByDescending(c => c.CreateDate);

            DataContext = this;

            foreach (var item in GetDocumentInSFIrises)
            {
               // item.Title
            }
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            RunService runService = new RunService();
            runService.Show();
        }

        private void BtnInDB_Click(object sender, RoutedEventArgs e)
        {

            //BtnSearch.Visibility = Visibility.Visible;
            //BtnSearchCancell.Visibility = Visibility.Visible;
            tabDocumentsDB.Visibility = Visibility.Visible;
            tabDocuments.Visibility = Visibility.Hidden;
            panelDepart.Visibility = Visibility.Hidden;

            gridDepartments.Visibility = Visibility.Hidden;
            gridEmployee.Visibility = Visibility.Hidden;
            gridContractors.Visibility = Visibility.Hidden;
            panelContragents.Visibility = Visibility.Hidden;
            panelEmp.Visibility = Visibility.Hidden;


            DataContext = parcedDocuments;
        }
    }
}
