<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="StyleGuideTest._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="scripts/TinyMCE/349/tiny_mce.js"></script>
    <script src="scripts/jquery-1.8.0.min.js"></script>
    <style type="text/css">
        #body1 {
            height: 400px;
            width: 400px;
        }
        #text1 {
            width: 400px;
        }
        #body2 {
            width: 400px;
            height: 88px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(

            function () {

                tinyMCE.init({ mode: "textareas", theme: "advanced", editor_selector: "mceEditor", plugins: "StyleGuide,inlinepopups", theme_advanced_buttons1: "StyleGuide" });
                setTimeout('autoload()', 100);
            }

        );


        var win;
        var content;

        function autoload() {
            reset1('body1');
        }

        function reset1(elemID) {

            var test = document.getElementById('divTest');
            tinyMCE.getInstanceById('body1').setContent(test.innerHTML);
        }

        function openSg() {
            var ifrmSg = $("#ifrmSg");
            ifrmSg.attr('src', '/StyleGuideUI/articlefr.aspx?Controls=historic;historic title;html,body1;Main Story;tinyMCE,body2;Summary Data;html');
            //ifrmSg.attr('src', '/StyleGuideUI/articlefr.aspx');
        }

    </script>
</head>
<body style="font-size:12pt;">
    <form id="form1" runat="server">
    <div>
        <input type="text" value="historic and aboriginal" id="historic" />
        <br />
        <br />
        <textarea id="body1" class="mceEditor">sdfsdfsdf</textarea>
        <br />
        <br />
        <textarea id="body2">here is historic and aboriginal for style guide test</textarea>
    </div>
    <div style="float:left;padding:5px; border:1px solid gray;margin-right:5px; cursor:pointer;color:blue;" onclick="reset1('body1')">RESET</div>    
    <div style="float:left;padding:5px;border:1px solid gray; cursor:pointer;color:blue;" onclick="openSg()">Style Guide</div>        

<div id="divTest" style="display:none;visibility:hidden;">




<p>
    Canada's colonial reality is now in the spotlight, as
    <a class="internallink" href="http://idlenomore.ca/" target="_blank">
        Idle No More
    </a>
    protests voice the struggles of indigenous people against sustained political and economic oppression.
</p>
<p>
    Thousands are joining
    <a class="internallink" href="http://www.j11action.com/" target="_blank">
        historic actions
    </a>
    to call for fundamental changes in Canada's relations to aboriginal people.
</p>
<p>
    Central to Idle No More are longstanding indigenous demands for justice around land rights, economic resources and self-determination
    that rest at the heart of both Canada's history and future.
</p>
<p>
    <strong>
        Winter hunger strike
    </strong>
</p>
<p>
    <a class="internallink" href="http://apihtawikosisan.com/2012/12/10/idle-no-more/" target="_blank">
        Idle No More protests
    </a>
    first took place across Canada to mark International Human Rights Day on December 10, 2012.
</p>
<%--<table class="Skyscrapper_Body" style="width: 250px; height: 50; float: right; background-color: #fb9d04; border-style: solid; border-color: white; border-collapse: collapse;"
border="10">
    <tbody>
        <tr>
            <td>
                <p>
                    <span style="font-family: arial, helvetica, sans-serif;">
                        <span style="font-size: 10pt; color: white;">
                            <strong>
                                "Politics surrounding aboriginal struggles in Canada are different after the historic action by Chief Spence, a catalyst
                                for the ongoing Idle No More grassroots protest movement.
                            </strong>
                        </span>
                        <span style="font-size: 10pt; color: white;">
                            <strong>
                                "
                            </strong>
                        </span>
                    </span>
                </p>
            </td>
        </tr>
    </tbody>
</table>--%>
<p>
    Early the next morning
    <a class="internallink" href="https://twitter.com/chieftheresa" target="_blank">
        Chief Theresa Spence
    </a>
    , from Attawapiskat First Nation in northern Ontario, began a hunger strike in a tepee on Victoria Island, just minutes
    away from Canada's Parliament in Ottawa.
</p>
<p>
    After surviving on only broth and medicinal tea for over six weeks, Chief Spence ended the political fast after
    <a class="internallink" href="http://quelquesnotes.wordpress.com/2013/01/11/110113/) " target="_blank">
        inspiring major protests
    </a>
    across Canada and parallel
    <a class="internallink" href="http://www.cbc.ca/news/canada/manitoba/story/2012/12/31/mb-hunger-strike-elder-raymond-robinson-manitoba.html"
    target="_blank">
        hunger strikes
    </a>
    in support.
