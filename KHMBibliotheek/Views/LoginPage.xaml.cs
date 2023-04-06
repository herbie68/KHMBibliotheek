namespace KHMBibliotheek.Views;
/// <summary>
/// Interaction logic for LoginPage.xaml
/// </summary>
public partial class LoginPage : Window
{
    public LoginPage ( )
    {
        InitializeComponent ( );
    }

    #region Button Close | Restore | Minimize 
    #region Button Close
    private void BtnClose_Click ( object sender, RoutedEventArgs e )
    {
        Close ( );
    }
    #endregion

    #region Button Restore
    private void BtnRestore_Click ( object sender, RoutedEventArgs e )
    {
        if ( WindowState == WindowState.Normal )
            WindowState = WindowState.Maximized;
        else
            WindowState = WindowState.Normal;
    }
    #endregion

    #region Button Minimize
    private void BtnMinimize_Click ( object sender, RoutedEventArgs e )
    {
        WindowState = WindowState.Minimized;
    }
    #endregion
    #endregion

    #region Drag Widow
    private void Window_MouseDown ( object sender, MouseButtonEventArgs e )
    {
        if ( e.LeftButton == MouseButtonState.Pressed )
        {
            DragMove ( );
        }
    }
    #endregion

    private void BtnLogin_Click ( object sender, RoutedEventArgs e )
    {
        tbInvalidLogin.Visibility = Visibility.Collapsed;
        int UserId = DBCommands.CheckUserPassword(tbUserName.Text, tbPassword.Password);
        if ( UserId != 0 )
        {
            LibraryUsers.SelectedUserId = UserId;
            ObservableCollection<UserModel> Users = DBCommands.GetUsers ( );

            foreach ( UserModel user in Users )
            {
                if ( user.UserId == UserId )
                {
                    LibraryUsers.SelectedUserName = user.UserName;
                    LibraryUsers.SelectedUserFullName = user.UserFullName;
                    LibraryUsers.SelectedUserPassword = user.UserPassword;
                    LibraryUsers.SelectedUserEmail = user.UserEmail;
                    LibraryUsers.SelectedUserRoleId = user.UserRoleId;
                }
            }

            // Write Login to Logfile
            DBCommands.WriteLog ( UserId, DBNames.LogUserLoggedIn, $"{LibraryUsers.SelectedUserFullName} is ingelogt" );

            int ForcePasswordReset = 1;
            if ( ForcePasswordReset != 0 )
            { }


            MainWindow mainWindow = new ();
            mainWindow.Show ( );
            this.Close ( );
        }
        else
        {
            tbInvalidLogin.Visibility = Visibility.Visible;
        }
    }

    private void PressedEnterOnPassword ( object sender, KeyEventArgs e )
    {
        if ( e.Key == Key.Enter )
        {
            btnLogin.RaiseEvent ( new RoutedEventArgs ( ButtonBase.ClickEvent ) );
        }
    }
}
