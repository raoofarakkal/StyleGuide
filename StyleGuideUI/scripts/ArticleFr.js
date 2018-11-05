
var currentViewIndex = -1;

$(document).ready(function () {
    var mControlNames;
    var mControls = getControls();
    if (mControls.length > 0) {
        setArticleBodyContentFromArray(mControls);
    } else {
        //setArticleBodyContent(document.getElementById('divTest').innerHTML); //testing purpose only
        setArticleBodyContent(window.parent.tinyMCE.activeEditor.getContent());
    }
});

function getControls() {
    var mControls = [];
    var mControlNames = [];
    try {
        var mTemp = getUrlParaCaseSensitive()['Controls'];
        if (mTemp) {
            //Controls=<ControlName>;<DisplayName>;<Type of Control>,<ControlName>;<DisplayName>;<Type of Control>,...     |||||| Type of Control = html|TinyMCE
            //example: Controls=historic;historic title;html,body1;Main Story;tinyMCE,body2;Summary Data;html
            mControlNames = mTemp.split(',');
        }
        for (i = 0; i < mControlNames.length; i++) {
            if (mControlNames[i]) {
                var mCtrlName = mControlNames[i].split(';')[0];
                var mDispName = mControlNames[i].split(';')[1];
                var mCtrlType = mControlNames[i].split(';')[2];
                var mtype;
                var mCtrlContent="";
                if (mCtrlType && mCtrlType.toLowerCase() == "tinymce") {
                    mtype = "tinymce";
                    try{
                        mCtrlContent = window.parent.tinyMCE.get(mCtrlName).getContent();
                    } catch (ex) { }
                } else {
                    mtype = "html";
                    try{
                        mCtrlContent = window.parent.$('#' + mCtrlName).val();
                    } catch (ex) { }
                }
                mControls.push({ name: mCtrlName, dispName: mDispName, type:mtype, content: mCtrlContent })
            }
        }
    }catch(ex){}
    return mControls;
}

function UpdateCaller() {
    var Elms = $('.BODYSGTEXT')
    var mIsCalledFromTinyMCE = false;
    if (Elms != null) {
        if (Elms.length > 0) {
            var lookFor = new Array(Elms.length);
            var replaceWith = new Array(Elms.length);
            for (i = 0; i < Elms.length; i++) {
                $(Elms[i]).removeAttr('style');
                $(Elms[i]).removeAttr('id');
                $(Elms[i]).removeAttr('title');
                $(Elms[i]).removeAttr('class');
                lookFor[i] = Elms[i].outerHTML;
                replaceWith[i] = Elms[i].innerHTML;
            }

            for (i = 0; i < lookFor.length; i++) {
                var changed = $('#dvArticleBodyWorkArea').html().replace(lookFor[i], replaceWith[i]);
                //$('#dvArticleBodyWorkArea').html(changed);
                document.getElementById('dvArticleBodyWorkArea').innerHTML = changed;
            }

            var mControls = getControls();
            if (mControls.length > 0) {
                for (i = 0; i < (mControls.length) ; i++) {
                    var mContent = $("#dvArticleBodyWorkArea #SG_" + mControls[i].name + "_content").html();
                    if (mControls[i].type == "tinymce") {
                        try{
                            window.parent.tinyMCE.get(mControls[i].name).setContent(mContent);
                        }catch(ex){}
                    } else {
                        try{
                            window.parent.$("#" + mControls[i].name).val(mContent)
                        }catch(ex){}
                    }
                }
            } else {
                var mContent = '';
                mContent = $("#dvArticleBodyWorkArea").html();
                window.parent.tinyMCE.activeEditor.setContent(mContent);
                mIsCalledFromTinyMCE = true;
            }

            //content = $('#dvArticleBodyWorkArea').html();
            //window.parent.tinyMCE.activeEditor.setContent(content);
            ////window.parent.tinyMCE.activeEditor.setContent(content, { format: 'raw' });
            ////window.parent.tinyMCE.activeEditor.dom.setHTML(window.parent.tinyMCE.activeEditor.id, content);
        }
    }
    closeSg(mIsCalledFromTinyMCE);
    //window.parent.tinyMCE.activeEditor.windowManager.close(this);
}

function closeSg(pFromTinyMCE) {
    if (pFromTinyMCE) {
        window.parent.tinyMCE.activeEditor.windowManager.close(this);
    } else {
        window.parent.clearPopUp();
    }
}

