¶

àC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IDetailedFundService.cs
	namespace 	
Investments
 
. 
Application !
.! "
	Contracts" +
{ 
public 

	interface  
IDetailedFundService )
{ 
Task		 
<		 
DetailedFunds		 
>		 &
GetDetailedFundByCodeAsync		 6
(		6 7
string		7 =
fundCode		> F
)		F G
;		G H
Task

 
<

 
IEnumerable

 
<

 
DetailedFunds

 &
>

& '
>

' ($
GetAllDetailedFundsAsync

) A
(

A B
)

B C
;

C D
Task 
< 
bool 
> !
AddDetailedFundsAsync (
(( )
IEnumerable) 4
<4 5
DetailedFunds5 B
>B C
detailedFundsD Q
)Q R
;R S
Task 
< 
bool 
> 
AddFundsYeldsAsync %
(% &
IEnumerable& 1
<1 2
DetailedFunds2 ?
>? @
detailedFundsA N
)N O
;O P
} 
} ‡
ÅC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IFundsService.cs
	namespace		 	
Investments		
 
.		 
Application		 !
.		! "
	Contracts		" +
{

 
public 

	interface 
IFundsService "
{ 
Task 
< 
Funds 
> !
UpdateFundByCodeAsync )
() *
string* 0
oldFundCode1 <
,< =
string> D
newFundCodeE P
)P Q
;Q R
Task 
< 
Funds 
> 
AddFundAsync  
(  !
string! '
fundCode( 0
)0 1
;1 2
Task 
< 
bool 
> 
AddFundsAsync  
(  !
IEnumerable! ,
<, -
DetailedFunds- :
>: ;
detailedFunds< I
)I J
;J K
Task 
< 
Funds 
> 
GetFundByCodeAsync &
(& '
string' -
fundCode. 6
)6 7
;7 8
Task 
< 
IEnumerable 
< 
Funds 
> 
>  
GetAllFundsAsync! 1
(1 2
)2 3
;3 4
Task 
< 
bool 
> !
DeleteFundByCodeAsync (
(( )
string) /
fundCode0 8
)8 9
;9 :
} 
} ƒ
ÜC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IFundsYieldService.cs
	namespace 	
Investments
 
. 
Application !
.! "
	Contracts" +
{ 
public 

	interface 
IFundsYieldService '
{ 
Task		 
<		 
IEnumerable		 
<		 
	FundsYeld		 "
>		" #
>		# $"
GetFundYeldByCodeAsync		% ;
(		; <
string		< B
fundCode		C K
)		K L
;		L M
Task

 
<

 
IEnumerable

 
<

 
	FundsYeld

 "
>

" #
>

# $ 
GetAllFundsYeldAsync

% 9
(

9 :
)

: ;
;

; <
Task 
< 
bool 
> 
AddFundsYieldsAsync &
(& '
IEnumerable' 2
<2 3
	FundsYeld3 <
>< =

fundsYelds> H
)H I
;I J
} 
} »	
éC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IRankOfTheBestFundsService.cs
	namespace 	
Investments
 
