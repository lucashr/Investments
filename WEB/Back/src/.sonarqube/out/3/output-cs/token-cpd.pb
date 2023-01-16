�

�C:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IDetailedFundService.cs
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
} �
�C:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IFundsService.cs
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
} �
�C:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IFundsYieldService.cs
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
} �	
�C:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IRankOfTheBestFundsService.cs
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
} �
�C:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IStocksService.cs
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
} �
�C:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IWebScrapingFundsAndYeldsService.cs
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
} �
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
}II �
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
} �
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
}`` �	
�C:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\FunctionsOfCalculationExtend\StandardDeviation.cs
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
} �D
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
�� 
result
�� 
=
�� 
await
�� "
_fundsPersist
��# 0
.
��0 1
AddFundsAsync
��1 >
(
��> ?
detailedFunds
��? L
)
��L M
;
��M N
return
�� 
result
�� 
;
�� 
}
�� 
catch
�� 
(
�� 
System
�� 
.
�� 
	Exception
�� #
ex
��$ &
)
��& '
{
�� 
throw
�� 
new
�� 
	Exception
�� #
(
��# $
ex
��$ &
.
��& '
Message
��' .
)
��. /
;
��/ 0
}
�� 
}
�� 	
}
�� 
}�� �
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
}FF �

�C:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\helpers\InvestimentosProfile.cs
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
} �O
�C:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\RankOfTheBestFundsService.cs
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
�� 
(
�� 
System
�� 
.
�� 
	Exception
�� #
ex
��$ &
)
��& '
{
�� 
throw
�� 
new
�� 
	Exception
�� #
(
��# $
ex
��$ &
.
��& '
Message
��' .
)
��. /
;
��/ 0
}
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 
IEnumerable
�� %
<
��% & 
RankOfTheBestFunds
��& 8
>
��8 9
>
��9 :(
GetRankOfTheBestFundsAsync
��; U
(
��U V
int
��V Y
?
��Y Z
totalFundsInRank
��[ k
=
��l m
null
��n r
)
��r s
{
�� 	
try
�� 
{
�� 
var
�� 
	bestFunds
�� 
=
�� 
await
��  %(
_rankOfTheBestFundsPersist
��& @
.
��@ A(
GetRankOfTheBestFundsAsync
��A [
(
��[ \
totalFundsInRank
��\ l
)
��l m
;
��m n
return
�� 
	bestFunds
��  
;
��  !
}
�� 
catch
�� 
(
�� 
System
�� 
.
�� 
	Exception
�� #
ex
��$ &
)
��& '
{
�� 
throw
�� 
new
�� 
	Exception
�� #
(
��# $
ex
��$ &
.
��& '
Message
��' .
)
��. /
;
��/ 0
}
�� 
}
�� 	
}
�� 
}�� �
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
.SA&apikey=QXCJRQTC6J5C74GS	k �
"
� �
;
� �
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
}66 ��
�C:\Users\lucas\Desktop\Repositorios\Meus repositorios\Investments\WEB\Back\src\Investments.Application\WebScrapingFundsAndYeldsService.cs
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
$str	))w �
)
))� �
)
))� �
;
))� �
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
)	@@ �
;
@@� �
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
driver	CC{ �
.
CC� �

PageSource
CC� �
)
CC� �
)
CC� �
;
CC� �
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
Ordem inválida! ee( 8
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
$str	ii{ �
)
ii� �
;
ii� �
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
$str	yy~ �
,
yy� �
$str
yy� �
)
yy� �
)
yy� �
,
yy� �
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
(	}} �
$str
}}� �
,
}}� �
$str
}}� �
)
}}� �
)
}}� �
}~~ 
;~~ 
await
�� 
VariablesManager
�� *
.
��* +!
ConectionsWebSocket
��+ >
.
��> ?
socketManager
��? L
.
��L M#
SendMessageToAllAsync
��M b
(
��b c
JsonConvert
��c n
.
��n o
SerializeObject
��o ~
(
��~ 
fund�� �
)��� �
)��� �
;��� �
Console
�� 
.
�� 
	WriteLine