function Busy(busy) {
    if (busy) {
        $('#AjaxLoader').show();
        $('#dvFindPane').hide();
        $('#dvNoSgEntFound').hide();
    } else {
        $('#dvFindPane').show();
        $('#AjaxLoader').hide();
        $('#dvNoSgEntFound').hide();
    }
}

function setArticleBodyContentFromArray(pArray) {
    var mSgScripts = "";
    var mContent = "";
    var mContentSG = "";
    var mSep = "";
    for (i = 0; i < (pArray.length) ; i++) {
        if (pArray[i].content) {
            mContent += mSep;
            mContent += "<div id='SG_" + pArray[i].name + "' title='" + pArray[i].name + "'>";
            mContent += "<div id='SG_" + pArray[i].name + "_name' style='padding:5px;background-color:#6186BD;color:White;border:1px solid #6186BD;font-weight:bold'></div>";
            mSgScripts += "\n$('#SG_" + pArray[i].name + "_name').html(unescape('" + pArray[i].dispName.toUpperCase() + "'));";
            mContent += "<div id='SG_" + pArray[i].name + "_content'>" + pArray[i].content + "</div>";
            mContent += "</div>";
            mContentSG += " " + pArray[i].content;
            mSep = "<div style='height:10px;'></div>";
        }
    }
    $("#dvArticleBodyWorkAreaScripts").html(mSgScripts);
    setArticleBodyContent(mContent, mContentSG);
}

function setArticleBodyContent(pContentWa,pContent4SG) {
    Busy(true);
    currentViewIndex = -1;
    var elemWa = document.getElementById('dvArticleBodyWorkArea');
    elemWa.innerHTML = pContentWa;
    if (!pContent4SG) {
        pContent4SG = pContentWa;
    }
    if (elemWa.innerText.length > 0) {
        $.ajax(
            {
                type: "POST"
                , async: true
                , url: "articlefr.ashx"
                , data: "cmd=getStyleGuideEntities&content=" + escape(pContent4SG)
                , error: function (xhr, ajaxOptions, thrownError) {
                    currentViewIndex = -1;
                    //alert('Error on request. Generica handler articlefr');
                    document.write('Error: articlefr.ashx ' + xhr.status + ' ' + thrownError + ' ' + xhr.responseText);
                    //alert('Error: articlefr.ashx' + xhr.status + ' ' + thrownError + ' ' + xhr.responseText);
                }
                , success: function (response) {
                    //alert(response);
                    Busy(false);
                    var JSON = null;
                    currentViewIndex = -1;
                    try {
                        JSON = jQuery.parseJSON(response);
                        if (JSON != undefined) {
                            process(JSON);
                        }
                    }
                    catch (e) {
                    }
                }
            }
        );
    } else {
        Busy(false);
        alert('No Style Guide text found');
    }
}

var Processed = new Array();
function process(Json) {
    Processed = new Array();
    if (Json.TotalEntities > 0) {
        for (i = 0; i < Json.TotalEntities; i++) {


            var suggestions = new Array();
            for (isg = 0; isg < Json.Entities[i].TotalSuggestions; isg++) {
                suggestions[isg] = $.trim($("<div/>").html(Json.Entities[i].Suggestions[isg]).html());
            }

            for (ii = 0; ii < Json.Entities[i].TotalSuspects; ii++) {
                var suspects = $.trim($("<div/>").html(Json.Entities[i].Suspects[ii]).html());
                if (!isPorcessed(suspects.toLowerCase())) {
                    var change2 = HtmlHighlight($('#dvArticleBodyWorkArea').html(), suspects, suggestions, '<span class=\'BODYSGTEXT\' title=\'' + Json.Entities[i].ID + '\' style=\'background-color:yellow;\'>', '</span>');
                    //$('#dvArticleBodyWorkArea').html(change2); //commented because sometimes its throw this error 'Could not complete the operation due to error 80020101'
                    document.getElementById('dvArticleBodyWorkArea').innerHTML = change2;

                    Processed[Processed.length] = suspects.toLowerCase();
                }
            }


        }
        currentViewIndex = 0;
        FocusSg();
        FillSuspectList(Json);
        eval($("#dvArticleBodyWorkAreaScripts").html());
        //setTimeout(eval($("#dvArticleBodyWorkAreaScripts").html()), 1000);
    } else {
        alert('No StyleGuide text found.');
    }

    var mHtml = $('#dvArticleBodyWorkArea').html();
    if (mHtml.indexOf('<span class=\'BODYSGTEXT\'') == -1) {
        $('#dvFindPane').hide();
        $('#AjaxLoader').hide();
        $('#dvNoSgEntFound').show();
    }
}