</p>
<p>
    <a class="internallink" href="http://aptn.ca/pages/news/2013/01/23/chief-spence-admitted-to-hospital-remains-on-iv/" target="_blank">
        Chief Spence was hospitalised
    </a>
    &nbsp;hours after the strike ended, spending a day and a half under medical supervision for dehydration and deterioration
    resulting from 44 days without food.
</p>
<p>
    Politics surrounding aboriginal struggles in Canada are different after the historic action by Chief Spence, a catalyst
    for the ongoing Idle No More grassroots movement.
</p>
<p>
    Canada's major opposition parties in Ottawa and the Assembly of First Nations (AFN) have
    <a class="internallink" href="http://www.cbc.ca/news/politics/inside-politics-blog/2013/01/full-text-of-declaration-that-will-end-attawapiskat-chiefs-six-week-protest.html"
    target="_blank">
        co-signed a joint declaration
    </a>
    &nbsp;in response, outlining "the need for fundamental change in the relationship of First Nations and the Crown", a text
    nearly unimaginable prior to Idle No More.
</p>
<p>
    Key to the declaration is the symbolic mention of the Crown, also highlighted by Chief Spence during the hunger strike in
    calls to include the Governor General, the representative of the British Crown in Canada, in any talks on aboriginal-Canada
    relations. A demand pointing clearly to the
    <a class="internallink" href="http://indigenousfoundations.arts.ubc.ca/home/government-policy/royal-proclamation-1763.html"
    target="_blank">
        Royal Proclamation of 1763
    </a>
    , a colonial document of persisting importance, commonly referenced in indigenous land struggles and legal negotiations,
    that forbids the colonial settlement of territories or utilisation of resources without the clear consent of aboriginal peoples.
</p>
<p>
    <span lang="EN-CA">
        Today, the historic importance of Chief Spence's hunger strike is clear, as political energy around Idle No More builds.
        Another
        <a class="internallink" href="http://www.mediacoop.ca/story/map-january-28th-idle-no-more-events/15964" target="_blank">
            national day of action
        </a>
        involved more than 30 cities in Canada yesterday,
        <a class="internallink" href="http://www.cbc.ca/news/politics/story/2013/01/28/parliament-hill-ottawa-setup.html" target="_blank">
            including a rally
        </a>
        outside Parliament in Ottawa.
    </span>
</p>
<p>
    <span lang="EN-CA">
        Still the Conservative government refus
    </span>
    es to engage directly with Idle No More, instead holding
    <a class="internallink" href="http://www.cbc.ca/news/politics/story/2013/01/10/pol-first-nations-chiefs-day-before-pm-meeting.html"
    target="_blank">
        discordant talks
    </a>
    with officials from the Assembly of First Nations (AFN), a political body strongly tied to the Canadian state, not historically
    involved in aboriginal protest movements.
</p>
<%--<table style="border-collapse: collapse; width: 33px;" border="0" cellspacing="0" cellpadding="0" align="right" bordercolor="white">
    <tbody>
        <tr>
            <td>
                <img src="/mritems/imagecache/218/330/mritems/Images/2013/1/29/2013129145029895734_20.jpg" border="0" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <span style="font-family: Verdana; font-size: xx-small;">
                    <strong>
                        Indigenous activist group Idle No More has launched fresh protests in Canada recently [Al Jazeera/Thien V]
                    </strong>
                </span>
            </td>
        </tr>
    </tbody>
</table>--%>
<p>
    "Decision-making in the name of the AFN is not designed for fighting government, but merely consulting with government,"
    writes
    <a class="internallink" href="http://www.mediacoop.ca/story/idle-no-more-effective-voice-indigenous-peoples/15603" target="_blank">
        Arthur Manuel
    </a>
    , spokesperson for Defenders of the Land network from the Secwepemc Nation, openly critiquing talks with the Conservative
    government until clear conditions on respecting treaty rights are outlined.
</p>
<p>
    "There is basically a fundamental change that Harper must make before 'engaging' with Harper could be useful," continues
    Manuel. "The Harper government does not recognise Aboriginal and Treaty Rights on the ground. Indigenous Peoples believe
    in Aboriginal and Treaty Rights on the ground. That is the fundamental difference."
</p>
<p>
    <strong>
        Canada's broken treaties
    </strong>
</p>
<p>
    Beyond contemporary extremes in inequality for aboriginal peoples in Canada, increasingly labelled "
    <a class="internallink" href="http://thetyee.ca/Opinion/2013/01/02/Idle-No-More/" target="_blank">
        Canadian apartheid
    </a>
    ", Idle No More actions sound the alarm on questions of colonial injustice that reach to the political depths of Canada's
    existence and national character.
