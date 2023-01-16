ó%
ÖC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.API\Controllers\DetailedFundsController.cs
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
$"## 3
'Erro ao tentar recuperar fundos. Erro: ## =
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
$"55 3
'Erro ao tentar recuperar fundos. Erro: 55 =
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
$"GG 3
'Erro ao tentar recuperar fundos. Erro: GG =
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
}NN Ç:
àC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.API\Controllers\FundRegistrationController.cs
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
 &
FundRegistrationController

 +
:

, -
ControllerBase

. <
{ 
private 
readonly 
IFundsService &
_fundsService' 4
;4 5
public &
FundRegistrationController )
() *
IFundsService* 7
fundsService8 D
)D E
{ 	
_fundsService 
= 
fundsService (
;( )
} 	
[ 	
HttpPut	 
( 
$str #
)# $
]$ %
public 
async 
Task 
< 
IActionResult '
>' (
FundRegistration) 9
(9 :
string: @
fundCodeA I
)I J
{ 	
try 
{ 
var 
funds 
= 
await !
_fundsService" /
./ 0
AddFundAsync0 <
(< =
fundCode= E
)E F
;F G
if 
( 
funds 
== 
null  
)  !
{ 
return 
Ok 
( 
$str D
)D E
;E F
} 
return!! 
Ok!! 
(!! 
funds!! 
)!!  
;!!  !
}"" 
catch## 
(## 
System## 
.## 
	Exception## #
ex##$ &
)##& '
{$$ 
return%% 
this%% 
.%% 

StatusCode%% &
(%%& '
StatusCodes%%' 2
.%%2 3(
Status500InternalServerError%%3 O
,%%O P
$"&& 3
'Erro ao tentar recuperar fundos. Erro: && =
{&&= >
ex&&> @
.&&@ A
Message&&A H
}&&H I
"&&I J
)&&J K
;&&K L
}'' 
})) 	
[++ 	

HttpDelete++	 
(++ 
$str++ 1
)++1 2
]++2 3
public,, 
async,, 
Task,, 
<,, 
IActionResult,, '
>,,' (
DeleteFundByCode,,) 9
(,,9 :
string,,: @
fundCode,,A I
),,I J
{-- 	
try// 
{00 
var11 
funds11 
=11 
await11 !
_fundsService11" /
.11/ 0!
DeleteFundByCodeAsync110 E
(11E F
fundCode11F N
)11N O
;11O P
return33 
Ok33 
(33 
funds33 
)33  
;33  !
}44 
catch55 
(55 
System55 
.55 
	Exception55 #
ex55$ &
)55& '
{66 
return77 
this77 
.77 

StatusCode77 &
(77& '
StatusCodes77' 2
.772 3(
Status500InternalServerError773 O
,77O P
$"88 3
'Erro ao tentar recuperar fundos. Erro: 88 =
{88= >
ex88> @
.88@ A
Message88A H
}88H I
"88I J
)88J K
;88K L
}99 
};; 	
[== 	
HttpPut==	 
(== 
$str== ?
)==? @
]==@ A
public>> 
async>> 
Task>> 
<>> 
IActionResult>> '
>>>' (
UpdateFundByCode>>) 9
(>>9 :
string>>: @
oldFundCode>>A L
,>>L M
string>>N T
newFundCode>>U `
)>>` a
{?? 	
tryAA 
{BB 
varCC 
fundsCC 
=CC 
awaitCC !
_fundsServiceCC" /
.CC/ 0!
UpdateFundByCodeAsyncCC0 E
(CCE F
oldFundCodeCCF Q
,CCQ R
newFundCodeCCS ^
)CC^ _
;CC_ `
returnEE 
OkEE 
(EE 
fundsEE 
)EE  
;EE  !
}FF 
catchGG 
(GG 
SystemGG 
.GG 
	ExceptionGG #
exGG$ &
)GG& '
{HH 
returnII 
thisII 
.II 

StatusCodeII &
(II& '
StatusCodesII' 2
.II2 3(
Status500InternalServerErrorII3 O
,IIO P
$"JJ 3
'Erro ao tentar recuperar fundos. Erro: JJ =
{JJ= >
exJJ> @
.JJ@ A
MessageJJA H
}JJH I
"JJI J
)JJJ K
;JJK L
}KK 
}MM 	
[OO 	
HttpGetOO	 
(OO 
$strOO +
)OO+ ,
]OO, -
publicPP 
asyncPP 
TaskPP 
<PP 
IActionResultPP '
>PP' (
GetFundbyCodePP) 6
(PP6 7
stringPP7 =
fundCodePP> F
)PPF G
{QQ 	
trySS 
{TT 
varUU 
fundsUU 
=UU 
awaitUU !
_fundsServiceUU" /
.UU/ 0
GetFundByCodeAsyncUU0 B
(UUB C
fundCodeUUC K
)UUK L
;UUL M
returnWW 
OkWW 
(WW 
fundsWW 
)WW  
;WW  !
}XX 
catchYY 
(YY 
SystemYY 
.YY 
	ExceptionYY #
exYY$ &
)YY& '
{ZZ 
return[[ 
this[[ 
.[[ 

StatusCode[[ &
([[& '
StatusCodes[[' 2
.[[2 3(
Status500InternalServerError[[3 O
,[[O P
$"\\ 3
'Erro ao tentar recuperar fundos. Erro: \\ =
{\\= >
ex\\> @
.\\@ A
Message\\A H
}\\H I
"\\I J
)\\J K
;\\K L
}]] 
}__ 	
[aa 	
HttpGetaa	 
(aa 
$straa 
)aa 
]aa 
publicbb 
asyncbb 
Taskbb 
<bb 
IActionResultbb '
>bb' (
ListAllFundsbb) 5
(bb5 6
)bb6 7
{cc 	
tryee 
{ff 
vargg 
fundsgg 
=gg 
awaitgg !
_fundsServicegg" /
.gg/ 0
GetAllFundsAsyncgg0 @
(gg@ A
)ggA B
;ggB C
returnii 
Okii 
(ii 
fundsii 
)ii  
;ii  !
}jj 
catchkk 
(kk 
Systemkk 
.kk 
	Exceptionkk #
exkk$ &
)kk& '
{ll 
returnmm 
thismm 
.mm 

StatusCodemm &
(mm& '
StatusCodesmm' 2
.mm2 3(
Status500InternalServerErrormm3 O
,mmO P
$"nn 3
'Erro ao tentar recuperar fundos. Erro: nn =
{nn= >
exnn> @
.nn@ A
MessagennA H
}nnH I
"nnI J
)nnJ K
;nnK L
}oo 
}qq 	
}ss 
}tt «&
ÇC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.API\Controllers\FundYieldsController.cs
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
$"%% 3
'Erro ao tentar recuperar fundos. Erro: %% =
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
$"77 3
'Erro ao tentar recuperar fundos. Erro: 77 =
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
$"II 3
'Erro ao tentar recuperar fundos. Erro: II =
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
}OO ø
äC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.API\Controllers\RankOfTheBestFundsController.cs
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
$"   3
'Erro ao tentar recuperar fundos. Erro:    =
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
}&& ô
~C:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.API\Controllers\StocksController.cs
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
} Õ0
ÜC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.API\Controllers\WebScrapingFundsAndYelds.cs
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
$"<< /
#Erro ao tentar obter fundos. Erro: << 9
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
.II? @
AddFundsYeldsAsyncII@ R
(IIR S
	fundYeldsIIS \
)II\ ]
;II] ^
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
$"ZZ /
#Erro ao tentar obter fundos. Erro: ZZ 9
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
}`` ∏
ìC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.API\data\ReApplyOptionalRouteParameterOperationFilter.cs
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
$" 
{{(?< "
{" #
captureName# .
}. /
>\\w+)\\?}}/ :
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
$str	!!, Ç
;
!!Ç É
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
})) †
àC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.API\Extensions\WebScrapingSocketMiddleware.cs
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
}77 ‚

iC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.API\Program.cs
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
} ÅA
iC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.API\Startup.cs
	namespace 	
Investments
 
. 
API 
{ 
public 

class 
Startup 
{ 
public 
Startup 
( 
IConfiguration %
configuration& 3
)3 4
{ 	
Configuration 
= 
configuration )
;) *
} 	
public 
IConfiguration 
Configuration +
{, -
get. 1
;1 2
}3 4
public 
void 
ConfigureServices %
(% &
IServiceCollection& 8
services9 A
)A B
{ 	
services   
.   
AddDbContext   !
<  ! "
InvestmentsContext  " 4
>  4 5
(  5 6
context!! 
=>!! 
context!! "
.!!" #
	UseSqlite!!# ,
(!!, -
Configuration!!- :
.!!: ;
GetConnectionString!!; N
(!!N O
$str!!O X
)!!X Y
)!!Y Z
)"" 
;"" 
services$$ 
.$$ 
AddControllers$$ #
($$# $
)$$$ %
.%% 
AddJsonOptions%% #
(%%# $
options%%$ +
=>%%, .
options&& 
.&&  !
JsonSerializerOptions&&  5
.&&5 6

Converters&&6 @
.&&@ A
Add&&A D
(&&D E
new&&E H#
JsonStringEnumConverter&&I `
(&&` a
)&&a b
)&&b c
)'' 
.(( 
AddJsonOptions(( #
(((# $
options(($ +
=>((, .
options)) 
.))  !
JsonSerializerOptions))  5
.))5 6
WriteIndented))6 C
=))D E
true))F J
)))J K
.** 
AddNewtonsoftJson** &
(**& '
options**' .
=>**/ 1
options++ 
.++  
SerializerSettings++  2
.++2 3!
ReferenceLoopHandling++3 H
=++I J

Newtonsoft,, "
.,," #
Json,,# '
.,,' (!
ReferenceLoopHandling,,( =
.,,= >
Ignore,,> D
)-- 
;-- 
services00 
.00 
AddAutoMapper00 "
(00" #
	AppDomain00# ,
.00, -
CurrentDomain00- :
.00: ;
GetAssemblies00; H
(00H I
)00I J
)00J K
;00K L
services22 
.22 
	AddScoped22 
<22  
IDetailedFundService22 3
,223 4
DetailedFundService225 H
>22H I
(22I J
)22J K
;22K L
services33 
.33 
	AddScoped33 
<33 
IFundsService33 ,
,33, -
FundsService33. :
>33: ;
(33; <
)33< =
;33= >
services44 
.44 
	AddScoped44 
<44 
IFundsYieldService44 1
,441 2
FundsYieldService443 D
>44D E
(44E F
)44F G
;44G H
services55 
.55 
	AddScoped55 
<55 &
IRankOfTheBestFundsService55 9
,559 :%
RankOfTheBestFundsService55; T
>55T U
(55U V
)55V W
;55W X
services66 
.66 
	AddScoped66 
<66 
IStocksService66 -
,66- .
StocksService66/ <
>66< =
(66= >
)66> ?
;66? @
services77 
.77 
	AddScoped77 
<77 ,
 IWebScrapingFundsAndYeldsService77 ?
,77? @+
WebScrapingFundsAndYeldsService77A `
>77` a
(77a b
)77b c
;77c d
services99 
.99 
	AddScoped99 
<99  
IDetailedFundPersist99 3
,993 4
DetailedFundPersist995 H
>99H I
(99I J
)99J K
;99K L
services:: 
.:: 
	AddScoped:: 
<:: 
IFundsPersist:: ,
,::, -
FundsPersist::. :
>::: ;
(::; <
)::< =
;::= >
services;; 
.;; 
	AddScoped;; 
<;; 
IGeneralPersist;; .
,;;. /
GeneralPersist;;0 >
>;;> ?
(;;? @
);;@ A
;;;A B
services<< 
.<< 
	AddScoped<< 
<<< 
IFundsYeldPersist<< 0
,<<0 1
FundYeldsPersist<<2 B
><<B C
(<<C D
)<<D E
;<<E F
services== 
.== 
	AddScoped== 
<== &
IRankOfTheBestFundsPersist== 9
,==9 :%
RankOfTheBestFundsPersist==; T
>==T U
(==U V
)==V W
;==W X
services>> 
.>> 
	AddScoped>> 
<>> ,
 IWebScrapingFundsAndYeldsPersist>> ?
,>>? @+
WebScrapingFundsAndYeldsPersist>>A `
>>>` a
(>>a b
)>>b c
;>>c d
services@@ 
.@@ 
AddSingleton@@ !
<@@! "$
WebScrapingSocketManager@@" :
>@@: ;
(@@; <
)@@< =
;@@= >
servicesBB 
.BB 
AddCorsBB 
(BB 
)BB 
;BB 
servicesCC 
.CC 
AddSwaggerGenCC "
(CC" #
cCC# $
=>CC% '
{DD 
cEE 
.EE 

SwaggerDocEE 
(EE 
$strEE !
,EE! "
newEE# &
OpenApiInfoEE' 2
{EE3 4
TitleEE5 :
=EE; <
$strEE= N
,EEN O
VersionEEP W
=EEX Y
$strEEZ ^
}EE_ `
)EE` a
;EEa b
}FF 
)FF 
;FF 
}II 	
publicLL 
voidLL 
	ConfigureLL 
(LL 
IApplicationBuilderLL 1
appLL2 5
,LL5 6
IWebHostEnvironmentLL7 J
envLLK N
)LLN O
{MM 	
ifOO 
(OO 
envOO 
.OO 
IsDevelopmentOO !
(OO! "
)OO" #
)OO# $
{PP 
appQQ 
.QQ %
UseDeveloperExceptionPageQQ -
(QQ- .
)QQ. /
;QQ/ 0
appRR 
.RR 

UseSwaggerRR 
(RR 
)RR  
;RR  !
appSS 
.SS 
UseSwaggerUISS  
(SS  !
cSS! "
=>SS# %
cSS& '
.SS' (
SwaggerEndpointSS( 7
(SS7 8
$strSS8 R
,SSR S
$strSST h
)SSh i
)SSi j
;SSj k
}TT 
appXX 
.XX 
UseHttpsRedirectionXX #
(XX# $
)XX$ %
;XX% &
appZZ 
.ZZ 

UseRoutingZZ 
(ZZ 
)ZZ 
;ZZ 
app\\ 
.\\ 
UseAuthorization\\  
(\\  !
)\\! "
;\\" #
app^^ 
.^^ 
UseCors^^ 
(^^ 
x^^ 
=>^^ 
x^^ 
.^^ 
AllowAnyHeader^^ -
(^^- .
)^^. /
.__ 
AllowAnyMethod__ -
(__- .
)__. /
.`` 
AllowAnyOrigin`` -
(``- .
)``. /
)``/ 0
;``0 1
appee 
.ee 
UseStaticFilesee 
(ee 
)ee  
;ee  !
appff 
.ff 
UseWebSocketsff 
(ff 
)ff 
;ff  
appgg 
.gg 
UseMiddlewaregg 
<gg 
VariablesManagergg .
.gg. /'
WebScrapingSocketMiddlewaregg/ J
>ggJ K
(ggK L
)ggL M
;ggM N
appii 
.ii 
UseEndpointsii 
(ii 
	endpointsii &
=>ii' )
{jj 
	endpointskk 
.kk 
MapControllerskk (
(kk( )
)kk) *
;kk* +
}ll 
)ll 
;ll 
}mm 	
}oo 
}pp 