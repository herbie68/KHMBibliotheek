﻿namespace KHMBibliotheek.Helpers;

public class DBNames
{
    #region Sql commands
    public static readonly string SqlSelect = "SELECT ";
    public static readonly string SqlSelectAll = "SELECT *";
    public static readonly string SqlDelete = "DELETE ";
    public static readonly string SqlSelectDistinct = "SELECT DISTINCT ";
    public static readonly string SqlCast = "CAST( ";
    public static readonly string SqlMax = "MAX( ";
    public static readonly string SqlMin = "MIN( ";
    public static readonly string SqlUnsigned = " as UNSIGNED) ";
    public static readonly string SqlDeleteFrom = "DELETE FROM ";
    public static readonly string SqlInsert = "INSERT INTO ";
    public static readonly string SqlUpdate = "UPDATE ";
    public static readonly string SqlFrom = " FROM ";
    public static readonly string SqlWhere = " WHERE ";
    public static readonly string SqlLimit = " LIMIT ";
    public static readonly string SqlAsc = " ASC ";
    public static readonly string SqlDesc = " DESC ";
    public static readonly string SqlValues = " VALUES ";
    public static readonly string SqlOrder = " ORDER BY ";
    public static readonly string SqlSet = " SET ";
    public static readonly string SqlAnd = " AND ";
    public static readonly string SqlOr = " OR ";
    public static readonly string SqlIsNull = " IS NULL ";
    public static readonly string SqlCount = " COUNT( ";
    public static readonly string SqlCountAll = " COUNT(*) ";
    public static readonly string SqlBetween = " BETWEEN ";
    #endregion

    #region Database
    public static readonly string Database = "KHMMuziekbibliotheek";
    public static readonly string UsersDatabase = "KHM";
    #endregion

    #region Table/View Accompaniment
    public static readonly string AccompanimentsTable = "Begeleiding";
    public static readonly string AccompanimentsFieldNameId = "ArchiveId";
    public static readonly string AccompanimentsFieldNameName = "Begeleiding";
    #endregion

    #region Table/View Archive
    public static readonly string ArchivesTable = "Archief";
    public static readonly string ArchivesFieldNameId = "ArchiveId";
    public static readonly string ArchivesFieldNameName = "Genre";
    #endregion

    #region Table/View Genre
    public static readonly string GenresTable = "Genre";
    public static readonly string GenresFieldNameId = "ArchiveId";
    public static readonly string GenresFieldNameName = "Genre";
    #endregion

    #region Table/View Repertoire
    public static readonly string RepertoiresTable = "Repertoire";
    public static readonly string RepertoiresFieldNameId = "ArchiveId";
    public static readonly string RepertoiresFieldNameName = "Repertoire";
    #endregion

    #region Table/View Languages
    public static readonly string LanguagesTable = "Taal";
    public static readonly string LanguagesFieldNameId = "ArchiveId";
    public static readonly string LanguagesFieldNameName = "Taal";
    #endregion

    #region Table/View Publisher
    public static readonly string PublishersTable = "Uitgever";
    public static readonly string PublishersFieldNameId = "Id";
    public static readonly string PublishersFieldNameName = "Naam";
    public static readonly string PublishersFieldNameAddress1 = "Adres1";
    public static readonly string PublishersFieldNameAddress2 = "Adres2";
    public static readonly string PublishersFieldNameZip = "Postcode";
    public static readonly string PublishersFieldNameCity = "Plaats";
    public static readonly string PublishersFieldNamePhone = "Telefoon";
    public static readonly string PublishersFieldNameURL = "Website";
    public static readonly string PublishersFieldNameCustomerNumber = "Klantnummer";
    public static readonly string PublishersFieldNameMemo = "Notities";
    #endregion

