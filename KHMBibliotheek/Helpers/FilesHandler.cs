using System.Collections.Generic;
using System.IO;

namespace KHMBibliotheek.Helpers;

public class FilesHandler
{
    public static void CheckFiles ( string [ ] files )
    {
        var hasVoiceString = "(Ingezongen)";
        bool Exists, hasVoice;
        uint fileSize, copiedFileSize = 0;
        int scoreId;
        string scoreNumber, scoreTitleSuffix, scoreTitle, _fileName, _newFileName, scorePart, filePathName, fileType;
        List<string> ErrorList = new();

        foreach ( var file in files )
        {
            var startIndex = 3;

            _fileName = Path.GetFileName ( file );
            filePathName = file;
            string[] fileNameSplitup = _fileName.Split('.');
            string[] fileInfo = fileNameSplitup[0].Split('-');

            fileType = fileNameSplitup [ 1 ].Trim ( ).ToLower ( );
            scoreNumber = fileInfo [ 0 ].Trim ( ).Substring ( 0, 3 );

            // Check if file Contains SubNumber
            if ( fileNameSplitup [ 0 ].Substring ( 3, 1 ) == "-" )
            {
                scoreNumber += fileNameSplitup [ 0 ].Substring ( 3, 3 );
                startIndex = 6;

                //The Documenttype also contains the sub number, trim this
                fileInfo [ 1 ] = fileInfo [ 1 ].Substring ( 2, fileInfo [ 1 ].Length - 3 );
            }

            scoreTitle = DBCommands.GetScoreField ( DBNames.ScoresView, "scoretitle", DBNames.ScoresViewFieldNameScore, scoreNumber );
            scoreId = int.Parse ( DBCommands.GetScoreField ( DBNames.ScoresView, "scoreid", DBNames.ScoresViewFieldNameScore, scoreNumber ) );
            if ( scoreTitle != "" )
            { Exists = true; }
            else
            { Exists = false; }

            // Check If ScoreNumber is a valid Number And if it is a valid file type And the uploaded Score Exists in the Library
            if ( int.Parse ( scoreNumber ) > 0 && ( fileType.ToLower ( ) == "mscx" || fileType.ToLower ( ) == "pdf" || fileType.ToLower ( ) == "mp3" ) && Exists )
            {
                scorePart = fileInfo [ 0 ].Trim ( ).Substring ( startIndex );

                if ( fileInfo [ 1 ].Trim ( ).ToLower ( ).Contains ( hasVoiceString.Trim ( ).ToLower ( ) ) )
                {
                    hasVoice = true;
                    scoreTitleSuffix = " " + hasVoiceString;
                }
                else
                {
                    hasVoice = false;
                    scoreTitleSuffix = "";
                };

                scoreTitle += scoreTitleSuffix;

                fileSize = Convert.ToUInt32 ( new System.IO.FileInfo ( file ).Length );
                copiedFileSize += fileSize;
                FileInfo fileDetails = new FileInfo(file);

                _newFileName = $"{scoreNumber}{scorePart} - {scoreTitle}.{fileType}";

                UploadFile ( _fileName, _newFileName, fileType, scoreId, scoreNumber, scorePart, hasVoice );
            }
            else
            {
                // Incorrect filename
                ErrorList.Add ( _fileName );
            }
        }
    }
    public static async void UploadFile ( string _filePath, string _fileName, string _fileType, int _scoreId, string _scoreNumber, string _scorePart, bool _hasVoice )
    {
        string _tableName = "";
        int fileId = 0;

        // Steps for adding a file to a database table
        // Determine file type to have the  right database table
        // CHeck if the file excists in the File Index
        // Store the file in the correct Database table (Insert on new, update on excisting)
        // Get the Id of that newly added record
        // Store fileId in FileIndex Table

        switch ( _fileType.ToLower ( ) )
        {
            case "mscx":
                _tableName = DBNames.FilesMSCTable;
                break;
            case "pdf":
                _tableName = DBNames.FilesPDFTable;
                break;
            case "mp3":
                switch ( _hasVoice )
                {
                    case true:
                        _tableName = DBNames.FilesMP3VoiceTable;
                        break;
                    case false:
                        _tableName = DBNames.FilesMP3Table;
                        break;
                }
                break;
        }

        if ( _tableName != "" )
        {
            // Check if ScoreId excists in the FIlesIndex, if not a new record can be created
            var _filesIndexId = DBCommands.GetFileIndexIfFromScoreId(_scoreId);

            if ( _filesIndexId != -1 )
            {
                // Record already exist update values
            }
            else
            {
                // Record does not exist add it
                DBCommands.StoreFile ( _tableName, _filePath, _fileName );
                fileId = DBCommands.GetAddedFileId ( _tableName );
            }

        }
    }
}