</p>
<p>
    Today, most of Canada falls under signed treaties, agreements between First Nations and Canadian settler society, outlining
    bilateral obligations in regards to political relations and land rights. "In places where treaties are in effect, every building,
    business, road, government, or other activity is made possible by a treaty," outlines a recent post on
    <a class="internallink" href="http://www.mediacoop.ca/blog/dru/15600" target="_blank">
        The Media Coop
    </a>
    .
</p>
<p>
    Central to the Idle No More movement is a call for all Canadians to respect
    <a class="internallink" href="http://briarpatchmagazine.com/articles/view/settler-treaty-rights/" target="_blank">
        treaty rights
    </a>
    , highlighting the constant refusal to acknowledge treaty obligations by successive Canadian governments over the past century.
</p>
<%--<table style="background-color: #fb9d04;" border="0" align="right">
    <tbody>
        <tr>
            <td>
                <div class="mceVideoBox" style="width: 330; height: 186; float: Right;">
                    <div id="bc_2067621150001" style="width: 330; height: 186;">
                        <!-- -->
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <p style="text-align: center;">
                    &nbsp;
                    <strong>
                        Indigenous movement gains momentum
                    </strong>
                </p>
            </td>
        </tr>
    </tbody>
</table>--%>
<p>
    "The spirit and intent of the Treaty agreement meant that First Nations peoples would share the land, but retain their inherent
    rights to lands and resources," outlines the Idle No More manifesto. "Instead, First Nations have experienced a history of
    colonisation which has resulted in outstanding land claims, lack of resources and unequal funding for services such as education
    and housing."
</p>
<p>
    Beyond treaty areas, large sections of Canada's north and the majority of British Columbia remain
    <a class="internallink" href="http://www.dominionpaper.ca/articles/2981" target="_blank">
        unceded indigenous territories
    </a>
    , lands where no signed treaty is in effect. Legally speaking Canadian society exists in colonial limbo on these lands,
    outside the framework of both Canadian and indigenous law, areas including major urban centres like Vancouver.
</p>
<p>
    Despite this legal reality, Canadian political and economic power rigorously avoids recognising the
    <a class="internallink" href="http://www.pmpress.org/content/article.php?story=GordHill" target="_blank">
        fundamentally colonial character
   </a>
    &nbsp;to large territories in Canada, that today can be understood as Canadian settlements in occupied indigenous lands.
</p>
<p>
    <strong>
        Idle No More vs conservative Canada
    </strong>
</p>
<p>
    Key to the political energy around Idle No More today in Canada is a growing political alliance against the conservative
    government.
</p>
<p>
    Aboriginal activists lead Idle No More, but the movement also involves
    <a class="internallink" href="http://www.alternet.org/environment/why-idle-no-more-movement-our-best-chance-clean-land-and-water"
    target="_blank">
        voices for environmental justice
    </a>
    ,&nbsp;while receiving active support from a broad spectrum of Canadian society critical toward the policies of the current
    conservative government.
</p>
<p>
    Recently in Quebec, l'Association pour une solidarit&eacute; syndicale &eacute;tudiante (ASS&Eacute;), the face of the 2012
    Montreal student uprising against austerity-driven tuition fee hikes, issued a
    <a class="internallink" href="http://rabble.ca/news/2013/01/ass&eacute;-supports-idle-no-more-quebec-students-welcome-native-spring-2013"
    target="_blank">
        strong declaration
    </a>
    &nbsp;supporting the Idle No More movement.
</p>
<%--<table class="Skyscrapper_Body" style="width: 250px; height: 50; float: right; background-color: #fb9d04; border-style: solid; border-color: white; border-collapse: collapse;"
border="10">
    <tbody>
        <tr>
            <td>
                <p>
                    <span style="font-size: 10pt; color: white;">
                        <strong>
                            "...&nbsp;
                            <span style="font-family: arial, helvetica, sans-serif;">
                                in relation to major 'developments', like pipeline extensions to Canada's oil sands industry, limitations that cut down
                                space for indigenous people to play a meaningful role in defining the future of their historic homelands.
                            </span>
                        </strong>
                    </span>
                    <span style="font-size: 10pt; color: white;">
                        <strong>
                            "
                        </strong>
                    </span>
                </p>
            </td>
        </tr>
    </tbody>
</table>--%>
<p>
    "Last year the streets of Quebec vibrated to the rhythm of hundreds of thousands of marching feet, as our student strike
    against an increase in university tuition fees blossomed into the political awakening of a society," writes ASS&Eacute;.