    #region Table/View Scores
    public static readonly string ScoresTable = "Bibliotheek";
    public static readonly string ScoresFieldNameId = "Id";
    public static readonly string ScoresFieldNameArchiveId = "ArchiefId";
    public static readonly string ScoresFieldNameRepertoireId = "RepertoireId";
    public static readonly string ScoresFieldNameScoreNumber = "Partituur";
    public static readonly string ScoresFieldNameScoreSubNumber = "SubNummer";
    public static readonly string ScoresFieldNameTitle = "Titel";
    public static readonly string ScoresFieldNameSubTitle = "Ondertitel";
    public static readonly string ScoresFieldNameComposer = "Componist";
    public static readonly string ScoresFieldNameTextwriter = "Tekstschrijver";
    public static readonly string ScoresFieldNameArranger = "Arrangement";
    public static readonly string ScoresFieldNameLanguageId = "TaalId";
    public static readonly string ScoresFieldNameGenreId = "GenreId";
    public static readonly string ScoresFieldNameLyrics = "Lyrics";
    public static readonly string ScoresFieldNameChecked = "Gecontroleerd";
    public static readonly string ScoresFieldNameDigitized = "Gedigitaliseerd";
    public static readonly string ScoresFieldNameModified = "Revisie";
    public static readonly string ScoresFieldNameAccompanimentId = "BegeleidingId";
    public static readonly string ScoresFieldNamePDFORP = "PDFORP";
    public static readonly string ScoresFieldNamePDFORK = "PDFORK";
    public static readonly string ScoresFieldNamePDFTOP = "PDFTOP";
    public static readonly string ScoresFieldNamePDFTOK = "PDFTOK";
    public static readonly string ScoresFieldNameMuseScoreORP = "MSCORP";
    public static readonly string ScoresFieldNameMuseScoreORK = "MSCORK";
    public static readonly string ScoresFieldNameMuseScoreTOP = "MSCTOP";
    public static readonly string ScoresFieldNameMuseScoreTOK = "MSCTOK";
    public static readonly string ScoresFieldNameMP3TOT = "MP3TOT";
    public static readonly string ScoresFieldNameMP3T1 = "MP3T1";
    public static readonly string ScoresFieldNameMP3T2 = "MP3T2";
    public static readonly string ScoresFieldNameMP3B1 = "MP3B1";
    public static readonly string ScoresFieldNameMP3B2 = "MP3B2";
    public static readonly string ScoresFieldNameMP3SOL = "MP3SOL";
    public static readonly string ScoresFieldNameMP3PIA = "MP3PIA";
    public static readonly string ScoresFieldNameOnline = "Online";
    public static readonly string ScoresFieldNameByHeart = "UHH";
    public static readonly string ScoresFieldNameMusicPiece = "Muziekstuk";
    public static readonly string ScoresFieldNameNotes = "Opmerkingen";
    public static readonly string ScoresFieldNameAmountPublisher1 = "AantalUitgever1";
    public static readonly string ScoresFieldNameAmountPublisher2 = "AantalUitgever2";
    public static readonly string ScoresFieldNameAmountPublisher3 = "AantalUitgever3";
    public static readonly string ScoresFieldNameAmountPublisher4 = "AantalUitgever4";
    public static readonly string ScoresFieldNamePublisher1Id = "Uitgever1Id";
    public static readonly string ScoresFieldNamePublisher2Id = "Uitgever2Id";
    public static readonly string ScoresFieldNamePublisher3Id = "Uitgever3Id";
    public static readonly string ScoresFieldNamePublisher4Id = "Uitgever4Id";
    public static readonly string ScoresFieldNameDuration = "Duur";

    public static readonly string ScoresView = "Bibliotheek_View";
    public static readonly string ScoresViewFieldNameScore = "PartituurNummer";
    public static readonly string ScoresViewFieldNameArchiveName = "ArchiefNaam";
    public static readonly string ScoresViewFieldNameRepertoireName = "RepertoireNaam";
    public static readonly string ScoresViewFieldNameLanguageName = "TaalNaam";
    public static readonly string ScoresViewFieldNameGenreName = "GenreNaam";
    public static readonly string ScoresViewFieldNameAccompanimentName = "BegeleidingNaam";
    public static readonly string ScoresViewFieldNamePublisher1Name = "Uitgever1Naam";
    public static readonly string ScoresViewFieldNamePublisher2Name = "Uitgever2Naam";
    public static readonly string ScoresViewFieldNamePublisher3Name = "Uitgever3Naam";
    public static readonly string ScoresViewFieldNamePublisher4Name = "Uitgever4Naam";
    #endregion

    #region Table/View Available Score Numbers
    public static readonly string AvailableScoresView = "AvailableScoresView";
    public static readonly string AvailableScoresFieldNameId = "Id";
    public static readonly string AvailableScoresFieldNameNumber = "Partituur";
    #endregion