function isPorcessed(word) {
    var ret = false;
    for (var i = Processed.length - 1; i >= 0; i--) {
        if (Processed[i] === word) {
            ret = true;
            break;
        }
    }
    return ret;
}

var SuspectList = new Array();
var SuspectListC = new Array();

function isExistInSuspectList(suspect) {
    var ret = -1;
    if(SuspectList.length>0){
        for (var i = SuspectList.length - 1; i >= 0; i--) {
            if (SuspectList[i] === suspect) {
                ret = i;
                break;
            }
        }
    }
    return ret;
}

function FillSuspectList(Json) {
    $('#divEntitesFound').html('');
    var html = '';
    SuspectList = new Array();
    SuspectListC = new Array();
    var Elms = getBodySgTextElements();
    if (Elms != null) {
        if (Elms.length > 0) {
            for (var iEl = 0; iEl < Elms.length; iEl++) {
                iSl = isExistInSuspectList($(Elms[iEl]).text());
                if (iSl == -1) {
                    var temp = SuspectList.length;
                    SuspectList[temp] = $(Elms[iEl]).text();
                    SuspectListC[temp] = 1;
                } else {
                    SuspectListC[iSl]++;
                }
            }
            for (var iSl = 0; iSl < SuspectList.length; iSl++) {
                var NoOfOc = SuspectListC[iSl];
                if (NoOfOc > 1) {
                    html += "<div style='cursor:pointer;' title='Found " + SuspectListC[iSl] + " times. Click here to locate' onclick='locate(this);' class='EntitesFoundItem' >" + SuspectList[iSl] + "</div>";
                } else {
                    html += "<div style='cursor:pointer;' title='Click here to locate' onclick='locate(this);' class='EntitesFoundItem' >" + SuspectList[iSl] + "</div>";
                }
            }
        }
    }

    $('#divEntitesFound').html(html);
}

function locate(obj) {
    var found = false;
    var Elms = getBodySgTextElements();//$('.BODYSGTEXT')
    if (Elms != null) {
        if (Elms.length > 0) {
            currentViewIndex++;
            var startIdx = currentViewIndex;
            if (startIdx >= Elms.length) {
                startIdx = 0;
            }
            for (var iEl = startIdx; iEl < Elms.length; iEl++) {
                if ($(Elms[iEl]).text() == $(obj).text()) {
                    currentViewIndex = iEl;
                    found = true;
                    FocusSg();
                    break;
                }
            }
            if (!found) {
                for (var iEl = 0; iEl < Elms.length; iEl++) {
                    if ($(Elms[iEl]).text() == $(obj).text()) {
                        currentViewIndex = iEl;
                        FocusSg();
                        break;
                    }
                }
            }
        }
    }


}

function getBodySgTextElements() {
    var Elms = $('.BODYSGTEXT')
    if (Elms != null) {
        if (Elms.length > 0) {
            for (var i = (Elms.length - 1) ; i >= 0; i--) {
                if (Elms[i].title == '') {
                    Elms.splice(i, 1);
                }
            }
        }
    }
    return Elms;
}

function FocusSg() {
    var Elms = getBodySgTextElements();//$('.BODYSGTEXT')
    if (Elms != null) {
        if (Elms.length > 0) {
            for (var i = 0; i < Elms.length; i++) {
                $(Elms[i]).css('background-color', 'yellow');
                //$(Elms[i]).css('border-top', '');
                $(Elms[i]).css('border-bottom', '');
                $(Elms[i]).css('line-height', '');

                $(Elms[i]).attr("id", "BODY_SG_" + i);
            }
            if (currentViewIndex >= 0 && currentViewIndex < Elms.length) {
                //$(Elms[currentViewIndex]).css('border-top', '3px solid red');
                $(Elms[currentViewIndex]).css('border-bottom', '3px solid red');
                $(Elms[currentViewIndex]).css('line-height', '25px');

                document.getElementById('dvArticleBodyWorkArea').scrollTop = Elms[currentViewIndex].offsetTop;


                setRightPane(Elms[currentViewIndex]);
            }
        }
    }
}

function nextSg() {
    var Elms = getBodySgTextElements();
    if (Elms != null) {
        if (Elms.length > 0) {
            if (currentViewIndex < (Elms.length - 1)) {
                currentViewIndex++;
            }
            FocusSg();
        }
    }
}

function PreviousSg() {
    var Elms = getBodySgTextElements();
    if (Elms != null) {
        if (Elms.length > 0) {
            if (currentViewIndex > 0) {
                currentViewIndex--;
            }
            FocusSg();
        }
    }
}