. 
Application !
.! "
	Contracts" +
{ 
public 

	interface &
IRankOfTheBestFundsService /
{ 
Task		 
<		 
IEnumerable		 
<		 
RankOfTheBestFunds		 +
>		+ ,
>		, -1
%GetCalculationRankOfTheBestFundsAsync		. S
(		S T
)		T U
;		U V
Task

 
<

 
IEnumerable

 
<

 
RankOfTheBestFunds

 +
>

+ ,
>

, -&
GetRankOfTheBestFundsAsync

. H
(

H I
int

I L
?

L M
totalFundsInRank

N ^
=

_ `
null

a e
)

e f
;

f g
Task 
< 
bool 
> &
AddRankOfTheBestFundsAsync -
(- .
IEnumerable. 9
<9 :
RankOfTheBestFunds: L
>L M
rankOfTheBestFundsN `
)` a
;a b
} 
} ´
ÇC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IStocksService.cs
	namespace 	
Investments
 
. 
Application !
.! "
	Contracts" +
{ 
public		 

	interface		 
IStocksService		 #
{

 
Task 
< 
	StocksDto 
> 
Get 
( 
string "
name# '
)' (
;( )
} 
} ¶
îC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IWebScrapingFundsAndYeldsService.cs
	namespace 	
Investments
 
. 
Application !
.! "
	Contracts" +
{ 
public		 

	interface		 ,
 IWebScrapingFundsAndYeldsService		 5
{

 
Task 
< 
IEnumerable 
< 
DetailedFunds &
>& '
>' (
GetFundsAsync) 6
(6 7
)7 8
;8 9
Task 
< 
IEnumerable 
< 
	FundsYeld "
>" #
># $
GetYeldsFundsAsync% 7
(7 8
IEnumerable8 C
<C D
DetailedFundsD Q
>Q R
detailedFundsS `
)` a
;a b
} 
} “
}C:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\DetailedFundService.cs
	namespace 	
Investments
 
. 
Application !
{		 
public

 

class

 
DetailedFundService

 $
:

% & 
IDetailedFundService

' ;
{ 
private 
readonly  
IDetailedFundPersist - 
_detailedFundPersist. B
;B C
public 
DetailedFundService "
(" # 
IDetailedFundPersist# 7
detailedFundPersist8 K
)K L
{ 	 
_detailedFundPersist  
=! "
detailedFundPersist# 6
;6 7
} 	
public 
async 
Task 
< 
bool 
> !
AddDetailedFundsAsync  5
(5 6
IEnumerable6 A
<A B
DetailedFundsB O
>O P
detailedFundsQ ^
)^ _
{ 	
try 
{ 
await  
_detailedFundPersist *
.* +!
AddDetailedFundsAsync+ @
(@ A
detailedFundsA N
)N O
;O P
return 
true 
; 
} 
catch 
( 
System 
. 
	Exception #
ex$ &
)& '
{   
Console!! 
.!! 
	WriteLine!! !
(!!! "
ex!!" $
.!!$ %
Message!!% ,
)!!, -
;!!- .
return"" 
false"" 
;"" 
}## 
}$$ 	
public&& 
Task&& 
<&& 
bool&& 
>&& 
AddFundsYeldsAsync&& ,
(&&, -
IEnumerable&&- 8
<&&8 9
DetailedFunds&&9 F
>&&F G
detailedFunds&&H U
)&&U V
{'' 	
throw(( 
new(( #
NotImplementedException(( -
(((- .
)((. /
;((/ 0
})) 	
public++ 
async++ 
Task++ 
<++ 
IEnumerable++ %
<++% &
DetailedFunds++& 3
>++3 4
>++4 5$
GetAllDetailedFundsAsync++6 N
(++N O
)++O P
{,, 	
try-- 
{.. 
var// 
allFunds// 
=// 
await// $ 
_detailedFundPersist//% 9
.//9 :$
GetAllDetailedFundsAsync//: R
(//R S
)//S T
;//T U
return11 
allFunds11 
;11  
}22 
catch33 
(33 
System33 
.33 
	Exception33 #
ex33$ &
)33& '
{44 
throw55 
new55 
	Exception55 #
(55# $
ex55$ &
.55& '
Message55' .
)55. /
;55/ 0
}66 
}77 	
public99 
async99 
Task99 
<99 
DetailedFunds99 '
>99' (&
GetDetailedFundByCodeAsync99) C
(99C D
string99D J
fundCode99K S
)99S T
{:: 	
try;; 
{<< 
var== 
funds== 
=== 
await== ! 
_detailedFundPersist==" 6
.==6 7&
GetDetailedFundByCodeAsync==7 Q
(==Q R
fundCode==R Z
)==Z [
;==[ \
return?? 
funds?? 
;?? 
}@@ 
catchAA 
(AA 
SystemAA 
.AA 
	ExceptionAA #
exAA$ &
)AA& '
{BB 
throwCC 
newCC 
	ExceptionCC #
(CC# $
exCC$ &
.CC& '
MessageCC' .
)CC. /
;CC/ 0
}DD 
}FF 	
}HH 
}II ‡
wC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\Dtos\FundsDto.cs
	namespace 	
Investments
 
. 
Application !
.! "
Dtos" &
{ 
public 

class 
FundsDto 
{		 
public 
string 
Papel 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Segmento 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
double 
Cotacao 
{ 
get  #
;# $
set% (
;( )
}* +
public 
double 
FFOYield 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
double 
DividendYield #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
double 
PVP 
{ 
get 
;  
set! $
;$ %
}& '
public 
double 
ValorDeMercado $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
double 
Liquidez 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
double 
QtdDeImoveis "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
double 
	PrecoDoM2 
{  !
get" %
;% &
set' *
;* +
}, -
public 
double 
AluguelPorM2 "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
double 
CapRate 
{ 
get  #
;# $
set% (
;( )
}* +
public 
double 
VacanciaMedia #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
} †
xC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\Dtos\StocksDto.cs
	namespace 	
Investments
 
. 
Application !
.! "
Dtos" &
{		 
public

 

partial

 
class

 
	StocksDto

 "
{ 
public 
MetaData 
MetaData  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 

Dictionary 
< 
string !
,! "
TimeSeriesDailyDto# 5
>5 6
TimeSeriesDaily7 F
{G H
getI L
;L M
setN Q
;Q R
}S T
} 
public   

partial   
class   
MetaData   !
{!! 
public11 
string11 
Information11 !
{11" #
get11$ '
;11' (
set11) ,
;11, -
}11. /
public44 
string44 
Symbol44 
{44 
get44 "
;44" #
set44$ '
;44' (
}44) *
public77 
DateTimeOffset77 
LastRefreshed77 +
{77, -
get77. 1
;771 2
set773 6
;776 7
}778 9
public:: 
string:: 

OutputSize::  
{::! "
get::# &
;::& '
set::( +
;::+ ,
}::- .
public== 
string== 
TimeZone== 
{==  
get==! $
;==$ %
set==& )
;==) *
}==+ ,
}>> 
public@@ 

partial@@ 
class@@ 
TimeSeriesDailyDto@@ +
{AA 
publicQQ 
stringQQ 
OpenQQ 
{QQ 
getQQ  
;QQ  !
setQQ" %
;QQ% &
}QQ' (
publicTT 
stringTT 
HighTT 
{TT 
getTT  
;TT  !
setTT" %
;TT% &
}TT' (
publicWW 
stringWW 
LowWW 
{WW 
getWW 
;WW  
setWW! $
;WW$ %
}WW& '
publicZZ 
stringZZ 
CloseZZ 
{ZZ 
getZZ !
;ZZ! "
setZZ# &
;ZZ& '
}ZZ( )
public]] 
long]] 
Volume]] 
{]] 
get]]  
;]]  !
set]]" %
;]]% &
}]]' (
}^^ 
}`` ¢	
òC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\FunctionsOfCalculationExtend\StandardDeviation.cs
	namespace 	
Investments
 
. 
Application !
{ 
public 

static 
class 
Extend 
{		 
public

 
static

 
double

 
StandardDeviation

 .
(

. /
this

/ 3
IEnumerable

4 ?
<

? @
double

@ F
>

F G
values

H N
)

N O
{ 	
double 
avg 
= 
values 
.  
Average  '
(' (
)( )
;) *
return 
Math 
. 
Sqrt 
( 
values #
.# $
Average$ +
(+ ,
v, -
=>- /
Math/ 3
.3 4
Pow4 7
(7 8
v8 9
-9 :
avg: =
,= >
$num> ?
)? @
)@ A
)A B
;B C
} 	
} 
} ÆD
vC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\FundsService.cs
	namespace		 	
Investments		
 
.		 
Application		 !
{

 
public 

class 
FundsService 
: 
IFundsService  -
{ 
private 
readonly 
IGeneralPersist (
_generalPersist) 8
;8 9
private 
readonly 
IFundsPersist &
_fundsPersist' 4
;4 5
private 
readonly 
IMapper  
_mapper! (
;( )
public 
FundsService 
( 
IGeneralPersist +
generalPersist, :
,: ;
IFundsPersist )
fundsPersist* 6
,6 7
IMapper #
mapper$ *
)* +
{ 	
_generalPersist 
= 
generalPersist ,
;, -
_fundsPersist 
= 
fundsPersist (
;( )
_mapper 
= 
mapper 
; 
} 	
public 
async 
Task 
< 
Funds 
>  
AddFundAsync! -
(- .
string. 4
fund5 9
)9 :
{ 	
try 
{ 
var 
retFund 
= 
await #
_fundsPersist$ 1
.1 2
GetFundByCodeAsync2 D
(D E
fundE I
)I J
;J K
if!! 
(!! 
retFund!! 
!=!! 
null!! "
)!!" #
return!!$ *
null!!+ /
;!!/ 0
var## 
newFund## 
=## 
new## !
Funds##" '
(##' (
)##( )
{##* +
FundCode##+ 3
=##4 5
fund##6 :
}##: ;
;##; <
_generalPersist%% 
.%%  
Add%%  #
<%%# $
Funds%%$ )
>%%) *
(%%* +
newFund%%+ 2
)%%2 3
;%%3 4
await'' 
_generalPersist'' %
.''% &
SaveChangesAsync''& 6
(''6 7
)''7 8
;''8 9
retFund)) 
=)) 
await)) 
_fundsPersist))  -
.))- .
GetFundByCodeAsync)). @
())@ A
newFund))A H
.))H I
FundCode))I Q
)))Q R
;))R S
return++ 
retFund++ 
;++ 
},, 
catch-- 
(-- 
System-- 
.-- 
	Exception-- #
ex--$ &
)--& '
{.. 
throw// 
new// 
	Exception// #
(//# $
ex//$ &
.//& '
Message//' .
)//. /
;/// 0
}00 
}22 	
public44 
Task44 
<44 
Funds44 
>44 
GetFundByCodeAsync44 -
(44- .
string44. 4
fundCode445 =
)44= >
{55 	
try66 
{77 
var88 
fund88 
=88 
_fundsPersist88 (
.88( )
GetFundByCodeAsync88) ;
(88; <
fundCode88< D
)88D E
;88E F
return:: 
fund:: 
;:: 
};; 
catch<< 
(<< 
System<< 
.<< 
	Exception<< #
ex<<$ &
)<<& '
{== 
throw>> 
new>> 
	Exception>> #
(>># $
ex>>$ &
.>>& '
Message>>' .
)>>. /
;>>/ 0
}?? 
}AA 	
publicCC 
TaskCC 
<CC 
IEnumerableCC 
<CC  
FundsCC  %
>CC% &
>CC& '
GetAllFundsAsyncCC( 8
(CC8 9
)CC9 :
{DD 	
tryEE 
{FF 
varGG 
fundGG 
=GG 
_fundsPersistGG (
.GG( )
GetAllFundsAsyncGG) 9
(GG9 :
)GG: ;
;GG; <
returnII 
fundII 
;II 
}JJ 
catchKK 
(KK 
SystemKK 
.KK 
	ExceptionKK #
exKK$ &
)KK& '
{LL 
throwMM 
newMM 
	ExceptionMM #
(MM# $
exMM$ &
.MM& '
MessageMM' .
)MM. /
;MM/ 0
}NN 
}OO 	
publicQQ 
asyncQQ 
TaskQQ 
<QQ 
boolQQ 
>QQ !
DeleteFundByCodeAsyncQQ  5
(QQ5 6
stringQQ6 <
fundCodeQQ= E
)QQE F
{RR 	
trySS 
{TT 
varUU 
fundUU 
=UU 
awaitUU  
_fundsPersistUU! .
.UU. /
GetFundByCodeAsyncUU/ A
(UUA B
fundCodeUUB J
)UUJ K
;UUK L
ifWW 
(WW 
fundWW 
==WW 
nullWW 
)WW  
throwWW! &
newWW' *
	ExceptionWW+ 4
(WW4 5
$strWW5 W
)WWW X
;WWX Y
_generalPersistYY 
.YY  
DeleteYY  &
<YY& '
FundsYY' ,
>YY, -
(YY- .
fundYY. 2
)YY2 3
;YY3 4
return[[ 
await[[ 
_generalPersist[[ ,
.[[, -
SaveChangesAsync[[- =
([[= >
)[[> ?
;[[? @
}]] 
catch^^ 
(^^ 
System^^ 
.^^ 
	Exception^^ #
ex^^$ &
)^^& '
{__ 
throw`` 
new`` 
	Exception`` #
(``# $
ex``$ &
.``& '
Message``' .
)``. /
;``/ 0
}aa 
}bb 	
publicdd 
asyncdd 
Taskdd 
<dd 
Fundsdd 
>dd  !
UpdateFundByCodeAsyncdd! 6
(dd6 7
stringdd7 =
oldFundCodedd> I
,ddI J
stringddK Q
newFundCodeddR ]
)dd] ^
{ee 	
tryff 
{gg 
varhh 
fundhh 
=hh 
awaithh  
_fundsPersisthh! .
.hh. /
GetFundByCodeAsynchh/ A
(hhA B
oldFundCodehhB M
)hhM N
;hhN O
ifjj 
(jj 
fundjj 
==jj 
nulljj 
)jj  
throwjj! &
newjj' *
	Exceptionjj+ 4
(jj4 5
$strjj5 [
)jj[ \
;jj\ ]
fundll 
.ll 
FundCodell 
=ll 
newFundCodell  +
;ll+ ,
_generalPersistnn 
.nn  
Updatenn  &
<nn& '
Fundsnn' ,
>nn, -
(nn- .
fundnn. 2
)nn2 3
;nn3 4
awaitpp 
_generalPersistpp %
.pp% &
SaveChangesAsyncpp& 6
(pp6 7
)pp7 8
;pp8 9
fundrr 
=rr 
awaitrr 
_fundsPersistrr *
.rr* +
GetFundByCodeAsyncrr+ =
(rr= >
newFundCoderr> I
)rrI J
;rrJ K
returntt 
fundtt 
;tt 
}uu 
catchvv 
(vv 
Systemvv 
.vv 
	Exceptionvv #
exvv$ &
)vv& '
{ww 
throwxx 
newxx 
	Exceptionxx #
(xx# $
exxx$ &
.xx& '
Messagexx' .
)xx. /
;xx/ 0
}yy 
}zz 	
public|| 
async|| 
Task|| 
<|| 
bool|| 
>|| 
AddFundsAsync||  -
(||- .
IEnumerable||. 9
<||9 :
DetailedFunds||: G
>||G H
detailedFunds||I V
)||V W
{}} 	
try~~ 
{ 
var
ÅÅ 
result
ÅÅ 
=
ÅÅ 
await
ÅÅ "
_fundsPersist
ÅÅ# 0
.
ÅÅ0 1
AddFundsAsync
ÅÅ1 >
(
ÅÅ> ?
detailedFunds
ÅÅ? L
)
ÅÅL M
;
ÅÅM N
return
ÉÉ 
result
ÉÉ 
;
ÉÉ 
}
ÖÖ 
catch
ÜÜ 
(
ÜÜ 
System
ÜÜ 
.
ÜÜ 
	Exception
ÜÜ #
ex
ÜÜ$ &
)
ÜÜ& '
{
áá 
throw
àà 
new
àà 
	Exception
àà #
(
àà# $
ex
àà$ &
.
àà& '
Message
àà' .
)
àà. /
;
àà/ 0
}
ââ 
}
ää 	
}
ãã 
}åå •
{C:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\FundsYieldService.cs
	namespace		 	
Investments		
 
.		 
Application		 !
{

 
public 

class 
FundsYieldService "
:# $
IFundsYieldService% 7
{ 
private 
readonly 
IFundsYeldPersist *
_fundsYeldPersist+ <
;< =
public 
FundsYieldService  
(  !
IFundsYeldPersist! 2
fundsYeldPersist3 C
)C D
{ 	
_fundsYeldPersist 
= 
fundsYeldPersist  0
;0 1
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
	FundsYeld& /
>/ 0
>0 1 
GetAllFundsYeldAsync2 F
(F G
)G H
{ 	
try 
{ 
var 
funds 
= 
await !
_fundsYeldPersist" 3
.3 4 
GetAllFundsYeldAsync4 H
(H I
)I J
;J K
return 
funds 
; 
} 
catch 
( 
System 
. 
	Exception #
ex$ &
)& '
{ 
throw 
new 
	Exception #
(# $
ex$ &
.& '
Message' .
). /
;/ 0
}   
}"" 	
public$$ 
async$$ 
Task$$ 
<$$ 
IEnumerable$$ %
<$$% &
	FundsYeld$$& /
>$$/ 0
>$$0 1"
GetFundYeldByCodeAsync$$2 H
($$H I
string$$I O
fundCode$$P X
)$$X Y
{%% 	
try&& 
{'' 
var(( 
funds(( 
=(( 
await(( !
_fundsYeldPersist((" 3
.((3 4"
GetFundYeldByCodeAsync((4 J
(((J K
fundCode((K S
)((S T
;((T U
return** 
funds** 
;** 
}++ 
catch,, 
(,, 
System,, 
.,, 
	Exception,, #
ex,,$ &
),,& '
{-- 
throw.. 
new.. 
	Exception.. #
(..# $
ex..$ &
...& '
Message..' .
)... /
;../ 0
}// 
}00 	
public22 
async22 
Task22 
<22 
bool22 
>22 
AddFundsYieldsAsync22  3
(223 4
IEnumerable224 ?
<22? @
	FundsYeld22@ I
>22I J

fundsYelds22K U
)22U V
{33 	
try44 
{55 
await77 
_fundsYeldPersist77 (
.77( )
AddFundsYieldsAsync77) <
(77< =

fundsYelds77= G
)77G H
;77H I
return<< 
true<< 
;<< 
}== 
catch>> 
(>> 
System>> 
.>> 
	Exception>> #
ex>>$ &
)>>& '
{?? 
Console@@ 
.@@ 
	WriteLine@@ !
(@@! "
ex@@" $
.@@$ %
Message@@% ,
)@@, -
;@@- .
returnAA 
falseAA 
;AA 
}BB 
}CC 	
}EE 
}FF ›

ÜC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\helpers\InvestimentosProfile.cs
	namespace 	
Investments
 
. 
Application !
.! "
helpers" )
{ 
public 

class 
InvestmentsProfile #
:$ %
Profile& -
{		 
public

 
InvestmentsProfile

 !
(

! "
)

" #
{ 	
	CreateMap 
< 
Stocks 
, 
	StocksDto '
>' (
(( )
)) *
.* +

ReverseMap+ 5
(5 6
)6 7
. 
	ForMember 
( 
dst 
=> 
dst !
.! "
MetaData" *
,* +
map 
=> 
map 
. 
MapFrom &
(& '
src' *
=>+ -
src. 1
.1 2
MetaData2 :
): ;
); <
;< =
	CreateMap 
< 
DetailedFunds #
,# $
RankOfTheBestFunds% 7
>7 8
(8 9
)9 :
.: ;

ReverseMap; E
(E F
)F G
;G H
} 	
} 
} †O
ÉC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\RankOfTheBestFundsService.cs
	namespace

 	
Investments


 
.

 
Application

 !
{ 
public 

class %
RankOfTheBestFundsService *
:+ ,&
IRankOfTheBestFundsService- G
{ 
private 
readonly &
IRankOfTheBestFundsPersist 3&
_rankOfTheBestFundsPersist4 N
;N O
private 
readonly  
IDetailedFundService - 
_detailedFundService. B
;B C
private 
readonly 
IFundsYieldService +
_fundsYeldService, =
;= >
private 
readonly 
IMapper  
_mapper! (
;( )
public %
RankOfTheBestFundsService (
(( )&
IRankOfTheBestFundsPersist) C%
rankOfTheBestFundsPersistD ]
,] ^ 
IDetailedFundService) =
detailedFundService> Q
,Q R
IFundsYieldService) ;
fundsYeldService< L
,L M
IMapper) 0
mapper1 7
)7 8
{ 	&
_rankOfTheBestFundsPersist &
=' (%
rankOfTheBestFundsPersist) B
;B C 
_detailedFundService  
=! "
detailedFundService# 6
;6 7
_fundsYeldService 
= 
fundsYeldService  0
;0 1
_mapper 
= 
mapper 
; 
} 	
public 
async 
Task 
< 
bool 
> &
AddRankOfTheBestFundsAsync  :
(: ;
IEnumerable; F
<F G
RankOfTheBestFundsG Y
>Y Z
rankOfTheBestFunds[ m
)m n
{   	
try!! 
{"" 
await## &
_rankOfTheBestFundsPersist## 0
.##0 1&
AddRankOfTheBestFundsAsync##1 K
(##K L
rankOfTheBestFunds##L ^
)##^ _
;##_ `
return%% 
true%% 
;%% 
}&& 
catch'' 
('' 
System'' 
.'' 
	Exception'' #
ex''$ &
)''& '
{(( 
throw)) 
new)) 
	Exception)) #