    #region Table/View Files... (table name differs, field names are similar)
    public static readonly string FilesMSCTable = "FilesMuseScore";
    public static readonly string FilesPDFTable = "FilesPDF";
    public static readonly string FilesMP3Table = "FilesMP3";
    public static readonly string FilesMP3VoiceTable = "FilesMP3Voice";
    public static readonly string FilesFieldNameId = "Id";
    public static readonly string FilesFieldNameScoreId = "ScoreId";
    public static readonly string FilesFieldNameFileName = "FileName";
    public static readonly string FilesFieldNameFileSize = "FileSize";
    public static readonly string FilesFieldNameContentType = "ContentType";
    public static readonly string FilesFieldNameFile = "File";
    #endregion

    #region Table/View FileIndex
    public static readonly string FilesIndexTable = "FilesIndex";
    public static readonly string FilesIndexFieldNameId = "Id";
    public static readonly string FilesIndexFieldNameScoreId = "ScoreId";
    public static readonly string FilesIndexFieldNameMuseScoreORPId = "MuseScoreORPId";
    public static readonly string FilesIndexFieldNameMuseScoreORKId = "MuseScoreORKId";
    public static readonly string FilesIndexFieldNameMuseScoreTOPId = "MuseScoreTOPId";
    public static readonly string FilesIndexFieldNameMuseScoreTOKId = "MuseScoreTOKId";
    public static readonly string FilesIndexFieldNamePDFORPId = "PDFORPId";
    public static readonly string FilesIndexFieldNamePDFORKId = "PDFORKId";
    public static readonly string FilesIndexFieldNamePDFTOPId = "PDFTOPId";
    public static readonly string FilesIndexFieldNamePDFTOKId = "PDFTOKId";
    public static readonly string FilesIndexFieldNameMP3B1Id = "MP3B1Id";
    public static readonly string FilesIndexFieldNameMP3B2Id = "MP3B2Id";
    public static readonly string FilesIndexFieldNameMP3T1Id = "MP3T1Id";
    public static readonly string FilesIndexFieldNameMP3T2Id = "MP3T2Id";
    public static readonly string FilesIndexFieldNameMP3SOLId = "MP3SOLId";
    public static readonly string FilesIndexFieldNameMP3TOTId = "MP3TOTId";
    public static readonly string FilesIndexFieldNameMP3PIAId = "MP3PIAId";
    public static readonly string FilesIndexFieldNameMP3VoiceB1Id = "MP3B1VoiceId";
    public static readonly string FilesIndexFieldNameMP3VoiceB2Id = "MP3B2VoiceId";
    public static readonly string FilesIndexFieldNameMP3VoiceT1Id = "MP3T1VoiceId";
    public static readonly string FilesIndexFieldNameMP3VoiceT2Id = "MP3T2VoiceId";
    public static readonly string FilesIndexFieldNameMP3VoiceSOLId = "MP3SOLVoiceId";
    public static readonly string FilesIndexFieldNameMP3VoiceTOTId = "MP3TOTVoiceId";
    #endregion

