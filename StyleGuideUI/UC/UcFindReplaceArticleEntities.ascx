<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcFindReplaceArticleEntities.ascx.cs" Inherits="StyleGuideUI.UC.UcFindReplaceArticleEntities" %>

<style type="text/css">
    .style1
    {
        width: 10px;
    }
</style>

<div class="dvMain" >
    <div>

        <table id="dvSgMain">
            <tr>
                <td class="style1">
                    <div id="dvArticleBodyWorkArea" class="divFindPane_ArticleBody divArticleBody">
                    </div>
                    <div id="dvArticleBodyWorkAreaScripts" style="display:none;"></div>
                </td>
                <td>
                    <div id="dvFindPane" class="divFindPane_ArticleBody divFindPane" style="display:none;">
                        <div class="FindPaneTitle">Text</div>
                        <div>
                            <input id="hfEntityId" type="hidden" />
                            <input id="hfBodySgTextId" type="hidden" />
                            <input id="tbEntity" type="text" Class="FindPaneData" readonly="readonly"/>
                        </div>
                        <div class="Hspacer"></div>
                        <div class="FindPaneTitle">Notes</div>
                        <div>
                            <textarea id="tbNotes" Class="FindPaneData" readonly="readonly" style="height:88px;"></textarea>
                        </div>
                        <div class="Hspacer"></div>
                        <div class="FindPaneTitle" id="divSuggestionsTitle">Suggestions</div>
                        <div>
                            <select id="lbSuggestions" size="5" style="width:365px; overflow:auto;color:darkgreen;font-weight:bold;" onchange="ReadyToReplace(this);" onDblClick="if(ReadyToReplace(this)) replace();"  >
                            </select>
                        </div>
                        <div class="Hspacer"></div>
                        <div class="FindPaneTitle">Replace with</div>
                        <div>
                            <input id="tbReplaceEntity" type="text" Class="FindPaneData FindPaneData2"/>
                            <input id="btReplace" onclick="replace()" type="button" Class="ButtonMedium" value="Replace" style="width:82px;" />
                        </div>
                        <div class="Hspacer"></div>
                        <div>

                            <input id="btPrevious" onclick="PreviousSg()" type="button" Class="ButtonMedium ToolButton" value="Previous"  />
                            &nbsp;
                            <input id="btNext" onclick="nextSg()" type="button" Class="ButtonMedium ToolButton" value="Next"  />
                            &nbsp;
                            <input id="btUpdate" onclick="UpdateCaller()" type="button" Class="ButtonMedium ToolButton" value="Apply & Close"  />
                        </div>
                    </div>
                    <div id="AjaxLoader" class="divFindPane_ArticleBody divFindPane" style="background-color:#ffffff;display:none;" >
                        <div style="text-align:center;padding-top:80px;font-weight:bold;font-size:14pt;">
                            <div style="display:none;">L O A D I N G . . . </div>
                            <div style="padding-top:60px;"><img src="images/ajax-loader.gif" style="width:100px;"/></div>
                        </div>
                    </div>
                    <div id="dvNoSgEntFound" class="divFindPane_ArticleBody divFindPane" style="background-color:#ffffff;display:none;" >
                        <div style="text-align:center;padding-top:160px;font-weight:bold;font-size:14pt;">
                            <div style="display:block;color:red;">No Style Guide suspects found</div>
                        </div>
                    </div>


                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="divEntitesFound" class="EntitesFound">

                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>



