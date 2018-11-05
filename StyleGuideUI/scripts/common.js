

//FUNCTION getUrlPara()
//USAGE:
//var UrlPara = getUrlPara();
//var IP = UrlPara["ip"]; // "127.0.0.1"

function getUrlPara() {
    var para = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        if (hash.length > 1) {
            para.push(hash[0].toLowerCase());
            para[hash[0].toLowerCase()] = hash[1].toLowerCase();
        } else {
            para = undefined;
            break;
        }
    }
    return para;
}

function getUrlParaCaseSensitive() {
    var para = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        if (hash.length > 1) {
            para.push(hash[0]);
            para[hash[0]] = hash[1];
        } else {
            para = undefined;
            break;
        }
    }
    return para;
}

//HTML Replace
//http://stackoverflow.com/questions/857079/search-text-in-other-text-and-highlight-it-using-javascript
//http://www.nsftools.com/misc/SearchAndHighlight.htm
//view-source:http://www.nsftools.com/misc/SearchAndHighlight.htm
function HtmlHighlight(bodyText, searchTerm,skipTermArray, highlightStartTag, highlightEndTag) {
    // the highlightStartTag and highlightEndTag parameters are optional
    if ((!highlightStartTag) || (!highlightEndTag)) {
        highlightStartTag = "<font style='color:blue; background-color:yellow;'>";
        highlightEndTag = "</font>";
    }

    // find all occurences of the search term in the given text,
    // and add some "highlight" tags to them (we're not using a
    // regular expression search, because we want to filter out
    // matches that occur within HTML tags and script blocks, so
    // we have to do a little extra validation)
    var newText = "";
    var i = -1;
    var lcSearchTerm = searchTerm.toLowerCase();
    var lcBodyText = bodyText.toLowerCase();

    while (bodyText.length > 0) {
        var prv_i = i;
        i = lcBodyText.indexOf(lcSearchTerm, i + 1);
        if (i < 0) {
            newText += bodyText;
            bodyText = "";
        } else {
            // skip anything inside an HTML tag
            if (bodyText.lastIndexOf(">", i) >= bodyText.lastIndexOf("<", i)) {
                // skip anything inside a <script> block
                if (lcBodyText.lastIndexOf("/script>", i) >= lcBodyText.lastIndexOf("<script", i)) {

                    //skip SkipTermArray
                    var skip = -1;
                    if (skipTermArray != null) {
                        if (skipTermArray.length > 0) {
                            for (skipIdx = 0; skipIdx < skipTermArray.length; skipIdx++) {
                                var skipTermArrayIdx = lcBodyText.indexOf(skipTermArray[skipIdx], prv_i + 1)
                                if (i == skipTermArrayIdx) {
                                    skip = skipIdx;
                                    break;
                                }
                            }
                        }
                    }

                    if (skip == -1) {

                        if(IsSearchTermValidForHighlighting(bodyText, searchTerm, i)){
                            newText += bodyText.substring(0, i) + highlightStartTag + bodyText.substr(i, searchTerm.length) + highlightEndTag;
                        }else{
                            newText += bodyText.substring(0, i) + bodyText.substr(i, searchTerm.length);
                        }

                        bodyText = bodyText.substr(i + searchTerm.length);
                        lcBodyText = bodyText.toLowerCase();

                        i = -1;
                    }

                }
            }
        }
    }
    return newText;
}



function IsSearchTermValidForHighlighting(bodyText, searchTerm, index) {
    var mRet = false;
    var leftCharOk = false;
    var rightCharOk = false;
    var s = " .;,\r\n'\"<>";
    if (index >= 0) {
        //check left side of searchTerm is valid char in bodyText
        if(index== 0){
            leftCharOk=true;
        }else{
            if (s.indexOf(bodyText.substr(index - 1, 1)) != -1) {
                leftCharOk =true;
            }
        }
        //check right side of searchTerm is valid char in bodyText
        
        if(bodyText.length == (index+searchTerm.length)){
            rightCharOk=true;
        }else{
            if(s.indexOf(bodyText.substr(index + searchTerm.length, 1)) != -1){
                rightCharOk =true;
            }
        }

        mRet = leftCharOk && rightCharOk;
    }

    return mRet;
    
}


