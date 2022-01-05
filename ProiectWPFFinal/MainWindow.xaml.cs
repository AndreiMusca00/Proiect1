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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using AtelierAutoModel;
using System.Data;

namespace ProiectWPFFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    enum ActionState {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        ActionState actionProgramari = ActionState.Nothing;
        ActionState actionFacturi = ActionState.Nothing;
        AtelierAutoEntitiesModel ctx = new AtelierAutoEntitiesModel();
        CollectionViewSource clientiVsource;
        CollectionViewSource reviewsVsource;
        CollectionViewSource specializariVsource;
        CollectionViewSource programariVsource;
        CollectionViewSource facturiVsource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnNextProg.Background = Brushes.Silver;
            btnPrevProg.Background = Brushes.Silver;
            btnSaveProg.Background = Brushes.Silver;
            btnCancelProg.Background = Brushes.Silver;
            btnEditProg.Background = Brushes.Silver;
            btnNewProg.Background = Brushes.Silver;
            btnDeleteProg.Background = Brushes.Silver;
            
            clientiVsource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientiViewSource")));
            clientiVsource.Source = ctx.clientis.Local;
            ctx.clientis.Load();

            reviewsVsource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("reviewViewSource")));
            reviewsVsource.Source = ctx.reviews.Local;
            ctx.reviews.Load();

            specializariVsource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("specializari_mecaniciViewSource")));
            specializariVsource.Source = ctx.specializari_mecanici.Local;
            ctx.specializari_mecanici.Load();

            programariVsource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientiprogramarisViewSource")));
            programariVsource.Source = ctx.programaris.Local;
            ctx.programaris.Load();

            facturiVsource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientifacturisViewSource")));
            facturiVsource.Source = ctx.facturis.Local;
            ctx.facturis.Load();


            cmdClienti.ItemsSource = ctx.clientis.Local;
            cmdClienti.SelectedItem = "id_client";

            cmbSpecializare.ItemsSource = ctx.specializari_mecanici.Local;
            cmbSpecializare.DisplayMemberPath = "denumire";
            cmbSpecializare.SelectedValuePath = "id_specializare";

            
        }
       
        
      
        
        /*Tab Programari 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */
        private void SalvareProgramari()
        {
            programari programare = null;
            if (actionProgramari == ActionState.New)
            {
                try
                {
                    clienti client = (clienti)cmdClienti.SelectedItem;
                    specializari_mecanici specializare = (specializari_mecanici)cmbSpecializare.SelectedItem;
                    
                    var ultimaProgramare = ctx.programaris.Max(s=>s.id_programare);
                    
                    programare = new programari()
                    {
                        data = dataDatePicker.SelectedDate.Value,
                        id_client = client.id_client,
                        id_specializare = specializare.id_specializare,
                        id_programare = ultimaProgramare+1
                    };
                    ctx.programaris.Add(programare);
                    ctx.SaveChanges();
                    MessageBox.Show("Inserare cu succes!");
                    
                }catch(Exception ex) 
                {
                    MessageBox.Show("Eroare la inserare!");
                }
            }
            else if(actionProgramari==ActionState.Edit){
                dynamic programareSelectata = programarisDataGrid.SelectedItem;
                int curr_id = programareSelectata.id_programare;
                var programareEditata = ctx.programaris.FirstOrDefault(s => s.id_programare == curr_id);
                clienti client = (clienti)cmdClienti.SelectedItem;
                specializari_mecanici spec = (specializari_mecanici)cmbSpecializare.SelectedItem;
                
                try
                {
                    if (programareEditata != null)
                    {
                        programareEditata.data = dataDatePicker.SelectedDate.Value;
                        programareEditata.id_client = client.id_client;
                        programareEditata.id_specializare = spec.id_specializare;
                        ctx.SaveChanges();
                        programariVsource.View.Refresh();
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show("Eroare la editare!");
                }
                
                programariVsource.View.MoveCurrentTo(programareSelectata);
            }
            else if (actionProgramari==ActionState.Delete)
            {
                try
                {
                    dynamic programareSelectata = programarisDataGrid.SelectedItem;
                    int curr_id = programareSelectata.id_programare;
                    var deleteProgramare = ctx.programaris.FirstOrDefault(s => s.id_programare == curr_id);
                    if (deleteProgramare != null)
                    {
                        ctx.programaris.Remove(deleteProgramare);
                        ctx.SaveChanges();
                        MessageBox.Show("Programare stearsa cu succes!");
                      
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare la stergere!");
                }

            }
            
        }
        private void btnSaveProg_Click(object sender, RoutedEventArgs e)
        {
            SalvareProgramari();
            actionProgramari = ActionState.Nothing;
            btnDeleteProg.Background = Brushes.Silver;
            btnNewProg.Background = Brushes.Silver;
            btnEditProg.Background = Brushes.Silver;
        }
        private void btnDeleteProg_Click(object sender, RoutedEventArgs e)
        {
            actionProgramari = ActionState.Delete;
            btnEditProg.Background = Brushes.Silver;
            btnNewProg.Background = Brushes.Silver;
            btnDeleteProg.Background = Brushes.Red;

        }
        private void btnNextProg_Click(object sender, RoutedEventArgs e)
        {
            programariVsource.View.MoveCurrentToNext();
            
        }

        private void btnPrevProg_Click(object sender, RoutedEventArgs e)
        {
            programariVsource.View.MoveCurrentToPrevious();
        }

        private void btnNewProg_Click(object sender, RoutedEventArgs e)
        {
            actionProgramari = ActionState.New;
            btnEditProg.Background = Brushes.Silver;
            btnNewProg.Background = Brushes.Red;
            btnDeleteProg.Background = Brushes.Silver;
        }

        private void btnEditProg_Click(object sender, RoutedEventArgs e)
        {
            actionProgramari = ActionState.Edit;
            btnEditProg.Background = Brushes.Red;
            btnNewProg.Background = Brushes.Silver;
            btnDeleteProg.Background = Brushes.Silver;
        }

        private void btnCancelProg_Click(object sender, RoutedEventArgs e)
        {
            actionProgramari = ActionState.Nothing;
            btnNextProg.Background = Brushes.Silver;
            btnPrevProg.Background = Brushes.Silver;
            btnSaveProg.Background = Brushes.Silver;
            btnCancelProg.Background = Brushes.Silver;
            btnEditProg.Background = Brushes.Silver;
            btnNewProg.Background = Brushes.Silver;
            btnDeleteProg.Background = Brushes.Silver;
        }
        /*Facturi
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
        */
        private void SalvareFacturi()
        {
            facturi Factura = null;
            if (actionFacturi == ActionState.New)
            {
                try
                {
                    Factura = new facturi()
                    {
                        id_client=Convert.ToInt32(id_clientTextBoxFacturi.Text),
                        id_factura=Convert.ToInt32(id_facturaTextBoxFacturi.Text),
                        valoare=Convert.ToDouble(valoareTextBoxFacturi.Text),
                        descriere=descriereTextBoxFacturi.Text.Trim(),
                        data=Convert.ToDateTime(dataDatePickerFacturi.Text),
                    };
                    ctx.facturis.Add(Factura);

                    facturiVsource.View.Refresh();
                    ctx.SaveChanges();

                }
                catch(DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }else if (actionFacturi == ActionState.Edit)
            {
                try
                {
                    facturi fact = (facturi)facturisDataGrid.SelectedItem;
                    fact.id_client = Convert.ToInt32(id_clientTextBoxFacturi.Text);
                    fact.valoare = Convert.ToDouble(valoareTextBoxFacturi.Text);
                    fact.descriere = descriereTextBoxFacturi.Text.Trim();
                    fact.data = Convert.ToDateTime(dataDatePickerFacturi.Text);
                    ctx.SaveChanges();
                }
                catch(DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if(actionFacturi==ActionState.Delete){
                try
                {
                    dynamic facturaSelectata = facturisDataGrid.SelectedItem;
                    int curr_id = facturaSelectata.id_factura;
                    var deleteFactura = ctx.facturis.FirstOrDefault(s => s.id_factura == curr_id);
                    if (deleteFactura != null)
                    {
                        ctx.facturis.Remove(deleteFactura);
                        
                        ctx.SaveChanges();
                        MessageBox.Show("Programare stearsa cu succes!");
                    }
                }
                catch(Exception ex)
                {

                }
            }
        }
        private void btnFacturaNoua_Click(object sender, RoutedEventArgs e)
        {
            actionFacturi = ActionState.New;
        }

        private void btnEditFactura_Click(object sender, RoutedEventArgs e)
        {
            actionFacturi = ActionState.Edit;
            id_facturaTextBoxFacturi.IsEnabled = false;
        }

        private void btnStergeFactura_Click(object sender, RoutedEventArgs e)
        {
            actionFacturi = ActionState.Delete;
            id_facturaTextBoxFacturi.IsEnabled = false;
        }

        private void btnSaveFactura_Click(object sender, RoutedEventArgs e)
        {
            SalvareFacturi();
        }

        private void btnCancelFactura_Click(object sender, RoutedEventArgs e)
        {
            actionFacturi = ActionState.Nothing;
            facturiVsource.View.MoveCurrentToFirst();
        }
        private void btnPrevFact_Click(object sender, RoutedEventArgs e)
        {
            facturiVsource.View.MoveCurrentToPrevious();
        }

        private void btnNextFact_Click(object sender, RoutedEventArgs e)
        {
            facturiVsource.View.MoveCurrentToNext();
        }


        /*Tab Clienti
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            ActionDisplay.Content = action;
            //Validare
            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(mailTextBox,TextBox.TextProperty);
            SetValidationBinding();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            ActionDisplay.Content = action;
            //validare
            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(mailTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            SetValidationBinding();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            ActionDisplay.Content = action;

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            clientiVsource.View.MoveCurrentToNext();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            clientiVsource.View.MoveCurrentToPrevious();
        }

        private void SaveClienti()
        {
            clienti client = null;
            if (action == ActionState.New)
            {
                try
                {
                    var ClientNou = ctx.clientis.Max(s=>s.id_client);
                    client = new clienti()
                    {
                        
                        id_client=ClientNou+1,
                        nume = numeTextBox.Text.Trim(),
                        prenume=prenumeTextBox.Text.Trim(),
                        mail=mailTextBox.Text.Trim(),
                        adresa=adresaTextBox.Text.Trim(),
                        telefon=telefonTextBox.Text.Trim()
                    };
                    
                    ctx.clientis.Add(client);
                    clientiVsource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Date Incorecte!");
                }
            }
            else
                if (action == ActionState.Edit)
            {
                try
                {
                    client = (clienti)clientiDataGrid.SelectedItem;
                    client.nume = numeTextBox.Text.Trim();
                    client.prenume = prenumeTextBox.Text.Trim();
                    client.mail = mailTextBox.Text.Trim();
                    client.adresa = adresaTextBox.Text.Trim();
                    client.telefon = telefonTextBox.Text.Trim();
                    ctx.SaveChanges();
                    

                }catch(Exception ex)
                {
                    MessageBox.Show("Date incorecte!");
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    client = (clienti)clientiDataGrid.SelectedItem;
                    ctx.clientis.Remove(client);
                    ctx.SaveChanges();
                }catch(Exception ex)
                {
                    MessageBox.Show("Selecteaza un client!");
                }
                clientiVsource.View.Refresh();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveClienti();
            action = ActionState.Nothing;
            ActionDisplay.Content = action;
            
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            ActionDisplay.Content = action;
            
        }
        /*Tab Review 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */

        private void btnNextReview_Click(object sender, RoutedEventArgs e)
        {
            reviewsVsource.View.MoveCurrentToNext();
            review rev = new review();
            rev = (review)reviewDataGrid.SelectedItem;

            try
            {
                lblReview.Content = "Review-ul "+rev.id_review;
                textReview.Text = rev.review1;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Nu mai derula in fata!");
            }
        }

        private void btnPrevReview_Click(object sender, RoutedEventArgs e)
        {
            reviewsVsource.View.MoveCurrentToPrevious();
            review rev = new review();
            rev = (review)reviewDataGrid.SelectedItem;
            try
            {
                textReview.Text = rev.review1;
                lblReview.Content = "Review-ul " + rev.id_review;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Nu mai derula inapoi!");
            }
            
        }
        /*Tab Specializari Mecanici 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */
        private void btnDeleteSpecializare_Click(object sender, RoutedEventArgs e)
        {
            specializari_mecanici spec = new specializari_mecanici();
            try
            {
                spec = (specializari_mecanici)specializari_mecaniciListView.SelectedItem;
                ctx.specializari_mecanici.Remove(spec);
                ctx.SaveChanges();
                specializariVsource.View.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Selectati o specializare!");
            }
        }

        private void btnAddSpecializare_Click(object sender, RoutedEventArgs e)
        {
            lblFormAdaugSpec.Visibility = Visibility.Visible;
            lblDenAdaugSpec.Visibility = Visibility.Visible;
            lblIdAdaugSpec.Visibility = Visibility.Visible;
            txtAdaugSpecId.Visibility = Visibility.Visible;
            txtAdaugSpecDen.Visibility = Visibility.Visible;
            btnSaveSpec.Visibility = Visibility.Visible;
            btnCancelSpec.Visibility = Visibility.Visible;


            var idSpec = ctx.specializari_mecanici.Max(s => s.id_specializare);
            int var = idSpec + 1;
            txtAdaugSpecId.Text = Convert.ToString(var);
            txtAdaugSpecId.IsEnabled = false;
            
        }

        private void btnSaveSpec_Click(object sender, RoutedEventArgs e)
        {
            specializari_mecanici SpecNoua = new specializari_mecanici();
            if (txtAdaugSpecId.Text != null & txtAdaugSpecDen.Text != null)
            {
                try
                {
                    SpecNoua.denumire = txtAdaugSpecDen.Text;
                    SpecNoua.id_specializare = Convert.ToInt32(this.txtAdaugSpecId.Text);

                    ctx.specializari_mecanici.Add(SpecNoua);
                    ctx.SaveChanges();
                    specializariVsource.View.Refresh();
                    lblFormAdaugSpec.Visibility = Visibility.Hidden;
                    lblDenAdaugSpec.Visibility = Visibility.Hidden;
                    lblIdAdaugSpec.Visibility = Visibility.Hidden;
                    txtAdaugSpecId.Visibility = Visibility.Hidden;
                    txtAdaugSpecDen.Visibility = Visibility.Hidden;
                    btnSaveSpec.Visibility = Visibility.Hidden;
                    btnCancelSpec.Visibility = Visibility.Hidden;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Date Incorecte!");
                }
            }
            
        }

        private void btnCancelSpec_Click(object sender, RoutedEventArgs e)
        {
            lblFormAdaugSpec.Visibility = Visibility.Hidden;
            lblDenAdaugSpec.Visibility = Visibility.Hidden;
            lblIdAdaugSpec.Visibility = Visibility.Hidden;
            txtAdaugSpecId.Visibility = Visibility.Hidden;
            txtAdaugSpecDen.Visibility = Visibility.Hidden;
            btnSaveSpec.Visibility = Visibility.Hidden;
            btnCancelSpec.Visibility = Visibility.Hidden;
        }

       //Validation binding 
       private void SetValidationBinding()
        {
            Binding numeValidationBinding = new Binding();
            numeValidationBinding.Source = clientiVsource;
            numeValidationBinding.Path = new PropertyPath("nume");
            numeValidationBinding.NotifyOnValidationError = true;
            numeValidationBinding.Mode = BindingMode.TwoWay;
            numeValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            numeValidationBinding.ValidationRules.Add(new StringMinLengthValidator());
            numeTextBox.SetBinding(TextBox.TextProperty,numeValidationBinding);

            Binding prenumeValidationBinding = new Binding();
            prenumeValidationBinding.Source = clientiVsource;
            prenumeValidationBinding.Path = new PropertyPath("prenume");
            prenumeValidationBinding.NotifyOnValidationError = true;
            prenumeValidationBinding.Mode = BindingMode.TwoWay;
            prenumeValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            prenumeValidationBinding.ValidationRules.Add(new FirstCapital());
            prenumeTextBox.SetBinding(TextBox.TextProperty, prenumeValidationBinding);

            Binding mailValidationBinding = new Binding();
            mailValidationBinding.Source = clientiVsource;
            mailValidationBinding.Path = new PropertyPath("mail");
            mailValidationBinding.NotifyOnValidationError = true;
            mailValidationBinding.Mode = BindingMode.TwoWay;
            mailValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            mailValidationBinding.ValidationRules.Add(new EmailValidator());
            mailTextBox.SetBinding(TextBox.TextProperty,mailValidationBinding);

        }
    }
}
