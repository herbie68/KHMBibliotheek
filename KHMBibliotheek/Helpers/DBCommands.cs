#pragma warning disable CS8602
#pragma warning disable CS8604

using System.IO;

namespace KHMBibliotheek.Helpers;

public class DBCommands
{
    #region GetData
    #region GetData unsorted
    public static DataTable GetData ( string _table )
    {
        string selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table;
        MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );
        DataTable table = new();
        MySqlDataAdapter adapter = new(selectQuery, connection);
        adapter.Fill ( table );
        connection.Close ( );
        return table;
    }
    #endregion

    #region GetData Sorted
    public static DataTable GetData ( string _table, string OrderByFieldName )
    {
        string selectQuery;
        if ( OrderByFieldName.ToLower ( ) == "nosort" )
        {
            selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table;
        }
        else
        {
            selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlOrder + OrderByFieldName;
        }

        MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );
        DataTable table = new();
        MySqlDataAdapter adapter = new(selectQuery, connection);
        adapter.Fill ( table );
        connection.Close ( );
        return table;
    }
    #endregion

    #region GetData Sorted and filtered
    public static DataTable GetData ( string _table, string OrderByFieldName, string WhereFieldName, string WhereFieldValue )
    {
        string selectQuery;
        if ( OrderByFieldName.ToLower ( ) == "nosort" )
        {
            selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlWhere + WhereFieldName + " = '" + WhereFieldValue + "';";
        }
        else
        {
            selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlWhere + WhereFieldName + " = '" + WhereFieldValue + "'" + DBNames.SqlOrder + OrderByFieldName + ";";
        }

        MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );
        DataTable table = new();
        MySqlDataAdapter adapter = new(selectQuery, connection);
        adapter.Fill ( table );
        connection.Close ( );
        return table;
    }


    /// <summary>
    /// Get Score info for a score with subnumber
    /// </summary>
    /// <param name="_table"></param>
    /// <param name="OrderByFieldName"></param>
    /// <param name="WhereFieldName"></param>
    /// <param name="WhereFieldValue"></param>
    /// <param name="AndWhereFieldName"></param>
    /// <param name="AndWhereFieldValue"></param>
    /// <returns></returns>
    public static DataTable GetData ( string _table, string OrderByFieldName, string WhereFieldName, string WhereFieldValue, string AndWhereFieldName, string AndWhereFieldValue )
    {
        string selectQuery;
        if ( OrderByFieldName.ToLower ( ) == "nosort" )
        {
            selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlWhere + WhereFieldName + " = '" + WhereFieldValue + "'" + DBNames.SqlAnd + AndWhereFieldName + " = '" + AndWhereFieldValue + "';";
        }
        else
        {
            selectQuery = DBNames.SqlSelectAll + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlWhere + WhereFieldName + " = '" + WhereFieldValue + "'" + DBNames.SqlAnd + AndWhereFieldName + " = '" + AndWhereFieldValue + "'" + DBNames.SqlOrder + OrderByFieldName + ";";
        }

        MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );
        DataTable table = new();
        MySqlDataAdapter adapter = new(selectQuery, connection);
        adapter.Fill ( table );
        connection.Close ( );
        return table;
    }
    #endregion
    #endregion

    #region Get 1 Field from the database table
    public static string GetScoreField ( string _table, string _retrieveField, string _whereFieldName, string _whereFieldValue )
    {
        // No matter what type the retrieved field is, it will always be returned as a string

        string? _result = "";
        int _retrieveItem;
        _ = new
        DataTable ( );

        DataTable dataTable = GetData ( _table, "nosort", _whereFieldName, _whereFieldValue );

        switch ( _retrieveField.ToLower ( ) )
        {
            case "scoreid":
                _retrieveItem = 0;
                break;
            case "score":
                _retrieveItem = 1;
                break;
            case "scorenumber":
                _retrieveItem = 2;
                break;
            case "scoresubnumber":
                _retrieveItem = 3;
                break;
            case "scoretitle":
                _retrieveItem = 4;
                break;
            case "scoresubtitle":
                _retrieveItem = 5;
                break;
            case "composer":
                _retrieveItem = 6;
                break;
            case "textwriter":
                _retrieveItem = 7;
                break;
            case "arranger":
                _retrieveItem = 8;
                break;
            default:
                _retrieveItem = 4; //Title
                break;
        }

        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
            {
                _result = dataTable.Rows [ i ].ItemArray [ _retrieveItem ].ToString ( );
            }
        }
        return _result;
    }
    #endregion

    #region Get Title from the database table
    public static string GetScoreTitle ( string _table, string _whereFieldName, string _whereFieldValue )
    {
        string? _result = "";

        DataTable dataTable = new ( );

        dataTable = GetData ( _table, "nosort", _whereFieldName, _whereFieldValue );

        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
            {
                _result = dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( );
            }
        }
        return _result;
    }
    #endregion

    #region Get FileIndex Id for ScoreId
    public static int GetFileIndexIfFromScoreId ( int _scoreId )
    {
        int id;
        string sqlQuery = DBNames.SqlSelect + DBNames.FilesIndexFieldNameId + DBNames.SqlFrom + DBNames.Database + "." + DBNames.FilesIndexTable + DBNames.SqlWhere + DBNames.FilesIndexFieldNameScoreId + " = " + _scoreId;

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        id = ( int ) cmd.ExecuteScalar ( );

        return id;
    }
    #endregion


    #region Get Available Scores
    public static ObservableCollection<ScoreModel> GetAvailableScores ( )
    {
        ObservableCollection<ScoreModel> Scores = new();

        DataTable dataTable = DBCommands.GetData(DBNames.AvailableScoresView, DBNames.AvailableScoresFieldNameNumber);

        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
            {
                Scores.Add ( new ScoreModel
                {
                    ScoreId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
                    ScoreNumber = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( )
                } );
            }
        }

        return Scores;
    }
    #endregion

    #region Get Scores
    public static ObservableCollection<ScoreModel> GetScores ( string _table, string _orderByFieldName, string _whereFieldName, string _whereFieldValue )
    {
        ObservableCollection<ScoreModel> Scores = new();

        _ = new DataTable ( );


        DataTable dataTable;
        if ( _whereFieldName != null )
        {
            dataTable = GetData ( _table, _orderByFieldName, _whereFieldName, _whereFieldValue );
        }
        else
        {
            dataTable = GetData ( _table, _orderByFieldName );
        }


        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
            {
                // Set the bools
                bool check, byHeart, pdfORP, pdfORK, pdfTOP, pdfTOK, mscORP, mscORK, mscTOP, mscTOK, mscOnline, mp3B1, mp3B2, mp3T1, mp3T2, mp3SOL, mp3TOT, mp3PIA;

                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 18 ].ToString ( ) ) == 0 )
                { check = false; }
                else
                { check = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 23 ].ToString ( ) ) == 0 )
                { pdfORP = false; }
                else
                { pdfORP = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 24 ].ToString ( ) ) == 0 )
                { pdfORK = false; }
                else
                { pdfORK = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 25 ].ToString ( ) ) == 0 )
                { pdfTOP = false; }
                else
                { pdfTOP = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 26 ].ToString ( ) ) == 0 )
                { pdfTOK = false; }
                else
                { pdfTOK = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 27 ].ToString ( ) ) == 0 )
                { mscORP = false; }
                else
                { mscORP = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 28 ].ToString ( ) ) == 0 )
                { mscORK = false; }
                else
                { mscORK = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 29 ].ToString ( ) ) == 0 )
                { mscTOP = false; }
                else
                { mscTOP = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 30 ].ToString ( ) ) == 0 )
                { mscTOK = false; }
                else
                { mscTOK = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 31 ].ToString ( ) ) == 0 )
                { mp3TOT = false; }
                else
                { mp3TOT = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 32 ].ToString ( ) ) == 0 )
                { mp3T1 = false; }
                else
                { mp3T1 = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 33 ].ToString ( ) ) == 0 )
                { mp3T2 = false; }
                else
                { mp3T2 = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 34 ].ToString ( ) ) == 0 )
                { mp3B1 = false; }
                else
                { mp3B1 = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 35 ].ToString ( ) ) == 0 )
                { mp3B2 = false; }
                else
                { mp3B2 = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 36 ].ToString ( ) ) == 0 )
                { mp3SOL = false; }
                else
                { mp3SOL = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 37 ].ToString ( ) ) == 0 )
                { mp3PIA = false; }
                else
                { mp3PIA = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 38 ].ToString ( ) ) == 0 )
                { mscOnline = false; }
                else
                { mscOnline = true; }
                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 39 ].ToString ( ) ) == 0 )
                { byHeart = false; }
                else
                { byHeart = true; }

                // Set the datestrings
                string dateCreated = "";
                if ( dataTable.Rows [ i ].ItemArray [ 19 ].ToString ( ) != "" )
                {
                    string[] _tempCreated = dataTable.Rows[i].ItemArray[19].ToString().Split(" ");
                    dateCreated = _tempCreated [ 0 ];
                }

                string dateModified = "";
                if ( dataTable.Rows [ i ].ItemArray [ 20 ].ToString ( ) != "" )
                {
                    string[] _tempModified = dataTable.Rows[i].ItemArray[20].ToString().Split(" ");
                    dateModified = _tempModified [ 0 ];
                }

                //var _duration="";
                int _minutes=0, _seconds = 0, _duration = 0;

                if ( int.Parse ( dataTable.Rows [ i ].ItemArray [ 54 ].ToString ( ) ) != 0 )
                {
                    _minutes = int.Parse ( dataTable.Rows [ i ].ItemArray [ 54 ].ToString ( ) ) / 60;
                    _seconds = int.Parse ( dataTable.Rows [ i ].ItemArray [ 54 ].ToString ( ) ) % 60;
                    //_duration = $"{_minutes}:{_seconds.ToString ( "00" )}";
                    _duration = int.Parse ( dataTable.Rows [ i ].ItemArray [ 54 ].ToString ( ) );
                }

                // When Title is empty don't add that row to the list
                if ( dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( ) != string.Empty )
                {
                    Scores.Add ( new ScoreModel
                    {
                        ScoreId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
                        Score = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( ),
                        ScoreNumber = dataTable.Rows [ i ].ItemArray [ 2 ].ToString ( ),
                        ScoreSubNumber = dataTable.Rows [ i ].ItemArray [ 3 ].ToString ( ),
                        ScoreTitle = dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( ),
                        ScoreSubTitle = dataTable.Rows [ i ].ItemArray [ 5 ].ToString ( ),
                        Composer = dataTable.Rows [ i ].ItemArray [ 6 ].ToString ( ),
                        Textwriter = dataTable.Rows [ i ].ItemArray [ 7 ].ToString ( ),
                        Arranger = dataTable.Rows [ i ].ItemArray [ 8 ].ToString ( ),
                        CheckInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 18 ].ToString ( ) ),
                        DateCreatedString = dateCreated,
                        DateModifiedString = dateModified,
                        Checked = check,
                        AccompanimentId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 21 ].ToString ( ) ),
                        AccompanimentName = dataTable.Rows [ i ].ItemArray [ 22 ].ToString ( ),
                        PDFORPInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 23 ].ToString ( ) ),
                        PDFORP = pdfORP,
                        PDFORKInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 24 ].ToString ( ) ),
                        PDFORK = pdfORK,
                        PDFTOPInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 25 ].ToString ( ) ),
                        PDFTOP = pdfTOP,
                        PDFTOKInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 26 ].ToString ( ) ),
                        PDFTOK = pdfTOK,
                        MuseScoreORPInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 27 ].ToString ( ) ),
                        MuseScoreORP = mscORP,
                        MuseScoreORKInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 28 ].ToString ( ) ),
                        MuseScoreORK = mscORK,
                        MuseScoreTOPInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 29 ].ToString ( ) ),
                        MuseScoreTOP = mscTOP,
                        MuseScoreTOKInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 30 ].ToString ( ) ),
                        MuseScoreTOK = mscTOK,
                        MP3TOTInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 31 ].ToString ( ) ),
                        MP3TOT = mp3TOT,
                        MP3T1Int = int.Parse ( dataTable.Rows [ i ].ItemArray [ 32 ].ToString ( ) ),
                        MP3T1 = mp3T1,
                        MP3T2Int = int.Parse ( dataTable.Rows [ i ].ItemArray [ 33 ].ToString ( ) ),
                        MP3T2 = mp3T2,
                        MP3B1Int = int.Parse ( dataTable.Rows [ i ].ItemArray [ 34 ].ToString ( ) ),
                        MP3B1 = mp3B1,
                        MP3B2Int = int.Parse ( dataTable.Rows [ i ].ItemArray [ 35 ].ToString ( ) ),
                        MP3B2 = mp3B2,
                        MP3SOLInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 36 ].ToString ( ) ),
                        MP3SOL = mp3SOL,
                        MP3PIAInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 37 ].ToString ( ) ),
                        MP3PIA = mp3PIA,
                        MuseScoreOnlineInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 38 ].ToString ( ) ),
                        MuseScoreOnline = mscOnline,
                        ByHeartInt = int.Parse ( dataTable.Rows [ i ].ItemArray [ 39 ].ToString ( ) ),
                        ByHeart = byHeart,
                        Duration = _duration,
                        DurationMinutes = _minutes,
                        DurationSeconds = _seconds,
                        SearchField = $"{dataTable.Rows [ i ].ItemArray [ 2 ]} {dataTable.Rows [ i ].ItemArray [ 4 ]}"
                    } );
                    ;
                }
            }
        }
        return Scores;
    }
    #endregion

    #region Delete User
    public static void DeleteUser ( string _userId )
    {
        var sqlQuery = DBNames.SqlDelete + DBNames.SqlFrom + DBNames.Database + "." + DBNames.UsersTable + DBNames.SqlWhere + DBNames.UsersFieldNameId + " = " + _userId + ";";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Add New User
    public static void AddNewUser ( )
    {
        // Add a new user as Super User (UserRole 5)
        var sqlQuery = DBNames.SqlInsert + DBNames.Database + "." + DBNames.UsersTable +
            " ( " + DBNames.UsersFieldNameRoleId + " ) " + DBNames.SqlValues +
            " ( 5 ) ";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Get Latest Added UserId
    public static int GetAddedUserId ( )
    {
        int userId;
        //SELECT * FROM users ORDER BY id DESC LIMIT 1
        var sqlQuery = DBNames.SqlSelectAll +
            DBNames.SqlFrom + DBNames.Database + "." + DBNames.UsersTable +
            DBNames.SqlOrder + DBNames.UserRolesFieldNameId + DBNames.SqlDesc + DBNames.SqlLimit + "1";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        userId = ( int ) cmd.ExecuteScalar ( );

        return userId;
    }
    #endregion

    #region GetUserRoles
    public static ObservableCollection<UserRoleModel> GetUserRoles ( )
    {
        ObservableCollection<UserRoleModel> UserRoles = new();

        DataTable dataTable = DBCommands.GetData(DBNames.UserRolesTable, DBNames.UserRolesFieldNameOrder);
        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
            {
                UserRoles.Add ( new UserRoleModel
                {
                    RoleId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
                    RoleOrder = int.Parse ( dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( ) ),
                    RoleName = dataTable.Rows [ i ].ItemArray [ 2 ].ToString ( ),
                    RoleDescription = dataTable.Rows [ i ].ItemArray [ 3 ].ToString ( )
                } );
            }
        }
        return UserRoles;
    }
    #endregion

    #region Update/Save Score
    public static void SaveScore ( ObservableCollection<SaveScoreModel> scoreList )
    {

        string sqlQuery = DBNames.SqlUpdate + DBNames.ScoresTable + DBNames.SqlSet;

        if ( scoreList [ 0 ].ByHeart != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameByHeart + " = @" + DBNames.ScoresFieldNameByHeart; }

        if ( scoreList [ 0 ].TitleChanged != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameTitle + " = @" + DBNames.ScoresFieldNameTitle; }
        if ( scoreList [ 0 ].SubTitleChanged != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameSubTitle + " = @" + DBNames.ScoresFieldNameSubTitle; }

        if ( scoreList [ 0 ].ComposerChanged != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameComposer + " = @" + DBNames.ScoresFieldNameComposer; }
        if ( scoreList [ 0 ].TextwriterChanged != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameTextwriter + " = @" + DBNames.ScoresFieldNameTextwriter; }
        if ( scoreList [ 0 ].ArrangerChanged != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameArranger + " = @" + DBNames.ScoresFieldNameArranger; }

        if ( scoreList [ 0 ].GenreId != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameGenreId + " = @" + DBNames.ScoresFieldNameGenreId; }

        if ( scoreList [ 0 ].DateDigitizedChanged != -1 && scoreList [ 0 ].DateDigitized != "" )
        { sqlQuery += ", " + DBNames.ScoresFieldNameDigitized + " = @" + DBNames.ScoresFieldNameDigitized; }
        if ( scoreList [ 0 ].DateModifiedChanged != -1 && scoreList [ 0 ].DateModified != "" )
        { sqlQuery += ", " + DBNames.ScoresFieldNameModified + " = @" + DBNames.ScoresFieldNameModified; }
        if ( scoreList [ 0 ].Checked != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameChecked + " = @" + DBNames.ScoresFieldNameChecked; }

        if ( scoreList [ 0 ].MuseScoreORP != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameMuseScoreORP + " = @" + DBNames.ScoresFieldNameMuseScoreORP; }
        if ( scoreList [ 0 ].MuseScoreORK != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameMuseScoreORK + " = @" + DBNames.ScoresFieldNameMuseScoreORK; }
        if ( scoreList [ 0 ].MuseScoreTOP != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameMuseScoreTOP + " = @" + DBNames.ScoresFieldNameMuseScoreTOP; }
        if ( scoreList [ 0 ].MuseScoreTOK != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameMuseScoreTOK + " = @" + DBNames.ScoresFieldNameMuseScoreTOK; }

        if ( scoreList [ 0 ].PDFORP != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNamePDFORP + " = @" + DBNames.ScoresFieldNamePDFORP; }
        if ( scoreList [ 0 ].PDFORK != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNamePDFORK + " = @" + DBNames.ScoresFieldNamePDFORK; }
        if ( scoreList [ 0 ].PDFTOP != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNamePDFTOP + " = @" + DBNames.ScoresFieldNamePDFTOP; }
        if ( scoreList [ 0 ].PDFTOK != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNamePDFTOK + " = @" + DBNames.ScoresFieldNamePDFTOK; }

        if ( scoreList [ 0 ].MP3B1 != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameMP3B1 + " = @" + DBNames.ScoresFieldNameMP3B1; }
        if ( scoreList [ 0 ].MP3B2 != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameMP3B2 + " = @" + DBNames.ScoresFieldNameMP3B2; }
        if ( scoreList [ 0 ].MP3T1 != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameMP3T1 + " = @" + DBNames.ScoresFieldNameMP3T1; }
        if ( scoreList [ 0 ].MP3T2 != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameMP3T2 + " = @" + DBNames.ScoresFieldNameMP3T2; }

        if ( scoreList [ 0 ].MP3SOL != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameMP3SOL + " = @" + DBNames.ScoresFieldNameMP3SOL; }
        if ( scoreList [ 0 ].MP3TOT != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameMP3TOT + " = @" + DBNames.ScoresFieldNameMP3TOT; }
        if ( scoreList [ 0 ].MP3PIA != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameMP3PIA + " = @" + DBNames.ScoresFieldNameMP3PIA; }

        if ( scoreList [ 0 ].MuseScoreOnline != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameOnline + " = @" + DBNames.ScoresFieldNameOnline; }

        if ( scoreList [ 0 ].DurationChanged != -1 )
        { sqlQuery += ", " + DBNames.ScoresFieldNameDuration + " = @" + DBNames.ScoresFieldNameDuration; }

        // Add the filter to the sqlQuery
        sqlQuery += DBNames.SqlWhere + DBNames.ScoresFieldNameId + " = @" + DBNames.ScoresFieldNameId + ";";
        //sqlQuery += DBNames.SqlWhere + DBNames.ScoresFieldNameScoreNumber + " = @" + DBNames.ScoresFieldNameScoreNumber + ";";

        try
        {
            ExecuteNonQueryScoresTable ( sqlQuery.Replace ( "SET , ", "SET " ), scoreList );
        }
        catch ( MySqlException ex )
        {
            Debug.WriteLine ( "Fout (UpdateScoresTable - MySqlException): " + ex.Message );
            throw ex;
        }
        catch ( Exception ex )
        {
            Debug.WriteLine ( "Fout (UpdateScoresTable): " + ex.Message );
            throw;
        }
    }
    #endregion

    #region Update User
    public static void UpdateUser ( ObservableCollection<UserModel> modifiedUser )
    {

        string sqlQuery = DBNames.SqlUpdate + DBNames.UsersTable + DBNames.SqlSet;

        if ( modifiedUser != null )
        {
            if ( modifiedUser [ 0 ].UserName != "" )
            { sqlQuery += DBNames.UsersFieldNameUserName + " = @" + DBNames.UsersFieldNameUserName; }

            if ( modifiedUser [ 0 ].UserFullName != "" )
            { sqlQuery += ", " + DBNames.UsersFieldNameFullName + " = @" + DBNames.UsersFieldNameFullName; }

            if ( modifiedUser [ 0 ].UserEmail != "" )
            { sqlQuery += ", " + DBNames.UsersFieldNameLogin + " = @" + DBNames.UsersFieldNameLogin; }

            if ( modifiedUser [ 0 ].UserRoleId != 0 )
            { sqlQuery += ", " + DBNames.UsersFieldNameRoleId + " = @" + DBNames.UsersFieldNameRoleId; }

            if ( modifiedUser [ 0 ].UserPassword != "" )
            { sqlQuery += ", `" + DBNames.UsersFieldNamePW + "` = @" + DBNames.UsersFieldNamePW; }

        }

        // Add the filter to the sqlQuery
        sqlQuery += DBNames.SqlWhere + DBNames.UsersFieldNameId + " = @" + DBNames.UsersFieldNameId + ";";

        try
        {
            ExecuteNonQueryUsersTable ( sqlQuery.Replace ( "SET , ", "SET " ), modifiedUser );
        }
        catch ( MySqlException ex )
        {
            Debug.WriteLine ( "Fout (UpdateScoresTable - MySqlException): " + ex.Message );
            throw ex;
        }
        catch ( Exception ex )
        {
            Debug.WriteLine ( "Fout (UpdateScoresTable): " + ex.Message );
            throw;
        }
    }
    #endregion

    #region Store file in database table
    public static void StoreFile ( string _table, string _path, string _fileName )
    {
        int fileSize;
        string sqlQuery;
        byte[] rawData;
        FileStream fs;

        try
        {
            fs = new FileStream ( @_path, FileMode.Open, FileAccess.Read );
            fileSize = Convert.ToInt32 ( fs.Length );

            rawData = new byte [ fileSize ];
            fs.Read ( rawData, 0, Convert.ToInt32 ( fs.Length ) );
            fs.Close ( );

            using MySqlConnection connection = new(DBConnect.ConnectionString);
            connection.Open ( );

            sqlQuery = DBNames.SqlInsert + _table + DBNames.SqlValues + "( NULL, @FileName, @FileSize, @File)";

            using MySqlCommand cmd = new(sqlQuery, connection);

            cmd.Connection = connection;
            cmd.CommandText = sqlQuery;
            cmd.Parameters.AddWithValue ( "@FileName", _fileName );
            cmd.Parameters.AddWithValue ( "@FileSize", fileSize );
            cmd.Parameters.AddWithValue ( "@File", rawData );

            cmd.ExecuteNonQuery ( );

            connection.Close ( );
        }
        catch ( MySql.Data.MySqlClient.MySqlException ex )
        {
            MessageBox.Show ( "Error " + ex.Number + " is opgetreden: " + ex.Message,
                "Error", MessageBoxButton.OK, MessageBoxImage.Error );
        }
    }
    #endregion

    #region Get latest FileId
    public static int GetAddedFileId ( string _tableName )
    {
        int fileId;
        var sqlQuery = DBNames.SqlSelectAll +
            DBNames.SqlFrom + DBNames.Database + "." + _tableName +
            DBNames.SqlOrder + DBNames.FilesFieldNameId + DBNames.SqlDesc + DBNames.SqlLimit + "1";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        fileId = ( int ) cmd.ExecuteScalar ( );

        return fileId;
    }
    #endregion

    #region Execute Non Query ScoresTable
    static void ExecuteNonQueryScoresTable ( string sqlQuery, ObservableCollection<SaveScoreModel> scoreList )
    {
        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        if ( scoreList [ 0 ].ByHeart != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameByHeart, MySqlDbType.Int32 ).Value = scoreList [ 0 ].ByHeart; }

        if ( scoreList [ 0 ].DurationChanged != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameDuration, MySqlDbType.Int32 ).Value = scoreList [ 0 ].Duration; }

        if ( scoreList [ 0 ].TitleChanged != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameTitle, MySqlDbType.VarChar ).Value = scoreList [ 0 ].Title; }
        if ( scoreList [ 0 ].SubTitleChanged != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameSubTitle, MySqlDbType.VarChar ).Value = scoreList [ 0 ].SubTitle; }

        if ( scoreList [ 0 ].ComposerChanged != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameComposer, MySqlDbType.VarChar ).Value = scoreList [ 0 ].Composer; }
        if ( scoreList [ 0 ].TextwriterChanged != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameTextwriter, MySqlDbType.VarChar ).Value = scoreList [ 0 ].Textwriter; }
        if ( scoreList [ 0 ].ArrangerChanged != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameArranger, MySqlDbType.VarChar ).Value = scoreList [ 0 ].Arranger; }

        if ( scoreList [ 0 ].GenreId != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameGenreId, MySqlDbType.Int32 ).Value = scoreList [ 0 ].GenreId; }

        if ( scoreList [ 0 ].DateDigitizedChanged != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameDigitized, MySqlDbType.String ).Value = scoreList [ 0 ].DateDigitized; }
        if ( scoreList [ 0 ].DateModifiedChanged != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameModified, MySqlDbType.String ).Value = scoreList [ 0 ].DateModified; }
        if ( scoreList [ 0 ].Checked != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameChecked, MySqlDbType.Int32 ).Value = scoreList [ 0 ].Checked; }

        if ( scoreList [ 0 ].MuseScoreORP != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMuseScoreORP, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MuseScoreORP; }
        if ( scoreList [ 0 ].MuseScoreORK != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMuseScoreORK, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MuseScoreORK; }
        if ( scoreList [ 0 ].MuseScoreTOP != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMuseScoreTOP, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MuseScoreTOP; }
        if ( scoreList [ 0 ].MuseScoreTOK != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMuseScoreTOK, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MuseScoreTOK; }

        if ( scoreList [ 0 ].PDFORP != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePDFORP, MySqlDbType.Int32 ).Value = scoreList [ 0 ].PDFORP; }
        if ( scoreList [ 0 ].PDFORK != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePDFORK, MySqlDbType.Int32 ).Value = scoreList [ 0 ].PDFORK; }
        if ( scoreList [ 0 ].PDFTOP != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePDFTOP, MySqlDbType.Int32 ).Value = scoreList [ 0 ].PDFTOP; }
        if ( scoreList [ 0 ].PDFTOK != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNamePDFTOK, MySqlDbType.Int32 ).Value = scoreList [ 0 ].PDFTOK; }

        if ( scoreList [ 0 ].MP3B1 != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3B1, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3B1; }
        if ( scoreList [ 0 ].MP3B2 != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3B2, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3B2; }
        if ( scoreList [ 0 ].MP3T1 != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3T1, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3T1; }
        if ( scoreList [ 0 ].MP3T2 != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3T2, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3T2; }

        if ( scoreList [ 0 ].MP3SOL != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3SOL, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3SOL; }
        if ( scoreList [ 0 ].MP3TOT != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3TOT, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3TOT; }
        if ( scoreList [ 0 ].MP3PIA != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameMP3PIA, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MP3PIA; }

        if ( scoreList [ 0 ].MuseScoreOnline != -1 )
        { cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameOnline, MySqlDbType.Int32 ).Value = scoreList [ 0 ].MuseScoreOnline; }

        // Add the Id value for the Score that has to be modified
        cmd.Parameters.Add ( "@" + DBNames.ScoresFieldNameId, MySqlDbType.Int32 ).Value = scoreList [ 0 ].ScoreId;

        //execute; returns the number of rows affected
        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Execute Non Query UserssTable
    static void ExecuteNonQueryUsersTable ( string sqlQuery, ObservableCollection<UserModel> modifiedUser )
    {
        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        if ( modifiedUser != null )
        {
            if ( modifiedUser [ 0 ].UserName != "" )
            { cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameUserName, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].UserName; }

            if ( modifiedUser [ 0 ].UserFullName != "" )
            { cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameFullName, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].UserFullName; }

            if ( modifiedUser [ 0 ].UserFullName != "" )
            { cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameLogin, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].UserEmail; }

            if ( modifiedUser [ 0 ].UserRoleId != 0 )
            { cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameRoleId, MySqlDbType.Int32 ).Value = modifiedUser [ 0 ].UserRoleId; }

            if ( modifiedUser [ 0 ].UserPassword != "" )
            { cmd.Parameters.Add ( "@" + DBNames.UsersFieldNamePW, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].UserPassword; }
        }

        // Add the userId value for the user that has to be modified
        cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameId, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].UserId;

        //execute; returns the number of rows affected
        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Check valid user password
    public static int CheckUserPassword ( string login, string password )
    {
        var _pwLogedInUser = Helper.HashPepperPassword(password, login);

        ObservableCollection<UserModel> Users = GetUsers();

        foreach ( var user in Users )
        {
            var _pwToCheck = Helper.HashPepperPassword(password, user.UserName);
            if ( _pwLogedInUser == _pwToCheck )
            {
                return user.UserId;
            }
        }
        return 0;
    }
    #endregion

    #region Check valid username
    public static bool CheckUserName ( string userName, int userId )
    {
        // Returns false if UserName does not already exist and true if it is already used (bool UserExists = DBCommands.CheckUserName( tbUserName.Text );)
        ObservableCollection<UserModel> Users = GetUsers();

        foreach ( var user in Users )
        {
            if ( user.UserName.ToLower ( ) == userName.ToLower ( ) )
            {
                // Still valid if the Username belongs to the selected User
                if ( user.UserId == userId )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }
    #endregion

    #region Check valid e-mail (Is it unique)
    public static bool CheckEMail ( string email, int userId )
    {
        ObservableCollection<UserModel> Users = GetUsers();

        foreach ( var user in Users )
        {
            if ( user.UserEmail.ToLower ( ) == email.ToLower ( ) )
            {
                // Still valid if the Username belongs to the selected User
                if ( user.UserId == userId )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }
    #endregion

    #region Check if the e-mail address has the correct format
    public static bool IsValidEmail ( string email )
    {
        if ( string.IsNullOrWhiteSpace ( email ) )
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace ( email, @"(@)(.+)$", DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds ( 200 ) );

            // Examines the domain part of the email and normalizes it.
            static string DomainMapper ( Match match )
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups [ 1 ].Value + domainName;
            }
        }
        catch ( RegexMatchTimeoutException )
        {
            return false;
        }
        catch ( ArgumentException )
        {
            return false;
        }

        try
        {
            return Regex.IsMatch ( email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds ( 250 ) );
        }
        catch ( RegexMatchTimeoutException )
        {
            return false;
        }
    }
    #endregion

    #region Get UserInfo
    #region Get Userinfo for 1 user
    public static ObservableCollection<UserModel> GetUsers ( int _userId )
    {
        ObservableCollection<UserModel> users = new();

        DataTable dataTable = DBCommands.GetData(DBNames.UsersTable, "nosort", DBNames.UsersFieldNameId, _userId.ToString());

        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
            {
                users.Add ( new UserModel
                {
                    UserId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
                    UserName = dataTable.Rows [ i ].ItemArray [ 2 ].ToString ( ),
                    UserEmail = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( ),
                    UserPassword = dataTable.Rows [ i ].ItemArray [ 3 ].ToString ( ),
                    UserFullName = dataTable.Rows [ i ].ItemArray [ 5 ].ToString ( ),
                    UserRoleId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( ) )
                } );
            }
        }
        return users;
    }
    #endregion

    #region Get Userinfo for all users
    public static ObservableCollection<UserModel> GetUsers ( )
    {
        ObservableCollection<UserModel> users = new();

        DataTable dataTable = DBCommands.GetData(DBNames.UsersView, DBNames.UsersFieldNameFullName);

        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
            {
                users.Add ( new UserModel
                {
                    UserId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
                    UserName = dataTable.Rows [ i ].ItemArray [ 2 ].ToString ( ),
                    UserEmail = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( ),
                    UserPassword = dataTable.Rows [ i ].ItemArray [ 3 ].ToString ( ),
                    UserFullName = dataTable.Rows [ i ].ItemArray [ 5 ].ToString ( ),
                    UserRoleId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( ) )
                } );
            }
        }
        return users;
    }
    #endregion

    #endregion

    #region Write History Logging
    public static void WriteLog ( int loggedInUser, string action, string description )
    {
        var sqlQuery = DBNames.SqlInsert + DBNames.Database + "." + DBNames.LogTable + " ( " +
            DBNames.LogFieldNameUserId + ", " +
            DBNames.LogFieldNameAction + ", " +
            DBNames.LogFieldNameDescription + " ) " + DBNames.SqlValues + " ( " +
            loggedInUser + ", '" + action + "', '" + description + "' );";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Write History Detail Logging
    public static void WriteDetailLog ( int logId, string field, string oldValue, string newValue )
    {
        var sqlQuery = DBNames.SqlInsert + DBNames.Database + "." + DBNames.LogDetailTable + " ( " +
            DBNames.LogDetailFieldNameLogId + ", " +
            DBNames.LogDetailFieldNameChanged + ", " +
            DBNames.LogDetailFieldNameOldValue + ", " +
            DBNames.LogDetailFieldNameNewValue + " ) " + DBNames.SqlValues + " ( " +
            logId + ", '" + field + "', '" + oldValue + "', '" + newValue + "' );";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
    }
    #endregion

    #region Get Latest Added HistoryId
    public static int GetAddedHistoryId ( )
    {
        int userId;
        var sqlQuery = DBNames.SqlSelectAll +
            DBNames.SqlFrom + DBNames.Database + "." + DBNames.LogTable +
            DBNames.SqlOrder + DBNames.LogFieldNameLogId + DBNames.SqlDesc + DBNames.SqlLimit + "1";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        userId = ( int ) cmd.ExecuteScalar ( );

        return userId;
    }
    #endregion

    #region Get HistoryLogging
    public static ObservableCollection<HistoryModel> GetHistoryLog ( )
    {
        ObservableCollection<HistoryModel> HistoryLog = new();
        _ = new DataTable ( );

        DataTable dataTable = GetData ( DBNames.LogView, DBNames.LogViewFieldNameLogid );

        if ( dataTable.Rows.Count > 0 )
        {

            for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
            {
                //Split-up DateTimeStamp in a date and a time
                string[] _dateTime = dataTable.Rows[i].ItemArray[1].ToString().Split(' ');
                string[] _date = _dateTime[0].Split("-");
                string logDate = $"{int.Parse(_date[0]):00}-{int.Parse(_date[1]):00)}-{_date[2]}";
                string logTime = _dateTime[1];

                HistoryLog.Add ( new HistoryModel
                {
                    LogId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
                    LogDate = logDate,
                    LogTime = logTime,
                    UserName = dataTable.Rows [ i ].ItemArray [ 2 ].ToString ( ),
                    PerformedAction = dataTable.Rows [ i ].ItemArray [ 3 ].ToString ( ),
                    Description = dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( ),
                    ModifiedField = dataTable.Rows [ i ].ItemArray [ 5 ].ToString ( ),
                    OldValue = dataTable.Rows [ i ].ItemArray [ 6 ].ToString ( ),
                    NewValue = dataTable.Rows [ i ].ItemArray [ 7 ].ToString ( )
                } );
            }
        }
        return HistoryLog;
    }
    #endregion
}
