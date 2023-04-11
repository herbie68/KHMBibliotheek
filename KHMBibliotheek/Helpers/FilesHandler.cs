using System.Collections.Generic;
using System.IO;

namespace KHMBibliotheek.Helpers;

public class FilesHandler
{
    public static void CheckFiles ( string [ ] files )
    {
    private bool Exists = false;
    private string scoreNumber = "";
    private string scoreName = "";
    private string scorePart = "";
    private string fileName = "";
    private string filePathName = "";
    private string fileType = "";
    private string fileFieldName = "";
    private Visibility PDFIcon = new();
    private Visibility MP3Icon = new();
    private Visibility MSCIcon = new();
    List<string> ErrorFiles = new();

    var hasVoiceString = "(ingezongen)";

        foreach (var file in files )
        {
            var hasVoice = false;
    var FileOk = false;
    var suffix = "";

    fileName = Path.GetFileName(file );
            filePathName = file;
            string[] fileNameSplitup = fileName.Split('.');
    string[] fileInfo = fileNameSplitup[0].Split('-');

    fileType = fileNameSplitup [ 1 ].Trim ( ).ToLower ( );
    scoreNumber = fileInfo [ 0 ].Trim ( ).Substring ( 0, 3 );
    scoreName = fileInfo [ 1 ].Replace ( hasVoiceString, "" ).Replace ( hasVoiceStringCap, "" ).Replace ( hasVoiceStringCaps, "" ).Trim ( );
    scorePart = fileInfo [ 0 ].Trim ( ).Substring ( 3 );

            if (fileInfo [ 1 ].Trim ( ).ToLower ( ).Contains ( hasVoiceString ) )
            { hasVoice = true; }
            else
            { hasVoice = false; };
fileSize = new System.IO.FileInfo ( file ).Length;
copiedFileSize += fileSize;
FileInfo fileDetails = new FileInfo(file);
        }
    }
}
