/*
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


using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;

using ASC.Core;
using ASC.VoipService;
using ASC.VoipService.Dao;
using ASC.Web.Core.Client.HttpHandlers;
using ASC.Web.Studio.Core.Voip;

namespace ASC.Web.Studio.Controls.Common
{
    public class VoipNavigation
    {
        public static string RenderCustomNavigation(Page page)
        {
            if (!VoipEnabled) return string.Empty;

            page.RegisterBodyScripts("~/js/asc/core/voip.countries.js");
            page.RegisterBodyScripts("~/js/asc/core/voip.phone.js");
            page.RegisterClientScript(typeof(VoipNumberData));

            return
                string.Format(@"<li class=""top-item-box voip"">
                                  <a class=""voipActiveBox inner-text"" title=""{0}"">
                                      <span class=""inner-label"">{1}</span>
                                  </a>
                                </li>",
                              "VoIP",
                              0);
        }

        internal static VoipPhone CurrentNumber
        {
            get { return new CachedVoipDao(CoreContext.TenantManager.GetCurrentTenant().TenantId, "crm").GetCurrentNumber(); }
        }

        public static bool VoipEnabled
        {
            get { return VoipPaymentSettings.IsEnabled && CurrentNumber != null; }
        }
    }

    public class VoipNumberData : ClientScript
    {
        protected override string BaseNamespace
        {
            get { return "ASC.Resources.Master"; }
        }

        protected override string GetCacheHash()
        {
            return SecurityContext.CurrentAccount.ID + VoipNavigation.CurrentNumber.Number +
                   (SecurityContext.IsAuthenticated && !CoreContext.Configuration.Personal
                        ? (CoreContext.UserManager.GetMaxUsersLastModified().Ticks.ToString(CultureInfo.InvariantCulture) +
                           CoreContext.UserManager.GetMaxGroupsLastModified().Ticks.ToString(CultureInfo.InvariantCulture))
                        : string.Empty);
        }

        protected override IEnumerable<KeyValuePair<string, object>> GetClientVariables(HttpContext context)
        {
            yield return RegisterObject("numberId", VoipNavigation.CurrentNumber.Number);
        }
    }
}