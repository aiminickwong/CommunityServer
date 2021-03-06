﻿/*
 *
 * (c) Copyright Ascensio System Limited 2010-2015
 *
 * This program is freeware. You can redistribute it and/or modify it under the terms of the GNU 
 * General Public License (GPL) version 3 as published by the Free Software Foundation (https://www.gnu.org/copyleft/gpl.html). 
 * In accordance with Section 7(a) of the GNU GPL its Section 15 shall be amended to the effect that 
 * Ascensio System SIA expressly excludes the warranty of non-infringement of any third-party rights.
 *
 * THIS PROGRAM IS DISTRIBUTED WITHOUT ANY WARRANTY; WITHOUT EVEN THE IMPLIED WARRANTY OF MERCHANTABILITY OR
 * FITNESS FOR A PARTICULAR PURPOSE. For more details, see GNU GPL at https://www.gnu.org/copyleft/gpl.html
 *
 * You can contact Ascensio System SIA by email at sales@onlyoffice.com
 *
 * The interactive user interfaces in modified source and object code versions of ONLYOFFICE must display 
 * Appropriate Legal Notices, as required under Section 5 of the GNU GPL version 3.
 *
 * Pursuant to Section 7 § 3(b) of the GNU GPL you must retain the original ONLYOFFICE logo which contains 
 * relevant author attributions when distributing the software. If the display of the logo in its graphic 
 * form is not reasonably feasible for technical reasons, you must include the words "Powered by ONLYOFFICE" 
 * in every copy of the program you distribute. 
 * Pursuant to Section 7 § 3(e) we decline to grant you any rights under trademark law for use of our trademarks.
 *
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using ASC.FederatedLogin.LoginProviders;
using ASC.Thrdparty;
using ASC.Thrdparty.Configuration;
using ASC.Web.Core.Mobile;
using ASC.Core;
using ASC.FederatedLogin;
using ASC.FederatedLogin.Profile;
using Newtonsoft.Json;

namespace ASC.Web.Studio.UserControls.Users.UserProfile
{
    public partial class AccountLinkControl : UserControl
    {
        public static string Location
        {
            get { return "~/UserControls/Users/UserProfile/AccountLinkControl.ascx"; }
        }

        public static bool IsNotEmpty
        {
            get
            {
                return
                    !string.IsNullOrEmpty(GoogleLoginProvider.GoogleOAuth20ClientId)
                    || !string.IsNullOrEmpty(FacebookLoginProvider.FacebookOAuth20ClientId)
                    || !string.IsNullOrEmpty(KeyStorage.Get("twitterKey"))
                    || !string.IsNullOrEmpty(LinkedInLoginProvider.LinkedInOAuth20ClientId);
            }
        }

        public bool SettingsView { get; set; }
        public bool InviteView { get; set; }

        protected ICollection<AccountInfo> Infos = new List<AccountInfo>();

        public AccountLinkControl()
        {
            ClientCallback = "loginCallback";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.RegisterStyle("~/usercontrols/users/userprofile/css/accountlink_style.less");
            Page.RegisterBodyScripts("~/usercontrols/users/userprofile/js/accountlinker.js");
            InitProviders();

            Page.RegisterInlineScript(String.Format(@" AccountLinkControl_Providers = {0};
                                                       AccountLinkControl_SettingsView = {1};
                                                       AccountLinkControl_InviteView = {2};",
                                    JsonConvert.SerializeObject(Infos),
                                    SettingsView.ToString().ToLower(),
                                    InviteView.ToString().ToLower()), onReady: false);
        }

        public string ClientCallback { get; set; }

        private void InitProviders()
        {
            IEnumerable<LoginProfile> linkedAccounts = new List<LoginProfile>();

            if (SecurityContext.IsAuthenticated)
            {
                linkedAccounts = GetLinker().GetLinkedProfiles(SecurityContext.CurrentAccount.ID.ToString());
            }

            var fromOnly = string.IsNullOrWhiteSpace(HttpContext.Current.Request["fromonly"]) ? string.Empty : HttpContext.Current.Request["fromonly"].ToLower();

            if (!string.IsNullOrEmpty(GoogleLoginProvider.GoogleOAuth20ClientId) && (string.IsNullOrEmpty(fromOnly) || fromOnly == "google" || fromOnly == "openid"))
                AddProvider(ProviderConstants.Google, linkedAccounts);

            if (!string.IsNullOrEmpty(FacebookLoginProvider.FacebookOAuth20ClientId) && (string.IsNullOrEmpty(fromOnly) || fromOnly == "facebook"))
                AddProvider(ProviderConstants.Facebook, linkedAccounts);

            if (!string.IsNullOrEmpty(KeyStorage.Get("twitterKey")) && (string.IsNullOrEmpty(fromOnly) || fromOnly == "twitter"))
                AddProvider(ProviderConstants.Twitter, linkedAccounts);

            if (!string.IsNullOrEmpty(LinkedInLoginProvider.LinkedInOAuth20ClientId) && (string.IsNullOrEmpty(fromOnly) || fromOnly == "linkedin"))
                AddProvider(ProviderConstants.LinkedIn, linkedAccounts);
        }

        private void AddProvider(string provider, IEnumerable<LoginProfile> linkedAccounts)
        {
            Infos.Add(new AccountInfo
                {
                    Linked = linkedAccounts.Any(x => x.Provider == provider),
                    Provider = provider,
                    Url = VirtualPathUtility.ToAbsolute("~/login.ashx")
                          + "?auth=" + provider
                          + (SettingsView || InviteView || !MobileDetector.IsMobile
                                 ? ("&mode=popup&callback=" + ClientCallback)
                                 : ("&mode=Redirect&returnurl=" + HttpUtility.UrlEncode(new Uri(Request.GetUrlRewriter(), "auth.aspx").ToString())))
                });
        }

        private static AccountLinker GetLinker()
        {
            return new AccountLinker("webstudio");
        }

        public IEnumerable<AccountInfo> GetLinkableProviders()
        {
            return Infos.Where(x => x.Provider.ToLower() != "twitter");
        }
    }

    public class AccountInfo
    {
        public string Provider { get; set; }
        public string Url { get; set; }
        public bool Linked { get; set; }
        public string Class { get; set; }
    }
}