<div id="divTest3" style="display:none;visibility:hidden;">
<p>twenty-somethings twentysomethings 20somethings
aboriginal
pro-abortion  pro-life proabortion anti-abortion antiabortion
Abyie Abiey Abeiy
African-American
afterward
Ahead of
airfare air-fare
airforce
air-hostess airhostess
airraid air-raid
airshow air-show
air base air-base
air-conditioning air conditioning
aircrafts air craft air-craft
air drop air-drop
air-lift air lift
air mail air-mail
air space air-space
air strip air-strip
air time air-time
AK Party AKP
Allawi, Ayad
al-Qaida Al-Qaida Qaida Qaeda
America
Apartheid Aparthed aparthed
archfoe
archrival
assasination assasination
attorney-general Attorney-General
Suu Kyi Su Kyi
Baluchistan
Ban ki-Moon Ban Ki-Moon
Zine al-Abidine Zine el-Abidine Zine al-Abedine Zine al-Abidine
Adulyadej Aduliadej
Bin Ladin bin Ladin
Blacks
business-like
buy-out
byelection bye-election bypoll by-poll
by-law
by-pass byepass bye-pass
byestander bye-stander by stander
Cabinet
congress
east Africa
east Asia
east Jerusalem
eastern Europe
e-mail
Falluja
fueling, fuelling
general assembly
government in exile
home maker home-maker
guerilla guerrila gorilla guerrillas guerillas gorillas
high tech hi-tech high-tech hi-tech
hostage taker
hostage taking
inspector-general
kmph
jetliner jet liner jet-liner
knee jerk
lawmaker
leftwing
longstanding
manmade
mid air mid-air
north Africa
north America
olympic games
over-run
papier mache paper mache paper-mache
paraleled
Parliament
passerby passer by
pass word
pasteurize
sidewalk kerb
pay day pay-day
pay-out
pickup pick up
presently
program
program
prophesy
prophecied
prophecy
pros & cons
protestor
publically
quick lime
quick sand
quick silver
quiz
Koran
rightwing
skeptical
skepticism
security council
shootout
smart phone
south Asia
southeast Asia
southern Africa
spark
stand-off
SUV
swathes
Tangiers Tanger
think tank
time-frame time frame
time-line time line
toward
underway
United Kingdom
war plane
west Africa
western Europe
witch hunt witchhunt
junta
mid day</p>
</div>

<div id="divTest2" style="display:none;visibility:hidden;">
doha Dohacenter called Doha 
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<tbody>
<tr id="trHeadline">
<td class="articleTitle" valign="top">
<h1 id="DetailedTitle">Typhoon-hit Philippines appeals for help</h1>
</td>
</tr>
<tr>
<td class="Tmp_hSpace10">&nbsp;</td>
</tr>
<tr>
<td>
<div id="ctl00_cphBody_dvArticleInfoBlock">
<div id="ctl00_cphBody_dvSummary" class="articleSumm">Government and UN ask international community to help the victims of Typhoon Bopha, as death toll rises to nearly 650.</div>
<div class="Tmp_hSpace5"><!-- --></div>
<div id="dvByLine_Date"><span id="dvArticleDate"> Last Modified: <span id="ctl00_cphBody_lblDate">10 Dec 2012 08:31</span></span></div>
</div>
<div class="Tmp_hSpace5" style="clear: both;"><!-- --></div>
</td>
</tr>
<tr id="articleMedia">
<td>&nbsp;</td>
</tr>
<tr>
<td id="tdTextContent" class="DetailedSummary">
<p>any more anymore anti-Semitic anti-Semitic anti-Jewish aeroplane airplane airliner</p>
<p>Manmohan singh Manmohan syng Manmohan sing Manmohen syng. The Philippine government and the United Nations are launching a global appeal to help the victims of Typhoon Bopha, as hundreds of people are still missing after the storm devastated the south of the country.<br> <br>The number of people killed by the typhoon has reached nearly 650, the country's disaster chief told Al Jazeera, and millions of people have been left homeless and are in desperate need of food aid and other basic goods.</p>
<p>Benito Ramos, United Nations, the country's disaster chief in Manila, said that 647 bodies have been found and 900 people are still missing, including hundreds of fishermen.</p>
<p>Families and fishing companies reported losing contact with more than 300 fishermen at sea.</p>
<p>Ramos said the authorities were unprepared for the unprecedented weather in those areas worst affected, and that it was struggling to cope with the disaster.</p>
<p>"Right now, we have some international organisations and governments assisting us, but our supplies are still insufficient at this moment," he said.</p>
<p>The fishermen from southern General Santos city and nearby Sarangani province left a few days before Bopha hit the main southern island of Mindanao on Tuesday, causing deadly flash floods, Ramos said.</p>
<p>Ramos said the fishermen were headed to the Spratly Islands in the South China Sea and to the Pacific Ocean. He said there has been no contact from them for a week.</p>
<p>He said the coast guard, navy and fishing vessels have launched a search.</p>
<p><strong>Searching under rubble</strong></p>
<p>doha</p>
<p>Rescuers continued searching for baRaCk oBaMa in doha or signs of life under tonnes of fallen trees and boulders in the worst-hit town of New Bataan, where rocks, mud and other rubble destroyed landmarks, making it doubly difficult to search places where houses once stood.</p>
<p>"This is a scale the Philippines has not previously seen, we’re talking about tens of thousands of homes destroyed across southeast Mindanao," Joe Curry of Catholic Relief Service told Al Jazeera.</p>
<p>"People live in fragile housing and when storms like this hit … it wipes out entire communities."</p>
<p>Hundreds of refugees, rescuers and aid workers took a break on Sunday to watch the Manny Pacquiao-Juan Manuel Marquez fight on a big TV screen, only to be dismayed by their hero's sixth-round knockout.</p>
<p>Nearly 400,000 people, mostly from Compostela Valley and nearby Davao Oriental province, have lost their homes and are crowded inside evacuation centres or staying with relatives.</p>
<p>Benigno Aquino III, the Philippine president, declared a state of national calamity on Friday, which allows for price controls on basic commodities in typhoon-affected areas and the quick release of emergency funds.</p>
</td>
</tr>
</tbody>
</table>
 newdohacenter Dohanews doha-news doha,news newdoha center-doha center,doha doha;capial  newdoha doha