�� %
(
��% &
$"
��& (
{
��( )
JsonConvert
��) 4
.
��4 5
SerializeObject
��5 D
(
��D E
fund
��E I
)
��I J
}
��J K
"
��K L
)
��L M
;
��M N
detailedFunds
�� !
.
��! "
Add
��" %
(
��% &
fund
��& *
)
��* +
;
��+ ,
}
�� 
driver
�� 
.
�� 
Close
�� 
(
�� 
)
�� 
;
�� 
await
�� 
VariablesManager
�� &
.
��& '!
ConectionsWebSocket
��' :
.
��: ;
socketManager
��; H
.
��H I#
SendMessageToAllAsync
��I ^
(
��^ _
JsonConvert
��_ j
.
��j k
SerializeObject
��k z
(
��z {
$str��{ �
)��� �
)��� �
;��� �
return
�� 
await
�� 
Task
�� !
.
��! "

FromResult
��" ,
<
��, -
IEnumerable
��- 8
<
��8 9
DetailedFunds
��9 F
>
��F G
>
��G H
(
��H I
detailedFunds
��I V
)
��V W
;
��W X
}
�� 
catch
�� 
(
�� 
System
�� 
.
�� 
	Exception
�� #
ex
��$ &
)
��& '
{
�� 
driver
�� 
.
�� 
Close
�� 
(
�� 
)
�� 
;
�� 
Console
�� 
.
�� 
	WriteLine
�� !
(
��! "
ex
��" $
.
��$ %
Message
��% ,
)
��, -
;
��- .
await
�� 
VariablesManager
�� &
.
��& '!
ConectionsWebSocket
��' :
.
��: ;
socketManager
��; H
.
��H I#
SendMessageToAllAsync
��I ^
(
��^ _
JsonConvert
��_ j
.
��j k
SerializeObject
��k z
(
��z {
$str��{ �
)��� �
)��� �
;��� �
return
�� 
await
�� 
Task
�� !
.
��! "

FromResult
��" ,
<
��, -
IEnumerable
��- 8
<
��8 9
DetailedFunds
��9 F
>
��F G
>
��G H
(
��H I
detailedFunds
��I V
)
��V W
;
��W X
}
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 
IEnumerable
�� %
<
��% &
	FundsYeld
��& /
>
��/ 0
>
��0 1 
GetYeldsFundsAsync
��2 D
(
��D E
IEnumerable
��E P
<
��P Q
DetailedFunds
��Q ^
>
��^ _
detailedFunds
��` m
)
��m n
{
�� 	
var
�� 

fundsYelds
�� 
=
�� 
new
��  
List
��! %
<
��% &
	FundsYeld
��& /
>
��/ 0
(
��0 1
)
��1 2
;
��2 3
var
�� 
fundsYeldsTmp
�� 
=
�� 
new
��  #
List
��$ (
<
��( )
	FundsYeld
��) 2
>
��2 3
(
��3 4
)
��4 5
;
��5 6
var
�� 
totalFundYeldsDb
��  
=
��! "
new
��# &
List
��' +
<
��+ ,
	FundsYeld
��, 5
>
��5 6
(
��6 7
)
��7 8
;
��8 9
dynamic
�� 

lastDateDB
�� 
=
��  
null
��! %
;
��% &
dynamic
�� 
rows
�� 
;
�� 
dynamic
�� 
columns
�� 
;
�� 
int
�� 
numberOfLines
�� 
=
�� 
$num
��  !
;
��! "
int
�� 
numberOfColumn
�� 
=
��  
$num
��! "
;
��" #
List
�� 
<
�� 
string
�� 
>
�� %
orderColumnTableOfFunds
�� 0
=
��1 2
new
��3 6
List
��7 ;
<
��; <
string
��< B
>
��B C
{
�� 
$str
�� !
,
��! "
$str
��# )
,
��) *
$str
��+ >
,
��> ?
$str
��@ G
}
�� 
;
�� 
int
�� #
totalOfColumnExpected
�� %
=
��& '%
orderColumnTableOfFunds
��( ?
.
��? @
Count
��@ E
;
��E F
try
�� 
{
�� 
await
�� 
VariablesManager
�� &
.
��& '!
ConectionsWebSocket
��' :
.
��: ;
socketManager
��; H
.
��H I#
SendMessageToAllAsync
��I ^
(
��^ _
JsonConvert
��_ j
.
��j k
SerializeObject
��k z
(
��z {
$str��{ �
)��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
�� 
fund
�� !
in
��" $
detailedFunds
��% 2
)
��2 3
{
�� 
GoToPage
�� 
(
�� 
$"
�� E
7https://www.fundamentus.com.br/fii_proventos.php?papel=
�� V
{
��V W
fund
��W [
.
��[ \
FundCode
��\ d
}
��d e
"
��e f
)
��f g
;
��g h
try
�� 
{
�� 
var
�� 
wait
��  
=
��! "
new
��# &
WebDriverWait
��' 4
(
��4 5
driver
��5 ;
,
��; <
TimeSpan
��= E
.
��E F
FromSeconds
��F Q
(
��Q R
$num
��R T
)
��T U
)
��U V
;
��V W
wait
�� 
.
�� 
Until
�� "
(
��" # 
ExpectedConditions
��# 5
.
��5 6.
 VisibilityOfAllElementsLocatedBy
��6 V
(
��V W
By
��W Y
.
��Y Z
XPath
��Z _
(
��_ `
$str
��` 
)�� �
)��� �
)��� �
;��� �
rows
�� 
=
�� 
driver
�� %
.
��% &
FindElements
��& 2
(
��2 3
By
��3 5
.
��5 6
XPath
��6 ;
(
��; <
$str
��< [
)
��[ \
)
��\ ]
;
��] ^
numberOfLines
�� %
=
��& '
rows
��( ,
.
��, -
Count
��- 2
;
��2 3
Console
�� 
.
��  
	WriteLine
��  )
(
��) *
$"
��* ,
Total de linhas 
��, <
{
��< =
numberOfLines
��= J
}
��J K
"
��K L
)
��L M
;
��M N
columns
�� 
=
��  !
driver
��" (
.
��( )
FindElements
��) 5
(
��5 6
By
��6 8
.
��8 9
XPath
��9 >
(
��> ?
$str
��? a
)
��a b
)
��b c
;
��c d
numberOfColumn
�� &
=
��' (
columns
��) 0
.
��0 1
Count
��1 6
;
��6 7
Console
�� 
.
��  
	WriteLine
��  )
(
��) *
$"
��* ,
Total de colunas 
��, =
{
��= >
numberOfColumn
��> L
}
��L M
"
��M N
)
��N O
;
��O P
if
�� 
(
�� #
totalOfColumnExpected
�� 0
!=
��1 3
numberOfColumn
��4 B
)
��B C
{
�� 
Console
�� #
.
��# $
	WriteLine
��$ -
(
��- .
$"
��. 00
"Total of columns expected invalid 
��0 R
{
��R S
numberOfColumn
��S a
}
��a b
"
��b c
)
��c d
;
��d e
}
�� 
Console
�� 
.
��  
	WriteLine
��  )
(
��) *
driver
��* 0
.
��0 1
FindElement
��1 <
(
��< =
By
��= ?
.
��? @
XPath
��@ E
(
��E F
$str
��F p
)
��p q
)
��q r
.
��r s
Text
��s w
)
��w x
;
��x y
}
�� 
catch
�� 
(
�� 
System
�� !
.
��! "
	Exception
��" +
)
��+ ,
{
�� 
continue
��  
;
��  !
}
�� 
if
�� 
(
�� 
numberOfLines
�� $
>
��% &
$num
��' )
)
��) *
{
�� 
numberOfLines
�� %
=
��& '
$num
��( *
;
��* +
}
�� 
for
�� 
(
�� 
int
�� 
j
�� 
=
��  
$num
��! "
;
��" #
j
��$ %
<=
��& (
numberOfColumn
��) 7
;
��7 8
j
��9 :
++
��: <
)
��< =
{
�� 
string
�� 
campo
�� $
=
��% &
driver
��' -
.
��- .
FindElement
��. 9
(
��9 :
By
��: <
.
��< =
XPath
��= B
(
��B C
$"
��C E,
//*[@id='resultado']/thead/tr[
��E c
{
��c d
$num
��d e
}
��e f
]/th[
��f k
{
��k l
j
��l m
}
��m n
]
��n o
"
��o p
)
��p q
)
��q r
.
��r s
Text
��s w
;
��w x
if
�� 
(
�� %
orderColumnTableOfFunds
�� 2
[
��2 3
j
��3 4
-
��4 5
$num
��5 6
]
��6 7
!=
��8 :
campo
��; @
)
��@ A
{
�� 
Console
�� #
.
��# $
Write
��$ )
(
��) *
$"
��* ,
Ordem inválida! 
��, <
{
��< =%
orderColumnTableOfFunds
��= T
[
��T U
j
��U V
-
��V W
$num
��W X
]
��X Y
}
��Y Z

 esperado 
��Z d
{
��d e
campo
��e j
}
��j k
"
��k l
)
��l m
;
��m n
return
�� "
await
��# (
Task
��) -
.
��- .

FromResult
��. 8
<
��8 9
IEnumerable
��9 D
<
��D E
	FundsYeld
��E N
>
��N O
>
��O P
(
��P Q

fundsYelds
��Q [
)
��[ \
;
��\ ]
}
�� 
Console
�� 
.
��  
Write
��  %
(
��% &
driver
��& ,
.
��, -
FindElement
��- 8
(
��8 9
By
��9 ;
.
��; <
XPath
��< A
(
��A B
$"
��B D,
//*[@id='resultado']/thead/tr[
��D b
{
��b c
$num
��c d
}
��d e
]/th[
��e j
{
��j k
j
��k l
}
��l m
]
��m n
"
��n o
)
��o p
)
��p q
.
��q r
Text
��r v
+
��w x
$str
��y ~
)
��~ 
;�� �
}
�� 
var
�� 
result
�� 
=
��  
await
��! &
_fundsYeldPersist
��' 8
.
��8 9$
GetFundYeldByCodeAsync
��9 O
(
��O P
fund
��P T
.
��T U
FundCode
��U ]
.
��] ^
ToUpper
��^ e
(
��e f
)
��f g
.
��g h
Trim
��h l
(
��l m
)
��m n
)
��n o
;
��o p
if
�� 
(
�� 
result
�� 
!=
��  
null
��! %
)
��% &
{
�� 
totalFundYeldsDb
�� (
=
��) *
result
��+ 1
.
��1 2
ToList
��2 8
(
��8 9
)
��9 :
;
��: ;

lastDateDB
�� "
=
��# $
result
��% +
.
��+ ,
OrderByDescending
��, =
(
��= >
x
��> ?
=>
��? A
x
��B C
.
��C D
LastComputedDate
��D T
)
��T U
.
��U V
Take
��V Z
(
��Z [
$num
��[ \
)
��\ ]
.
��] ^
Select
��^ d
(
��d e
x
��e f
=>
��f h
x
��h i
.
��i j
LastComputedDate
��j z
)
��z {
.
��{ |
FirstOrDefault��| �
(��� �
)��� �
;��� �
}
�� 
for
�� 
(
�� 
int
�� 
i
�� 
=
��  
$num
��! "
;
��" #
i
��$ %
<=
��& (
numberOfLines
��) 6
;
��6 7
i
��8 9
++
��9 ;
)
��; <
{
�� 
string
�� 
[
�� 
]
��  
obj
��! $
=
��% &
new
��' *
string
��+ 1
[
��1 2
$num
��2 3
]
��3 4
;
��4 5
for
�� 
(
�� 
int
��  
j
��! "
=
��# $
$num
��% &
;
��& '
j
��( )
<=
��* ,
numberOfColumn
��- ;
;
��; <
j
��= >
++
��> @
)
��@ A
{
�� 
var
�� 
e
��  !
=
��" #
driver
��$ *
.
��* +
FindElement
��+ 6
(
��6 7
By
��7 9
.
��9 :
XPath
��: ?
(
��? @
$"
��@ B,
//*[@id='resultado']/tbody/tr[
��B `
{
��` a
i
��a b
}
��b c
]/td[
��c h
{
��h i
j
��i j
}
��j k
]
��k l
"
��l m
)
��m n
)
��n o
.
��o p
Text
��p t
;
��t u
obj
�� 
[
��  
j
��  !
-
��" #
$num
��$ %
]
��% &
=
��' (
e
��) *
;
��* +
}
�� 
var
�� 
fY
�� 
=
��  
new
��! $
	FundsYeld
��% .
(
��. /
)
��/ 0
{
��0 1
FundCode
�� $
=
��% &
fund
��' +
.
��+ ,
FundCode
��, 4
,
��4 5
LastComputedDate
�� ,
=
��- .
Convert
��/ 6
.
��6 7

ToDateTime
��7 A
(
��A B
obj
��B E
[
��E F
$num
��F G
]
��G H
)
��H I
.
��I J
AddDays
��J Q
(
��Q R
$num
��R S
)
��S T
,
��T U
Type
��  
=
��! "
obj
��# &
[
��& '
$num
��' (
]
��( )
,
��) *
DatePayment
�� '
=
��( )
Convert
��* 1
.
��1 2

ToDateTime
��2 <
(
��< =
obj
��= @
[
��@ A
$num
��A B
]
��B C
)
��C D
,
��D E
Value
�� !
=
��" #
Convert
��$ +
.
��+ ,
ToDouble
��, 4
(
��4 5
obj
��5 8
[
��8 9
$num
��9 :
]
��: ;
)
��; <
}
�� 
;
�� 
Console
�� 
.
��  
	WriteLine
��  )
(
��) *
$"
��* ,
{
��, -
JsonConvert
��- 8
.
��8 9
SerializeObject
��9 H
(
��H I
fY
��I K
)
��K L
}
��L M
"
��M N
)
��N O
;
��O P
await
�� 
VariablesManager
�� .
.
��. /!
ConectionsWebSocket
��/ B
.
��B C
socketManager
��C P
.
��P Q#
SendMessageToAllAsync
��Q f
(
��f g
JsonConvert
��g r
.
��r s
SerializeObject��s �
(��� �
fY��� �
)��� �
)��� �
;��� �
if
�� 
(
�� 

lastDateDB
�� %
!=
��& (
null
��) -
)
��- .
{
�� 
if
�� 
(
�� 

lastDateDB
�� )
>=
��* ,
fY
��- /
.
��/ 0
LastComputedDate
��0 @
)
��@ A
{
�� 
break
��  %
;
��% &
}
�� 
}
�� 
fundsYeldsTmp
�� %
.
��% &
Add
��& )
(
��) *
fY
��* ,
)
��, -
;
��- .
}
�� 
}
�� 
if
�� 
(
�� 
fundsYeldsTmp
��  
.
��  !
Count
��! &
(
��& '
)
��' (
==
��) +
$num
��, -
)
��- .
{
�� 
return
�� 
await
��  
Task
��! %
.
��% &

FromResult
��& 0
<
��0 1
IEnumerable
��1 <
<
��< =
	FundsYeld
��= F
>
��F G
>
��G H
(
��H I

fundsYelds
��I S
)
��S T
;
��T U
}
�� 
if
�� 
(
�� 
totalFundYeldsDb
�� #
.
��# $
Count
��$ )
>
��* +
$num
��, -
)
��- .
{
�� 
fundsYeldsTmp
�� !
=
��" #
fundsYeldsTmp
��$ 1
.
��1 2
OrderByDescending
��2 C
(
��C D
x
��D E
=>
��E G
x
��G H
.
��H I
LastComputedDate
��I Y
)
��Y Z
.
��Z [
ToList
��[ a
(
��a b
)
��b c
;
��c d
int
�� 
totalItemsNew
�� %
=
��& '
fundsYeldsTmp
��( 5
.
��5 6
Count
��6 ;
(
��; <
)
��< =
;
��= >
int
�� 
totalItemsDb
�� $
=
��% &
totalFundYeldsDb
��' 7
.
��7 8
Count
��8 =
(
��= >
)
��> ?
;
��? @
int
�� 

totalItems
�� "
=
��# $
totalItemsNew
��% 2
+
��3 4
totalItemsDb
��5 A
;
��A B
if
�� 
(
�� 

totalItems
�� !
>
��" #
$num
��$ &
)
��& '
{
�� 
int
��  
totalItemsToRemove
�� .
=
��/ 0

totalItems
��1 ;
-
��< =
$num
��> @
;
��@ A
var
�� 
removeItems
�� '
=
��( )
totalFundYeldsDb
��* :
.
��: ;
OrderBy
��; B
(
��B C
x
��C D
=>
��E G
x
��H I
.
��I J
LastComputedDate
��J Z
)
��Z [
.
��[ \
Take
��\ `
(
��` a 
totalItemsToRemove
��a s
)
��s t
;
��t u
_generalPersist
�� '
.
��' (
DeleteRange
��( 3
<
��3 4
	FundsYeld
��4 =
>
��= >
(
��> ?
removeItems
��? J
.
��J K
ToArray
��K R
(
��R S
)
��S T
)
��T U
;
��U V
}
�� 
}
�� 

fundsYelds
�� 
.
�� 
AddRange
�� #
(
��# $
fundsYeldsTmp
��$ 1
)
��1 2
;
��2 3
fundsYeldsTmp
�� 
.
�� 
Clear
�� #
(
��# $
)
��$ %
;
��% &
totalFundYeldsDb
��  
.
��  !
Clear
��! &
(
��& '
)
��' (
;
��( )
driver
�� 
.
�� 
Close
�� 
(
�� 
)
�� 
;
�� 
await
�� 
VariablesManager
�� &
.
��& '!
ConectionsWebSocket
��' :
.
��: ;
socketManager
��; H
.
��H I#
SendMessageToAllAsync
��I ^
(
��^ _
JsonConvert
��_ j
.
��j k
SerializeObject
��k z
(
��z {
$str��{ �
)��� �
)��� �
;��� �
return
�� 
await
�� 
Task
�� !
.
��! "

FromResult
��" ,
<
��, -
IEnumerable
��- 8
<
��8 9
	FundsYeld
��9 B
>
��B C
>
��C D
(
��D E

fundsYelds
��E O
)
��O P
;
��P Q
}
�� 
catch
�� 
(
�� 
System
�� 
.
�� 
	Exception
�� #
ex
��$ &
)
��& '
{
�� 
driver
�� 
.
�� 
Close
�� 
(
�� 
)
�� 
;
�� 
Console
�� 
.
�� 
	WriteLine
�� !
(
��! "
ex
��" $
.
��$ %
Message
��% ,
)
��, -
;
��- .
await
�� 
VariablesManager
�� &
.
��& '!
ConectionsWebSocket
��' :
.
��: ;
socketManager
��; H
.
��H I#
SendMessageToAllAsync
��I ^
(
��^ _
JsonConvert
��_ j
.
��j k
SerializeObject
��k z
(
��z {
$str��{ �
)��� �
)��� �
;��� �
return
�� 
await
�� 
Task
�� !
.
��! "

FromResult
��" ,
<
��, -
IEnumerable
��- 8
<
��8 9
	FundsYeld
��9 B
>
��B C
>
��C D
(
��D E

fundsYelds
��E O
)
��O P
;
��P Q
}
�� 
}
�� 	
public
�� 
void
�� 
GoToPage
�� 
(
�� 
string
�� #
linkPage
��$ ,
)
��, -
{
�� 	
try
�� 
{
�� 
driver
�� 
.
�� 
Navigate
�� 
(
��  
)
��  !
.
��! "
GoToUrl
��" )
(
��) *
linkPage
��* 2
)
��2 3
;
��3 4
}
�� 
catch
�� 
(
�� 
System
�� 
.
�� 
	Exception
�� #
)
��# $
{
�� 
GoToPage
�� 
(
�� 
linkPage
�� !
)
��! "
;
��" #
}
�� 
}
�� 	
public
�� 
void
�� 
ConfigDriver
��  
(
��  !
)
��! "
{
�� 	
var
�� 
options
�� 
=
�� 
new
�� 
ChromeOptions
�� +
(
��+ ,
)
��, -
;
��- .
options
�� 
.
�� 
AddArguments
��  
(
��  !
$str
��! +
)
��+ ,
;
��, -
driver
�� 
=
�� 
new
�� 
ChromeDriver
�� %
(
��% &
Path
�� 
.
�� 
GetDirectoryName
�� %
(
��% &
Assembly
��& .
.
��. /"
GetExecutingAssembly
��/ C
(
��C D
)
��D E
.
��E F
Location
��F N
)
��N O
,
��O P
options
��Q X
)
��X Y
;
��Y Z
driver
�� 
.
�� 
Manage
�� 
(
�� 
)
�� 
.
�� 
Window
�� "
.
��" #
Maximize
��# +
(
��+ ,
)
��, -
;
��- .
driver
�� 
.
�� 
Manage
�� 
(
�� 
)
�� 
.
�� 
Timeouts
�� $
(
��$ %
)
��% &
.
��& '
PageLoad
��' /
=
��0 1
TimeSpan
��2 :
.
��: ;
FromSeconds
��; F
(
��F G
$num
��G H
)
��H I
;
��I J
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 
bool
�� 
>
�� 
AddFundsAsync
��  -
(
��- .
IEnumerable
��. 9
<
��9 :
DetailedFunds
��: G
>
��G H
detailedFunds
��I V
)
��V W
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
Funds
�� 
>
�� 
funds
�� !
=
��" #
new
��$ '
List
��( ,
<
��, -
Funds
��- 2
>
��2 3
(
��3 4
)
��4 5
;
��5 6
foreach
�� 
(
�� 
var
�� 
item
�� !
in
��" $
detailedFunds
��% 2
)
��2 3
{
�� 
var
�� 
fY
�� 
=
�� 
new
��  
Funds
��! &
(
��& '
)
��' (
{
��( )
FundCode
��  
=
��! "
item
��# '
.
��' (
FundCode
��( 0
}
�� 
;
�� 
funds
�� 
.
�� 
Add
�� 
(
�� 
fY
��  
)
��  !
;
��! "
}
�� 
_generalPersist
�� 
.
��  
AddRange
��  (
(
��( )
funds
��) .
.
��. /
ToArray
��/ 6
(
��6 7
)
��7 8
)
��8 9
;
��9 :
await
�� 
_generalPersist
�� %
.
��% &
SaveChangesAsync
��& 6
(
��6 7
)
��7 8
;
��8 9
return
�� 
true
�� 
;
�� 
}
�� 
catch
�� 
(
�� 
System
�� 
.
�� 
	Exception
�� #
ex
��$ &
)
��& '
{
�� 
Console
�� 
.
�� 
	WriteLine
�� !
(
��! "
ex
��" $
.
��$ %
Message
��% ,
)
��, -
;
��- .
return
�� 
false
�� 
;
�� 
}
�� 
}
�� 	
}
�� 
}�� 