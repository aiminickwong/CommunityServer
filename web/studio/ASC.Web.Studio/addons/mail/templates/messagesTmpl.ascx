﻿<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" %>
<%@ Assembly Name="ASC.Web.Mail" %>
<%@ Import Namespace="ASC.Web.Mail.Resources" %>

<script id="messagesTmpl" type="text/x-jquery-tmpl">
<table class="messages" anchor="new"{{if has_next}} has_next="true"{{/if}}{{if has_prev}} has_prev="true"{{/if}}>
  <tbody>
    {{tmpl(messages, { htmlEncode : $item.htmlEncode, getTagsToDisplay: $item.getTagsToDisplay, getCountTagsInMore: $item.getCountTagsInMore }) "messageItemTmpl"}}
  </tbody>
</table>
</script>

<script id="messageItemTmpl" type="text/x-jquery-tmpl">
<tr data_id="${id}" date="${date}" chain_date="${chainDate}" fromCRM="${isFromCRM}" fromTL="${isFromTL}" class="row{{if isNew}} new{{/if}}"{{if restoreFolderId}} PrevFolderId="${restoreFolderId}"{{/if}}>
  <td class="checkbox"><input type="checkbox" data_id="${id}" title="<%: MailResource.Select %>"></input></td>
  <td class="importance"><i class="icon-{{if important!=true}}un{{/if}}important" 
      title="{{if important}}<%: MailScriptResource.ImportantLabel %>{{else}}<%: MailScriptResource.NotImportantLabel %>{{/if}}"></i>
  </td>
  <td class="from">
    <a href="${anchor}">
      <span class="author" title="${author!='' ? author : sender}" email="${sender}">
        {{if author=='' && sender==''}}<%: MailResource.NoAddress %>{{else}}${author!='' ? author : sender}{{/if}}
      </span>
      {{if chainLength > 1}}<span class="chain-counter">(${chainLength})</span>{{/if}}
    </a>
  </td>
  <td class="subject" title="{{if subject==''}}<%: MailResource.NoSubject %>{{else}}${$item.htmlEncode(subject)}{{/if}}">
    <a href="${anchor}" _tags="{{each tagIds}}{{if $index>0}},{{/if}}${$value}{{/each}}">
      {{tmpl($item.getTagsToDisplay(tags), { htmlEncode : $item.htmlEncode }) "messageItemTagsTmpl"}}
      {{tmpl($item.getCountTagsInMore(tags)) "messageItemTagsMoreTmpl"}}
      <span class="subject-text">{{if subject==''}}<%: MailResource.NoSubject %>{{else}}${$item.htmlEncode(subject)}{{/if}}</span>
    </a>
  </td>
  <td class="attachment"><a href="${anchor}">{{if hasAttachments==true}}<i class="icon-attachment"></i>{{/if}}</a></td>
  <td class="date"><a href="${anchor}">{{if isToday}}<%: MailResource.TodayLabel %>{{else isYesterday}}<%: MailResource.YesterdayLabel %>{{else}}${displayDate}{{/if}}</a></td>
  <td class="time"><a href="${anchor}">${displayTime}</a></td>
</tr>
</script>

<script id="messageItemTagsTmpl" type="text/x-jquery-tmpl">
  <span labelid="${id}" class="tag tagArrow tag${style}" title="${$item.htmlEncode(name)}">${$item.htmlEncode(short_name)}</span>
</script>

<script id="messageItemTagsMoreTmpl" type="text/x-jquery-tmpl">
  {{if $data > 0}}<span class="more-tags link dotted">${"<%: MailScriptResource.More %>".replace('%1', $data)}</span>{{/if}}
</script>