())# $
ex))$ &
.))& '
Message))' .
))). /
;))/ 0
}** 
},, 	
publicBB 
asyncBB 
TaskBB 
<BB 
IEnumerableBB %
<BB% &
RankOfTheBestFundsBB& 8
>BB8 9
>BB9 :1
%GetCalculationRankOfTheBestFundsAsyncBB; `
(BB` a
)BBa b
{CC 	
tryDD 
{EE 
varFF 
fundsFF 
=FF 
awaitFF ! 
_detailedFundServiceFF" 6
.FF6 7$
GetAllDetailedFundsAsyncFF7 O
(FFO P
)FFP Q
;FFQ R
varHH 
	bestFundsHH 
=HH 
_mapperHH  '
.HH' (
MapHH( +
<HH+ ,
IEnumerableHH, 7
<HH7 8
RankOfTheBestFundsHH8 J
>HHJ K
>HHK L
(HHL M
fundsHHM R
)HHR S
;HHS T
	bestFundsKK 
=KK 
	bestFundsKK %
.KK% &
WhereKK& +
(KK+ ,
xKK, -
=>KK. 0
xKK1 2
.KK2 3
	LiquidityKK3 <
>=KK= ?
$numKK@ G
)KKG H
;KKH I
	bestFundsNN 
=NN 
	bestFundsNN %
.NN% &
OrderByDescendingNN& 7
(NN7 8
xNN8 9
=>NN: <
xNN= >
.NN> ?
DividendYieldNN? L
)NNL M
;NNM N
forRR 
(RR 
intRR 
iRR 
=RR 
$numRR 
;RR 
iRR  !
<RR" #
	bestFundsRR$ -
.RR- .
CountRR. 3
(RR3 4
)RR4 5
;RR5 6
iRR7 8
++RR8 :
)RR: ;
{SS 
	bestFundsTT 
.TT 
	ElementAtTT '
(TT' (
iTT( )
)TT) *
.TT* + 
DividendYieldRankingTT+ ?
=TT@ A
iTTB C
+TTC D
$numTTD E
;TTE F
}UU 
	bestFundsYY 
=YY 
	bestFundsYY %
.YY% &
OrderByYY& -
(YY- .
xYY. /
=>YY0 2
xYY3 4
.YY4 5
PriceEquityValueYY5 E
)YYE F
;YYF G
for[[ 
([[ 
int[[ 
i[[ 
=[[ 
$num[[ 
;[[ 
i[[  !
<[[" #
	bestFunds[[$ -
.[[- .
Count[[. 3
([[3 4
)[[4 5
;[[5 6
i[[7 8
++[[8 :
)[[: ;
{\\ 
	bestFunds]] 
.]] 
	ElementAt]] '
(]]' (
i]]( )
)]]) *
.]]* +
	RankPrice]]+ 4
=]]5 6
i]]7 8
+]]8 9
$num]]9 :
;]]: ;
}^^ 
forbb 
(bb 
intbb 
ibb 
=bb 
$numbb 
;bb 
ibb  !
<bb" #
	bestFundsbb$ -
.bb- .
Countbb. 3
(bb3 4
)bb4 5
;bb5 6
ibb7 8
++bb8 :
)bb: ;
{cc 
	bestFundsdd 
.dd 
	ElementAtdd '
(dd' (
idd( )
)dd) *
.dd* +
MultiplierRankingdd+ <
=dd= >
	bestFundsee '
.ee' (
	ElementAtee( 1
(ee1 2
iee2 3
)ee3 4
.ee4 5 
DividendYieldRankingee5 I
+eeJ K
	bestFundseeL U
.eeU V
	ElementAteeV _
(ee_ `
iee` a
)eea b
.eeb c
	RankPriceeec l
;eel m
}ff 
	bestFundshh 
=hh 
	bestFundshh %
.hh% &
OrderByhh& -
(hh- .
xhh. /
=>hh0 2
xhh3 4
.hh4 5
MultiplierRankinghh5 F
)hhF G
;hhG H
forll 
(ll 
intll 
ill 
=ll 
$numll 
;ll 
ill  !
<ll" #
	bestFundsll$ -
.ll- .
Countll. 3
(ll3 4
)ll4 5
;ll5 6
ill7 8
++ll8 :
)ll: ;
{mm 
varoo 
resultoo 
=oo  
awaitoo! &
_fundsYeldServiceoo' 8
.oo8 9"
GetFundYeldByCodeAsyncoo9 O
(ooO P
	bestFundsooP Y
.ooY Z
	ElementAtooZ c
(ooc d
iood e
)ooe f
.oof g
FundCodeoog o
)ooo p
;oop q
resultqq 
=qq 
resultqq #
.qq# $
OrderByDescendingqq$ 5
(qq5 6
xqq6 7
=>qq8 :
xqq; <
.qq< =
LastComputedDateqq= M
)qqM N
.qqN O
TakeqqO S
(qqS T
$numqqT V
)qqV W
;qqW X
varss 
standardDeviationss )
=ss* +
resultss, 2
.ss2 3
Selectss3 9
(ss9 :
xss: ;
=>ss< >
xss? @
.ss@ A
ValuessA F
)ssF G
.ssG H
StandardDeviationssH Y
(ssY Z
)ssZ [
;ss[ \
varuu 
averageuu 
=uu  !
resultuu" (
.uu( )
Selectuu) /
(uu/ 0
xuu0 1
=>uu2 4
xuu5 6
.uu6 7
Valueuu7 <
)uu< =
.uu= >
Averageuu> E
(uuE F
)uuF G
;uuG H
varww !
CoefficienOfVariationww -
=ww. /
(ww0 1
standardDeviationww1 B
/wwC D
averagewwE L
)wwL M
*wwN O
$numwwP S
;wwS T
	bestFundsyy 
.yy 
	ElementAtyy '
(yy' (
iyy( )
)yy) *
.yy* +"
CoefficientOfVariationyy+ A
=yyB C!
CoefficienOfVariationyyD Y
;yyY Z
}zz 
	bestFunds|| 
=|| 
	bestFunds|| %
.||% &
Where||& +
(||+ ,
x||, -
=>||. 0
x||1 2
.||2 3"
CoefficientOfVariation||3 I
<=||J L
$num||M O
)||O P
;||P Q
return~~ 
	bestFunds~~  
;~~  !
} 
catch
ÄÄ 
(
ÄÄ 
System
ÄÄ 
.
ÄÄ 
	Exception
ÄÄ #
ex
ÄÄ$ &
)
ÄÄ& '
{
ÅÅ 
throw
ÇÇ 
new
ÇÇ 
	Exception
ÇÇ #
(
ÇÇ# $
ex
ÇÇ$ &
.
ÇÇ& '
Message
ÇÇ' .
)
ÇÇ. /
;
ÇÇ/ 0
}
ÉÉ 
}
ÑÑ 	
public
ÜÜ 
async
ÜÜ 
Task
ÜÜ 
<
ÜÜ 
IEnumerable
ÜÜ %
<
ÜÜ% & 
RankOfTheBestFunds
ÜÜ& 8
>
ÜÜ8 9
>
ÜÜ9 :(
GetRankOfTheBestFundsAsync
ÜÜ; U
(
ÜÜU V
int
ÜÜV Y
?
ÜÜY Z
totalFundsInRank
ÜÜ[ k
=
ÜÜl m
null
ÜÜn r
)
ÜÜr s
{
áá 	
try
àà 
{
ââ 
var
ãã 
	bestFunds
ãã 
=
ãã 
await
ãã  %(
_rankOfTheBestFundsPersist
ãã& @
.
ãã@ A(
GetRankOfTheBestFundsAsync
ããA [
(
ãã[ \
totalFundsInRank
ãã\ l
)
ããl m
;
ããm n
return
çç 
	bestFunds
çç  
;
çç  !
}
èè 
catch
êê 
(
êê 
System
êê 
.
êê 
	Exception
êê #
ex
êê$ &
)
êê& '
{
ëë 
throw
íí 
new
íí 
	Exception
íí #
(
íí# $
ex
íí$ &
.
íí& '
Message
íí' .
)
íí. /
;
íí/ 0
}
ìì 
}
îî 	
}
ññ 
}óó ç
wC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\StocksService.cs
	namespace 	
Investments
 
. 
Application !
{ 
public 

class 
StocksService 
:  
IStocksService! /
{ 
private 
IMapper 
_mapper 
;  
public 
StocksService 
( 
IMapper $
mapper% +
)+ ,
{ 	
_mapper 
= 
mapper 
; 
} 	
public 
async 
Task 
< 
	StocksDto #
># $
Get% (
(( )
string) /
name0 4
)4 5
{ 	
string 
	QUERY_URL 
= 
$" !P
Dhttps://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=! e
{e f
namef j
}j k(
.SA&apikey=QXCJRQTC6J5C74GS	k Ü
"
Ü á
;
á à
Uri 
queryUri 
= 
new 
Uri "
(" #
	QUERY_URL# ,
), -
;- .
Stocks 
stocks 
; 
	StocksDto 
	stocksDto 
=  !
new" %
	StocksDto& /
(/ 0
)0 1
;1 2
using 
( 

HttpClient 
client $
=% &
new' *

HttpClient+ 5
(5 6
)6 7
)7 8
{ 
client   
.   
BaseAddress   "
=  # $
queryUri  % -
;  - .
client!! 
.!! !
DefaultRequestHeaders!! ,
.!!, -
Accept!!- 3
.!!3 4
Clear!!4 9
(!!9 :
)!!: ;
;!!; <
client"" 
."" !
DefaultRequestHeaders"" ,
."", -
Accept""- 3
.""3 4
Add""4 7
(""7 8
new""8 ;+
MediaTypeWithQualityHeaderValue""< [
(""[ \
$str""\ n
)""n o
)""o p
;""p q
var$$ 
response$$ 
=$$ 
await$$ $
client$$% +
.$$+ ,
GetAsync$$, 4
($$4 5
	QUERY_URL$$5 >
)$$> ?
;$$? @
string%% 
data%% 
=%% 
await%% #
response%%$ ,
.%%, -
Content%%- 4
.%%4 5
ReadAsStringAsync%%5 F
(%%F G
)%%G H
;%%H I
stocks'' 
='' 
JsonConvert'' %
.''% &
DeserializeObject''& 7
<''7 8
Stocks''8 >
>''> ?
(''? @
data''@ D
)''D E
;''E F
try)) 
{** 
var++ 
kkkk++ 
=++ 
_mapper++ &
.++& '
Map++' *
<++* +
	StocksDto+++ 4
>++4 5
(++5 6
stocks++6 <
)++< =
;++= >
},, 
catch-- 
(-- 
System-- 
.-- 
	Exception-- &
ex--' )
)--) *
{.. 
Console// 
.// 
Write// !
(//! "
ex//" $
.//$ %
Message//% ,
)//, -
;//- .
}00 
}11 
return33 
	stocksDto33 
;33 
}44 	
}55 
}66 ˜Æ
âC:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\WebScrapingFundsAndYeldsService.cs
	namespace 	
Investments
 
. 
Application !
{ 
public 

class +
WebScrapingFundsAndYeldsService 0
:1 2,
 IWebScrapingFundsAndYeldsService3 S
{ 

IWebDriver 
driver 
; 
IGeneralPersist 
_generalPersist '
;' (
IFundsPersist 
_fundsPersist #
;# $
IFundsYeldPersist 
_fundsYeldPersist +
;+ ,,
 IWebScrapingFundsAndYeldsPersist (,
 _webScrapingFundsAndYeldsPersist) I
;I J
public +
WebScrapingFundsAndYeldsService .
(. /
IGeneralPersist/ >
generalPersist? M
,M N
IFundsPersist/ <
fundsPersist= I
,I J
IFundsYeldPersist/ @
fundsYeldPersistA Q
,Q R,
 IWebScrapingFundsAndYeldsPersist/ O+
webScrapingFundsAndYeldsPersistP o
)o p
{ 	
_generalPersist 
= 
generalPersist ,
;, -
_fundsPersist 
= 
fundsPersist (
;( )
_fundsYeldPersist   
=   
fundsYeldPersist    0
;  0 1,
 _webScrapingFundsAndYeldsPersist!! ,
=!!- .+
webScrapingFundsAndYeldsPersist!!/ N
;!!N O
ConfigDriver## 
(## 
)## 
;## 
}$$ 	
public&& 
async&& 
Task&& 
<&& 
IEnumerable&& %
<&&% &
DetailedFunds&&& 3
>&&3 4
>&&4 5
GetFundsAsync&&6 C
(&&C D
)&&D E
{'' 	
await)) 
VariablesManager)) "
.))" #
ConectionsWebSocket))# 6
.))6 7
socketManager))7 D
.))D E!
SendMessageToAllAsync))E Z
())Z [
JsonConvert))[ f
.))f g
SerializeObject))g v
())v w
$str	))w ë
)
))ë í
)
))í ì
;
))ì î
GoToPage++ 
(++ 
$str++ G
)++G H
;++H I
List-- 
<-- 
string-- 
>-- #
orderColumnTableOfFunds-- 0
=--1 2
new--3 6
List--7 ;
<--; <
string--< B
>--B C
{.. 
$str// 
,// 
$str// #
,//# $
$str//% .
,//. /
$str//0 ;
,//; <
$str00  
,00  !
$str00" (
,00( )
$str00* <
,00< =
$str11 
,11 
$str11 ,
,11, -
$str11. ;
,11; <
$str22  
,22  !
$str22" ,
,22, -
$str22. >
,22> ?
$str33 
}44 
;44 
int66 !
totalOfColumnExpected66 %
=66& '#
orderColumnTableOfFunds66( ?
.66? @
Count66@ E
;66E F
var77 
detailedFunds77 
=77 
new77  #
List77$ (
<77( )
DetailedFunds77) 6
>776 7
(777 8
)778 9
;779 :
try99 
{:: 
var<< 
wait<< 
=<< 
new<< 
WebDriverWait<< ,
(<<, -
driver<<- 3
,<<3 4
TimeSpan<<5 =
.<<= >
FromSeconds<<> I
(<<I J
$num<<J L
)<<L M
)<<M N
;<<N O
wait@@ 
.@@ 
Until@@ 
(@@ 
ExpectedConditions@@ -
.@@- .,
 VisibilityOfAllElementsLocatedBy@@. N
(@@N O
By@@O Q
.@@Q R
XPath@@R W
(@@W X
$str@@X }
)@@} ~
)@@~ 
)	@@ Ä
;
@@Ä Å
awaitCC 
VariablesManagerCC &
.CC& '
ConectionsWebSocketCC' :
.CC: ;
socketManagerCC; H
.CCH I!
SendMessageToAllAsyncCCI ^
(CC^ _
JsonConvertCC_ j
.CCj k
SerializeObjectCCk z
(CCz {
driver	CC{ Å
.
CCÅ Ç

PageSource
CCÇ å
)
CCå ç
)
CCç é
;
CCé è
varPP 
rowsPP 
=PP 
driverPP !
.PP! "
FindElementsPP" .
(PP. /
ByPP/ 1
.PP1 2
XPathPP2 7
(PP7 8
$strPP8 ]
)PP] ^
)PP^ _
;PP_ `
intQQ 
numberOfLinesQQ !
=QQ" #
rowsQQ$ (
.QQ( )
CountQQ) .
;QQ. /
ConsoleSS 
.SS 
	WriteLineSS !
(SS! "
$"SS" $
Total de linhas SS$ 4
{SS4 5
numberOfLinesSS5 B
}SSB C
"SSC D
)SSD E
;SSE F
varUU 
columnsUU 
=UU 
driverUU $
.UU$ %
FindElementsUU% 1
(UU1 2
ByUU2 4
.UU4 5
XPathUU5 :
(UU: ;
$strUU; c
)UUc d
)UUd e
;UUe f
intVV 
numberOfColumnVV "
=VV# $
columnsVV% ,
.VV, -
CountVV- 2
;VV2 3
ConsoleXX 
.XX 
	WriteLineXX !
(XX! "
$"XX" $
Total de colunas XX$ 5
{XX5 6
numberOfColumnXX6 D
}XXD E
"XXE F
)XXF G
;XXG H
ifZZ 
(ZZ !
totalOfColumnExpectedZZ (
!=ZZ) +
numberOfColumnZZ, :
)ZZ: ;
{[[ 
Console\\ 
.\\ 
	WriteLine\\ %
(\\% &
$"\\& (.
"Total of columns expected invalid \\( J
{\\J K
numberOfColumn\\K Y
}\\Y Z
"\\Z [
)\\[ \
;\\\ ]
}]] 
for__ 
(__ 
int__ 
j__ 
=__ 
$num__ 
;__ 
j__  !
<=__" $
numberOfColumn__% 3
-__4 5
$num__6 7
;__7 8
j__9 :
++__: <
)__< =
{`` 
stringaa 
campoaa  
=aa! "
driveraa# )
.aa) *
FindElementaa* 5
(aa5 6
Byaa6 8
.aa8 9
XPathaa9 >
(aa> ?
$"aa? A0
$//*[@id='tabelaResultado']/thead/tr[aaA e
{aae f
$numaaf g
}aag h
]/th[aah m
{aam n
jaan o
}aao p
]aap q
"aaq r
)aar s
)aas t
.aat u
Textaau y
;aay z
ifcc 
(cc #
orderColumnTableOfFundscc .
[cc. /
jcc/ 0
-cc0 1
$numcc1 2
]cc2 3
!=cc4 6
campocc7 <
)cc< =
{dd 
Consoleee 
.ee  
Writeee  %
(ee% &
$"ee& (
Ordem inv√°lida! ee( 8
{ee8 9#
orderColumnTableOfFundsee9 P
[eeP Q
jeeQ R
-eeR S
$numeeS T
]eeT U
}eeU V

 esperado eeV `
{ee` a
campoeea f
}eef g
"eeg h
)eeh i
;eei j
returnff 
awaitff $
Taskff% )
.ff) *

FromResultff* 4
<ff4 5
IEnumerableff5 @
<ff@ A
DetailedFundsffA N
>ffN O
>ffO P
(ffP Q
detailedFundsffQ ^
)ff^ _
;ff_ `
}gg 
Consoleii 
.ii 
Writeii !
(ii! "
driverii" (
.ii( )
FindElementii) 4
(ii4 5
Byii5 7
.ii7 8
XPathii8 =
(ii= >
$"ii> @0
$//*[@id='tabelaResultado']/thead/tr[ii@ d
{iid e
$numiie f
}iif g
]/th[iig l
{iil m
jiim n
}iin o
]iio p
"iip q
)iiq r
)iir s
.iis t
Textiit x
+iiy z
$str	ii{ Ä
)
iiÄ Å
;
iiÅ Ç
}jj 
forll 
(ll 
intll 
ill 
=ll 
$numll 
;ll 
ill  !
<=ll" $
numberOfLinesll% 2
;ll2 3
ill4 5
++ll5 7
)ll7 8
{mm 
stringnn 
[nn 
]nn 
objnn  
=nn! "
newnn# &
stringnn' -
[nn- .
$numnn. 0
]nn0 1
;nn1 2
objoo 
[oo 
$numoo 
]oo 
=oo 
ioo 
.oo 
ToStringoo '
(oo' (
)oo( )
;oo) *
forqq 
(qq 
intqq 
jqq 
=qq  
$numqq! "
;qq" #
jqq$ %
<=qq& (
numberOfColumnqq) 7
-qq8 9
$numqq: ;
;qq; <
jqq= >
++qq> @
)qq@ A
{rr 
varss 
ess 
=ss 
driverss  &
.ss& '
FindElementss' 2
(ss2 3
Byss3 5
.ss5 6
XPathss6 ;
(ss; <
$"ss< >0
$//*[@id='tabelaResultado']/tbody/tr[ss> b
{ssb c
issc d
}ssd e
]/td[sse j
{ssj k
jssk l
}ssl m
]ssm n
"ssn o
)sso p
)ssp q
.ssq r
Textssr v
;ssv w
objtt 
[tt 
jtt 
]tt 
=tt  
ett! "
;tt" #
}uu 
varww 
fundww 
=ww 
newww "
DetailedFundsww# 0
(ww0 1
)ww1 2
{ww2 3
FundCodexx  
=xx! "
objxx# &
[xx& '
$numxx' (
]xx( )
,xx) *
Segmentxx+ 2
=xx3 4
objxx5 8
[xx8 9
$numxx9 :
]xx: ;
,xx; <
	Quotationxx= F