</div>

<div id="div1" style="display:none;visibility:hidden;">
    pro-abortion  
pro-abortion rights anti-abortion rights pro-abortion rights anti-abortion rights anti-abortion rights heightening pros and cons quicklime quicksand quicksilver Security Council smartphone South Asia Southeast Asia Southern Africa think-tank UK warplane West Africa Western Europe witch-hunt witch-hunt military government midday

</div>

<div id="divTest4" style="display:none;visibility:hidden;">
pro-abortion pro-abortion    Suggestions
    20-somethings
Aboriginal afterward

pro-abortion rights anti-abortion rights
Abyei
African American black
afterwards
before in advance of in the run-up to
air fare
air force
air hostess
air raid
air show
airbase
airconditioning
aircraft
airdrop
airlift
airmail
airspace
airstrip
airtime
AK party
Allawi, Iyad
al-Qaeda
United States or US
apartheid
arch foe
arch rival
assassination
attorney general
Aung San Suu Kyi
Balochistan
Ban Ki-moon
Zine El Abidine Ben Ali
Bhuimibol
Bin Laden
blacks
businesslike
buyout
by-election
bye-law
bypass
bystander
cabinet
Congress
East Africa
East Asia
East Jerusalem
Eastern Europe
email
Fallujah
heightening raising increasing
General Assembly
government-in-exile
homemaker
Use fighters, armed group or rebels unless attributed to a source, normally within a quote.
hi-tech
hostage-taker
hostage-taking
inspector general
kph
plane jet airliner
kneejerk
Preferably use politicians, MPs (if appropriate) or legislators but lawmaker is allowed. 
left-wing
long-standing
man-made
midair
North Africa
North America
Olympic Games
overrun
papier-mache
paralelled
parliament
passer-by
password
pasteurise
pavement
payday
payout
pick-up truck pick-up van
currently at present
programme
programme
prophecy
prophesied
prophesy
pros and cons
protester
publicly
quicklime
quicksand
quicksilver
quiz
Quran
right-wing
sceptical
scepticism
Security Council
shoot-out
smartphone
South Asia
Southeast Asia
Southern Africa
leading to prompting
standoff
four-wheel drive 4WD 4X4
swaths
Tangier
violence attacks violent campaign
think-tank
timeframe
timeline
towards
under way
UK
warplane fighter jet
West Africa
Western Europe
witch-hunt
military government
midday


suspects

    twenty-somethings  twentysomethings  20somethings
aboriginal
pro-abortion  pro-life proabortion anti-abortion antiabortion
Abyie Abiey Abeiy
adhoc ad-hoc