//function validText(chr) {
//    if(chr.length >0){
//        var s = " .;,\r\n'\"<>";
//        if (s.indexOf(chr) != -1) {
//            return true;
//        } else {
//            return false;
//        }
//    }else{
//        return true;
//    }
//}


//function HtmlReplace2(bodyText, searchTerm, highlightStartTag, highlightEndTag) {
//    // the highlightStartTag and highlightEndTag parameters are optional
//    if ((!highlightStartTag) || (!highlightEndTag)) {
//        highlightStartTag = "<font style='color:blue; background-color:yellow;'>";
//        highlightEndTag = "</font>";
//    }

//    // find all occurences of the search term in the given text,
//    // and add some "highlight" tags to them (we're not using a
//    // regular expression search, because we want to filter out
//    // matches that occur within HTML tags and script blocks, so
//    // we have to do a little extra validation)
//    var newText = "";
//    var i = -1;
//    var lcSearchTerm = searchTerm.toLowerCase();
//    var lcBodyText = bodyText.toLowerCase();

//    while (bodyText.length > 0) {
//        i = lcBodyText.indexOf(lcSearchTerm, i + 1);
//        if (i < 0) {
//            newText += bodyText;
//            bodyText = "";
//        } else {
//            // skip anything inside an HTML tag
//            if (bodyText.lastIndexOf(">", i) >= bodyText.lastIndexOf("<", i)) {
//                // skip anything inside a <script> block
//                if (lcBodyText.lastIndexOf("/script>", i) >= lcBodyText.lastIndexOf("<script", i)) {

//                    if (validText(bodyText.substr(i + searchTerm.length, 1))) {
//                        if (validText(bodyText.substr(i - 1, 1))) {
//                            newText += bodyText.substring(0, i) + highlightStartTag + bodyText.substr(i, searchTerm.length) + highlightEndTag;
//                        } else {
//                            newText += bodyText.substring(0, i) + bodyText.substr(i, searchTerm.length);
//                        }
//                    } else {
//                        newText += bodyText.substring(0, i) + bodyText.substr(i, searchTerm.length);
//                    }

//                    bodyText = bodyText.substr(i + searchTerm.length);
//                    lcBodyText = bodyText.toLowerCase();

//                    i = -1;

//                }
//            }
//        }
//    }
//    return newText;
//}

//function HtmlReplace1(bodyText, searchTerm, highlightStartTag, highlightEndTag) {
//    // the highlightStartTag and highlightEndTag parameters are optional
//    if ((!highlightStartTag) || (!highlightEndTag)) {
//        highlightStartTag = "<font style='color:blue; background-color:yellow;'>";
//        highlightEndTag = "</font>";
//    }

//    // find all occurences of the search term in the given text,
//    // and add some "highlight" tags to them (we're not using a
//    // regular expression search, because we want to filter out
//    // matches that occur within HTML tags and script blocks, so
//    // we have to do a little extra validation)
//    var newText = "";
//    var i = -1;
//    var lcSearchTerm = searchTerm.toLowerCase();
//    var lcBodyText = bodyText.toLowerCase();

//    while (bodyText.length > 0) {
//        i = lcBodyText.indexOf(lcSearchTerm, i + 1);
//        if (i < 0) {
//            newText += bodyText;
//            bodyText = "";
//        } else {
//            // skip anything inside an HTML tag
//            if (bodyText.lastIndexOf(">", i) >= bodyText.lastIndexOf("<", i)) {
//                // skip anything inside a <script> block
//                if (lcBodyText.lastIndexOf("/script>", i) >= lcBodyText.lastIndexOf("<script", i)) {
//                    newText += bodyText.substring(0, i) + highlightStartTag + bodyText.substr(i, searchTerm.length) + highlightEndTag;
//                    bodyText = bodyText.substr(i + searchTerm.length);
//                    lcBodyText = bodyText.toLowerCase();
//                    i = -1;
//                }
//            }
//        }
//    }
//    return newText;
//}
