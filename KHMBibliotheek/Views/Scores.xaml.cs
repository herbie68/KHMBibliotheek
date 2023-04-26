using System.Linq;

namespace KHMBibliotheek.Views;
/// <summary>
/// Interaction logic for Scores.xaml
/// </summary>
public partial class Scores : Page
{
    public MusicFilesViewModel? scores;
    public MusicFilesModel? SelectedScore;
    public bool orgMSCORP, orgMSCORK, orgMSCTOP, orgMSCTOK, orgPDFORP, orgPDFORK, orgPDFTOP, orgPDFTOK, orgPDFPIA;
    public bool orgMP3B1, orgMP3B2, orgMP3T1, orgMP3T2, orgMP3SOL, orgMP3TOT, orgMP3UITV, orgMP3PIA;
    public bool Changed;
    public bool changedPDFORP, changedPDFORK, changedPDFTOP, changedPDFTOK, changedPDFPIA;
    public bool changedMSCORP, changedMSCORK, changedMSCTOP, changedMSCTOK, changedOnline;
    public bool changedMP3B1, changedMP3B2, changedMP3T1, changedMP3T2, changedMP3SOL, changedMP3TOT, changedMP3PIA, changedMP3UITV;
    public int MSCORPFileId, MSSCORKFileId, MSCTOPFileId, MSCTOKFileId, PDFORPFileId, PDFORKFileId, PDFTOPFileId, PDFTOKFileId, PDFPIAFileId;
    public int MP3B1FileId, MP3B2FileId, MP3T1FileId, MP3T2FileId, MP3SOLFileId, MP3TOTFileId, MP3UITVFileId, MP3PIAFileId;

    public Scores ( )
    {
        InitializeComponent ( );
        scores = new MusicFilesViewModel ( );
        ScoresDataGrid.ItemsSource = scores.Scores;
        var _screenHeight = Application.Current.MainWindow.ActualHeight - 70 - 30 - 10 - 26 - 80;
        ScoresDataGrid.Height = _screenHeight;
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
                var filteredList = scores.Scores.Where(x => x.SearchField.ToLower().Contains(tbSearch.Text.ToLower()));
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

        MusicFilesModel selectedRow = (MusicFilesModel)dg.SelectedItem;

        if ( selectedRow == null )
        {
            object item = dg.Items[0];
            dg.SelectedItem = item;
            selectedRow = ( MusicFilesModel ) dg.SelectedItem;

            // Scroll to the item in the DataGrid
            dg.ScrollIntoView ( dg.Items [ 0 ] );
        }

        #region Score Number and Title
        tbScoreNumber.Text = selectedRow.ScoreNumber;
        tbTitle.Text = selectedRow.ScoreTitle;
        #endregion

        #region MuseScore check boxes
        chkMSCORP.IsChecked = selectedRow.MSCORP == 1 ? true : false;
        chkMSCORK.IsChecked = selectedRow.MSCORK == 1 ? true : false;
        chkMSCTOP.IsChecked = selectedRow.MSCTOP == 1 ? true : false;
        chkMSCTOK.IsChecked = selectedRow.MSCTOK == 1 ? true : false;
        #endregion

        #region PDF check boxes
        chkPDFORP.IsChecked = selectedRow.PDFORP == 1 ? true : false;
        chkPDFORK.IsChecked = selectedRow.PDFORK == 1 ? true : false;
        chkPDFTOP.IsChecked = selectedRow.PDFTOP == 1 ? true : false;
        chkPDFTOK.IsChecked = selectedRow.PDFTOK == 1 ? true : false;
        chkPDFPIA.IsChecked = selectedRow.PDFPIA == 1 ? true : false;
        #endregion

        #region MP3 check boxes
        chkMP3B1.IsChecked = selectedRow.MP3B1 == 1 ? true : false;
        chkMP3B2.IsChecked = selectedRow.MP3B2 == 1 ? true : false;
        chkMP3T1.IsChecked = selectedRow.MP3T1 == 1 ? true : false;
        chkMP3T2.IsChecked = selectedRow.MP3T2 == 1 ? true : false;

        chkMP3SOL.IsChecked = selectedRow.MP3SOL == 1 ? true : false;
        chkMP3TOT.IsChecked = selectedRow.MP3TOT == 1 ? true : false;
        chkMP3UITV.IsChecked = selectedRow.MP3UITV == 1 ? true : false;
        chkMP3PIA.IsChecked = selectedRow.MP3PIA == 1 ? true : false;
        #endregion

        SelectedScore = selectedRow;
        DataContext = selectedRow;
    }
    #endregion

    private void chkMusicFiles_Changed ( object sender, RoutedEventArgs e )
    {
        // Check the change of the Checkboxes, to enable/disable the save button

    }

    private void btnDownloadClick ( object sender, RoutedEventArgs e )
    {
        // Download the file of the selected row/column
    }

    private void btnViewClick ( object sender, RoutedEventArgs e )
    {
        // View the PDF file in the selected row/column
    }

    private void btnPlayClick ( object sender, RoutedEventArgs e )
    {
        // Play the MP3 file in the selected row/column
    }
}