</p>
<p>
    "Today, malls and public squares and railways across Canada are vibrating to another rhythm, the drum beat of a surging
    and inspiring movement of Indigenous peoples, for cultural renewal, for land rights, for environmental protection, and for
    decolonisation."&nbsp;
</p>
<p>
    In Canada, Idle No More is building
    <a class="internallink" href="http://rabble.ca/news/2013/01/importance-idle-no-more-and-power-collective-action" target="_blank">
        creative political space
    </a>
    &nbsp;to openly challenge controversial Conservative policies, including key provisions in the government's
    <a class="internallink" href="http://www.cbc.ca/news/politics/story/2012/10/19/pol-list-2nd-omnibus-bill.html" target="_blank">
        recent omnibus federal budget bill C-45
    </a>
    .
</p>
<p>
    Including changes to Canada's Indian Act, toward easing regulations on the commercial leasing of reservation lands, that
    will, if implemented, equal the further erosion First Nations territory within Canada's borderlines.
</p>
<p>
    On Canada's Navigation Protection Act, the Conservative bill includes changes to allow for more rapid confirmations on industrial
    development projects over waterways, namely power and pipe lines.
</p>
<p>
    <a class="internallink" href="http://www.cbc.ca/news/politics/story/2012/10/18/pol-navigable-waters-protection-budget-bill.html"
    target="_blank">
        Altercations
    </a>
    &nbsp;that erase earlier requirements for major development projects to not damage or destroy navigable waterways in Canada.
    Idle No More outlines that these changes will "remove protection for 99.9 percent of lakes and rivers in Canada".
</p>
<p>
    Also the Conservative bill aims to limit Canada's Environmental Assessment Act, in relation to major "developments", like
    pipeline extensions to
    <a class="internallink" href="http://www.greenpeace.org/canada/en/campaigns/Energy/tarsands/" target="_blank">
        Canada's oil sands industry
    </a>
    , limitations that cut down space for indigenous people to play a meaningful role in defining the future of their historic
    homelands.
</p>
<p>
    "Idle No More revolves around Indigenous ways of knowing rooted in Indigenous sovereignty to protect water, air, land and
    all creation for future generations," outlines an
    <a class="internallink" href="http://idlenomore.ca/index.php/about-us/press-releases/item/83-press-release-january-10-2013-for-immediate-release"
    target="_blank">
        official movement text
    </a>
    .
</p>
<p>
    "The conservative government bills beginning with Bill C-45 threaten Treaties and this Indigenous vision of sovereignty.
    The goal of the movement is education and to revitalise Indigenous peoples through awareness and empowerment. This message
    has been heard around the world and the world is watching how Canada responds to the message sent by many Idle No More supporters."
</p>
<p>
    <strong>
        Apartheid economics
    </strong>
</p>
<p>
    Recent changes to Canadian law, introduced by the Conservative government, in relation to aboriginal rights, are directly
    rooted in
    <a class="internallink" href="http://www.economist.com/news/americas/21569708-protests-native-peoples-pose-awkward-questions-their-leaders-and-stephen-harpers"
    target="_blank">
        Canada's growing economic dependence on natural resources
    </a>
    .
</p>
<p>
    Today, Canada's economy is
    <a class="internallink" href="http://www.economist.com/node/16060113" target="_blank">
        often highlighted
    </a>
    &nbsp;as an example of relative stability amidst global financial turmoil.
</p>
<%--<table class="Skyscrapper_Body" style="width: 250px; height: 50; float: right; background-color: #fb9d04; border-style: solid; border-color: white; border-collapse: collapse;"
border="10">
    <tbody>
        <tr>
            <td>
                <p>
                    <span style="font-size: 10pt; color: white;">
                        <strong>
                            "
                            <span style="font-family: arial, helvetica, sans-serif;">
                                Today the annual median income for aboriginal people is 30 percent lower than the Canadian average, according to recent
                                national census data.
                            </span>
                        </strong>
                    </span>
                    <span style="font-size: 10pt; color: white;">
                        <strong>
                            "
                        </strong>
                    </span>
                </p>
            </td>
        </tr>
    </tbody>
</table>--%>
<p>
    <!-- PAGELOADEDSUCCESSFULLY-->
</p>
<p>
    "The Canadian economy is still performing relatively well, despite the challenges in Europe and elsewhere,"
    <a class="internallink" href="http://www.bloomberg.com/news/2012-01-05/cibc-leads-canadian-banks-for-mergers-ousting-goldman-sachs.html"
    target="_blank">
        outlines
    </a>
    &nbsp;a major Canadian bank official, "we're continuing to see demand in interest for the resource sectors in Canada, both
    mining and oil and gas."
