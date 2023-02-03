®$
…C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.API\Controllers\DetailedFundsController.cs
	namespace		 	
Investments		
 
.		 
API		 
.		 
Controllers		 %
{

 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public 

class #
DetailedFundsController (
:) *
ControllerBase+ 9
{ 
private 
readonly  
IDetailedFundService - 
_detailedFundService. B
;B C
public #
DetailedFundsController &
(& ' 
IDetailedFundService' ;
detailedFundService< O
)O P
{ 	 
_detailedFundService  
=! "
detailedFundService# 6
;6 7
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetAllFunds) 4
(4 5
)5 6
{ 	
try 
{ 
var 
allFunds 
= 
await $ 
_detailedFundService% 9
.9 :$
GetAllDetailedFundsAsync: R
(R S
)S T
;T U
return 
Ok 
( 
allFunds "
)" #
;# $
} 
catch   
(   
System   
.   
	Exception   #
ex  $ &
)  & '
{!! 
return"" 
this"" 
."" 

StatusCode"" &
(""& '
StatusCodes""' 2
.""2 3(
Status500InternalServerError""3 O
,""O P
$"## 
$str## =
{##= >
ex##> @
.##@ A
Message##A H
}##H I
"##I J
)##J K
;##K L
}$$ 
}&& 	
[(( 	
HttpGet((	 
((( 
$str(( 
)(( 
](( 
public)) 
async)) 
Task)) 
<)) 
IActionResult)) '
>))' (
GetFundsByCode))) 7
())7 8
string))8 >
fundCode))? G
)))G H
{** 	
try,, 
{-- 
var.. 
funds.. 
=.. 
await.. ! 
_detailedFundService.." 6
...6 7&
GetDetailedFundByCodeAsync..7 Q
(..Q R
fundCode..R Z
)..Z [
;..[ \
return00 
Ok00 
(00 
funds00 
)00  
;00  !
}11 
catch22 
(22 
System22 
.22 
	Exception22 #
ex22$ &
)22& '
{33 
return44 
this44 
.44 

StatusCode44 &
(44& '
StatusCodes44' 2
.442 3(
Status500InternalServerError443 O
,44O P
$"55 
$str55 =
{55= >
ex55> @
.55@ A
Message55A H
}55H I
"55I J
)55J K
;55K L
}66 
}88 	
[:: 	
HttpPost::	 
(:: 
$str::  
)::  !
]::! "
public;; 
async;; 
Task;; 
<;; 
IActionResult;; '
>;;' (
AddDetailedFunds;;) 9
(;;9 :
IEnumerable;;: E
<;;E F
DetailedFunds;;F S
>;;S T
detailedFunds;;U b
);;b c
{<< 	
try>> 
{?? 
var@@ 
funds@@ 
=@@ 
await@@ ! 
_detailedFundService@@" 6
.@@6 7!
AddDetailedFundsAsync@@7 L
(@@L M
detailedFunds@@M Z
)@@Z [
;@@[ \
returnBB 
OkBB 
(BB 
fundsBB 
)BB  
;BB  !
}CC 
catchDD 
(DD 
SystemDD 
.DD 
	ExceptionDD #
exDD$ &
)DD& '
{EE 
returnFF 
thisFF 
.FF 

StatusCodeFF &
(FF& '
StatusCodesFF' 2
.FF2 3(
Status500InternalServerErrorFF3 O
,FFO P
$"GG 
$strGG =
{GG= >
exGG> @
.GG@ A
MessageGGA H
}GGH I
"GGI J
)GGJ K
;GGK L
}HH 
}JJ 	
}MM 
}NN çI
ˆC:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.API\Controllers\FundRegistrationController.cs
	namespace		 	
Investments		
 
.		 
API		 
.		 
Controllers		 %
{

 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public 

class &
FundRegistrationController +
:, -
ControllerBase. <
{ 
private 
readonly 
IFundsService &
_fundsService' 4
;4 5
public &
FundRegistrationController )
() *
IFundsService* 7
fundsService8 D
)D E
{ 	
_fundsService 
= 
fundsService (
;( )
} 	
[ 	
HttpPut	 
( 
$str #
)# $
]$ %
public 
async 
Task 
< 
IActionResult '
>' (
FundRegistration) 9
(9 :
string: @
fundCodeA I
)I J
{ 	
try 
{ 
var 
funds 
= 
await !
_fundsService" /
./ 0
AddFundAsync0 <
(< =
fundCode= E
)E F
;F G
if 
( 
funds 
== 
null  
)  !
{   
return!! 
Ok!! 
(!! 
$str!! D
)!!D E
;!!E F
}"" 
return$$ 
Ok$$ 
($$ 
funds$$ 
)$$  
;$$  !
}%% 
catch&& 
(&& 
System&& 
.&& 
	Exception&& #
ex&&$ &
)&&& '
{'' 
return(( 
this(( 
.(( 

StatusCode(( &
(((& '
StatusCodes((' 2
.((2 3(
Status500InternalServerError((3 O
,((O P
$")) 
$str)) =
{))= >
ex))> @
.))@ A
Message))A H
}))H I
"))I J
)))J K
;))K L
}** 
},, 	
[.. 	
HttpPut..	 
(.. 
$str.. $
)..$ %
]..% &
public// 
async// 
Task// 
<// 
IActionResult// '
>//' (
FundsRegistration//) :
(//: ;
List//; ?
<//? @
string//@ F
>//F G
fundCode//H P
)//P Q
{00 	
try22 
{33 
var44 
detailedFunds44 !
=44" #
new44$ '
List44( ,
<44, -
DetailedFunds44- :
>44: ;
(44; <
)44< =
;44= >
detailedFunds55 
.55 
AddRange55 &
(55& '
(55' (
IEnumerable55( 3
<553 4
DetailedFunds554 A
>55A B
)55B C
fundCode55C K
)55K L
;55L M
var77 
funds77 
=77 
await77 !
_fundsService77" /
.77/ 0
AddFundsAsync770 =
(77= >
detailedFunds77> K
)77K L
;77L M
if99 
(99 
funds99 
)99 
{:: 
return;; 
Ok;; 
(;; 
$str;; D
);;D E
;;;E F
}<< 
return>> 
Ok>> 
(>> 
funds>> 
)>>  
;>>  !
}?? 
catch@@ 
(@@ 
System@@ 
.@@ 
	Exception@@ #
ex@@$ &
)@@& '
{AA 
returnBB 
thisBB 
.BB 

StatusCodeBB &
(BB& '
StatusCodesBB' 2
.BB2 3(
Status500InternalServerErrorBB3 O
,BBO P
$"CC 
$strCC =
{CC= >
exCC> @
.CC@ A
MessageCCA H
}CCH I
"CCI J
)CCJ K
;CCK L
}DD 
}FF 	
[HH 	

HttpDeleteHH	 
(HH 
$strHH 1
)HH1 2
]HH2 3
publicII 
asyncII 
TaskII 
<II 
IActionResultII '
>II' (
DeleteFundByCodeII) 9
(II9 :
stringII: @
fundCodeIIA I
)III J
{JJ 	
tryLL 
{MM 
varNN 
fundsNN 
=NN 
awaitNN !
_fundsServiceNN" /
.NN/ 0!
DeleteFundByCodeAsyncNN0 E
(NNE F
fundCodeNNF N
)NNN O
;NNO P
returnPP 
OkPP 
(PP 
fundsPP 
)PP  
;PP  !
}QQ 
catchRR 
(RR 
SystemRR 
.RR 
	ExceptionRR #
exRR$ &
)RR& '
{SS 
returnTT 
thisTT 
.TT 

StatusCodeTT &
(TT& '
StatusCodesTT' 2
.TT2 3(
Status500InternalServerErrorTT3 O
,TTO P
$"UU 
$strUU =
{UU= >
exUU> @
.UU@ A
MessageUUA H
}UUH I
"UUI J
)UUJ K
;UUK L
}VV 
}XX 	
[ZZ 	
HttpPutZZ	 
(ZZ 
$strZZ ?
)ZZ? @
]ZZ@ A
public[[ 
async[[ 
Task[[ 
<[[ 
IActionResult[[ '
>[[' (
UpdateFundByCode[[) 9
([[9 :
string[[: @
oldFundCode[[A L
,[[L M
string[[N T
newFundCode[[U `
)[[` a
{\\ 	
try^^ 
{__ 
var`` 
funds`` 
=`` 
await`` !
_fundsService``" /
.``/ 0!
UpdateFundByCodeAsync``0 E
(``E F
oldFundCode``F Q
,``Q R
newFundCode``S ^
)``^ _
;``_ `
returnbb 
Okbb 
(bb 
fundsbb 
)bb  
;bb  !
}cc 
catchdd 
(dd 
Systemdd 
.dd 
	Exceptiondd #
exdd$ &
)dd& '
{ee 
returnff 
thisff 
.ff 

StatusCodeff &
(ff& '
StatusCodesff' 2
.ff2 3(
Status500InternalServerErrorff3 O
,ffO P
$"gg 
$strgg =
{gg= >
exgg> @
.gg@ A
MessageggA H
}ggH I
"ggI J
)ggJ K
;ggK L
}hh 
}jj 	
[ll 	
HttpGetll	 
(ll 
$strll +
)ll+ ,
]ll, -
publicmm 
asyncmm 
Taskmm 
<mm 
IActionResultmm '
>mm' (
GetFundbyCodemm) 6
(mm6 7
stringmm7 =
fundCodemm> F
)mmF G
{nn 	
trypp 
{qq 
varrr 
fundsrr 
=rr 
awaitrr !
_fundsServicerr" /
.rr/ 0
GetFundByCodeAsyncrr0 B
(rrB C
fundCoderrC K
)rrK L
;rrL M
returntt 
Oktt 
(tt 
fundstt 
)tt  
;tt  !
}uu 
catchvv 
(vv 
Systemvv 
.vv 
	Exceptionvv #
exvv$ &
)vv& '
{ww 
returnxx 
thisxx 
.xx 

StatusCodexx &
(xx& '
StatusCodesxx' 2
.xx2 3(
Status500InternalServerErrorxx3 O
,xxO P
$"yy 
$stryy =
{yy= >
exyy> @
.yy@ A
MessageyyA H
}yyH I
"yyI J
)yyJ K
;yyK L
}zz 
}|| 	
[~~ 	
HttpGet~~	 
(~~ 
$str~~ 
)~~ 
]~~ 
public 
async 
Task 
< 
IActionResult '
>' (
ListAllFunds) 5
(5 6
)6 7
{
€€ 	
try
‚‚ 
{
ƒƒ 
var
„„ 
funds
„„ 
=
„„ 
await
„„ !
_fundsService
„„" /
.
„„/ 0
GetAllFundsAsync
„„0 @
(
„„@ A
)
„„A B
;
„„B C
var
…… 
result
…… 
=
…… 
JsonConvert
…… (
.
……( )
SerializeObject
……) 8
(
……8 9
funds
……9 >
)
……> ?
;
……? @
return
‡‡ 
Ok
‡‡ 
(
‡‡ 
result
‡‡  
)
‡‡  !
;
‡‡! "
}
ˆˆ 
catch
‰‰ 
(
‰‰ 
System
‰‰ 
.
‰‰ 
	Exception
‰‰ #
ex
‰‰$ &
)
‰‰& '
{
ŠŠ 
return
‹‹ 
this
‹‹ 
.
‹‹ 

StatusCode
‹‹ &
(
‹‹& '
StatusCodes
‹‹' 2
.
‹‹2 3*
Status500InternalServerError
‹‹3 O
,
‹‹O P
$"
ŒŒ 
$str
ŒŒ =
{
ŒŒ= >
ex
ŒŒ> @
.
ŒŒ@ A
Message
ŒŒA H
}
ŒŒH I
"
ŒŒI J
)
ŒŒJ K
;
ŒŒK L
}
 
}
 	
}
‘‘ 
}’’ Þ%
‚C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.API\Controllers\FundYieldsController.cs
	namespace

 	
Investments


 
.

 
API

 
.

 
Controllers

 %
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public 

class 
FundYeldsController $
:% &
ControllerBase' 5
{ 
private 
readonly 
IFundsYieldService +
_fundsYeldService, =
;= >
public 
FundYeldsController "
(" #
IFundsYieldService# 5
fundsYeldService6 F
)F G
{ 	
_fundsYeldService 
= 
fundsYeldService  0
;0 1
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetFundsYeldByCode) ;
(; <
string< B
fundCodeC K
)K L
{ 	
try 
{ 
var 
funds 
= 
await !
_fundsYeldService" 3
.3 4"
GetFundYeldByCodeAsync4 J
(J K
fundCodeK S
)S T
;T U
return   
Ok   
(   
funds   
)    
;    !
}!! 
catch"" 
("" 
System"" 
."" 
	Exception"" #
ex""$ &
)""& '
{## 
return$$ 
this$$ 
.$$ 

StatusCode$$ &
($$& '
StatusCodes$$' 2
.$$2 3(
Status500InternalServerError$$3 O
,$$O P
$"%% 
$str%% =
{%%= >
ex%%> @
.%%@ A
Message%%A H
}%%H I
"%%I J
)%%J K
;%%K L
}&& 
}(( 	
[** 	
HttpGet**	 
(** 
$str** 
)**  
]**  !
public++ 
async++ 
Task++ 
<++ 
IActionResult++ '
>++' (
GetAllFundsYeld++) 8
(++8 9
)++9 :
{,, 	
try.. 
{// 
var00 
funds00 
=00 
await00 !
_fundsYeldService00" 3
.003 4 
GetAllFundsYeldAsync004 H
(00H I
)00I J
;00J K
return22 
Ok22 
(22 
funds22 
.22  
Where22  %
(22% &
x22& '
=>22' )
x22* +
.22+ ,
FundCode22, 4
.224 5
Contains225 =
(22= >
$str22> B
)22B C
)22C D
)22D E
;22E F
}33 
catch44 
(44 
System44 
.44 
	Exception44 #
ex44$ &
)44& '
{55 
return66 
this66 
.66 

StatusCode66 &
(66& '
StatusCodes66' 2
.662 3(
Status500InternalServerError663 O
,66O P
$"77 
$str77 =
{77= >
ex77> @
.77@ A
Message77A H
}77H I
"77I J
)77J K
;77K L
}88 
}:: 	
[<< 	
HttpPost<<	 
(<< 
$str<<  
)<<  !
]<<! "
public== 
async== 
Task== 
<== 
IActionResult== '
>==' (
AddFundsYields==) 7
(==7 8
IEnumerable==8 C
<==C D
	FundsYeld==D M
>==M N

fundsYelds==O Y
)==Y Z
{>> 	
try@@ 
{AA 
varBB 
fundsBB 
=BB 
awaitBB !
_fundsYeldServiceBB" 3
.BB3 4
AddFundsYieldsAsyncBB4 G
(BBG H

fundsYeldsBBH R
)BBR S
;BBS T
returnDD 
OkDD 
(DD 
fundsDD 
)DD  
;DD  !
}EE 
catchFF 
(FF 
SystemFF 
.FF 
	ExceptionFF #
exFF$ &
)FF& '
{GG 
returnHH 
thisHH 
.HH 

StatusCodeHH &
(HH& '
StatusCodesHH' 2
.HH2 3(
Status500InternalServerErrorHH3 O
,HHO P
$"II 
$strII =
{II= >
exII> @
.II@ A
MessageIIA H
}IIH I
"III J
)IIJ K
;IIK L
}JJ 
}LL 	
}NN 
}OO œ
ŠC:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.API\Controllers\RankOfTheBestFundsController.cs
	namespace 	
Investments
 
. 
API 
. 
Controllers %
{ 
[ 
ApiController 
] 
[		 
Route		 

(		
 
$str		 
)		 
]		 
public

 

class

 (
RankOfTheBestFundsController

 -
:

. /
ControllerBase

0 >
{ 
private 
readonly &
IRankOfTheBestFundsService 3&
_rankOfTheBestFundsService4 N
;N O
public (
RankOfTheBestFundsController +
(+ ,&
IRankOfTheBestFundsService, F%
rankOfTheBestFundsServiceG `
)` a
{ 	&
_rankOfTheBestFundsService &
=' (%
rankOfTheBestFundsService) B
;B C
} 	
[ 	
HttpGet	 
( 
$str 3
)3 4
]4 5
public 
async 
Task 
< 
IActionResult '
>' (
ListBestFunds) 6
(6 7
int7 :
?: ;
totalFundsInRank< L
=M N
nullO S
)S T
{ 	
try 
{ 
var 
allFunds 
= 
await $&
_rankOfTheBestFundsService% ?
.? @&
GetRankOfTheBestFundsAsync@ Z
(Z [
totalFundsInRank[ k
)k l
;l m
return 
Ok 
( 
allFunds "
)" #
;# $
} 
catch 
( 
System 
. 
	Exception #
ex$ &
)& '
{ 
return 
this 
. 

StatusCode &
(& '
StatusCodes' 2
.2 3(
Status500InternalServerError3 O
,O P
$"   
$str   =
{  = >
ex  > @
.  @ A
Message  A H
}  H I
"  I J
)  J K
;  K L
}!! 
}## 	
}%% 
}&& ™
~C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.API\Controllers\StocksController.cs
	namespace 	
Investments
 
. 
API 
. 
Controllers %
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public		 

class		 
StocksController		 !
:		" #
ControllerBase		$ 2
{

 
private 
readonly 
IStocksService '
_stockService( 5
;5 6
public 
StocksController 
(  
IStocksService  .
stockService/ ;
); <
{ 	
_stockService 
= 
stockService (
;( )
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetStockByCode) 7
(7 8
string8 >
name? C
)C D
{ 	
var 
stocks 
= 
await 
_stockService ,
., -
Get- 0
(0 1
name1 5
)5 6
;6 7
return 
Ok 
( 
stocks 
) 
; 
} 	
} 
} ’0
†C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.API\Controllers\WebScrapingFundsAndYelds.cs
	namespace		 	
Investments		
 
.		 
API		 
.		 
Controllers		 %
{

 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public 

class .
"WebScrapingFundsAndYeldsController 3
:4 5
ControllerBase6 D
{ 
private 
readonly ,
 IWebScrapingFundsAndYeldsService 9%
_webScrapingFundsAndYelds: S
;S T
private 
readonly &
IRankOfTheBestFundsService 3&
_rankOfTheBestFundsService4 N
;N O
private 
readonly $
WebScrapingSocketManager 1
_socketManager2 @
;@ A
private 
readonly  
IDetailedFundService - 
_detailedFundService. B
;B C
private 
readonly 
IFundsService &
_fundsService' 4
;4 5
public .
"WebScrapingFundsAndYeldsController 1
(1 2,
 IWebScrapingFundsAndYeldsService2 R$
webScrapingFundsAndYeldsS k
,k l&
IRankOfTheBestFundsService2 L%
rankOfTheBestFundsServiceM f
,f g$
WebScrapingSocketManager2 J
socketManagerK X
,X Y 
IDetailedFundService2 F
detailedFundServiceG Z
,Z [
IFundsService2 ?
fundsService@ L
)L M
{ 	%
_webScrapingFundsAndYelds %
=& '$
webScrapingFundsAndYelds( @
;@ A&
_rankOfTheBestFundsService &
=' (%
rankOfTheBestFundsService) B
;B C
_socketManager 
= 
socketManager *
;* + 
_detailedFundService  
=! "
detailedFundService# 6
;6 7
_fundsService 
= 
fundsService (
;( )
}   	
static"" 
dynamic"" 
result"" 
;"" 
[$$ 	
HttpGet$$	 
($$ 
$str$$ 
)$$ 
]$$ 
public%% 
async%% 
Task%% 
<%% 
IActionResult%% '
>%%' (
GetFundsAsync%%) 6
(%%6 7
)%%7 8
{&& 	
try'' 
{(( 
_socketManager** 
.** 
GetAll** %
(**% &
)**& '
;**' (
result++ 
=++ 
await++ %
_webScrapingFundsAndYelds++ 8
.++8 9
GetFundsAsync++9 F
(++F G
)++G H
;++H I
bool,, 
storageWentOK,, "
=,,# $
await,,% * 
_detailedFundService,,+ ?
.,,? @!
AddDetailedFundsAsync,,@ U
(,,U V
result,,V \
),,\ ]
;,,] ^
if// 
(// 
storageWentOK//  
)//  !
{00 
return11 
Ok11 
(11 
result11 $
)11$ %
;11% &
}22 
else33 
{44 
return55 
Ok55 
(55 
storageWentOK55 +
)55+ ,
;55, -
}66 
}88 
catch99 
(99 
System99 
.99 
	Exception99 #
ex99$ &
)99& '
{:: 
return;; 
this;; 
.;; 

StatusCode;; &
(;;& '
StatusCodes;;' 2
.;;2 3(
Status500InternalServerError;;3 O
,;;O P
$"<< 
$str<< 9
{<<9 :
ex<<: <
.<<< =
Message<<= D
}<<D E
"<<E F
)<<F G
;<<G H
}== 
}?? 	
[AA 	
HttpGetAA	 
(AA 
$strAA 
)AA 
]AA 
publicBB 
asyncBB 
TaskBB 
<BB 
IActionResultBB '
>BB' (
GetYeldsFundsAsyncBB) ;
(BB; <
)BB< =
{CC 	
tryEE 
{FF 
varHH 
	fundYeldsHH 
=HH 
awaitHH  %%
_webScrapingFundsAndYeldsHH& ?
.HH? @
GetYeldsFundsAsyncHH@ R
(HHR S
resultHHS Y
)HHY Z
;HHZ [
boolII 
storageWentOKII "
=II# $
awaitII% * 
_detailedFundServiceII+ ?
.II? @!
AddDetailedFundsAsyncII@ U
(IIU V
	fundYeldsIIV _
)II_ `
;II` a
ifKK 
(KK 
storageWentOKKK  
)KK  !
{LL 
varMM !
rankingOfTheBestFundsMM -
=MM. /
awaitMM0 5&
_rankOfTheBestFundsServiceMM6 P
.MMP Q1
%GetCalculationRankOfTheBestFundsAsyncMMQ v
(MMv w
)MMw x
;MMx y
awaitNN &
_rankOfTheBestFundsServiceNN 4
.NN4 5&
AddRankOfTheBestFundsAsyncNN5 O
(NNO P!
rankingOfTheBestFundsNNP e
)NNe f
;NNf g
returnOO 
OkOO 
(OO 
	fundYeldsOO '
)OO' (
;OO( )
}PP 
elseQQ 
{RR 
returnSS 
OkSS 
(SS 
storageWentOKSS +
)SS+ ,
;SS, -
}TT 
}VV 
catchWW 
(WW 
SystemWW 
.WW 
	ExceptionWW #
exWW$ &
)WW& '
{XX 
returnYY 
thisYY 
.YY 

StatusCodeYY &
(YY& '
StatusCodesYY' 2
.YY2 3(
Status500InternalServerErrorYY3 O
,YYO P
$"ZZ 
$strZZ 9
{ZZ9 :
exZZ: <
.ZZ< =
MessageZZ= D
}ZZD E
"ZZE F
)ZZF G
;ZZG H
}[[ 
}]] 	
}__ 
}`` °
“C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.API\data\ReApplyOptionalRouteParameterOperationFilter.cs
	namespace 	
Investments
 
. 
API 
. 
data 
{ 
public 

class 8
,ReApplyOptionalRouteParameterOperationFilter =
:> ?
IOperationFilter@ P
{ 
const		 
string		 
captureName		  
=		! "
$str		# 3
;		3 4
public 
void 
Apply 
( 
OpenApiOperation *
	operation+ 4
,4 5"
OperationFilterContext6 L
contextM T
)T U
{ 	
var  
httpMethodAttributes $
=% &
context' .
.. /

MethodInfo/ 9
. 
GetCustomAttributes $
($ %
true% )
)) *
. 
OfType 
< 
	Microsoft !
.! "

AspNetCore" ,
., -
Mvc- 0
.0 1
Routing1 8
.8 9
HttpMethodAttribute9 L
>L M
(M N
)N O
;O P
var "
httpMethodWithOptional &
=' ( 
httpMethodAttributes) =
?= >
.> ?
FirstOrDefault? M
(M N
mN O
=>P R
mS T
.T U
TemplateU ]
?] ^
.^ _
Contains_ g
(g h
$strh k
)k l
??m o
falsep u
)u v
;v w
if 
( "
httpMethodWithOptional &
==' )
null* .
). /
return 
; 
string 
regex 
= 
$" 
$str "
{" #
captureName# .
}. /
$str/ :
": ;
;; <
var 
matches 
= 
System  
.  !
Text! %
.% &
RegularExpressions& 8
.8 9
Regex9 >
.> ?
Matches? F
(F G"
httpMethodWithOptionalG ]
.] ^
Template^ f
,f g
regexh m
)m n
;n o
foreach 
( 
System 
. 
Text  
.  !
RegularExpressions! 3
.3 4
Match4 9
match: ?
in@ B
matchesC J
)J K
{ 
var 
name 
= 
match  
.  !
Groups! '
[' (
captureName( 3
]3 4
.4 5
Value5 :
;: ;
var 
	parameter 
= 
	operation  )
.) *

Parameters* 4
.4 5
FirstOrDefault5 C
(C D
pD E
=>F H
pI J
.J K
InK M
==N P
ParameterLocationQ b
.b c
Pathc g
&&h j
pk l
.l m
Namem q
==r t
nameu y
)y z
;z {
if 
( 
	parameter 
!=  
null! %
)% &
{ 
	parameter   
.   
AllowEmptyValue   -
=  . /
true  0 4
;  4 5
	parameter!! 
.!! 
Description!! )
=!!* +
$str	!!, ‚
;
!!‚ ƒ
	parameter"" 
."" 
Required"" &
=""' (
false"") .
;"". /
	parameter$$ 
.$$ 
Schema$$ $
.$$$ %
Nullable$$% -
=$$. /
true$$0 4
;$$4 5
}%% 
}&& 
}'' 	
}(( 
}))  
ˆC:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.API\Extensions\WebScrapingSocketMiddleware.cs
	namespace 	
Investments
 
. 
VariablesManager &
{ 
public		 

class		 '
WebScrapingSocketMiddleware		 ,
{

 
private 
readonly 
RequestDelegate (
_next) .
;. /
private 
readonly $
WebScrapingSocketManager 1
_socketManager2 @
;@ A
public '
WebScrapingSocketMiddleware *
(* +
RequestDelegate+ :
next; ?
,? @$
WebScrapingSocketManager+ C
socketManagerD Q
)Q R
{ 	
_next 
= 
next 
; 
_socketManager 
= 
socketManager *
;* +
} 	
public 
async 
Task 
Invoke  
(  !
HttpContext! ,
context- 4
)4 5
{ 	
if 
( 
! 
context 
. 

WebSockets #
.# $
IsWebSocketRequest$ 6
)6 7
{ 
await 
_next 
. 
Invoke "
(" #
context# *
)* +
;+ ,
return 
; 
} 
var 
socket 
= 
await 
context &
.& '

WebSockets' 1
.1 2 
AcceptWebSocketAsync2 F
(F G
)G H
;H I
var 
id 
= 
_socketManager #
.# $
	AddSocket$ -
(- .
socket. 4
)4 5
;5 6
await   
Receive   
(   
socket    
,    !
async  " '
(  ( )
result  ) /
,  / 0
buffer  1 7
)  7 8
=>  9 ;
{!! 
if"" 
("" 
result"" 
."" 
MessageType"" &
==""' ) 
WebSocketMessageType""* >
.""> ?
Close""? D
)""D E
{## 
await$$ 
_socketManager$$ (
.$$( )
RemoveSocket$$) 5
($$5 6
id$$6 8
)$$8 9
;$$9 :
return%% 
;%% 
}&& 
}'' 
)'' 
;'' 
}(( 	
private** 
async** 
Task** 
Receive** "
(**" #
	WebSocket**# ,
socket**- 3
,**3 4
Action**5 ;
<**; <"
WebSocketReceiveResult**< R
,**R S
byte**T X
[**X Y
]**Y Z
>**Z [
handleMessage**\ i
)**i j
{++ 	
var,, 
buffer,, 
=,, 
new,, 
byte,, !
[,,! "
$num,," &
*,,' (
$num,,) *
],,* +
;,,+ ,
while.. 
(.. 
socket.. 
... 
State.. 
==..  "
WebSocketState..# 1
...1 2
Open..2 6
)..6 7
{// 
var00 
result00 
=00 
await00 "
socket00# )
.00) *
ReceiveAsync00* 6
(006 7
buffer007 =
:00= >
new00? B
ArraySegment00C O
<00O P
byte00P T
>00T U
(00U V
buffer00V \
)00\ ]
,00] ^
cancellationToken117 H
:11H I
CancellationToken11J [
.11[ \
None11\ `
)11` a
;11a b
handleMessage33 
(33 
result33 $
,33$ %
buffer33& ,
)33, -
;33- .
}44 
}55 	
}66 
}77 â

iC:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.API\Program.cs
	namespace

 	
Investments


 
.

 
API

 
{ 
public 

class 
Program 
{ 
public 
static 
void 
Main 
(  
string  &
[& '
]' (
args) -
)- .
{ 	
CreateHostBuilder 
( 
args "
)" #
.# $
Build$ )
() *
)* +
.+ ,
Run, /
(/ 0
)0 1
;1 2
} 	
public 
static 
IHostBuilder "
CreateHostBuilder# 4
(4 5
string5 ;
[; <
]< =
args> B
)B C
=>D F
Host 
.  
CreateDefaultBuilder %
(% &
args& *
)* +
. $
ConfigureWebHostDefaults )
() *

webBuilder* 4
=>5 7
{ 

webBuilder 
. 

UseStartup )
<) *
Startup* 1
>1 2
(2 3
)3 4
;4 5
} 
) 
; 
} 
} ùF
iC:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.API\Startup.cs
	namespace 	
Investments
 
. 
API 
{ 
public 

class 
Startup 
{ 
public 
Startup 
( 
IConfiguration %
configuration& 3
)3 4
{ 	
Configuration 
= 
configuration )
;) *
} 	
public 
IConfiguration 
Configuration +
{, -
get. 1
;1 2
}3 4
public   
void   
ConfigureServices   %
(  % &
IServiceCollection  & 8
services  9 A
)  A B
{!! 	
services## 
.## 
AddDbContext## !
<##! "
InvestmentsContext##" 4
>##4 5
(##5 6
context$$ 
=>$$ 
context$$ "
.$$" #
	UseSqlite$$# ,
($$, -
Configuration$$- :
.$$: ;
GetConnectionString$$; N
($$N O
$str$$O X
)$$X Y
)$$Y Z
)%% 
;%% 
services'' 
.'' 
AddControllers'' #
(''# $
)''$ %
.(( 
AddJsonOptions(( #
(((# $
options(($ +
=>((, .
options)) 
.))  !
JsonSerializerOptions))  5
.))5 6

Converters))6 @
.))@ A
Add))A D
())D E
new))E H#
JsonStringEnumConverter))I `
())` a
)))a b
)))b c
)** 
.++ 
AddJsonOptions++ #
(++# $
options++$ +
=>++, .
options,, 
.,,  !
JsonSerializerOptions,,  5
.,,5 6
WriteIndented,,6 C
=,,D E
true,,F J
),,J K
.-- 
AddNewtonsoftJson-- &
(--& '
options--' .
=>--/ 1
options.. 
...  
SerializerSettings..  2
...2 3!
ReferenceLoopHandling..3 H
=..I J

Newtonsoft// "
.//" #
Json//# '
.//' (!
ReferenceLoopHandling//( =
.//= >
Ignore//> D
)00 
;00 
if22 
(22 
	AppDomain22 
.22 
CurrentDomain22 &
.22& '
BaseDirectory22' 4
.224 5
ToLower225 <
(22< =
)22= >
.22> ?
Contains22? G
(22G H
$str22H P
)22P Q
)22Q R
{33 
var44 
assembly44 
=44 
typeof44 %
(44% &
Program44& -
)44- .
.44. /
GetTypeInfo44/ :
(44: ;
)44; <
.44< =
Assembly44= E
;44E F
services55 
.55 
AddAutoMapper55 &
(55& '
assembly55' /
)55/ 0
;550 1
}66 
else77 
{88 
services99 
.99 
AddAutoMapper99 &
(99& '
	AppDomain99' 0
.990 1
CurrentDomain991 >
.99> ?
GetAssemblies99? L
(99L M
)99M N
)99N O
;99O P
}:: 
services<< 
.<< 
	AddScoped<< 
<<<  
IDetailedFundService<< 3
,<<3 4
DetailedFundService<<5 H
><<H I
(<<I J
)<<J K
;<<K L
services== 
.== 
	AddScoped== 
<== 
IFundsService== ,
,==, -
FundsService==. :
>==: ;
(==; <
)==< =
;=== >
services>> 
.>> 
	AddScoped>> 
<>> 
IFundsYieldService>> 1
,>>1 2
FundsYieldService>>3 D
>>>D E
(>>E F
)>>F G
;>>G H
services?? 
.?? 
	AddScoped?? 
<?? &
IRankOfTheBestFundsService?? 9
,??9 :%
RankOfTheBestFundsService??; T
>??T U
(??U V
)??V W
;??W X
services@@ 
.@@ 
	AddScoped@@ 
<@@ 
IStocksService@@ -
,@@- .
StocksService@@/ <
>@@< =
(@@= >
)@@> ?
;@@? @
servicesAA 
.AA 
	AddScopedAA 
<AA ,
 IWebScrapingFundsAndYeldsServiceAA ?
,AA? @+
WebScrapingFundsAndYeldsServiceAAA `
>AA` a
(AAa b
)AAb c
;AAc d
servicesCC 
.CC 
	AddScopedCC 
<CC  
IDetailedFundPersistCC 3
,CC3 4
DetailedFundPersistCC5 H
>CCH I
(CCI J
)CCJ K
;CCK L
servicesDD 
.DD 
	AddScopedDD 
<DD 
IFundsPersistDD ,
,DD, -
FundsPersistDD. :
>DD: ;
(DD; <
)DD< =
;DD= >
servicesEE 
.EE 
	AddScopedEE 
<EE 
IGeneralPersistEE .
,EE. /
GeneralPersistEE0 >
>EE> ?
(EE? @
)EE@ A
;EEA B
servicesFF 
.FF 
	AddScopedFF 
<FF 
IFundsYeldPersistFF 0
,FF0 1
FundYeldsPersistFF2 B
>FFB C
(FFC D
)FFD E
;FFE F
servicesGG 
.GG 
	AddScopedGG 
<GG &
IRankOfTheBestFundsPersistGG 9
,GG9 :%
RankOfTheBestFundsPersistGG; T
>GGT U
(GGU V
)GGV W
;GGW X
servicesHH 
.HH 
	AddScopedHH 
<HH ,
 IWebScrapingFundsAndYeldsPersistHH ?
,HH? @+
WebScrapingFundsAndYeldsPersistHHA `
>HH` a
(HHa b
)HHb c
;HHc d
servicesJJ 
.JJ 
AddSingletonJJ !
<JJ! "$
WebScrapingSocketManagerJJ" :
>JJ: ;
(JJ; <
)JJ< =
;JJ= >
servicesLL 
.LL 
AddCorsLL 
(LL 
)LL 
;LL 
servicesMM 
.MM 
AddSwaggerGenMM "
(MM" #
cMM# $
=>MM% '
{NN 
cOO 
.OO 

SwaggerDocOO 
(OO 
$strOO !
,OO! "
newOO# &
OpenApiInfoOO' 2
{OO3 4
TitleOO5 :
=OO; <
$strOO= N
,OON O
VersionOOP W
=OOX Y
$strOOZ ^
}OO_ `
)OO` a
;OOa b
}PP 
)PP 
;PP 
}SS 	
publicVV 
voidVV 
	ConfigureVV 
(VV 
IApplicationBuilderVV 1
appVV2 5
,VV5 6
IWebHostEnvironmentVV7 J
envVVK N
)VVN O
{WW 	
ifYY 
(YY 
envYY 
.YY 
IsDevelopmentYY !
(YY! "
)YY" #
)YY# $
{ZZ 
app[[ 
.[[ %
UseDeveloperExceptionPage[[ -
([[- .
)[[. /
;[[/ 0
app\\ 
.\\ 

UseSwagger\\ 
(\\ 
)\\  
;\\  !
app]] 
.]] 
UseSwaggerUI]]  
(]]  !
c]]! "
=>]]# %
c]]& '
.]]' (
SwaggerEndpoint]]( 7
(]]7 8
$str]]8 R
,]]R S
$str]]T h
)]]h i
)]]i j
;]]j k
}^^ 
appbb 
.bb 
UseHttpsRedirectionbb #
(bb# $
)bb$ %
;bb% &
appdd 
.dd 

UseRoutingdd 
(dd 
)dd 
;dd 
appff 
.ff 
UseAuthorizationff  
(ff  !
)ff! "
;ff" #
apphh 
.hh 
UseCorshh 
(hh 
xhh 
=>hh 
xhh 
.hh 
AllowAnyHeaderhh -
(hh- .
)hh. /
.ii 
AllowAnyMethodii -
(ii- .
)ii. /
.jj 
AllowAnyOriginjj -
(jj- .
)jj. /
)jj/ 0
;jj0 1
appoo 
.oo 
UseStaticFilesoo 
(oo 
)oo  
;oo  !
apppp 
.pp 
UseWebSocketspp 
(pp 
)pp 
;pp  
appqq 
.qq 
UseMiddlewareqq 
<qq 
VariablesManagerqq .
.qq. /'
WebScrapingSocketMiddlewareqq/ J
>qqJ K
(qqK L
)qqL M
;qqM N
appss 
.ss 
UseEndpointsss 
(ss 
	endpointsss &
=>ss' )
{tt 
	endpointsuu 
.uu 
MapControllersuu (
(uu( )
)uu) *
;uu* +
}vv 
)vv 
;vv 
}ww 	
}yy 
}zz 