African-American
afterward
Ahead of
airfare air-fare
airforce
air-hostess airhostess
airraid air-raid
airshow air-show
air base air-base
air-conditioning air conditioning
aircrafts air craft air-craft
air drop air-drop
air-lift air lift
air mail air-mail
air space air-space
air strip air-strip
air time air-time
AK Party AKP








Allawi, Ayad
al-Qaida Al-Qaida Qaida Qaeda
America
anymore
Apartheid Aparthed aparthed

archfoe
archrival  arch-rival
assasination  asassination
attorney-general Attorney-General
Suu Kyi Su Kyi
Baluchistan
Ban ki-Moon Ban Ki-Moon
Zine al-Abidine  Zine el-Abidine  Zine al-Abedine  Zine al-Abidine

Adulyadej Aduliadej
billion
Bin Ladin bin Ladin
birthrate
birth place
Blacks


bureaux

business-like
business man
business person
business woman
buy-out
byelection bye-election bypoll by-poll
by-law
by-pass byepass bye-pass
byestander bye-stander by stander
Cabinet
auto automobile

castoff
cast off
castoff
catholic
cease-fire  cease fire
cellphone


Central Intelligence Agency




coast guard

common-sense  commonsense
commonsense  common sense
communique
compare
complement
complementary


congress
connexion
consult with

continual
convenor

co-operate  co-operation  co-operative
counter-attack
counter-insurgency
courtmartial
criterion  criteria
kerb
cyber space

damage



diffuse

dependent
deserter
easygoing
ecommerce  emarketing


disabled
discernable
disclose
discolor
discrete
discreet
disinterested
disk
despatch
disassociate  disassociation
doctor
dollar

driving license
drugs dealer  drugs raid  drugs squad  drugs companies
E-coli  E coli
east Africa



east Asia
east Jerusalem
eastern Europe


e-mail
enroll  enroling  enrollment

EU
euro zone Euro zone Eurozone
Falluja
fatwa
Federal Bureau of Investigation

militants  radicals  insurgents  terrorists  fundamentalists  extremists
filmmaker  film maker
front runner front-runner
fuel
fueling  fuelling
fund raising
Group of Seven
Group of Eight
GDP
general assembly
government in exile

grafiti  grafitti
graft
grand prix
grandad

grass roots grass-roots
HIV positive
homebuyer homeowner
home maker home-maker
guerilla guerrila gorilla guerrillas guerillas gorillas
gun boat
gun fight
gun fire
gun man
gun powder
gun shot
health care
high court
high profile
historic historical
historic
high tech hi-tech high-tech hi-tech
hostage taker
hostage taking
International Monetary Fund
inspector-general
internet
South Kordofan south Kordofan
kmph
jetliner jet liner jet-liner
kickstart
knee jerk
lawmaker
leftwing
lock-down
longstanding
major general
manmade
mid air mid-air
Mideast Mid-east
guardsman guardsmen
north Africa
north America
olympic games
over-run
papier mache paper mache paper-mache
paraleled
Parliament
passerby passer by
pass word
pasteurize
sidewalk kerb
pay day pay-day
pay-out
gasoline gas
pickup pick up
pled
postwar
practise
practice
presently
prewar
program
program
prophesy
prophecied
prophecy
pros & cons
protestor
publically
quick lime
quick sand
quick silver
quiz
Koran
radical
rightwing
runup run up
Salafist Salafist
skeptical
skepticism
secretary general
security council
sharia
shootout
smart phone
south Asia
southeast Asia
southern Africa
spark
stand-off
supreme court Supreme Court
SUV
swathes
Tangiers Tanger
think tank
time-frame time frame
time-line time line
toward
underway
United Kingdom
United Nations
United States
Vice-Admiral vice-admiral
Vice-President vice-president
war on terror
war plane
west Africa
western Europe
witch hunt witchhunt
Al-Shabab  al-Shabaab  Al-Shabaab  Shabaab  Shabab
gunbattle
junta  Junta
mid day
Delhi
Arab Israelis, Israeli Arabs

amongst
birth right





</div>

<div id="divTest5" style="display:none;visibility:hidden;">
pro-abortion united states pro-abortion
</div>
<div id="divTest" style="display:none;visibility:hidden;">
pro-abortion rights
</div>


