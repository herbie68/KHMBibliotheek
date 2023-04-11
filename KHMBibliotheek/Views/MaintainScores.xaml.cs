using System.Linq;
using Microsoft.Win32;

namespace KHMBibliotheek.Views;
/// <summary>
/// Interaction logic for MaintainScores.xaml
/// </summary>
public partial class MaintainScores : Page
{
    public ScoreViewModel? scores;

    public ScoreModel? SelectedScore;
    private string[]? files;
    private long totalSize = 0;
    private long fileSize = 0;
    private long copiedFileSize = 0;

    public MaintainScores ( )
    {
        InitializeComponent ( );
        scores = new ScoreViewModel ( );
        ScoresDataGrid.ItemsSource = scores.Scores;
        //DataContext = scores;
    }

    #region Save Changes
    private void BtnSaveClick ( object sender, RoutedEventArgs e )
    {

    }
    #endregion

    #region Changed status of a checkbox
    private void CheckBoxChanged ( object sender, RoutedEventArgs e )
    {

    }
    #endregion

    #region DatePicker field changed
    private void DatePickerChanged ( object sender, SelectionChangedEventArgs e )
    {
        var propertyName = ((DatePicker)sender).Name;

        if ( SelectedScore != null )
        {
            switch ( propertyName )
            {
                case "dpDigitized":
                    DateTime _CreatedDateTime = new ();

                    // If the change event is triggered a data has been entered, this always differs if no date is in the database
                    if ( SelectedScore.DateCreatedString != "" )
                    {
                        var _selectedDateTime = SelectedScore.DateCreatedString.ToString () + " 00:00:00 AM";
                        _CreatedDateTime = DateTime.Parse ( _selectedDateTime );

                        if ( dpDigitized.SelectedDate == _CreatedDateTime )
                        { cbDigitized.IsChecked = false; }
                        else
                        { cbDigitized.IsChecked = true; }
                    }
                    else
                    {
                        // If the change event is triggered a data has been entered, this always differs if no date is in the database
                        cbDigitized.IsChecked = true;
                    }
                    break;

                case "dpModified":
                    DateTime _ModifiedDateTime = new ();

                    // If the change event is triggered a data has been entered, this always differs if no date is in the database
                    if ( SelectedScore.DateModifiedString != "" )
                    {
                        var _selectedDateTime = SelectedScore.DateModifiedString.ToString () + " 00:00:00 AM";
                        _ModifiedDateTime = DateTime.Parse ( _selectedDateTime );

                        if ( dpModified.SelectedDate == _ModifiedDateTime )
                        { cbModified.IsChecked = false; }
                        else
                        { cbModified.IsChecked = true; }
                    }
                    else
                    {
                        // If the change event is triggered a data has been entered, this always differs if no date is in the database
                        cbModified.IsChecked = true;
                    }
                    break;
            }
        }
        //CheckChanged ( );
    }
    #endregion

    #region Clicked: GoTo Next Record
    private void BtnNextClick ( object sender, RoutedEventArgs e )
    {
        if ( ScoresDataGrid.SelectedIndex + 1 < ScoresDataGrid.Items.Count )
        {
            ScoresDataGrid.SelectedIndex += 1;
        }
        else
        {
            ScoresDataGrid.SelectedIndex = 0;
        }

        // Scroll to the item in the GridView
        ScoresDataGrid.ScrollIntoView ( ScoresDataGrid.Items [ ScoresDataGrid.SelectedIndex ] );
    }
    #endregion

    #region Clicked: Goto Previous Record
    private void BtnPreviousClick ( object sender, RoutedEventArgs e )
    {
        if ( ScoresDataGrid.SelectedIndex > 0 )
        {
            ScoresDataGrid.SelectedIndex -= 1;
        }
        else
        {
            ScoresDataGrid.SelectedIndex = ScoresDataGrid.Items.Count - 1;
        }

        // Scroll to the item in the GridView
        ScoresDataGrid.ScrollIntoView ( ScoresDataGrid.Items [ ScoresDataGrid.SelectedIndex ] );
    }
    #endregion

    #region Clicked: GoTo Last Record
    private void BtnLastClick ( object sender, RoutedEventArgs e )
    {
        ScoresDataGrid.SelectedIndex = ScoresDataGrid.Items.Count - 1;

        // Scroll to the item in the GridView
        ScoresDataGrid.ScrollIntoView ( ScoresDataGrid.Items [ ScoresDataGrid.SelectedIndex ] );
    }
    #endregion

