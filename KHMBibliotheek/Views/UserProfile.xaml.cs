using System.Windows.Forms;

namespace KHMBibliotheek.Views;

#pragma warning disable CS8629 // Nullable value type may be null.
#pragma warning disable CS8602
#pragma warning disable CS8604

/// <summary>
/// Interaction logic for UserProfile.xaml
/// </summary>
public partial class UserProfile : Page
{
    public UserProfile ( )
    {
        InitializeComponent ( );

        // Fill the text changed TextBoxes
        tbCheckFullName.Text = LibraryUsers.SelectedUserFullName;
        tbCheckEMail.Text = LibraryUsers.SelectedUserEmail;
        tbCheckPassword.Text = LibraryUsers.SelectedUserPassword;
        if ( LibraryUsers.SelectedDownloadFolderSaved )
        { tbCheckDownloadFolder.Text = LibraryUsers.SelectedDownloadFolder; }
        else
        { tbCheckDownloadFolder.Text = ""; }

        // Fill The Form fields
        tbUserName.Text = LibraryUsers.SelectedUserName;
        tbFullName.Text = LibraryUsers.SelectedUserFullName;
        tbEMail.Text = LibraryUsers.SelectedUserEmail;
        tbDownLoadFolder.Text = LibraryUsers.SelectedDownloadFolder;

        cbFullNameChanged.IsChecked = false;
        cbEMailChanged.IsChecked = false;
        cbPasswordChanged.IsChecked = false;
        if ( LibraryUsers.SelectedDownloadFolderSaved )
        {
            cbDownloadFolderChanged.IsChecked = false;
            btnSaveUserProfile.IsEnabled = false;
        }
        else
        {
            cbDownloadFolderChanged.IsChecked = true;
            btnSaveUserProfile.IsEnabled = true;
        }

        //ResetChanged ( );
    }
    private void TextBoxChanged ( object sender, TextChangedEventArgs e )
    {
        var propertyName = ((System.Windows.Controls.TextBox)sender).Name;

        switch ( propertyName )
        {
            case "tbFullName":
                if ( tbFullName.Text == tbCheckFullName.Text )
                { cbFullNameChanged.IsChecked = false; }
                else
                { cbFullNameChanged.IsChecked = true; }
                break;
            case "tbEMail":
                if ( tbEMail.Text == tbCheckEMail.Text )
                { cbEMailChanged.IsChecked = false; }
                else
                { cbEMailChanged.IsChecked = true; }
                break;
            case "tbDownLoadFolder":
                if ( tbDownLoadFolder.Text == tbCheckDownloadFolder.Text )
                { cbDownloadFolderChanged.IsChecked = false; }
                else
                { cbDownloadFolderChanged.IsChecked = true; }
                break;
        }
        CheckChanged ( );
    }
    private void PasswordChanged ( object sender, RoutedEventArgs e )
    {
        var checkPassword = Helper.HashPepperPassword(pbPassword.Password, tbUserName.Text);
        if ( checkPassword == tbCheckPassword.Text )
        { cbPasswordChanged.IsChecked = false; }
        else
        { cbPasswordChanged.IsChecked = true; }
        CheckChanged ( );
    }
    private void CheckChanged ( )
    {
        if ( cbFullNameChanged.IsChecked == true ||
            cbEMailChanged.IsChecked == true ||
            cbPasswordChanged.IsChecked == true || cbDownloadFolderChanged.IsChecked == true )
        {
            btnSaveUserProfile.IsEnabled = true;
            btnSaveUserProfile.ToolTip = "Sla de gewijzigde gegevens op";
        }
        else
        {
            btnSaveUserProfile.IsEnabled = false;
            btnSaveUserProfile.ToolTip = "Er zijn geen gegevens aangepast, opslaan niet mogelijk";
        }

    }
    public void ResetChanged ( )
    {
        cbFullNameChanged.IsChecked = false;
        cbEMailChanged.IsChecked = false;
        cbPasswordChanged.IsChecked = false;
        cbDownloadFolderChanged.IsChecked = false;
        btnSaveUserProfile.IsEnabled = false;
        btnSaveUserProfile.ToolTip = "Er zijn geen gegevens aangepast, opslaan niet mogelijk";
    }
    private void SaveUserProfileClicked ( object sender, RoutedEventArgs e )
    {
        ObservableCollection<UserModel> ModifiedUser = new();

        string _FullName = "", _EMail = "", _Password = "";

        if ( ( bool ) cbFullNameChanged.IsChecked )
        {
            _FullName = tbFullName.Text;
            LibraryUsers.SelectedUserFullName = _FullName;
        }

        if ( ( bool ) cbEMailChanged.IsChecked )
        {
            _EMail = tbEMail.Text;
            LibraryUsers.SelectedUserEmail = _EMail;
        }

        if ( ( bool ) cbPasswordChanged.IsChecked )
        {
            var checkPassword = Helper.HashPepperPassword(pbPassword.Password, tbUserName.Text);
            _Password = checkPassword;
            LibraryUsers.SelectedUserPassword = _Password;
        }

        ModifiedUser.Add ( new UserModel
        {
            UserId = LibraryUsers.SelectedUserId,
            UserName = LibraryUsers.SelectedUserName,
            UserFullName = _FullName,
            UserEmail = _EMail,
            UserPassword = _Password,
            DownloadFolder = LibraryUsers.SelectedDownloadFolder
        } );

        // Set the value that the Store Path is save to true,this way it is clear the folder is stored in the database
        LibraryUsers.SelectedDownloadFolderSaved = true;

        DBCommands.UpdateUser ( ModifiedUser );

        WriteHistory ( ModifiedUser );

        ModifyScoreUserData ( ModifiedUser );

        ResetChanged ( );

        MainWindow.ReloadMainWindow ( );
    }