</p>
<p>
    Today,
    <a class="internallink" href="http://fin.gc.ca/n12/12-143_1-eng.asp" target="_blank">
        Conservative politicians openly claim
    </a>
    &nbsp;that Canada has "weathered the storm" of global financial crisis, very often pointing to the strong "energy sector",
    while never addressing the intensely colonial dimensions to Canadian economics.
</p>
<p>
    Canada's major mining and oil and gas sectors are largely wired to totally ignore and undercut previously signed treaty
    agreements and
    <a class="internallink" href="http://www.amnesty.ca/category/issue/the-united-nations-declaration-on-the-rights-of-indigenous-peoples"
    target="_blank">
        Canada's international legal obligations
    </a>
    to aboriginal people.
</p>
<p>
    <span lang="EN-CA">
        In
        <a class="internallink" href="http://www.guardian.co.uk/commentisfree/2011/dec/11/canada-third-world-first-nation-attawapiskat"
        target="_blank">
            Attawapiskat
        </a>
        &nbsp;First Nation, Chief Spence declared a state of emergency in 2011, to draw focus to serious poverty on the isolated
        reserve, where many families live in wooden sheds, without running water or adequate insulation to face Canada's northern
        winter winds. Only 90km away from Attawapiskat is Victor Diamond Mine, operated by De Beers, that
        <a class="internallink" href="http://www.aljazeera.com/indepth/features/2012/02/201221017545565952.html" target="_blank">
            according to reports
        </a>
        is extracting around 600,000 carats of diamonds per year.
    </span>
</p>
<p>
    "Great riches are being taken from our land for the benefit of a few, including the government of Canada and Ontario, who
    receive large royalty payments, while we receive so little," outlines Chief Spence in a 2011 speech.
</p>
<p>
    Today the annual median income for aboriginal people is 30 percent lower than the Canadian average, according to
    <a class="internallink" href="http://cupe.ca/aboriginal/federal-budget-2012-systemic" target="_blank">
        recent national census data
    </a>
    .
</p>
<p>
    In reality the development of extraordinary mineral and energy wealth on First Nations territories, has done little to address
    the intense poverty and political marginalisation for the majority of aboriginal people.
</p>
<p>
    Idle No More sounds an alarm on this colonial reality, accurately highlighting Canada's relative economic success as dependent
    on
    <a class="internallink" href="http://www.mediacoop.ca/story/economics-insurgency/15610" target="_blank">
        harvesting land and resources on indigenous territories
    </a>
    &nbsp;without meaningful consultation, consent or remittance.
</p>
<p>
    <strong>
        Decolonising Canada
    </strong>
</p>
<p>
    Central to understanding this current winter unrest, sparked by Idle No More, are the
    <a class="internallink" href="http://apihtawikosisan.com/2012/12/11/the-natives-are-restless-wondering-why/" target="_blank">
        urgent calls
    </a>
    &nbsp;to revise Canada-aboriginal relations against the backdrop of persisting colonial injustice.
</p>
<p>
    "It's high time for Canada to scrap discriminatory approaches dating back to colonial times and begin to respect the rights
    of First Nations, Inuit and Metis peoples under Canadian and international law," outlines
    <a class="internallink" href="http://www.amnesty.org/en/news/canada-indigenous-protest-movement-highlights-deep-rooted-injustices-2013-01-04"
    target="_blank">
        Amnesty International
    </a>
    &nbsp;in a recent statement on Idle No More.
</p>
<p>
    Canada's political landscape now faces an alarm on colonial questions commonly evaded in the halls of power.
</p>
<p>
    Idle No More presents an incredible opening to collectively reenvision Canada, to finally address Canada's unjust past and
    present policies toward aboriginal people. A call to collectively explore a new social contract, rooted in indigenous traditions
    and contemporary conceptions of social justice, that can unravel the violent colonial roots to current economic modes that
    are destroying mother earth.
</p>
<p>
    <strong>
        <em>
            Stefan Christoff is a Montreal based community activist, musician and writer.
        </em>
    </strong>
</p>
<p>
    <strong>
        <em>
            Follow him on Twitter:
            <a class="internallink" href="http://www.twitter.com/spirodon/" target="_blank">
                @spirodon
            </a>
        </em>
    </strong>
</p>


</div>
        <br />
        <div style="position:fixed;left:430px;top:10px;background-color:white;">
            <iframe id="ifrmSg" src="" style="border:1px solid gray;width:810px;height:540px;"/>
        </div>
    </form>




</body>
</html>
