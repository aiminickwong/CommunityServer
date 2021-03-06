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


window.folderPanel = (function($) {
    var isInit = false,
        wndQuestion = undefined,
        clearFolderId = -1,
        folders = [];

    function init() {
        if (isInit === false) {
            isInit = true;

            serviceManager.bind(window.Teamlab.events.getMailFolders, onGetMailFolders);
            
            if (ASC.Mail.Presets.Folders) {
                onGetMailFolders({}, ASC.Mail.Presets.Folders);
            }

            wndQuestion = $('#removeQuestionWnd');
            wndQuestion.find('.buttons .cancel').bind('click', function() {
                hide();
                return false;
            });

            wndQuestion.find('.buttons .remove').bind('click', function() {
                if (clearFolderId) {
                    serviceManager.removeMailFolderMessages(clearFolderId, {}, {}, window.MailScriptResource.DeletionMessage);
                    serviceManager.getMailFolders();
                    serviceManager.getTags();
                }

                var text = "";
                if (clearFolderId == 4) {
                    text = "trash";
                }
                if (clearFolderId == 5) {
                    text = "spam";
                }
                window.ASC.Mail.ga_track(ga_Categories.folder, ga_Actions.filterClick, text);

                clearFolderId = -1;
                hide();
                return false;
            });
        }
    }

    function showQuestionBox(folderId) {
        clearFolderId = folderId;
        var questionText = (folderId === TMMail.sysfolders.trash.id
                            ? window.MailScriptResource.RemoveTrashQuestion
                            : window.MailScriptResource.RemoveSpamQuestion);

        wndQuestion.find('.mail-confirmationAction p.questionText').text(questionText);
        wndQuestion.find('div.containerHeaderBlock:first td:first').html(window.MailScriptResource.Delete);

        var margintop = jq(window).scrollTop() - 135;
        margintop = margintop + 'px';
        jq.blockUI({
            message: wndQuestion,
            css: {
                left: '50%',
                top: '25%',
                opacity: '1',
                border: 'none',
                padding: '0px',
                width: '335px',

                cursor: 'default',
                textAlign: 'left',
                position: 'absolute',
                'margin-left': '-167px',
                'margin-top': margintop,
                'background-color': 'White'
            },
            overlayCSS: {
                backgroundColor: '#AAA',
                cursor: 'default',
                opacity: '0.3'
            },
            focusInput: false,
            baseZ: 666,

            fadeIn: 0,
            fadeOut: 0,

            onBlock: function() {
            }
        });
    }

    function hide() {
        $.unblockUI();
    }

    function initFolders() {
        $('#foldersContainer').children().each(function() {
            var $this = $(this);
            var folder = parseInt($this.attr('folderid'));
            $this.find('a').attr('href', '#' + TMMail.getSysFolderNameById(folder));
            if (folder === TMMail.sysfolders.trash.id ||
                folder === TMMail.sysfolders.spam.id) {

                var deleteTrash = $this.find('.delete_mails');
                if (deleteTrash.length > 0) {
                    deleteTrash.unbind('click').bind('click', { folder: folder }, function (e) {
                        showQuestionBox(e.data.folder);
                    });
                }
            }

        });
    }

    function markFolder(folderId) {
        $('#foldersContainer > .active').removeClass('active');
        $('#foldersContainer').children('[folderid=' + folderId + ']').addClass('active');
    }

    function getMarkedFolder() {
        return $('#foldersContainer').children('.active').attr('folderid');
    }

    function searchFolderById(collection, id) {
        var pos = -1;
        var i, len = collection.length;
        for (i = 0; i < len; i++) {
            var folder = collection[i];
            if (folder.id == id) {
                pos = i;
                break;
            }
        }
        return pos;
    }

    function onGetMailFolders(params, newFolders) {
        if (undefined == newFolders || undefined == newFolders.length) {
            return;
        }

        var marked = getMarkedFolder() || -1; // -1 if no folder selected

        if (folders.length != 0) {
            var i, len = newFolders.length, changedFolders = [], pos;

            for (i = 0; i < len; i++) {
                var newFolder = newFolders[i];
                pos = searchFolderById(folders, newFolder.id);
                if (pos > -1) {
                    var oldFolder = folders[pos];
                    if (oldFolder.unread !== newFolder.unread ||
                        oldFolder.total_count !== newFolder.total_count ||
                        oldFolder.time_modified !== newFolder.time_modified) {
                        changedFolders.push(newFolder);
                        mailBox.markFolderAsChanged(newFolder.id);
                    }
                } else {
                    changedFolders.push(newFolder);
                    mailBox.markFolderAsChanged(newFolder.id);
                }
            }

            if (changedFolders.length === 0) {
                return;
            }

            if (params.check_conversations_on_changes) {
                var currentFolder = MailFilter.getFolder();
                pos = searchFolderById(changedFolders, currentFolder);
                if (pos > -1)
                    serviceManager.getMailFilteredConversations();
            }
        }

        folders = newFolders;

        var html = $.tmpl('foldersTmpl', newFolders, { marked: marked });
        $('#foldersContainer').html(html);

        initFolders();

        if (TMMail.pageIs('sysfolders') && marked > -1) {
            marked = TMMail.extractFolderIdFromAnchor();
            TMMail.setPageHeaderFolderName(marked);
        }

        // sets unread count from inbox to top panel mail icon
        $.each(newFolders, function (index, value) {
            if (value.id == TMMail.sysfolders.inbox.id) {
                setTpUnreadMessagesCount(value.unread);
            }
        });
    }

    function setTpUnreadMessagesCount(unreadCount) {
        $('#TPUnreadMessagesCount').text(unreadCount > 100 ? '>100' : unreadCount);
        $('#TPUnreadMessagesCount').parent().toggleClass('has-led', unreadCount != 0);
    }

    function unmarkFolders() {
        $('#foldersContainer').children().removeClass('active');
    }

    function decrementUnreadCount(folderId) {
        var folderEl = getFolderEl(folderId);
        var unread = folderEl.attr('unread');
        unread = unread - 1 > 0 ? unread - 1 : 0;
        setCount(folderEl, unread);

        if (folderId == TMMail.sysfolders.inbox.id) {
            setTpUnreadMessagesCount(unread);
        }
    }

    function setCount(folderEl, count) {
        folderEl.attr('unread', count);
        var countText = count ? count : "";
        folderEl.find('.unread').text(countText);
    }

    function getFolderEl(folderId) {
        return $('#foldersContainer').children('[folderid=' + folderId + ']');
    }

    function getFolders() {
        return folders;
    }

    return {
        init: init,
        markFolder: markFolder,
        getMarkedFolder: getMarkedFolder,

        unmarkFolders: unmarkFolders,
        decrementUnreadCount: decrementUnreadCount,
        getFolders: getFolders
    };
})(jQuery);