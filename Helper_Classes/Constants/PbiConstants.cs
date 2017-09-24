using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuilderWebApp3.Helper_Classes.Constants {
    public class PbiConstants {

        public static string BaseUri = "https://api.powerbi.com/beta/myorg/";
       
        //Resource Uri for Power BI API
        public static string ResourceUri = "https://analysis.windows.net/powerbi/api";

        public static string ClientId = "8e114947-9caa-475e-97ad-ceb4302a3a7d";
        public static string RedirectUri = "https://login.live.com/oauth20_desktop.srf";

        // OAuth2 authority Uri
        public static string AuthorityUri = "https://login.windows.net/common/oauth2/authorize";
    }
}