    #region Table/View MusicFiles
    public static readonly string MusicFilesView = "MusicFiles_View";
    public static readonly string MusicFilesFieldNameId = "Id";
    public static readonly string MusicFilesFieldNameScoreNumber = "PartituurNummer";
    public static readonly string MusicFilesFieldNameFilesIndexId = "FilesIndexId";
    public static readonly string MusicFilesFieldNamePDFORP = "PDFORP";
    public static readonly string MusicFilesFieldNamePDFORPId = "PDFORPId";
    public static readonly string MusicFilesFieldNamePDFORK = "PDFORK";
    public static readonly string MusicFilesFieldNamePDFORKId = "PDFORKId";
    public static readonly string MusicFilesFieldNamePDFTOP = "PDFTOP";
    public static readonly string MusicFilesFieldNamePDFTOPId = "PDFTOPId";
    public static readonly string MusicFilesFieldNamePDFTOK = "PDFTOK";
    public static readonly string MusicFilesFieldNamePDFTOKid = "PDFTOKId";
    public static readonly string MusicFilesFieldNamePDFPIA = "PDFPIA";
    public static readonly string MusicFilesFieldNamePDFPIAId = "PDFPIAId";
    public static readonly string MusicFilesFieldNameMSCORP = "MSCORP";
    public static readonly string MusicFilesFieldNameMSCORPid = "MSCORPId";
    public static readonly string MusicFilesFieldNameMSCORK = "MSCORK";
    public static readonly string MusicFilesFieldNameMSCORKid = "MSCORKId";
    public static readonly string MusicFilesFieldNameMSCTOP = "MSCTOP";
    public static readonly string MusicFilesFieldNameMSCTOPid = "MSCTOPId";
    public static readonly string MusicFilesFieldNameMSCTOK = "MSCTOK";
    public static readonly string MusicFilesFieldNameMSCTOKid = "MSCTOKId";
    public static readonly string MusicFilesFieldNameMP3TOT = "MP3TOT";
    public static readonly string MusicFilesFieldNameMP3TOTId = "MP3TOTId";
    public static readonly string MusicFilesFieldNameMP3T1 = "MP3T1";
    public static readonly string MusicFilesFieldNameMP3T1Id = "MP3T1Id";
    public static readonly string MusicFilesFieldNameMP3T2 = "MP3T2";
    public static readonly string MusicFilesFieldNameMP3T2Id = "MP3T2Id";
    public static readonly string MusicFilesFieldNameMP3B1 = "MP3B1";
    public static readonly string MusicFilesFieldNameMP3B1Id = "MP3B1Id";
    public static readonly string MusicFilesFieldNameMP3B2 = "MP3B2";
    public static readonly string MusicFilesFieldNameMP3B2Id = "MP3B2Id";
    public static readonly string MusicFilesFieldNameMP3SOL = "MP3SOL";
    public static readonly string MusicFilesFieldNameMP3SOLId = "MP3SOLId";
    public static readonly string MusicFilesFieldNameMP3PIA = "MP3PIA";
    public static readonly string MusicFilesFieldNameMP3PIAId = "MP3PIAId";
    public static readonly string MusicFilesFieldNameMP3UITV = "MP3UITV";
    public static readonly string MusicFilesFieldNameMP3UITVId = "MP3UITVId";
    #endregion

    #region Table/View Users
    public static readonly string UsersTable = "Users";
    public static readonly string UsersFieldNameId = "Id";
    public static readonly string UsersFieldNameLogin = "EMail";
    public static readonly string UsersFieldNameUserName = "UserName";
    public static readonly string UsersFieldNameFullName = "Fullname";
    public static readonly string UsersFieldNamePW = "Password";
    public static readonly string UsersFieldNameRoleId = "RoleId";
    public static readonly string UsersFieldNameCoverSheetFolder = "CoverSheetFolder";
    public static readonly string UsersFieldNameDownloadFolder = "DownloadFolder";

    public static readonly string UsersView = "view_Users";
    public static readonly string UsersViewFieldNameRoleOrder = "RoleOrder";
    public static readonly string UsersViewFieldNameRoleName = "RoleName";
    public static readonly string UsersViewFieldNameRoleDescription = "RoleDescription";
    #endregion

    #region Table/View UserRoles
    public static readonly string UserRolesTable = "UserRoles";
    public static readonly string UserRolesFieldNameId = "Id";
    public static readonly string UserRolesFieldNameOrder = "RoleOrder";
    public static readonly string UserRolesFieldNameName = "RoleName";
    public static readonly string UserRolesFieldNameDescription = "Description";
    #endregion

    #region Table/View Settings
    public static readonly string SettingsTable = "Settings";
    #endregion

    #region Log history
    #region Table History
    public static readonly string LogTable = "History";
    public static readonly string LogFieldNameLogId = "Id";
    public static readonly string LogFieldNameTimeStamp = "LogDate";
    public static readonly string LogFieldNameUserId = "UserId";
    public static readonly string LogFieldNameAction = "Action";
    public static readonly string LogFieldNameDescription = "Description";
    #endregion

    #region View History
    public static readonly string LogView = "view_History";
    public static readonly string LogViewFieldNameLogid = "LogId";
    public static readonly string LogViewFieldNameLogDate = "LogDate";
    public static readonly string LogViewFieldNameUserName = "User";
    public static readonly string LogViewFieldNameAction = "Action";
    public static readonly string LogViewFieldNameDescription = "Description";
    public static readonly string LogViewFieldNameField = "ModifiedField";
    public static readonly string LogViewFieldNameOldValue = "OldValue";
    public static readonly string LogViewFieldNameNewValue = "NewValue";
    #endregion