function setRightPane(elem) {
    var tbEntity = document.getElementById('tbEntity');
    tbEntity.value = elem.innerText;
    tbEntity.title = elem.title;

    var hfEntityId = document.getElementById('hfEntityId');
    hfEntityId.value = elem.title;

    var hfBodySgTextId = document.getElementById('hfBodySgTextId');
    hfBodySgTextId.value = elem.id;

    getEntity(hfEntityId.value, elem.innerText);

}


function getEntity(EntityId, sourceWord) {
    Busy(true);
    $.ajax(
        {
            type: "POST"
            , async: true
            , url: "articlefr.ashx"
            , data: "cmd=getEntity&id=" + EntityId
            , error: function (xhr, ajaxOptions, thrownError) {
                //alert('Error on request. Generica handler articlefr');
                document.write('Error: articlefr.ashx ' + xhr.status + ' ' + thrownError + ' ' + xhr.responseText);
                //alert('Error: articlefr.ashx ' + xhr.status + ' ' + thrownError + ' ' + xhr.responseText);
            }
            , success: function (response) {
                //alert(response);
                document.getElementById('tbNotes').value = "";
                document.getElementById('tbReplaceEntity').value = sourceWord;

                var JSON = null;
                try {
                    try {
                        JSON = jQuery.parseJSON(response);
                    } catch (e) {
                        response = response.replace(/\r/g, '\\r');
                        response = response.replace(/\n/g, '\\n');
                        JSON = jQuery.parseJSON(response);
                    }
                    if (JSON != undefined) {
                        document.getElementById('tbEntity').value = sourceWord;
                        document.getElementById('tbNotes').value = JSON.Notes;
                        //if (JSON.Name != sourceWord) {
                        //    document.getElementById('tbReplaceEntity').value = JSON.Name;
                        //} else {
                        document.getElementById('tbReplaceEntity').value = sourceWord;
                        //}

                        var lbSuggestions = document.getElementById('lbSuggestions')
                        lbSuggestions.options.length = 0;
                        var Suggestions;
                        for (var i = 0; i < JSON.TotalSuggestions; i++) {
                            Suggestions = document.createElement("Option");
                            var sugg = JSON.Suggestions[i];
                            Suggestions.text = sugg;
                            Suggestions.value = sugg; //elem.id;
                            lbSuggestions.add(Suggestions);
                        }
                        var comma = "";
                        document.getElementById('lbSuggestions').title = "Suspects: ";
                        for (var i = 0; i < JSON.TotalSuspects; i++) {
                            document.getElementById('lbSuggestions').title += comma + " " + JSON.Suspects[i];
                            comma = ","
                        }
                            //Suspects

                        JSON.Susp
                        Busy(false);
                    } else {
                        alert('Invalid JSON');
                        Busy(false);
                    }
                }
                catch (e) {
                    alert('Unable to parse JSON. ' + e.message);
                    Busy(false);
                }
            }
        }
    );
}

function ReadyToReplace(lb) {
    if (lb[lb.selectedIndex].text != "no suggestions found") {
        document.getElementById('tbReplaceEntity').value = lb[lb.selectedIndex].text;
        return true;
    } else {
        return false;
    }
}

function replace() {
    var tbReplaceEntity = document.getElementById('tbReplaceEntity');
    var hfBodySgTextId = document.getElementById('hfBodySgTextId');

    var BodySgText = document.getElementById(hfBodySgTextId.value)
    BodySgText.innerHTML = tbReplaceEntity.value;
    $(BodySgText).removeAttr('style');
    var id = $(BodySgText).attr("id");
    $(BodySgText).attr("id", id.replace('BODY_SG_','BODY_SG_R_'));
    $(BodySgText).removeAttr('title');
    $(BodySgText).attr("style", "border-bottom:1px dashed green;");

    tbReplaceEntity.value = "";
    document.getElementById('tbNotes').value = '';
    document.getElementById('tbEntity').value = '';
    hfBodySgTextId.value = '';
    document.getElementById('hfEntityId').value = '';


    FocusSg();
}

/* BEGIN: StyleGuide Check */
function StyleGuideCheck(ctrlName) {
    if (tinyMCE.get(ctrlName).isDirty()) {
        if (jConfirm('WARNING!', 'Content was modified, would you like to do Style Guide Check now?')) {
            tinyMCE.get(ctrlName).execCommand('mceStyleGuide');
            tinyMCE.get(ctrlName).isNotDirty = true;
        } else { return true; }
    } else { return true; }
}
/* END: StyleGuide Check */