=xxG H
ConvertxxI P
.xxP Q
ToDoublexxQ Y
(xxY Z
objxxZ ]
[xx] ^
$numxx^ _
]xx_ `
)xx` a
,xxa b
FFOYieldyy  
=yy! "
Convertyy# *
.yy* +
ToDoubleyy+ 3
(yy3 4
objyy4 7
[yy7 8
$numyy8 9
]yy9 :
.yy: ;
Replaceyy; B
(yyB C
$stryyC F
,yyF G
$stryyH J
)yyJ K
)yyK L
,yyL M
DividendYieldyyN [
=yy\ ]
Convertyy^ e
.yye f
ToDoubleyyf n
(yyn o
objyyo r
[yyr s
$numyys t
]yyt u
.yyu v
Replaceyyv }
(yy} ~
$str	yy~ Å
,
yyÅ Ç
$str
yyÉ Ö
)
yyÖ Ü
)
yyÜ á
,
yyá à
PriceEquityValuezz (
=zz) *
Convertzz+ 2
.zz2 3
ToDoublezz3 ;
(zz; <
objzz< ?
[zz? @
$numzz@ A
]zzA B
)zzB C
,zzC D
ValueOfMarketzzE R
=zzS T
ConvertzzU \
.zz\ ]
ToDoublezz] e
(zze f
objzzf i
[zzi j
$numzzj k
]zzk l
)zzl m
,zzm n
	Liquidity{{ !
={{" #
Convert{{$ +
.{{+ ,
ToDouble{{, 4
({{4 5
obj{{5 8
[{{8 9
$num{{9 :
]{{: ;
){{; <
,{{< =
NumberOfProperties{{> P
={{Q R
Convert{{S Z
.{{Z [
ToDouble{{[ c
({{c d
obj{{d g
[{{g h
$num{{h i
]{{i j
){{j k
,{{k l
SquareMeterPrice|| (
=||) *
Convert||+ 2
.||2 3
ToDouble||3 ;
(||; <
obj||< ?
[||? @
$num||@ B
]||B C
)||C D
,||D E
RentPerSquareMeter||F X
=||Y Z
Convert||[ b
.||b c
ToDouble||c k
(||k l
obj||l o
[||o p
$num||p r
]||r s
)||s t
,||t u
CapRate}} 
=}}  !
Convert}}" )
.}}) *
ToDouble}}* 2
(}}2 3
obj}}3 6
[}}6 7
$num}}7 9
]}}9 :
.}}: ;
Replace}}; B
(}}B C
$str}}C F
,}}F G
$str}}H J
)}}J K
)}}K L
,}}L M
AverageVacancy}}N \
=}}] ^
Convert}}_ f
.}}f g
ToDouble}}g o
(}}o p
obj}}p s
[}}s t
$num}}t v
]}}v w
.}}w x
Replace}}x 
(	}} Ä
$str
}}Ä É
,
}}É Ñ
$str
}}Ö á
)
}}á à
)
}}à â
}~~ 
;~~ 
await
ÄÄ 
VariablesManager
ÄÄ *
.
ÄÄ* +!
ConectionsWebSocket
ÄÄ+ >
.
ÄÄ> ?
socketManager
ÄÄ? L
.
ÄÄL M#
SendMessageToAllAsync
ÄÄM b
(
ÄÄb c
JsonConvert
ÄÄc n
.
ÄÄn o
SerializeObject
ÄÄo ~
(
ÄÄ~ 
fundÄÄ É
)ÄÄÉ Ñ
)ÄÄÑ Ö
;ÄÄÖ Ü
Console
ÅÅ 
.
ÅÅ 
	WriteLine
