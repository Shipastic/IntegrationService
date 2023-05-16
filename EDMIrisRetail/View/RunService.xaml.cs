using Diadoc.Api.Proto.Employees;
using EDMIrisRetail.Controller;
using EDMIrisRetail.Model;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace EDMIrisRetail.View
{
    /// <summary>
    /// Логика взаимодействия для RunService.xaml
    /// </summary>
    public partial class RunService : Window
    {

        WorkWithDoc workWithDoc = new WorkWithDoc();

        WorkWithDocSign workWithDocSign = new WorkWithDocSign();

        List<WorkWithDoc> workWithDocs = new List<WorkWithDoc>();

        List<WorkWithDocNew> workWithDocNews = new List<WorkWithDocNew>();
        List<WorkWithDocNew> docNews = new List<WorkWithDocNew>();

        List<WorkWithDocSign> workWithDocSigns = new List<WorkWithDocSign>();
        List<WorkWithDocSign> docSigns = new List<WorkWithDocSign>();

        WorkWithDocController workWithDocController = new WorkWithDocController();
        public RunService()
        {
            InitializeComponent();

            workWithDocs = workWithDocController.GetConnect();

            workWithDocSigns = workWithDocController.GetDocSign(docSigns);

            workWithDocNews = workWithDocController.GetDocNew(docNews);

            DocForRel.ItemsSource = workWithDocs;

            DocForRelSign.ItemsSource = workWithDocSigns;

            NewDoc.ItemsSource = workWithDocNews;

            foreach (WorkWithDoc work in workWithDocs)
            {
                countContrGet.Text = work.CountContractor.ToString();

                countEmpGet.Text = work.CountUserIris.ToString();

                countDepartGet.Text = work.CountDepartmentIris.ToString();
            }

            DataContext = this;
        }

    }
}
