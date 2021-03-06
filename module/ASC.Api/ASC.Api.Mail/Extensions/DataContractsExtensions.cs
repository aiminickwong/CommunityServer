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
using System.Linq;
using ASC.Api.Mail.DataContracts;
using ASC.Core;
using ASC.Mail.Aggregator;
using ASC.Mail.Aggregator.Common.Extension;
using ASC.Mail.Aggregator.Dal;
using ASC.Web.Core.Utility.Settings;

namespace ASC.Api.Mail.Extensions
{
    public static class DataContractsExtensions
    {

        public static List<MailAccountData> ToAddressData(this AccountInfo account)
        {
            var fromEmailList = new List<MailAccountData>();
            var mailBoxAccountSettings =
                SettingsManager.Instance.LoadSettingsFor<MailBoxAccountSettings>(SecurityContext.CurrentAccount.ID);

            var emailData = new MailAccountData
            {
                MailboxId = account.Id,
                Email = account.Email,
                Name = account.Name,
                Enabled = account.Enabled,
                OAuthConnection = account.OAuthConnection,
                AuthError = account.AuthError,
                QuotaError = account.QuotaError,
                Signature = account.Signature,
                EMailInFolder = account.EMailInFolder,
                IsAlias = false,
                IsGroup = false,
                IsTeamlabMailbox = account.IsTeamlabMailbox,
                IsDefault = mailBoxAccountSettings.DefaultEmail == account.Email,
                IsSharedDomainMailbox = account.IsSharedDomainMailbox
            };
            fromEmailList.Add(emailData);

            foreach (var alias in account.Aliases)
            {
                emailData = new MailAccountData
                {
                    MailboxId = account.Id,
                    Email = alias.Email,
                    Name = account.Name,
                    Enabled = account.Enabled,
                    OAuthConnection = false,
                    AuthError = account.AuthError,
                    QuotaError = account.QuotaError,
                    Signature = account.Signature,
                    EMailInFolder = account.EMailInFolder,
                    IsAlias = true,
                    IsGroup = false,
                    IsTeamlabMailbox = account.IsTeamlabMailbox,
                    IsDefault = mailBoxAccountSettings.DefaultEmail == alias.Email
                };
                fromEmailList.Add(emailData);
            }

            foreach (
                var @group in
                    account.Groups.Where(@group => fromEmailList.FindIndex(e => e.Email.Equals(@group.Email)) == -1))
            {
                emailData = new MailAccountData
                {
                    MailboxId = account.Id,
                    Email = @group.Email,
                    Name = "",
                    Enabled = true,
                    OAuthConnection = false,
                    AuthError = false,
                    QuotaError = false,
                    Signature = new SignatureDto(-1, account.Signature.Tenant, "", false),
                    EMailInFolder = "",
                    IsAlias = false,
                    IsGroup = true,
                    IsTeamlabMailbox = true
                };
                fromEmailList.Add(emailData);
            }

            return fromEmailList;
        }

        public static List<MailAccountData> ToAddressData(this List<AccountInfo> accounts)
        {
            var fromEmailList = new List<MailAccountData>();

            fromEmailList = accounts.Aggregate(fromEmailList, (current, account) => current.Concat(account.ToAddressData()).ToList());

            return fromEmailList.DistinctBy(a => a.Email).ToList();
        }

        public static void GetNeededAccounts(this List<MailAccountData> accounts, out MailAccountData defaultAccount,
            out List<MailAccountData> commonAccounts, out List<MailAccountData> serverAccounts,
            out List<MailAccountData> aliases, out List<MailAccountData> groups)
        {
            defaultAccount = null;
            commonAccounts = new List<MailAccountData>();
            serverAccounts = new List<MailAccountData>();
            aliases = new List<MailAccountData>();
            groups = new List<MailAccountData>();

            if (accounts == null)
            {
                return;
            }

            foreach (var account in accounts)
            {
                if (account.IsDefault)
                {
                    defaultAccount = account;
                }
                else if (account.IsGroup)
                {
                    groups.Add(account);
                }
                else if (account.IsAlias)
                {
                    aliases.Add(account);
                }
                else if (account.IsTeamlabMailbox)
                {
                    serverAccounts.Add(account);
                }
                else
                {
                    commonAccounts.Add(account);
                }
            }
        }

        public static List<MailFolderData> ToFolderData(this List<MailBoxManager.MailFolderInfo> folders)
        {
            return folders.Select(ToFolderData).ToList();
        }

        public static MailFolderData ToFolderData(this MailBoxManager.MailFolderInfo folder)
        {
            return new MailFolderData
                {
                    Id = folder.id,
                    UnreadCount = folder.unread,
                    TotalCount = folder.total_count,
                    TimeModified = folder.time_modified
                };
        }

        public static MailTagData ToTagData(this MailTag tag)
        {
            return new MailTagData
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Style = tag.Style,
                    Addresses = new MailTagData.AddressesList<string>(tag.Addresses),
                    LettersCount = tag.LettersCount
                };
        }

        public static List<MailTagData> ToTagData(this List<MailTag> tags)
        {
            return tags.Select(t => new MailTagData
            {
                Id = t.Id,
                Name = t.Name,
                Style = t.Style,
                Addresses = new MailTagData.AddressesList<string>(t.Addresses),
                LettersCount = t.LettersCount
            }).ToList();
        }
    }
}