ÅÅ %
(
ÅÅ% &
$"
ÅÅ& (
{
ÅÅ( )
JsonConvert
ÅÅ) 4
.
ÅÅ4 5
SerializeObject
ÅÅ5 D
(
ÅÅD E
fund
ÅÅE I
)
ÅÅI J
}
ÅÅJ K
"
ÅÅK L
)
ÅÅL M
;
ÅÅM N
detailedFunds
ÉÉ !
.
ÉÉ! "
Add
ÉÉ" %
(
ÉÉ% &
fund
ÉÉ& *
)
ÉÉ* +
;
ÉÉ+ ,
}
ÑÑ 
driver
ÜÜ 
.
ÜÜ 
Close
ÜÜ 
(
ÜÜ 
)
ÜÜ 
;
ÜÜ 
await
àà 
VariablesManager
àà &
.
àà& '!
ConectionsWebSocket
àà' :
.
àà: ;
socketManager
àà; H
.
ààH I#
SendMessageToAllAsync
ààI ^
(
àà^ _
JsonConvert
àà_ j
.
ààj k
SerializeObject
ààk z
(
ààz {
$stràà{ ò
)ààò ô
)ààô ö
;ààö õ
return
ää 
await
ää 
Task
ää !
.
ää! "

FromResult
ää" ,
<
ää, -
IEnumerable
ää- 8
<
ää8 9
DetailedFunds
ää9 F
>
ääF G
>
ääG H
(
ääH I
detailedFunds
ääI V
)
ääV W
;
ääW X
}
åå 
catch
çç 
(
çç 
System
çç 
.
çç 
	Exception
çç #
ex
çç$ &
)
çç& '
{
éé 
driver
èè 
.
èè 
Close
èè 
(
èè 
)
èè 
;
èè 
Console
êê 
.
êê 
	WriteLine
êê !
(
êê! "
ex
êê" $
.
êê$ %
Message
êê% ,
)
êê, -
;
êê- .
await
ëë 
VariablesManager
ëë &
.
ëë& '!
ConectionsWebSocket
ëë' :
.
ëë: ;
socketManager
ëë; H
.
ëëH I#
SendMessageToAllAsync
ëëI ^
(
ëë^ _
JsonConvert
ëë_ j
.
ëëj k
SerializeObject
ëëk z
(
ëëz {
$strëë{ ì
)ëëì î
)ëëî ï
;ëëï ñ
return
íí 
await
íí 
Task
íí !
.
íí! "

FromResult
íí" ,
<
íí, -
IEnumerable
íí- 8
<
íí8 9
DetailedFunds
íí9 F
>
ííF G
>
ííG H
(
ííH I
detailedFunds
ííI V
)
ííV W
;
ííW X
}
ìì 
}
ïï 	
public
óó 
async
óó 
Task
óó 
<
óó 
IEnumerable
óó %
<
óó% &
	FundsYeld
óó& /
>
óó/ 0
>
óó0 1 
GetYeldsFundsAsync
óó2 D
(
óóD E
IEnumerable
óóE P
<
óóP Q
DetailedFunds
óóQ ^
>
óó^ _
detailedFunds
óó` m
)
óóm n
{
òò 	
var
öö 

fundsYelds
öö 
=
öö 
new
öö  
List
öö! %
<
öö% &
	FundsYeld
öö& /
>
öö/ 0
(
öö0 1
)
öö1 2
;
öö2 3
var
õõ 
fundsYeldsTmp
õõ 
=
õõ 
new
õõ  #
List
õõ$ (
<
õõ( )
	FundsYeld
õõ) 2
>
õõ2 3
(
õõ3 4
)
õõ4 5
;
õõ5 6
var
úú 
totalFundYeldsDb
úú  
=
úú! "
new
úú# &
List
úú' +
<
úú+ ,
	FundsYeld
úú, 5
>
úú5 6
(
úú6 7
)
úú7 8
;
úú8 9
dynamic
ûû 

lastDateDB
ûû 
=
ûû  
null
ûû! %
;
ûû% &
dynamic
üü 
rows
üü 
;
üü 
dynamic
†† 
columns
†† 
;
†† 
int
¢¢ 
numberOfLines
¢¢ 
=
¢¢ 
$num
¢¢  !
;
¢¢! "
int
££ 
numberOfColumn
££ 
=
££  
$num
££! "
;
££" #
List
•• 
<
•• 
string
•• 
>
•• %
orderColumnTableOfFunds
•• 0
=
••1 2
new
••3 6
List
••7 ;
<
••; <
string
••< B
>
••B C
{
¶¶ 
$str
ßß !
,
ßß! "
$str
ßß# )
,
ßß) *
$str
ßß+ >
,
ßß> ?
$str
ßß@ G
}
®® 
;
®® 
int
™™ #
totalOfColumnExpected
™™ %
=
™™& '%
orderColumnTableOfFunds
™™( ?
.
™™? @
Count
™™@ E
;
™™E F
try
¨¨ 
{
≠≠ 
await
ØØ 
VariablesManager
ØØ &
.
ØØ& '!
ConectionsWebSocket
ØØ' :
.
ØØ: ;
socketManager
ØØ; H
.
ØØH I#
SendMessageToAllAsync
ØØI ^
(
ØØ^ _
JsonConvert
ØØ_ j
.
ØØj k
SerializeObject
ØØk z
(
ØØz {
$strØØ{ ô
)ØØô ö
)ØØö õ
;ØØõ ú
foreach
±± 
(
±± 
var
±± 
fund
±± !
in
±±" $
detailedFunds
±±% 2
)
±±2 3
{
≤≤ 
GoToPage
¥¥ 
(
¥¥ 
$"
¥¥ E
7https://www.fundamentus.com.br/fii_proventos.php?papel=
¥¥ V
{
¥¥V W
fund
¥¥W [
.
¥¥[ \
FundCode
¥¥\ d
}
¥¥d e
"
¥¥e f
)
¥¥f g
;
¥¥g h
try
∂∂ 
{
∑∑ 
var
ππ 
wait
ππ  
=
ππ! "
new
ππ# &
WebDriverWait
ππ' 4
(
ππ4 5
driver
ππ5 ;
,
ππ; <
TimeSpan
ππ= E
.
ππE F
FromSeconds
ππF Q
(
ππQ R
$num
ππR T
)
ππT U
)
ππU V
;
ππV W
wait
ºº 
.
ºº 
Until
ºº "
(
ºº" # 
ExpectedConditions
ºº# 5
.
ºº5 6.
 VisibilityOfAllElementsLocatedBy
ºº6 V
(
ººV W
By
ººW Y
.
ººY Z
XPath
ººZ _
(
ºº_ `
$str
ºº` 
)ºº Ä
)ººÄ Å
)ººÅ Ç
;ººÇ É
rows
ææ 
=
ææ 
driver
ææ %
.
ææ% &
FindElements
ææ& 2
(
ææ2 3
By
ææ3 5
.
ææ5 6
XPath
ææ6 ;
(
ææ; <
$str
ææ< [
)
ææ[ \
)
ææ\ ]
;
ææ] ^
numberOfLines
øø %
=
øø& '
rows
øø( ,
.
øø, -
Count
øø- 2
;
øø2 3
Console
¡¡ 
.
¡¡  
	WriteLine
¡¡  )
(
¡¡) *
$"
¡¡* ,
Total de linhas 
¡¡, <
{
¡¡< =
numberOfLines
¡¡= J
}
¡¡J K
"
¡¡K L
)
¡¡L M
;
¡¡M N
columns
√√ 
=
√√  !
driver
√√" (
.
√√( )
FindElements
√√) 5
(
√√5 6
By
√√6 8
.
√√8 9
XPath
√√9 >
(
√√> ?
$str
√√? a
)
√√a b
)
√√b c
;
√√c d
numberOfColumn
ƒƒ &
=
ƒƒ' (
columns
ƒƒ) 0
.
ƒƒ0 1
Count
ƒƒ1 6
;
ƒƒ6 7
Console
∆∆ 
.
∆∆  
	WriteLine
∆∆  )
(
∆∆) *
$"
∆∆* ,
Total de colunas 
∆∆, =
{
∆∆= >
numberOfColumn
∆∆> L
}
∆∆L M
"
∆∆M N
)
∆∆N O
;
∆∆O P
if
»» 
(
»» #
totalOfColumnExpected
»» 0
!=
»»1 3
numberOfColumn
»»4 B
)
»»B C
{
…… 
Console
   #
.
  # $
	WriteLine
  $ -
(
  - .
$"
  . 00
"Total of columns expected invalid 
  0 R
{
  R S
numberOfColumn
  S a
}
  a b
"
  b c
)
  c d
;
  d e
}
ÀÀ 
Console
ÕÕ 
.
ÕÕ  
	WriteLine
ÕÕ  )
(
ÕÕ) *
driver
ÕÕ* 0
.
ÕÕ0 1
FindElement
ÕÕ1 <
(
ÕÕ< =
By
ÕÕ= ?
.
ÕÕ? @
XPath
ÕÕ@ E
(
ÕÕE F
$str
ÕÕF p
)
ÕÕp q
)
ÕÕq r
.
ÕÕr s
Text
ÕÕs w
)
ÕÕw x
;
ÕÕx y
}
ŒŒ 
catch
œœ 
(
œœ 
System
œœ !
.
œœ! "
	Exception
œœ" +
)
œœ+ ,
{
–– 
continue
——  
;
——  !
}
““ 
if
‘‘ 
(
‘‘ 
numberOfLines
‘‘ $
>
‘‘% &
$num
‘‘' )
)
‘‘) *
{
’’ 
numberOfLines
÷÷ %
=
÷÷& '
$num
÷÷( *
;
÷÷* +
}
◊◊ 
for
ŸŸ 
(
ŸŸ 
int
ŸŸ 
j
ŸŸ 
=
ŸŸ  
$num
ŸŸ! "
;
ŸŸ" #
j
ŸŸ$ %
<=
ŸŸ& (
numberOfColumn
ŸŸ) 7
;
ŸŸ7 8
j
ŸŸ9 :
++
ŸŸ: <
)
ŸŸ< =
{
⁄⁄ 
string
€€ 
campo
€€ $
=
€€% &
driver
€€' -
.
€€- .
FindElement
€€. 9
(
€€9 :
By
€€: <
.
€€< =
XPath
€€= B
(
€€B C
$"
€€C E,
//*[@id='resultado']/thead/tr[
€€E c
{
€€c d
$num
€€d e
}
€€e f
]/th[
€€f k
{
€€k l
j
€€l m
}
€€m n
]
€€n o
"
€€o p
)
€€p q
)
€€q r
.
€€r s
Text
€€s w
;
€€w x
if
›› 
(
›› %
orderColumnTableOfFunds
›› 2
[
››2 3
j
››3 4
-
››4 5
$num
››5 6
]
››6 7
!=
››8 :
campo
››; @
)
››@ A
{
ﬁﬁ 
Console
ﬂﬂ #
.
ﬂﬂ# $
Write
ﬂﬂ$ )
(
ﬂﬂ) *
$"
ﬂﬂ* ,
Ordem inv√°lida! 
ﬂﬂ, <
{
ﬂﬂ< =%
orderColumnTableOfFunds
ﬂﬂ= T
[
ﬂﬂT U
j
ﬂﬂU V
-
ﬂﬂV W
$num
ﬂﬂW X
]
ﬂﬂX Y
}
ﬂﬂY Z

 esperado 
ﬂﬂZ d
{
ﬂﬂd e
campo
ﬂﬂe j
}
ﬂﬂj k
"
ﬂﬂk l
)
ﬂﬂl m
;
ﬂﬂm n
return
‡‡ "
await
‡‡# (
Task
‡‡) -
.
‡‡- .

FromResult
‡‡. 8
<
‡‡8 9
IEnumerable
‡‡9 D
<
‡‡D E
	FundsYeld
‡‡E N
>
‡‡N O
>
‡‡O P
(
‡‡P Q

fundsYelds
‡‡Q [
)
‡‡[ \
;
‡‡\ ]
}
·· 
Console
„„ 
.
„„  
Write
„„  %
(
„„% &
driver
„„& ,
.
„„, -
FindElement
„„- 8
(
„„8 9
By
„„9 ;
.
„„; <
XPath
„„< A
(
„„A B
$"
„„B D,
//*[@id='resultado']/thead/tr[
„„D b
{
„„b c
$num
„„c d
}
„„d e
]/th[
„„e j
{
„„j k
j
„„k l
}
„„l m
]
„„m n
"
„„n o
)
„„o p
)
„„p q
.
„„q r
Text
„„r v
+
„„w x
$str
„„y ~
)
„„~ 
;„„ Ä
}
‰‰ 
var
ÊÊ 
result
ÊÊ 
=
ÊÊ  
await
ÊÊ! &
_fundsYeldPersist
ÊÊ' 8
.
ÊÊ8 9$
GetFundYeldByCodeAsync
ÊÊ9 O
(
ÊÊO P
fund
ÊÊP T
.
ÊÊT U
FundCode
ÊÊU ]
.
ÊÊ] ^
ToUpper
ÊÊ^ e
(
ÊÊe f
)
ÊÊf g
.
ÊÊg h
Trim
ÊÊh l
(
ÊÊl m
)
ÊÊm n
)
ÊÊn o
;
ÊÊo p
if
ËË 
(
ËË 
result
ËË 
!=
ËË  
null
ËË! %
)
ËË% &
{
ÈÈ 
totalFundYeldsDb
ÍÍ (
=
ÍÍ) *
result
ÍÍ+ 1
.
ÍÍ1 2
ToList
ÍÍ2 8
(
ÍÍ8 9
)
ÍÍ9 :
;
ÍÍ: ;

lastDateDB
ÏÏ "
=
ÏÏ# $
result
ÏÏ% +
.
ÏÏ+ ,
OrderByDescending
ÏÏ, =
(
ÏÏ= >
x
ÏÏ> ?
=>
ÏÏ? A
x
ÏÏB C
.
ÏÏC D
LastComputedDate
ÏÏD T
)
ÏÏT U
.
ÏÏU V
Take
ÏÏV Z
(
ÏÏZ [
$num
ÏÏ[ \
)
ÏÏ\ ]
.
ÏÏ] ^
Select
ÏÏ^ d
(
ÏÏd e
x
ÏÏe f
=>
ÏÏf h
x
ÏÏh i
.
ÏÏi j
LastComputedDate
ÏÏj z
)
ÏÏz {
.
ÏÏ{ |
FirstOrDefaultÏÏ| ä
(ÏÏä ã
)ÏÏã å
;ÏÏå ç
}
ÌÌ 
for
ÔÔ 
(
ÔÔ 
int
ÔÔ 
i
ÔÔ 
=
ÔÔ  
$num
ÔÔ! "
;
ÔÔ" #
i
ÔÔ$ %
<=
ÔÔ& (
numberOfLines
ÔÔ) 6
;
ÔÔ6 7
i
ÔÔ8 9
++
ÔÔ9 ;
)
ÔÔ; <
{
 
string
ÚÚ 
[
ÚÚ 
]
ÚÚ  
obj
ÚÚ! $
=
ÚÚ% &
new
ÚÚ' *
string
ÚÚ+ 1
[
ÚÚ1 2
$num
ÚÚ2 3
]
ÚÚ3 4
;
ÚÚ4 5
for
ÙÙ 
(
ÙÙ 
int
ÙÙ  
j
ÙÙ! "
=
ÙÙ# $
$num
ÙÙ% &
;
ÙÙ& '
j
ÙÙ( )
<=
ÙÙ* ,
numberOfColumn
ÙÙ- ;
;
ÙÙ; <
j
ÙÙ= >
++
ÙÙ> @
)
ÙÙ@ A
{
ıı 
var
ˆˆ 
e
ˆˆ  !
=
ˆˆ" #
driver
ˆˆ$ *
.
ˆˆ* +
FindElement
ˆˆ+ 6
(
ˆˆ6 7
By
ˆˆ7 9
.
ˆˆ9 :
XPath
ˆˆ: ?
(
ˆˆ? @
$"
ˆˆ@ B,
//*[@id='resultado']/tbody/tr[
ˆˆB `
{
ˆˆ` a
i
ˆˆa b
}
ˆˆb c
]/td[
ˆˆc h
{
ˆˆh i
j
ˆˆi j
}
ˆˆj k
]
ˆˆk l
"
ˆˆl m
)
ˆˆm n
)
ˆˆn o
.
ˆˆo p
Text
ˆˆp t
;
ˆˆt u
obj
˜˜ 
[
˜˜  
j
˜˜  !
-
˜˜" #
$num
˜˜$ %
]
˜˜% &
=
˜˜' (
e
˜˜) *
;
˜˜* +
}
¯¯ 
var
˙˙ 
fY
˙˙ 
=
˙˙  
new
˙˙! $
	FundsYeld
˙˙% .
(
˙˙. /
)
˙˙/ 0
{
˙˙0 1
FundCode
˚˚ $
=
˚˚% &
fund
˚˚' +
.
˚˚+ ,
FundCode
˚˚, 4
,
˚˚4 5
LastComputedDate
¸¸ ,
=
¸¸- .
Convert
¸¸/ 6
.
¸¸6 7

ToDateTime
¸¸7 A
(
¸¸A B
obj
¸¸B E
[
¸¸E F
$num
¸¸F G
]
¸¸G H
)
¸¸H I
.
¸¸I J
AddDays
¸¸J Q
(
¸¸Q R
$num
¸¸R S
)
¸¸S T
,
¸¸T U
Type
˝˝  
=
˝˝! "
obj
˝˝# &
[
˝˝& '
$num
˝˝' (
]
˝˝( )
,
˝˝) *
DatePayment
˛˛ '
=
˛˛( )
Convert
˛˛* 1
.
˛˛1 2

ToDateTime
˛˛2 <
(
˛˛< =
obj
˛˛= @
[
˛˛@ A
$num
˛˛A B
]
˛˛B C
)
˛˛C D
,
˛˛D E
Value
ˇˇ !
=
ˇˇ" #
Convert
ˇˇ$ +
.
ˇˇ+ ,
ToDouble
ˇˇ, 4
(
ˇˇ4 5
obj
ˇˇ5 8
[
ˇˇ8 9
$num
ˇˇ9 :
]
ˇˇ: ;
)
ˇˇ; <
}
ÄÄ 
;
ÄÄ 
Console
ÇÇ 
.
ÇÇ  
	WriteLine
ÇÇ  )
(
ÇÇ) *
$"
ÇÇ* ,
{
ÇÇ, -
JsonConvert
ÇÇ- 8
.
ÇÇ8 9
SerializeObject
ÇÇ9 H
(
ÇÇH I
fY
ÇÇI K
)
ÇÇK L
}
ÇÇL M
"
ÇÇM N
)
ÇÇN O
;
ÇÇO P
await
ÉÉ 
VariablesManager
ÉÉ .
.
ÉÉ. /!
ConectionsWebSocket
ÉÉ/ B
.
ÉÉB C
socketManager
ÉÉC P
.
ÉÉP Q#
SendMessageToAllAsync
ÉÉQ f
(
ÉÉf g
JsonConvert
ÉÉg r
.
ÉÉr s
SerializeObjectÉÉs Ç
(ÉÉÇ É
fYÉÉÉ Ö
)ÉÉÖ Ü
)ÉÉÜ á
;ÉÉá à
if
ÖÖ 
(
ÖÖ 

lastDateDB
ÖÖ %
!=
ÖÖ& (
null
ÖÖ) -
)
ÖÖ- .
{
ÜÜ 
if
áá 
(
áá 

lastDateDB
áá )
>=
áá* ,
fY
áá- /
.
áá/ 0
LastComputedDate
áá0 @
)
áá@ A
{
àà 
break
ââ  %
;
ââ% &
}
ää 
}
ãã 
fundsYeldsTmp
çç %
.
çç% &
Add
çç& )
(
çç) *
fY
çç* ,
)
çç, -
;
çç- .
}
èè 
}
ëë 
if
ìì 
(
ìì 
fundsYeldsTmp
ìì  
.
ìì  !
Count
ìì! &
(
ìì& '
)
ìì' (
==
ìì) +
$num
ìì, -
)
ìì- .
{
îî 
return
ïï 
await
ïï  
Task
ïï! %
.
ïï% &

FromResult
ïï& 0
<
ïï0 1
IEnumerable
ïï1 <
<
ïï< =
	FundsYeld
ïï= F
>
ïïF G
>
ïïG H
(
ïïH I

fundsYelds
ïïI S
)
ïïS T
;
ïïT U
}
ññ 
if
òò 
(
òò 
totalFundYeldsDb
òò #
.
òò# $
Count
òò$ )
>
òò* +
$num
òò, -
)
òò- .
{
ôô 
fundsYeldsTmp
öö !
=
öö" #
fundsYeldsTmp
öö$ 1
.
öö1 2
OrderByDescending
öö2 C
(
ööC D
x
ööD E
=>
ööE G
x
ööG H
.
ööH I
LastComputedDate
ööI Y
)
ööY Z
.
ööZ [
ToList
öö[ a
(
ööa b
)
ööb c
;
ööc d
int
úú 
totalItemsNew
úú %
=
úú& '
fundsYeldsTmp
úú( 5
.
úú5 6
Count
úú6 ;
(
úú; <
)
úú< =
;
úú= >
int
ùù 
totalItemsDb
ùù $
=
ùù% &
totalFundYeldsDb
ùù' 7
.
ùù7 8
Count
ùù8 =
(
ùù= >
)
ùù> ?
;
ùù? @
int
ûû 

totalItems
ûû "
=
ûû# $
totalItemsNew
ûû% 2
+
ûû3 4
totalItemsDb
ûû5 A
;
ûûA B
if
†† 
(
†† 

totalItems
†† !
>
††" #
$num
††$ &
)
††& '
{
°° 
int
¢¢  
totalItemsToRemove
¢¢ .
=
¢¢/ 0

totalItems
¢¢1 ;
-
¢¢< =
$num
¢¢> @
;
¢¢@ A
var
££ 
removeItems
££ '
=
££( )
totalFundYeldsDb
££* :
.
££: ;
OrderBy
££; B
(
££B C
x
££C D
=>
££E G
x
££H I
.
££I J
LastComputedDate
££J Z
)
££Z [
.
££[ \
Take
££\ `
(
££` a 
totalItemsToRemove
££a s
)
££s t
;
££t u
_generalPersist
§§ '
.
§§' (
DeleteRange
§§( 3
<
§§3 4
	FundsYeld
§§4 =
>
§§= >
(
§§> ?
removeItems
§§? J
.
§§J K
ToArray
§§K R
(
§§R S
)
§§S T
)
§§T U
;
§§U V
}
•• 
}
¶¶ 

fundsYelds
®® 
.
®® 
AddRange
®® #
(
®®# $
fundsYeldsTmp
®®$ 1
)
®®1 2
;
®®2 3
fundsYeldsTmp
™™ 
.
™™ 
Clear
™™ #
(
™™# $
)
™™$ %
;
™™% &
totalFundYeldsDb
´´  
.
´´  !
Clear
´´! &
(
´´& '
)
´´' (
;
´´( )
driver
≠≠ 
.
≠≠ 
Close
≠≠ 
(
≠≠ 
)
≠≠ 
;
≠≠ 
await
ØØ 
VariablesManager
ØØ &
.
ØØ& '!
ConectionsWebSocket
ØØ' :
.
ØØ: ;
socketManager
ØØ; H
.
ØØH I#
SendMessageToAllAsync
ØØI ^
(
ØØ^ _
JsonConvert
ØØ_ j
.
ØØj k
SerializeObject
ØØk z
(
ØØz {
$strØØ{ ò
)ØØò ô
)ØØô ö
;ØØö õ
return
±± 
await
±± 
Task
±± !
.
±±! "

FromResult
±±" ,
<
±±, -
IEnumerable
±±- 8
<
±±8 9
	FundsYeld
±±9 B
>
±±B C
>
±±C D
(
±±D E

fundsYelds
±±E O
)
±±O P
;
±±P Q
}
≤≤ 
catch
≥≥ 
(
≥≥ 
System
≥≥ 
.
≥≥ 
	Exception
≥≥ #
ex
≥≥$ &
)
≥≥& '
{
¥¥ 
driver
µµ 
.
µµ 
Close
µµ 
(
µµ 
)
µµ 
;
µµ 
Console
∂∂ 
.
∂∂ 
	WriteLine
∂∂ !
(
∂∂! "
ex
∂∂" $
.
∂∂$ %
Message
∂∂% ,
)
∂∂, -
;
∂∂- .
await
∑∑ 
VariablesManager
∑∑ &
.
∑∑& '!
ConectionsWebSocket
∑∑' :
.
∑∑: ;
socketManager
∑∑; H
.
∑∑H I#
SendMessageToAllAsync
∑∑I ^
(
∑∑^ _
JsonConvert
∑∑_ j
.
∑∑j k
SerializeObject
∑∑k z
(
∑∑z {
$str∑∑{ î
)∑∑î ï
)∑∑ï ñ
;∑∑ñ ó
return
∏∏ 
await
∏∏ 
Task
∏∏ !
.
∏∏! "

FromResult
∏∏" ,
<
∏∏, -
IEnumerable
∏∏- 8
<
∏∏8 9
	FundsYeld
∏∏9 B
>
∏∏B C
>
∏∏C D
(
∏∏D E

fundsYelds
∏∏E O
)
∏∏O P
;
∏∏P Q
}
ππ 
}
ªª 	
public
ΩΩ 
void
ΩΩ 
GoToPage
ΩΩ 
(
ΩΩ 
string
ΩΩ #
linkPage
ΩΩ$ ,
)
ΩΩ, -
{
ææ 	
try
øø 
{
¿¿ 
driver
¡¡ 
.
¡¡ 
Navigate
¡¡ 
(
¡¡  
)
¡¡  !
.
¡¡! "
GoToUrl
¡¡" )
(
¡¡) *
linkPage
¡¡* 2
)
¡¡2 3
;
¡¡3 4
}
¬¬ 
catch
√√ 
(
√√ 
System
√√ 
.
√√ 
	Exception
√√ #
)
√√# $
{
ƒƒ 
GoToPage
≈≈ 
(
≈≈ 
linkPage
≈≈ !
)
≈≈! "
;
≈≈" #
}
∆∆ 
}
«« 	
public
…… 
void
…… 
ConfigDriver
……  
(
……  !
)
……! "
{
   	
var
ÀÀ 
options
ÀÀ 
=
ÀÀ 
new
ÀÀ 
ChromeOptions
ÀÀ +
(
ÀÀ+ ,
)
ÀÀ, -
;
ÀÀ- .
options
ÃÃ 
.
ÃÃ 
AddArguments
ÃÃ  
(
ÃÃ  !
$str
ÃÃ! +
)
ÃÃ+ ,
;
ÃÃ, -
driver
ŒŒ 
=
ŒŒ 
new
ŒŒ 
ChromeDriver
ŒŒ %
(
ŒŒ% &
Path
œœ 
.
œœ 
GetDirectoryName
œœ %
(
œœ% &
Assembly
œœ& .
.
œœ. /"
GetExecutingAssembly
œœ/ C
(
œœC D
)
œœD E
.
œœE F
Location
œœF N
)
œœN O
,
œœO P
options
œœQ X
)
œœX Y
;
œœY Z
driver
—— 
.
—— 
Manage
—— 
(
—— 
)
—— 
.
—— 
Window
—— "
.
——" #
Maximize
——# +
(
——+ ,
)
——, -
;
——- .
driver
““ 
.
““ 
Manage
““ 
(
““ 
)
““ 
.
““ 
Timeouts
““ $
(
““$ %
)
““% &
.
““& '
PageLoad
““' /
=
““0 1
TimeSpan
““2 :
.
““: ;
FromSeconds
““; F
(
““F G
$num
““G H
)
““H I
;
““I J
}
”” 	
public
ÁÁ 
async
ÁÁ 
Task
ÁÁ 
<
ÁÁ 
bool
ÁÁ 
>
ÁÁ 
AddFundsAsync
ÁÁ  -
(
ÁÁ- .
IEnumerable
ÁÁ. 9
<
ÁÁ9 :
DetailedFunds
ÁÁ: G
>
ÁÁG H
detailedFunds
ÁÁI V
)
ÁÁV W
{
ËË 	
try
ÍÍ 
{
ÎÎ 
List
ÌÌ 
<
ÌÌ 
Funds
ÌÌ 
>
ÌÌ 
funds
ÌÌ !
=
ÌÌ" #
new
ÌÌ$ '
List
ÌÌ( ,
<
ÌÌ, -
Funds
ÌÌ- 2
>
ÌÌ2 3
(
ÌÌ3 4
)
ÌÌ4 5
;
ÌÌ5 6
foreach
ÔÔ 
(
ÔÔ 
var
ÔÔ 
item
ÔÔ !
in
ÔÔ" $
detailedFunds
ÔÔ% 2
)
ÔÔ2 3
{
 
var
ÚÚ 
fY
ÚÚ 
=
ÚÚ 
new
ÚÚ  
Funds
ÚÚ! &
(
ÚÚ& '
)
ÚÚ' (
{
ÚÚ( )
FundCode
ÛÛ  
=
ÛÛ! "
item
ÛÛ# '
.
ÛÛ' (
FundCode
ÛÛ( 0
}
ÙÙ 
;
ÙÙ 
funds
ˆˆ 
.
ˆˆ 
Add
ˆˆ 
(
ˆˆ 
fY
ˆˆ  
)
ˆˆ  !
;
ˆˆ! "
}
¯¯ 
_generalPersist
˙˙ 
.
˙˙  
AddRange
˙˙  (
(
˙˙( )
funds
˙˙) .
.
˙˙. /
ToArray
˙˙/ 6
(
˙˙6 7
)
˙˙7 8
)
˙˙8 9
;
˙˙9 :
await
˚˚ 
_generalPersist
˚˚ %
.
˚˚% &
SaveChangesAsync
˚˚& 6
(
˚˚6 7
)
˚˚7 8
;
˚˚8 9
return
˝˝ 
true
˝˝ 
;
˝˝ 
}
˛˛ 
catch
ˇˇ 
(
ˇˇ 
System
ˇˇ 
.
ˇˇ 
	Exception
ˇˇ #
ex
ˇˇ$ &
)
ˇˇ& '
{
ÄÄ 
Console
ÅÅ 
.
ÅÅ 
	WriteLine
ÅÅ !
(
ÅÅ! "
ex
ÅÅ" $
.
ÅÅ$ %
Message
ÅÅ% ,
)
ÅÅ, -
;
ÅÅ- .
return
ÇÇ 
false
ÇÇ 
;
ÇÇ 
}
ÉÉ 
}
ÑÑ 	
}
ôô 
}öö 