    #region Table HistoryDetails
    public static readonly string LogDetailTable = "HistoryDetails";
    public static readonly string LogDetailFieldNameDetailId = "Id";
    public static readonly string LogDetailFieldNameLogId = "LogId";
    public static readonly string LogDetailFieldNameChanged = "ModifiedField";
    public static readonly string LogDetailFieldNameOldValue = "OldValue";
    public static readonly string LogDetailFieldNameNewValue = "NewValue";
    #endregion

    #region Log Actions
    public static readonly string LogUserAdded = "Gebruiker toegevoegd";
    public static readonly string LogUserChanged = "Gebruiker gewijzigd";
    public static readonly string LogUserDeleted = "Gebruiker verwijderd";
    public static readonly string LogUserEMail = "E-mail adres gewijzigd";
    public static readonly string LogUserFullName = "Naam gewijzigd";
    public static readonly string LogUserRole = "Rol gewijzigd";
    public static readonly string LogUserPassword = "Wachtwoord gewijzigd";
    public static readonly string LogUserLoggedIn = "Gebruiker ingelogt";
    public static readonly string LogUserLoggedOut = "Gebruiker uitgelogt";
    public static readonly string LogUserCoverSheetFolder = "Map voor de voorbladen aangepast";
    public static readonly string LogCoverSheetCreated = "Voorblad aangemaakt";
    public static readonly string LogUserInvalidLogin = "Foutieve inlog poging";

    public static readonly string LogScoreAdded = "Partituur toegevoegd";
    public static readonly string LogScoreChanged = "Partituur gewijzigd";
    public static readonly string LogScoreDeleted = "Partituur verwijderd";
    public static readonly string LogScoreRenumbered = "Partituur omgenummerd";

    public static readonly string LogAccompaniment = "Begeleiding";
    public static readonly string LogAmountPublisher = "Aantal in bezit";
    public static readonly string LogArranger = "Arrangement";
    public static readonly string LogArchive = "Archief";
    public static readonly string LogByHeart = "Uit het hoofd";
    public static readonly string LogChecked = "Gecontroleerd";
    public static readonly string LogComposer = "Componist";
    public static readonly string LogDigitized = "Gedigitaliseerd";
    public static readonly string LogGenre = "Genre";
    public static readonly string LogLanguage = "Taal";
    public static readonly string LogLyrics = "Liedtekst aangepast";
    public static readonly string LogModified = "Laatste aanpassing";
    public static readonly string LogMSCORP = "MuseScore Origineel Totaal";
    public static readonly string LogMSCORK = "MuseScore Origineel Koor";
    public static readonly string LogMSCTOP = "MuseScore Bewerkt Totaal";
    public static readonly string LogMSCTOK = "MuseScore Bewerkt Koor";
    public static readonly string LogMSCOnline = "MuseScore Online gepost";
    public static readonly string LogMP3B1 = "Audiofile Bariton";
    public static readonly string LogMP3B2 = "Audiofile Bas";
    public static readonly string LogMP3T1 = "Audiofile 1e Tenor";
    public static readonly string LogMP3T2 = "Audiofile 2e Tenor";
    public static readonly string LogMP3SOL = "Audiofile Solist";
    public static readonly string LogMP3TOT = "Audiofile Totaal";
    public static readonly string LogMP3PIA = "Audiofile Piano";
    public static readonly string LogMusicPiece = "Muziekstuk";
    public static readonly string LogNotes = "Opmerkingen aangepast";
    public static readonly string LogPDFORP = "PDF Origineel Totaal";
    public static readonly string LogPDFORK = "PDF Origineel Koor";
    public static readonly string LogPDFTOP = "PDF Bewerkt Totaal";
    public static readonly string LogPDFTOK = "PDF Bewerkt Koor";
    public static readonly string LogPublisher = "Uitgever";
    public static readonly string LogRepertoire = "Repertoire";
    public static readonly string LogScoreNumber = "Partituur nummer";
    public static readonly string LogSubTitle = "Ondertitel";
    public static readonly string LogTextwriter = "Tekstschrijver";
    public static readonly string LogTitle = "Titel";
    public static readonly string LogYes = "Ja";
    public static readonly string LogNo = "Nee";
    public static readonly string LogNew = "<Nieuw>";
    public static readonly string LogInvalidLoginUsername = "Ingevoerde gebruikersnaam";
    public static readonly string LogInvalidLoginPassword = "Ingevoerd wachtwoord";
    #endregion
    #endregion
}