    private void WriteHistory ( ObservableCollection<UserModel> modifiedUser )
    {
        DBCommands.WriteLog ( LibraryUsers.SelectedUserId, DBNames.LogUserChanged, LibraryUsers.SelectedUserFullName );

        int historyId = DBCommands.GetAddedHistoryId();

        if ( modifiedUser [ 0 ].UserFullName != "" )
        {
            DBCommands.WriteDetailLog ( historyId, DBNames.LogUserFullName, tbCheckFullName.Text, modifiedUser [ 0 ].UserFullName );
        }

        if ( modifiedUser [ 0 ].UserEmail != "" )
        {
            DBCommands.WriteDetailLog ( historyId, DBNames.LogUserEMail, tbCheckEMail.Text, modifiedUser [ 0 ].UserEmail );
        }

        if ( modifiedUser [ 0 ].UserPassword != "" )
        {
            DBCommands.WriteDetailLog ( historyId, DBNames.LogUserPassword, "", "" );
        }
    }

    private static void ModifyScoreUserData ( ObservableCollection<UserModel> modifiedUser )
    {
        if ( modifiedUser [ 0 ].UserFullName != "" )
        {
            LibraryUsers.SelectedUserFullName = modifiedUser [ 0 ].UserFullName;
        }

        if ( modifiedUser [ 0 ].UserEmail != "" )
        {
            LibraryUsers.SelectedUserEmail = modifiedUser [ 0 ].UserEmail;
        }

        if ( modifiedUser [ 0 ].UserPassword != "" )
        {
            LibraryUsers.SelectedUserPassword = modifiedUser [ 0 ].UserPassword;
        }
    }

    #region Select Folder to store files
    private void BrowseToFolder_Click ( object sender, RoutedEventArgs e )
    {
        var senderButton = sender as System.Windows.Controls.Button;

        var dialogDescription = "Selecteer de map om bestanden op te slaan";

        using ( var dialog = new FolderBrowserDialog ( ) )
        {
            dialog.InitialDirectory = @tbDownLoadFolder.Text;
            dialog.Description = dialogDescription;
            dialog.ShowNewFolderButton = true;
            dialog.UseDescriptionForTitle = true;

            if ( dialog.ShowDialog ( ) == DialogResult.OK )
            {
                if ( senderButton != null )
                {
                    tbDownLoadFolder.Text = dialog.SelectedPath;
                }
            }
        }
    }
    #endregion
}
#pragma warning restore CS8629 // Nullable value type may be null.
#pragma warning restore CS8602
#pragma warning restore CS8604
