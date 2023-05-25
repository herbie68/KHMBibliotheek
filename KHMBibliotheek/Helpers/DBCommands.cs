#pragma warning disable CS8602
#pragma warning disable CS8604

using System.Collections.Generic;
using System.IO;
using NAudio.Wave;

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

        DataTable dataTable = GetData(_table, "nosort", _whereFieldName, _whereFieldValue);

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

    #region Get Id Based on Where field
    public static int GetId ( string _table, string _getFieldName, string _whereFieldName, string _whereFieldValue )
    {
        int _result;
        string sqlQuery = $"{DBNames.SqlSelect}{_getFieldName}{DBNames.SqlFrom}{DBNames.Database}.{_table}{DBNames.SqlWhere}{_whereFieldName}  = '{_whereFieldValue}';";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        try
        {
            _result = ( int ) cmd.ExecuteScalar ( );
        }
        catch ( Exception )
        {
            _result = -1;
        }
        return _result;
    }
    #endregion

    #region Get String Based on Where field
    public static string GetField ( string _table, string _getFieldName, string _whereFieldName, string _whereFieldValue )
    {
        string _result;
        string sqlQuery = $"{DBNames.SqlSelect}{_getFieldName}{DBNames.SqlFrom}{DBNames.Database}.{_table}{DBNames.SqlWhere}{_whereFieldName}  = '{_whereFieldValue}';";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        try
        {
            _result = ( string ) cmd.ExecuteScalar ( );
        }
        catch ( Exception )
        {
            _result = "";
        }
        return _result;
    }
    #endregion

    #region Get fileId from database table
    public static int GetFileId ( string _table, string _getFieldName, string _whereFieldName, int _whereFieldValue )
    {
        int id;
        string sqlQuery = DBNames.SqlSelect + _getFieldName + DBNames.SqlFrom + DBNames.Database + "." + _table + DBNames.SqlWhere + _whereFieldName + " = " + _whereFieldValue;

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        try
        {
            id = ( int ) cmd.ExecuteScalar ( );
        }
        catch ( Exception )
        {
            id = -1;
        }
        return id;
    }
    #endregion

    #region Get FileIndex Id for ScoreId
    public static int GetFileIndexIfFromScoreId ( int _scoreId )
    {
        int id;
        string sqlQuery = $"{DBNames.SqlSelect}{DBNames.FilesIndexFieldNameId}{DBNames.SqlFrom}{DBNames.Database}.{DBNames.FilesIndexTable}{DBNames.SqlWhere}{DBNames.FilesIndexFieldNameScoreId} = {_scoreId};";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        try
        {
            id = ( int ) cmd.ExecuteScalar ( );
        }
        catch ( Exception )
        {
            id = -1;
        }
        return id;
    }
    #endregion

    #region Get specific FileId from FileIndex Id for FileIndexId
    public static int GetFileIdFromFilesIndex ( int _fileIndexId, string _fieldName )
    {
        int id;
        string sqlQuery = DBNames.SqlSelect + _fieldName + DBNames.SqlFrom + DBNames.Database + "." + DBNames.FilesIndexTable + DBNames.SqlWhere + DBNames.FilesIndexFieldNameId + " = " + _fileIndexId;

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        id = ( int ) cmd.ExecuteScalar ( );

        return id;
    }
    #endregion

    #region Add new Score to FilesIndex
    public static void AddNewFileIndex ( int _scoreId )
    {
        var sqlQuery = DBNames.SqlInsert + DBNames.Database + "." + DBNames.FilesIndexTable +
            " ( " + DBNames.FilesFieldNameScoreId + " ) " + DBNames.SqlValues +
            " ( " + _scoreId + " ) ";

        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        using MySqlCommand cmd = new(sqlQuery, connection);

        int rowsAffected = cmd.ExecuteNonQuery();
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

    #region Get MusicInformation
    public static ObservableCollection<MusicFilesModel> GetMusicFileInfo ( string _table, string _orderByFieldName = "nosort", string _whereFieldName = null, string _whereFieldValue = null )
    {
        ObservableCollection<MusicFilesModel> Scores = new();

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
#pragma warning disable IDE0059 // Unnecessary assignment of a value                
                bool pdfORP = int.Parse ( dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( ) ) == 0 ? false : true;
                bool pdfORK = int.Parse ( dataTable.Rows [ i ].ItemArray [ 6 ].ToString ( ) ) == 0 ? false : true;
                bool pdfTOP = int.Parse ( dataTable.Rows [ i ].ItemArray [ 8 ].ToString ( ) ) == 0 ? false : true;
                bool pdfTOK = int.Parse ( dataTable.Rows [ i ].ItemArray [ 10 ].ToString ( ) ) == 0 ? false : true;
                bool pdfPIA = int.Parse ( dataTable.Rows [ i ].ItemArray [ 12 ].ToString ( ) ) == 0 ? false : true;

                bool mscORP = int.Parse ( dataTable.Rows [ i ].ItemArray [ 14 ].ToString ( ) ) == 0 ? false : true;
                bool mscORK = int.Parse ( dataTable.Rows [ i ].ItemArray [ 16 ].ToString ( ) ) == 0 ? false : true;
                bool mscTOP = int.Parse ( dataTable.Rows [ i ].ItemArray [ 18 ].ToString ( ) ) == 0 ? false : true;
                bool mscTOK = int.Parse ( dataTable.Rows [ i ].ItemArray [ 20 ].ToString ( ) ) == 0 ? false : true;

                bool mp3TOT = int.Parse ( dataTable.Rows [ i ].ItemArray [ 22 ].ToString ( ) ) == 0 ? false : true;
                bool mp3T1 = int.Parse ( dataTable.Rows [ i ].ItemArray [ 24 ].ToString ( ) ) == 0 ? false : true;
                bool mp3T2 = int.Parse ( dataTable.Rows [ i ].ItemArray [ 26 ].ToString ( ) ) == 0 ? false : true;
                bool mp3B1 = int.Parse ( dataTable.Rows [ i ].ItemArray [ 28 ].ToString ( ) ) == 0 ? false : true;
                bool mp3B2 = int.Parse ( dataTable.Rows [ i ].ItemArray [ 30 ].ToString ( ) ) == 0 ? false : true;
                bool mp3SOL = int.Parse ( dataTable.Rows [ i ].ItemArray [ 32 ].ToString ( ) ) == 0 ? false : true;
                bool mp3PIA = int.Parse ( dataTable.Rows [ i ].ItemArray [ 34 ].ToString ( ) ) == 0 ? false : true;
                bool mp3UITV = int.Parse ( dataTable.Rows [ i ].ItemArray [ 36 ].ToString ( ) ) == 0 ? false : true;

                bool mp3TOTVoice = int.Parse ( dataTable.Rows [ i ].ItemArray [ 38 ].ToString ( ) ) == 0  ? false : true;
                bool mp3T1Voice = int.Parse ( dataTable.Rows [ i ].ItemArray [ 40 ].ToString ( ) ) == 0 ? false : true;
                bool mp3T2Voice = int.Parse ( dataTable.Rows [ i ].ItemArray [ 42 ].ToString ( ) ) == 0 ? false : true;
                bool mp3B1Voice = int.Parse ( dataTable.Rows [ i ].ItemArray [ 44 ].ToString ( ) ) == 0 ? false : true;
                bool mp3B2Voice = int.Parse ( dataTable.Rows [ i ].ItemArray [ 46 ].ToString ( ) ) == 0 ? false : true;
                bool mp3SOLVoice = int.Parse ( dataTable.Rows [ i ].ItemArray [ 48 ].ToString ( ) ) == 0 ? false : true;

                // When Title is empty don't add that row to the list
                if ( dataTable.Rows [ i ].ItemArray [ 2 ].ToString ( ) != string.Empty )
                {
                    Scores.Add ( new MusicFilesModel
                    {
                        ScoreId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
                        ScoreNumber = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( ),
                        ScoreTitle = dataTable.Rows [ i ].ItemArray [ 2 ].ToString ( ),
                        FilesIndexId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 3 ].ToString ( ) ),
                        PDFORP = pdfORP,
                        PDFORPId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 5 ].ToString ( ) ),
                        PDFORK = pdfORK,
                        PDFORKId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 7 ].ToString ( ) ),
                        PDFTOP = pdfTOP,
                        PDFTOPId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 9 ].ToString ( ) ),
                        PDFTOK = pdfTOK,
                        PDFTOKId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 11 ].ToString ( ) ),
                        PDFPIA = pdfPIA,
                        PDFPIAId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 13 ].ToString ( ) ),
                        MSCORP = mscORP,
                        MSCORPId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 15 ].ToString ( ) ),
                        MSCORK = mscORK,
                        MSCORKId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 17 ].ToString ( ) ),
                        MSCTOP = mscTOP,
                        MSCTOPId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 19 ].ToString ( ) ),
                        MSCTOK = mscTOK,
                        MSCTOKId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 21 ].ToString ( ) ),
                        MP3TOT = mp3TOT,
                        MP3TOTId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 23 ].ToString ( ) ),
                        MP3T1 = mp3T1,
                        MP3T1Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 25 ].ToString ( ) ),
                        MP3T2 = mp3T2,
                        MP3T2Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 27 ].ToString ( ) ),
                        MP3B1 = mp3B1,
                        MP3B1Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 29 ].ToString ( ) ),
                        MP3B2 = mp3B2,
                        MP3B2Id = int.Parse ( dataTable.Rows [ i ].ItemArray [ 31 ].ToString ( ) ),
                        MP3SOL = mp3SOL,
                        MP3SOLId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 33 ].ToString ( ) ),
                        MP3PIA = mp3PIA,
                        MP3PIAId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 35 ].ToString ( ) ),
                        MP3UITV = mp3UITV,
                        MP3UITVId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 37 ].ToString ( ) ),
                        MP3TOTVoice = mp3TOTVoice,
                        MP3TOTVoiceId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 39 ].ToString ( ) ),
                        MP3T1Voice = mp3T1Voice,
                        MP3T1VoiceId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 41 ].ToString ( ) ),
                        MP3T2Voice = mp3T2Voice,
                        MP3T2VoiceId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 43 ].ToString ( ) ),
                        MP3B1Voice = mp3B1Voice,
                        MP3B1VoiceId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 45 ].ToString ( ) ),
                        MP3B2Voice = mp3B2Voice,
                        MP3B2VoiceId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 47 ].ToString ( ) ),
                        MP3SOLVoice = mp3SOLVoice,
                        MP3SOLVoiceId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 49 ].ToString ( ) ),
                        SearchField = $"{dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( )} {dataTable.Rows [ i ].ItemArray [ 2 ].ToString ( )}"
                    } );
                    ;
                }
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

        foreach ( DataRow row in dataTable.Rows )
        {


            Console.WriteLine ( row );
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
                int _minutes = 0, _seconds = 0, _duration = 0;

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
#pragma warning restore IDE0059 // Unnecessary assignment of a value
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
                    Id = i,
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

            if ( modifiedUser [ 0 ].DownloadFolder != "" )
            { sqlQuery += ", " + DBNames.UsersFieldNameDownloadFolder + " = @" + DBNames.UsersFieldNameDownloadFolder; }

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
    public static void StoreFile ( string _table, int _scoreId, string _path, string _fileName )
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

            sqlQuery = $"{DBNames.SqlInsert} {_table} ( {DBNames.FilesFieldNameScoreId}, {DBNames.FilesFieldNameFileName}, {DBNames.FilesFieldNameFileSize}, {DBNames.FilesFieldNameFile} ) {DBNames.SqlValues} ( @ScoreId, @FileName, @FileSize, @File );";

            using MySqlCommand cmd = new(sqlQuery, connection);

            cmd.Connection = connection;
            cmd.CommandText = sqlQuery;
            cmd.Parameters.AddWithValue ( "@ScoreId", _scoreId );
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

    #region Update file in database table
    public static void UpdateFile ( string _table, string _path, string _fileName, int _fileId )
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

            sqlQuery = DBNames.SqlUpdate + _table + DBNames.SqlSet + DBNames.FilesFieldNameFileName + " = @FileName, " + DBNames.FilesFieldNameFileSize + " = @FileSize, " + DBNames.FilesFieldNameFile + " = @File" + DBNames.SqlWhere + DBNames.FilesFieldNameId + " = " + _fileId + ";";

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

    #region update FilesIndex tables, set the correct FileId for a specific Score
    public static void UpdateFilesIndex ( string _fieldName, int _fileId, string _whereIdField, int _whereId )
    {
        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        var sqlQuery = $"{DBNames.SqlUpdate} {DBNames.FilesIndexTable} {DBNames.SqlSet} {_fieldName}=@FileId {DBNames.SqlWhere} {_whereIdField} = {_whereId};";

        using MySqlCommand cmd = new(sqlQuery, connection);

        cmd.Connection = connection;
        cmd.CommandText = sqlQuery;
        cmd.Parameters.AddWithValue ( "@FileId", _fileId );

        cmd.ExecuteNonQuery ( );

        connection.Close ( );
    }
    #endregion

    #region update Library table, set the field for the available file for a specific Score
    public static void UpdateLibraryForFile ( string _fieldName, int _fieldValue, string _whereIdField, int _whereId )
    {
        using MySqlConnection connection = new(DBConnect.ConnectionString);
        connection.Open ( );

        var sqlQuery = DBNames.SqlUpdate + DBNames.ScoresTable + DBNames.SqlSet + _fieldName + " = " + _fieldValue + DBNames.SqlWhere + _whereIdField + " = " + _whereId + ";";

        using MySqlCommand cmd = new(sqlQuery, connection);

        cmd.Connection = connection;
        cmd.CommandText = sqlQuery;

        cmd.ExecuteNonQuery ( );

        connection.Close ( );
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

    #region Check existence of DownloadPath, if not exists create it
    public static void CheckFolder ( string _path )
    {
        if ( !Directory.Exists ( _path ) )
        {
            Directory.CreateDirectory ( _path );
        }
    }

    #endregion

    #region Download File
    public static void DownloadFile ( int _fileId, string _fileTable, string _filePathSuffix, string _fileName )
    {
        string? _downloadPath, selectQuery;

        _downloadPath = $"{FilePaths.DownloadPath}\\{_filePathSuffix}";
        CheckFolder ( _downloadPath );

        selectQuery = $"{DBNames.SqlSelect}{DBNames.FilesFieldNameFile}{DBNames.SqlFrom}{_fileTable}{DBNames.SqlWhere}{DBNames.FilesFieldNameId} = @{DBNames.FilesFieldNameId};";

        // Verbinding maken met de MySQL-database
        using ( MySqlConnection connection = new MySqlConnection ( DBConnect.ConnectionString ) )
        {
            connection.Open ( );

            using ( MySqlCommand command = new MySqlCommand ( selectQuery, connection ) )
            {
                command.Parameters.AddWithValue ( $"@{DBNames.FilesFieldNameId}", _fileId );

                using ( MySqlDataReader reader = command.ExecuteReader ( CommandBehavior.SingleRow ) )
                {
                    if ( reader.Read ( ) )
                    {
                        using ( FileStream fileStream = new ( @$"{_downloadPath}\{_fileName}", FileMode.Create, FileAccess.Write ) )
                        {
                            using ( BinaryWriter binaryWriter = new ( fileStream ) )
                            {
                                long startIndex = 0;
                                const int bufferSize = 1024;
                                byte[] buffer = new byte[bufferSize];

                                long bytesRead = reader.GetBytes(0, startIndex, buffer, 0, bufferSize);

                                while ( bytesRead == bufferSize )
                                {
                                    binaryWriter.Write ( buffer );
                                    binaryWriter.Flush ( );

                                    startIndex += bufferSize;
                                    bytesRead = reader.GetBytes ( 0, startIndex, buffer, 0, bufferSize );
                                }

                                // Schrijf de resterende bytes naar het bestand
                                binaryWriter.Write ( buffer, 0, ( int ) bytesRead );
                                binaryWriter.Flush ( );
                            }
                        }
                    }
                }
            }
            connection.Close ( );
        }
    }
    #endregion

    #region Play MP3 File
    public static void PlayMP3File ( int _fileId, string _fileTable, string _filePathSuffix, string _fileName )
    {
        string? _downloadPath, selectQuery;

        _downloadPath = $"{FilePaths.DownloadPath}\\{_filePathSuffix}";


        selectQuery = $"{DBNames.SqlSelect}{DBNames.FilesFieldNameFile}{DBNames.SqlFrom}{_fileTable}{DBNames.SqlWhere}{DBNames.FilesFieldNameId} = @{DBNames.FilesFieldNameId};";

        // Verbinding maken met de MySQL-database
        using ( MySqlConnection connection = new MySqlConnection ( DBConnect.ConnectionString ) )
        {
            connection.Open ( );

            using ( MySqlCommand command = new MySqlCommand ( selectQuery, connection ) )
            {
                command.Parameters.AddWithValue ( $"@{DBNames.FilesFieldNameId}", _fileId );

                using ( MySqlDataReader reader = command.ExecuteReader ( CommandBehavior.SingleRow ) )
                {
                    if ( reader.Read ( ) )
                    {
                        byte[] mp3Data = (byte[])reader[$"{DBNames.FilesFieldNameFile}"]; // Het mp3-bestand als byte-array uit de database halen

                        using ( MemoryStream ms = new ( mp3Data ) )
                        {
                            using ( Mp3FileReader mp3Reader = new ( ms ) )
                            {
                                using ( WaveOutEvent waveOut = new ( ) )
                                {
                                    waveOut.Init ( mp3Reader );
                                    waveOut.Play ( );

                                    Console.WriteLine ( "Het mp3-bestand wordt afgespeeld. Druk op Enter om te stoppen." );
                                    Console.ReadLine ( );
                                }
                            }
                        }
                    }
                }
                connection.Close ( );
            }
        }
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

            if ( modifiedUser [ 0 ].DownloadFolder != "" )
            { cmd.Parameters.Add ( "@" + DBNames.UsersFieldNameDownloadFolder, MySqlDbType.VarChar ).Value = modifiedUser [ 0 ].DownloadFolder; }

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
    public static ObservableCollection<UserModel> GetUsers ( )
    {
        ObservableCollection<UserModel> users = new();
        ObservableCollection<UserRoleModel> userRoles = new();

        DataTable dataTable = DBCommands.GetData(DBNames.UsersView, DBNames.UsersFieldNameFullName);
        userRoles = GetUserRoles ( );

        if ( dataTable.Rows.Count > 0 )
        {
            for ( int i = 0 ; i < dataTable.Rows.Count ; i++ )
            {
                string roleDescription = "";

                foreach ( var role in userRoles )
                {
                    if ( role.RoleId == int.Parse ( dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( ) ) )
                    {
                        roleDescription = role.RoleDescription;
                        break;
                    }
                }

                users.Add ( new UserModel
                {
                    UserId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 0 ].ToString ( ) ),
                    UserName = dataTable.Rows [ i ].ItemArray [ 2 ].ToString ( ),
                    UserEmail = dataTable.Rows [ i ].ItemArray [ 1 ].ToString ( ),
                    UserPassword = dataTable.Rows [ i ].ItemArray [ 3 ].ToString ( ),
                    UserFullName = dataTable.Rows [ i ].ItemArray [ 5 ].ToString ( ),
                    UserRoleId = int.Parse ( dataTable.Rows [ i ].ItemArray [ 4 ].ToString ( ) ),
                    RoleDescription = roleDescription,
                    DownloadFolder = dataTable.Rows [ i ].ItemArray [ 10 ].ToString ( )
                } );
            }
        }
        return users;
    }
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

        DataTable dataTable = GetData(DBNames.LogView, DBNames.LogViewFieldNameLogid);

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

    #region ContentTypes for FileDownload
    private static IDictionary<string, string> ContentType = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
    {
        #region FileTypes
        {".323", "text/h323"},
        {".3g2", "video/3gpp2"},
        {".3gp", "video/3gpp"},
        {".3gp2", "video/3gpp2"},
        {".3gpp", "video/3gpp"},
        {".7z", "application/x-7z-compressed"},
        {".aa", "audio/audible"},
        {".AAC", "audio/aac"},
        {".aaf", "application/octet-stream"},
        {".aax", "audio/vnd.audible.aax"},
        {".ac3", "audio/ac3"},
        {".aca", "application/octet-stream"},
        {".accda", "application/msaccess.addin"},
        {".accdb", "application/msaccess"},
        {".accdc", "application/msaccess.cab"},
        {".accde", "application/msaccess"},
        {".accdr", "application/msaccess.runtime"},
        {".accdt", "application/msaccess"},
        {".accdw", "application/msaccess.webapplication"},
        {".accft", "application/msaccess.ftemplate"},
        {".acx", "application/internet-property-stream"},
        {".AddIn", "text/xml"},
        {".ade", "application/msaccess"},
        {".adobebridge", "application/x-bridge-url"},
        {".adp", "application/msaccess"},
        {".ADT", "audio/vnd.dlna.adts"},
        {".ADTS", "audio/aac"},
        {".afm", "application/octet-stream"},
        {".ai", "application/postscript"},
        {".aif", "audio/x-aiff"},
        {".aifc", "audio/aiff"},
        {".aiff", "audio/aiff"},
        {".air", "application/vnd.adobe.air-application-installer-package+zip"},
        {".amc", "application/x-mpeg"},
        {".application", "application/x-ms-application"},
        {".art", "image/x-jg"},
        {".asa", "application/xml"},
        {".asax", "application/xml"},
        {".ascx", "application/xml"},
        {".asd", "application/octet-stream"},
        {".asf", "video/x-ms-asf"},
        {".ashx", "application/xml"},
        {".asi", "application/octet-stream"},
        {".asm", "text/plain"},
        {".asmx", "application/xml"},
        {".aspx", "application/xml"},
        {".asr", "video/x-ms-asf"},
        {".asx", "video/x-ms-asf"},
        {".atom", "application/atom+xml"},
        {".au", "audio/basic"},
        {".avi", "video/x-msvideo"},
        {".axs", "application/olescript"},
        {".bas", "text/plain"},
        {".bcpio", "application/x-bcpio"},
        {".bin", "application/octet-stream"},
        {".bmp", "image/bmp"},
        {".c", "text/plain"},
        {".cab", "application/octet-stream"},
        {".caf", "audio/x-caf"},
        {".calx", "application/vnd.ms-office.calx"},
        {".cat", "application/vnd.ms-pki.seccat"},
        {".cc", "text/plain"},
        {".cd", "text/plain"},
        {".cdda", "audio/aiff"},
        {".cdf", "application/x-cdf"},
        {".cer", "application/x-x509-ca-cert"},
        {".chm", "application/octet-stream"},
        {".class", "application/x-java-applet"},
        {".clp", "application/x-msclip"},
        {".cmx", "image/x-cmx"},
        {".cnf", "text/plain"},
        {".cod", "image/cis-cod"},
        {".config", "application/xml"},
        {".contact", "text/x-ms-contact"},
        {".coverage", "application/xml"},
        {".cpio", "application/x-cpio"},
        {".cpp", "text/plain"},
        {".crd", "application/x-mscardfile"},
        {".crl", "application/pkix-crl"},
        {".crt", "application/x-x509-ca-cert"},
        {".cs", "text/plain"},
        {".csdproj", "text/plain"},
        {".csh", "application/x-csh"},
        {".csproj", "text/plain"},
        {".css", "text/css"},
        {".csv", "text/csv"},
        {".cur", "application/octet-stream"},
        {".cxx", "text/plain"},
        {".dat", "application/octet-stream"},
        {".datasource", "application/xml"},
        {".dbproj", "text/plain"},
        {".dcr", "application/x-director"},
        {".def", "text/plain"},
        {".deploy", "application/octet-stream"},
        {".der", "application/x-x509-ca-cert"},
        {".dgml", "application/xml"},
        {".dib", "image/bmp"},
        {".dif", "video/x-dv"},
        {".dir", "application/x-director"},
        {".disco", "text/xml"},
        {".dll", "application/x-msdownload"},
        {".dll.config", "text/xml"},
        {".dlm", "text/dlm"},
        {".doc", "application/msword"},
        {".docm", "application/vnd.ms-word.document.macroEnabled.12"},
        {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
        {".dot", "application/msword"},
        {".dotm", "application/vnd.ms-word.template.macroEnabled.12"},
        {".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
        {".dsp", "application/octet-stream"},
        {".dsw", "text/plain"},
        {".dtd", "text/xml"},
        {".dtsConfig", "text/xml"},
        {".dv", "video/x-dv"},
        {".dvi", "application/x-dvi"},
        {".dwf", "drawing/x-dwf"},
        {".dwp", "application/octet-stream"},
        {".dxr", "application/x-director"},
        {".eml", "message/rfc822"},
        {".emz", "application/octet-stream"},
        {".eot", "application/octet-stream"},
        {".eps", "application/postscript"},
        {".etl", "application/etl"},
        {".etx", "text/x-setext"},
        {".evy", "application/envoy"},
        {".exe", "application/octet-stream"},
        {".exe.config", "text/xml"},
        {".fdf", "application/vnd.fdf"},
        {".fif", "application/fractals"},
        {".filters", "Application/xml"},
        {".fla", "application/octet-stream"},
        {".flr", "x-world/x-vrml"},
        {".flv", "video/x-flv"},
        {".fsscript", "application/fsharp-script"},
        {".fsx", "application/fsharp-script"},
        {".generictest", "application/xml"},
        {".gif", "image/gif"},
        {".group", "text/x-ms-group"},
        {".gsm", "audio/x-gsm"},
        {".gtar", "application/x-gtar"},
        {".gz", "application/x-gzip"},
        {".h", "text/plain"},
        {".hdf", "application/x-hdf"},
        {".hdml", "text/x-hdml"},
        {".hhc", "application/x-oleobject"},
        {".hhk", "application/octet-stream"},
        {".hhp", "application/octet-stream"},
        {".hlp", "application/winhlp"},
        {".hpp", "text/plain"},
        {".hqx", "application/mac-binhex40"},
        {".hta", "application/hta"},
        {".htc", "text/x-component"},
        {".htm", "text/html"},
        {".html", "text/html"},
        {".htt", "text/webviewhtml"},
        {".hxa", "application/xml"},
        {".hxc", "application/xml"},
        {".hxd", "application/octet-stream"},
        {".hxe", "application/xml"},
        {".hxf", "application/xml"},
        {".hxh", "application/octet-stream"},
        {".hxi", "application/octet-stream"},
        {".hxk", "application/xml"},
        {".hxq", "application/octet-stream"},
        {".hxr", "application/octet-stream"},
        {".hxs", "application/octet-stream"},
        {".hxt", "text/html"},
        {".hxv", "application/xml"},
        {".hxw", "application/octet-stream"},
        {".hxx", "text/plain"},
        {".i", "text/plain"},
        {".ico", "image/x-icon"},
        {".ics", "application/octet-stream"},
        {".idl", "text/plain"},
        {".ief", "image/ief"},
        {".iii", "application/x-iphone"},
        {".inc", "text/plain"},
        {".inf", "application/octet-stream"},
        {".inl", "text/plain"},
        {".ins", "application/x-internet-signup"},
        {".ipa", "application/x-itunes-ipa"},
        {".ipg", "application/x-itunes-ipg"},
        {".ipproj", "text/plain"},
        {".ipsw", "application/x-itunes-ipsw"},
        {".iqy", "text/x-ms-iqy"},
        {".isp", "application/x-internet-signup"},
        {".ite", "application/x-itunes-ite"},
        {".itlp", "application/x-itunes-itlp"},
        {".itms", "application/x-itunes-itms"},
        {".itpc", "application/x-itunes-itpc"},
        {".IVF", "video/x-ivf"},
        {".jar", "application/java-archive"},
        {".java", "application/octet-stream"},
        {".jck", "application/liquidmotion"},
        {".jcz", "application/liquidmotion"},
        {".jfif", "image/pjpeg"},
        {".jnlp", "application/x-java-jnlp-file"},
        {".jpb", "application/octet-stream"},
        {".jpe", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".jpg", "image/jpeg"},
        {".js", "application/x-javascript"},
        {".json", "application/json"},
        {".jsx", "text/jscript"},
        {".jsxbin", "text/plain"},
        {".latex", "application/x-latex"},
        {".library-ms", "application/windows-library+xml"},
        {".lit", "application/x-ms-reader"},
        {".loadtest", "application/xml"},
        {".lpk", "application/octet-stream"},
        {".lsf", "video/x-la-asf"},
        {".lst", "text/plain"},
        {".lsx", "video/x-la-asf"},
        {".lzh", "application/octet-stream"},
        {".m13", "application/x-msmediaview"},
        {".m14", "application/x-msmediaview"},
        {".m1v", "video/mpeg"},
        {".m2t", "video/vnd.dlna.mpeg-tts"},
        {".m2ts", "video/vnd.dlna.mpeg-tts"},
        {".m2v", "video/mpeg"},
        {".m3u", "audio/x-mpegurl"},
        {".m3u8", "audio/x-mpegurl"},
        {".m4a", "audio/m4a"},
        {".m4b", "audio/m4b"},
        {".m4p", "audio/m4p"},
        {".m4r", "audio/x-m4r"},
        {".m4v", "video/x-m4v"},
        {".mac", "image/x-macpaint"},
        {".mak", "text/plain"},
        {".man", "application/x-troff-man"},
        {".manifest", "application/x-ms-manifest"},
        {".map", "text/plain"},
        {".master", "application/xml"},
        {".mda", "application/msaccess"},
        {".mdb", "application/x-msaccess"},
        {".mde", "application/msaccess"},
        {".mdp", "application/octet-stream"},
        {".me", "application/x-troff-me"},
        {".mfp", "application/x-shockwave-flash"},
        {".mht", "message/rfc822"},
        {".mhtml", "message/rfc822"},
        {".mid", "audio/mid"},
        {".midi", "audio/mid"},
        {".mix", "application/octet-stream"},
        {".mk", "text/plain"},
        {".mmf", "application/x-smaf"},
        {".mno", "text/xml"},
        {".mny", "application/x-msmoney"},
        {".mod", "video/mpeg"},
        {".mov", "video/quicktime"},
        {".movie", "video/x-sgi-movie"},
        {".mp2", "video/mpeg"},
        {".mp2v", "video/mpeg"},
        {".mp3", "audio/mpeg"},
        {".mp4", "video/mp4"},
        {".mp4v", "video/mp4"},
        {".mpa", "video/mpeg"},
        {".mpe", "video/mpeg"},
        {".mpeg", "video/mpeg"},
        {".mpf", "application/vnd.ms-mediapackage"},
        {".mpg", "video/mpeg"},
        {".mpp", "application/vnd.ms-project"},
        {".mpv2", "video/mpeg"},
        {".mqv", "video/quicktime"},
        {".ms", "application/x-troff-ms"},
        {".mscx", "application/vnd.recordare.musicxml"},
        {".mscz", "application/vnd.recordare.musicxml"},
        {".msi", "application/octet-stream"},
        {".mso", "application/octet-stream"},
        {".mts", "video/vnd.dlna.mpeg-tts"},
        {".mtx", "application/xml"},
        {".mvb", "application/x-msmediaview"},
        {".mvc", "application/x-miva-compiled"},
        {".mxp", "application/x-mmxp"},
        {".nc", "application/x-netcdf"},
        {".nsc", "video/x-ms-asf"},
        {".nws", "message/rfc822"},
        {".ocx", "application/octet-stream"},
        {".oda", "application/oda"},
        {".odc", "text/x-ms-odc"},
        {".odh", "text/plain"},
        {".odl", "text/plain"},
        {".odp", "application/vnd.oasis.opendocument.presentation"},
        {".ods", "application/oleobject"},
        {".odt", "application/vnd.oasis.opendocument.text"},
        {".one", "application/onenote"},
        {".onea", "application/onenote"},
        {".onepkg", "application/onenote"},
        {".onetmp", "application/onenote"},
        {".onetoc", "application/onenote"},
        {".onetoc2", "application/onenote"},
        {".orderedtest", "application/xml"},
        {".osdx", "application/opensearchdescription+xml"},
        {".p10", "application/pkcs10"},
        {".p12", "application/x-pkcs12"},
        {".p7b", "application/x-pkcs7-certificates"},
        {".p7c", "application/pkcs7-mime"},
        {".p7m", "application/pkcs7-mime"},
        {".p7r", "application/x-pkcs7-certreqresp"},
        {".p7s", "application/pkcs7-signature"},
        {".pbm", "image/x-portable-bitmap"},
        {".pcast", "application/x-podcast"},
        {".pct", "image/pict"},
        {".pcx", "application/octet-stream"},
        {".pcz", "application/octet-stream"},
        {".pdf", "application/pdf"},
        {".pfb", "application/octet-stream"},
        {".pfm", "application/octet-stream"},
        {".pfx", "application/x-pkcs12"},
        {".pgm", "image/x-portable-graymap"},
        {".pic", "image/pict"},
        {".pict", "image/pict"},
        {".pkgdef", "text/plain"},
        {".pkgundef", "text/plain"},
        {".pko", "application/vnd.ms-pki.pko"},
        {".pls", "audio/scpls"},
        {".pma", "application/x-perfmon"},
        {".pmc", "application/x-perfmon"},
        {".pml", "application/x-perfmon"},
        {".pmr", "application/x-perfmon"},
        {".pmw", "application/x-perfmon"},
        {".png", "image/png"},
        {".pnm", "image/x-portable-anymap"},
        {".pnt", "image/x-macpaint"},
        {".pntg", "image/x-macpaint"},
        {".pnz", "image/png"},
        {".pot", "application/vnd.ms-powerpoint"},
        {".potm", "application/vnd.ms-powerpoint.template.macroEnabled.12"},
        {".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
        {".ppa", "application/vnd.ms-powerpoint"},
        {".ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12"},
        {".ppm", "image/x-portable-pixmap"},
        {".pps", "application/vnd.ms-powerpoint"},
        {".ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
        {".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
        {".ppt", "application/vnd.ms-powerpoint"},
        {".pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
        {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
        {".prf", "application/pics-rules"},
        {".prm", "application/octet-stream"},
        {".prx", "application/octet-stream"},
        {".ps", "application/postscript"},
        {".psc1", "application/PowerShell"},
        {".psd", "application/octet-stream"},
        {".psess", "application/xml"},
        {".psm", "application/octet-stream"},
        {".psp", "application/octet-stream"},
        {".pub", "application/x-mspublisher"},
        {".pwz", "application/vnd.ms-powerpoint"},
        {".qht", "text/x-html-insertion"},
        {".qhtm", "text/x-html-insertion"},
        {".qt", "video/quicktime"},
        {".qti", "image/x-quicktime"},
        {".qtif", "image/x-quicktime"},
        {".qtl", "application/x-quicktimeplayer"},
        {".qxd", "application/octet-stream"},
        {".ra", "audio/x-pn-realaudio"},
        {".ram", "audio/x-pn-realaudio"},
        {".rar", "application/octet-stream"},
        {".ras", "image/x-cmu-raster"},
        {".rat", "application/rat-file"},
        {".rc", "text/plain"},
        {".rc2", "text/plain"},
        {".rct", "text/plain"},
        {".rdlc", "application/xml"},
        {".resx", "application/xml"},
        {".rf", "image/vnd.rn-realflash"},
        {".rgb", "image/x-rgb"},
        {".rgs", "text/plain"},
        {".rm", "application/vnd.rn-realmedia"},
        {".rmi", "audio/mid"},
        {".rmp", "application/vnd.rn-rn_music_package"},
        {".roff", "application/x-troff"},
        {".rpm", "audio/x-pn-realaudio-plugin"},
        {".rqy", "text/x-ms-rqy"},
        {".rtf", "application/rtf"},
        {".rtx", "text/richtext"},
        {".ruleset", "application/xml"},
        {".s", "text/plain"},
        {".safariextz", "application/x-safari-safariextz"},
        {".scd", "application/x-msschedule"},
        {".sct", "text/scriptlet"},
        {".sd2", "audio/x-sd2"},
        {".sdp", "application/sdp"},
        {".sea", "application/octet-stream"},
        {".searchConnector-ms", "application/windows-search-connector+xml"},
        {".setpay", "application/set-payment-initiation"},
        {".setreg", "application/set-registration-initiation"},
        {".settings", "application/xml"},
        {".sgimb", "application/x-sgimb"},
        {".sgml", "text/sgml"},
        {".sh", "application/x-sh"},
        {".shar", "application/x-shar"},
        {".shtml", "text/html"},
        {".sit", "application/x-stuffit"},
        {".sitemap", "application/xml"},
        {".skin", "application/xml"},
        {".sldm", "application/vnd.ms-powerpoint.slide.macroEnabled.12"},
        {".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide"},
        {".slk", "application/vnd.ms-excel"},
        {".sln", "text/plain"},
        {".slupkg-ms", "application/x-ms-license"},
        {".smd", "audio/x-smd"},
        {".smi", "application/octet-stream"},
        {".smx", "audio/x-smd"},
        {".smz", "audio/x-smd"},
        {".snd", "audio/basic"},
        {".snippet", "application/xml"},
        {".snp", "application/octet-stream"},
        {".sol", "text/plain"},
        {".sor", "text/plain"},
        {".spc", "application/x-pkcs7-certificates"},
        {".spl", "application/futuresplash"},
        {".src", "application/x-wais-source"},
        {".srf", "text/plain"},
        {".SSISDeploymentManifest", "text/xml"},
        {".ssm", "application/streamingmedia"},
        {".sst", "application/vnd.ms-pki.certstore"},
        {".stl", "application/vnd.ms-pki.stl"},
        {".sv4cpio", "application/x-sv4cpio"},
        {".sv4crc", "application/x-sv4crc"},
        {".svc", "application/xml"},
        {".swf", "application/x-shockwave-flash"},
        {".t", "application/x-troff"},
        {".tar", "application/x-tar"},
        {".tcl", "application/x-tcl"},
        {".testrunconfig", "application/xml"},
        {".testsettings", "application/xml"},
        {".tex", "application/x-tex"},
        {".texi", "application/x-texinfo"},
        {".texinfo", "application/x-texinfo"},
        {".tgz", "application/x-compressed"},
        {".thmx", "application/vnd.ms-officetheme"},
        {".thn", "application/octet-stream"},
        {".tif", "image/tiff"},
        {".tiff", "image/tiff"},
        {".tlh", "text/plain"},
        {".tli", "text/plain"},
        {".toc", "application/octet-stream"},
        {".tr", "application/x-troff"},
        {".trm", "application/x-msterminal"},
        {".trx", "application/xml"},
        {".ts", "video/vnd.dlna.mpeg-tts"},
        {".tsv", "text/tab-separated-values"},
        {".ttf", "application/octet-stream"},
        {".tts", "video/vnd.dlna.mpeg-tts"},
        {".txt", "text/plain"},
        {".u32", "application/octet-stream"},
        {".uls", "text/iuls"},
        {".user", "text/plain"},
        {".ustar", "application/x-ustar"},
        {".vb", "text/plain"},
        {".vbdproj", "text/plain"},
        {".vbk", "video/mpeg"},
        {".vbproj", "text/plain"},
        {".vbs", "text/vbscript"},
        {".vcf", "text/x-vcard"},
        {".vcproj", "Application/xml"},
        {".vcs", "text/plain"},
        {".vcxproj", "Application/xml"},
        {".vddproj", "text/plain"},
        {".vdp", "text/plain"},
        {".vdproj", "text/plain"},
        {".vdx", "application/vnd.ms-visio.viewer"},
        {".vml", "text/xml"},
        {".vscontent", "application/xml"},
        {".vsct", "text/xml"},
        {".vsd", "application/vnd.visio"},
        {".vsi", "application/ms-vsi"},
        {".vsix", "application/vsix"},
        {".vsixlangpack", "text/xml"},
        {".vsixmanifest", "text/xml"},
        {".vsmdi", "application/xml"},
        {".vspscc", "text/plain"},
        {".vss", "application/vnd.visio"},
        {".vsscc", "text/plain"},
        {".vssettings", "text/xml"},
        {".vssscc", "text/plain"},
        {".vst", "application/vnd.visio"},
        {".vstemplate", "text/xml"},
        {".vsto", "application/x-ms-vsto"},
        {".vsw", "application/vnd.visio"},
        {".vsx", "application/vnd.visio"},
        {".vtx", "application/vnd.visio"},
        {".wav", "audio/wav"},
        {".wave", "audio/wav"},
        {".wax", "audio/x-ms-wax"},
        {".wbk", "application/msword"},
        {".wbmp", "image/vnd.wap.wbmp"},
        {".wcm", "application/vnd.ms-works"},
        {".wdb", "application/vnd.ms-works"},
        {".wdp", "image/vnd.ms-photo"},
        {".webarchive", "application/x-safari-webarchive"},
        {".webtest", "application/xml"},
        {".wiq", "application/xml"},
        {".wiz", "application/msword"},
        {".wks", "application/vnd.ms-works"},
        {".WLMP", "application/wlmoviemaker"},
        {".wlpginstall", "application/x-wlpg-detect"},
        {".wlpginstall3", "application/x-wlpg3-detect"},
        {".wm", "video/x-ms-wm"},
        {".wma", "audio/x-ms-wma"},
        {".wmd", "application/x-ms-wmd"},
        {".wmf", "application/x-msmetafile"},
        {".wml", "text/vnd.wap.wml"},
        {".wmlc", "application/vnd.wap.wmlc"},
        {".wmls", "text/vnd.wap.wmlscript"},
        {".wmlsc", "application/vnd.wap.wmlscriptc"},
        {".wmp", "video/x-ms-wmp"},
        {".wmv", "video/x-ms-wmv"},
        {".wmx", "video/x-ms-wmx"},
        {".wmz", "application/x-ms-wmz"},
        {".wpl", "application/vnd.ms-wpl"},
        {".wps", "application/vnd.ms-works"},
        {".wri", "application/x-mswrite"},
        {".wrl", "x-world/x-vrml"},
        {".wrz", "x-world/x-vrml"},
        {".wsc", "text/scriptlet"},
        {".wsdl", "text/xml"},
        {".wvx", "video/x-ms-wvx"},
        {".x", "application/directx"},
        {".xaf", "x-world/x-vrml"},
        {".xaml", "application/xaml+xml"},
        {".xap", "application/x-silverlight-app"},
        {".xbap", "application/x-ms-xbap"},
        {".xbm", "image/x-xbitmap"},
        {".xdr", "text/plain"},
        {".xht", "application/xhtml+xml"},
        {".xhtml", "application/xhtml+xml"},
        {".xla", "application/vnd.ms-excel"},
        {".xlam", "application/vnd.ms-excel.addin.macroEnabled.12"},
        {".xlc", "application/vnd.ms-excel"},
        {".xld", "application/vnd.ms-excel"},
        {".xlk", "application/vnd.ms-excel"},
        {".xll", "application/vnd.ms-excel"},
        {".xlm", "application/vnd.ms-excel"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
        {".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"},
        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        {".xlt", "application/vnd.ms-excel"},
        {".xltm", "application/vnd.ms-excel.template.macroEnabled.12"},
        {".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
        {".xlw", "application/vnd.ms-excel"},
        {".xml", "text/xml"},
        {".xmta", "application/xml"},
        {".xof", "x-world/x-vrml"},
        {".XOML", "text/plain"},
        {".xpm", "image/x-xpixmap"},
        {".xps", "application/vnd.ms-xpsdocument"},
        {".xrm-ms", "text/xml"},
        {".xsc", "application/xml"},
        {".xsd", "text/xml"},
        {".xsf", "text/xml"},
        {".xsl", "text/xml"},
        {".xslt", "text/xml"},
        {".xsn", "application/octet-stream"},
        {".xss", "application/xml"},
        {".xtp", "application/octet-stream"},
        {".xwd", "image/x-xwindowdump"},
        {".z", "application/x-compress"},
        {".zip", "application/x-zip-compressed"}
#endregion
    };
    #endregion
}