    #region Clicked: GoTo First Record
    private void BtnFirstClick ( object sender, RoutedEventArgs e )
    {
        ScoresDataGrid.SelectedIndex = 0;

        // Scroll to the item in the GridView
        ScoresDataGrid.ScrollIntoView ( ScoresDataGrid.Items [ ScoresDataGrid.SelectedIndex ] );
    }
    #endregion

    #region Create a cover sheet document
    private void CreateCoverSheet ( object sender, RoutedEventArgs e )
    {

    }
    #endregion

    #region Clear content of the Search box
    private void btnClearSearch_Click ( object sender, RoutedEventArgs e )
    {
        tbSearch.Text = "";
        ScoresDataGrid.ItemsSource = scores.Scores;
    }
    #endregion

    #region Filter DataGrid on content of Search box
    private void tbSearch_TextChanged ( object sender, TextChangedEventArgs e )
    {
        var search = sender as TextBox;

        if ( search.Text.Length > 1 )
        {
            if ( !string.IsNullOrEmpty ( search.Text ) )
            {
                var filteredList = scores.Scores.Where(x=> x.SearchField.ToLower().Contains(tbSearch.Text.ToLower()));
                ScoresDataGrid.ItemsSource = filteredList;
            }
            else
            {
                ScoresDataGrid.ItemsSource = scores.Scores;
            }
        }
    }
    #endregion

    #region Selected row in DataGrid changed
    private void SelectedScoreChanged ( object sender, SelectionChangedEventArgs e )
    {
        DataGrid dg = (DataGrid)sender;

        ScoreModel selectedRow = (ScoreModel)dg.SelectedItem;

        if ( selectedRow == null )
        {
            object item = dg.Items[0];
            dg.SelectedItem = item;
            selectedRow = ( ScoreModel ) dg.SelectedItem;

            // Scroll to the item in the DataGrid
            dg.ScrollIntoView ( dg.Items [ 0 ] );
        }

        #region Score Number and Title
        tbScoreNumber.Text = selectedRow.Score;
        tbTitle.Text = selectedRow.ScoreTitle;
        #endregion

        #region Digitized Section
        dpDigitized.Text = selectedRow.DateCreatedString;
        dpModified.Text = selectedRow.DateModifiedString;

        if ( selectedRow.Checked )
        { chkChecked.IsChecked = true; }
        else
        { chkChecked.IsChecked = false; }
        #endregion

        #region MuseScore check boxes
        chkMSCORP.IsChecked = selectedRow.MuseScoreORP;
        chkMSCORK.IsChecked = selectedRow.MuseScoreORK;
        chkMSCTOP.IsChecked = selectedRow.MuseScoreTOP;
        chkMSCTOK.IsChecked = selectedRow.MuseScoreTOK;
        #endregion

        #region PDF check boxes
        chkPDFORP.IsChecked = selectedRow.PDFORP;
        chkPDFORK.IsChecked = selectedRow.PDFORK;
        chkPDFTOP.IsChecked = selectedRow.PDFTOP;
        chkPDFTOK.IsChecked = selectedRow.PDFTOK;
        #endregion

        #region MP3 check boxes
        chkMP3B1.IsChecked = selectedRow.MP3B1;
        chkMP3B2.IsChecked = selectedRow.MP3B2;
        chkMP3T1.IsChecked = selectedRow.MP3T1;
        chkMP3T2.IsChecked = selectedRow.MP3T2;

        chkMP3SOL.IsChecked = selectedRow.MP3SOL;
        chkMP3TOT.IsChecked = selectedRow.MP3TOT;
        chkMP3PIA.IsChecked = selectedRow.MP3PIA;
        #endregion

        #region MuseScore Online checkbox
        chkMSCOnline.IsChecked = selectedRow.MuseScoreOnline;
        #endregion

        SelectedScore = selectedRow;
        DataContext = selectedRow;
    }
    #endregion

    #region Select Files Button
    private void BtnSelectFiles ( object sender, RoutedEventArgs e )
    {
        OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = true };
        openFileDialog.Filter = "Muziek bestanden (MSCZ, MSCX,PDF,MP3)|*.MSC?;*.PDF;*.MP3";
        bool? response = openFileDialog.ShowDialog();
        if ( response == true )
        {
            //Get Selected Files
            files = openFileDialog.FileNames;
            CheckFiles ( files );

            CalculateTotalFilesize ( files );
            //if ( !worker.IsBusy )
            //{ worker.RunWorkerAsync ( ); }
        }
    }
    #endregion

    #region Calculate total file size
    private void CalculateTotalFilesize ( string [ ] files )
    {
        foreach ( var file in files )
        {
            long fileSize = new System.IO.FileInfo(file).Length;
            totalSize += fileSize;
        }

        //progressBar.Maximum = totalSize;
    }
    #endregion
}
