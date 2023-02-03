�
�C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IDetailedFundService.cs
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
;R S
} 
} �
�C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IFundsService.cs
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
�C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IFundsYieldService.cs
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
�C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IRankOfTheBestFundsService.cs
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
�C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IStocksService.cs
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
} �
�C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\ITesteService.cs
	namespace 	
Investments
 
. 
Application !
.! "
	Contracts" +
{ 
public 

	interface 
ITesteService "
{		 
Task

 
<

 
double

 
>

 
Soma

 
(

 
double

  
a

! "
,

" #
double

$ *
b

+ ,
)

, -
;

- .
} 
} �
�C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\Contracts\IWebScrapingFundsAndYeldsService.cs
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
} �
uC:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\DetachLocal.cs
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
class 
DetachLocal 
{ 
private 
readonly 
InvestmentsContext +
_context, 4
;4 5
public 
DetachLocal 
( 
InvestmentsContext -
context. 5
)5 6
{ 	
_context 
= 
context 
; 
} 	
public 
virtual 
void 
Detach "
<" #
T# $
>$ %
(% &
Func& *
<* +
T+ ,
,, -
bool. 2
>2 3
	predicate4 =
)= >
where? D
TE F
:G H
classI N
{ 	
dynamic 
local 
= 
_context $
.$ %
Set% (
<( )
T) *
>* +
(+ ,
), -
.- .
Local. 3
.3 4
Where4 9
(9 :
	predicate: C
)C D
.D E
FirstOrDefaultE S
(S T
)T U
;U V
if 
( 
! 
local 
. 
IsNull 
( 
) 
) 
{ 
_context 
. 
Entry 
( 
local $
)$ %
.% &
State& +
=, -
EntityState. 9
.9 :
Detached: B
;B C
} 
}   	
}"" 
}## �
}C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\DetailedFundService.cs
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
public&& 
async&& 
Task&& 
<&& 
IEnumerable&& %
<&&% &
DetailedFunds&&& 3
>&&3 4
>&&4 5$
GetAllDetailedFundsAsync&&6 N
(&&N O
)&&O P
{'' 	
try(( 
{)) 
var** 
allFunds** 
=** 
await** $ 
_detailedFundPersist**% 9
.**9 :$
GetAllDetailedFundsAsync**: R
(**R S
)**S T
;**T U
return,, 
allFunds,, 
;,,  
}-- 
catch.. 
(.. 
System.. 
... 
	Exception.. #
ex..$ &
)..& '
{// 
throw00 
new00 
	Exception00 #
(00# $
ex00$ &
.00& '
Message00' .
)00. /
;00/ 0
}11 
}22 	
public44 
async44 
Task44 
<44 
DetailedFunds44 '
>44' (&
GetDetailedFundByCodeAsync44) C
(44C D
string44D J
fundCode44K S
)44S T
{55 	
try66 
{77 
var88 
funds88 
=88 
await88 ! 
_detailedFundPersist88" 6
.886 7&
GetDetailedFundByCodeAsync887 Q
(88Q R
fundCode88R Z
)88Z [
;88[ \
return:: 
funds:: 
;:: 
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
}AA 	
}CC 
}DD �
wC:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\Dtos\FundsDto.cs
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
xC:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\Dtos\StocksDto.cs
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
�C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\FunctionsOfCalculationExtend\StandardDeviation.cs
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
} �H
vC:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\FundsService.cs
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
class 
FundsService 
: 
IFundsService  -
{ 
private 
readonly 
IGeneralPersist (
_generalPersist) 8
;8 9
private 
readonly 
IFundsPersist &
_fundsPersist' 4
;4 5
private 
readonly 
IMapper  
_mapper! (
;( )
public 
FundsService 
( 
IGeneralPersist +
generalPersist, :
,: ;
IFundsPersist )
fundsPersist* 6
,6 7
IMapper #
mapper$ *
)* +
{ 	
_generalPersist 
= 
generalPersist ,
;, -
_fundsPersist 
= 
fundsPersist (
;( )
_mapper 
= 
mapper 
; 
} 	
public 
async 
Task 
< 
Funds 
>  
AddFundAsync! -
(- .
string. 4
fund5 9
)9 :
{ 	
try 
{ 
var   
retFund   
=   
await   #
_fundsPersist  $ 1
.  1 2
GetFundByCodeAsync  2 D
(  D E
fund  E I
)  I J
;  J K
if"" 
("" 
retFund"" 
!="" 
null"" "
)""" #
return""$ *
null""+ /
;""/ 0
var$$ 
newFund$$ 
=$$ 
new$$ !
Funds$$" '
($$' (
)$$( )
{$$* +
FundCode$$+ 3
=$$4 5
fund$$6 :
}$$: ;
;$$; <
_generalPersist&& 
.&&  
Add&&  #
<&&# $
Funds&&$ )
>&&) *
(&&* +
newFund&&+ 2
)&&2 3
;&&3 4
await(( 
_generalPersist(( %
.((% &
SaveChangesAsync((& 6
(((6 7
)((7 8
;((8 9
retFund** 
=** 
await** 
_fundsPersist**  -
.**- .
GetFundByCodeAsync**. @
(**@ A
newFund**A H
.**H I
FundCode**I Q
)**Q R
;**R S
return,, 
retFund,, 
;,, 
}-- 
catch.. 
(.. 
System.. 
... 
	Exception.. #
ex..$ &
)..& '
{// 
throw00 
new00 
	Exception00 #
(00# $
ex00$ &
.00& '
Message00' .
)00. /
;00/ 0
}11 
}33 	
public55 
Task55 
<55 
Funds55 
>55 
GetFundByCodeAsync55 -
(55- .
string55. 4
fundCode555 =
)55= >
{66 	
try77 
{88 
var99 
fund99 
=99 
_fundsPersist99 (
.99( )
GetFundByCodeAsync99) ;
(99; <
fundCode99< D
)99D E
;99E F
return;; 
fund;; 
;;; 
}<< 
catch== 
(== 
System== 
.== 
	Exception== #
ex==$ &
)==& '
{>> 
throw?? 
new?? 
	Exception?? #
(??# $
ex??$ &
.??& '
Message??' .
)??. /
;??/ 0
}@@ 
}BB 	
publicDD 
TaskDD 
<DD 
IEnumerableDD 
<DD  
FundsDD  %
>DD% &
>DD& '
GetAllFundsAsyncDD( 8
(DD8 9
)DD9 :
{EE 	
tryFF 
{GG 
varHH 
fundHH 
=HH 
_fundsPersistHH (
.HH( )
GetAllFundsAsyncHH) 9
(HH9 :
)HH: ;
;HH; <
returnJJ 
fundJJ 
;JJ 
}KK 
catchLL 
(LL 
SystemLL 
.LL 
	ExceptionLL #
exLL$ &
)LL& '
{MM 
throwNN 
newNN 
	ExceptionNN #
(NN# $
exNN$ &
.NN& '
MessageNN' .
)NN. /
;NN/ 0
}OO 
}PP 	
publicRR 
asyncRR 
TaskRR 
<RR 
boolRR 
>RR !
DeleteFundByCodeAsyncRR  5
(RR5 6
stringRR6 <
fundCodeRR= E
)RRE F
{SS 	
tryTT 
{UU 
varVV 
fundVV 
=VV 
awaitVV  
_fundsPersistVV! .
.VV. /
GetFundByCodeAsyncVV/ A
(VVA B
fundCodeVVB J
)VVJ K
;VVK L
ifXX 
(XX 
fundXX 
==XX 
nullXX 
)XX  
throwXX! &
newXX' *
	ExceptionXX+ 4
(XX4 5
$strXX5 W
)XXW X
;XXX Y
_generalPersistZZ 
.ZZ  
DetachLocalZZ  +
<ZZ+ ,
FundsZZ, 1
>ZZ1 2
(ZZ2 3
xZZ3 4
=>ZZ4 6
xZZ6 7
.ZZ7 8
IdZZ8 :
==ZZ; =
fundZZ> B
.ZZB C
IdZZC E
)ZZE F
;ZZF G
_generalPersist\\ 
.\\  
Delete\\  &
<\\& '
Funds\\' ,
>\\, -
(\\- .
fund\\. 2
)\\2 3
;\\3 4
return^^ 
true^^ 
;^^ 
}`` 
catchaa 
(aa 
Systemaa 
.aa 
	Exceptionaa #
exaa$ &
)aa& '
{bb 
throwcc 
newcc 
	Exceptioncc #
(cc# $
excc$ &
.cc& '
Messagecc' .
)cc. /
;cc/ 0
}dd 
}ee 	
publicgg 
asyncgg 
Taskgg 
<gg 
Fundsgg 
>gg  !
UpdateFundByCodeAsyncgg! 6
(gg6 7
stringgg7 =
oldFundCodegg> I
,ggI J
stringggK Q
newFundCodeggR ]
)gg] ^
{hh 	
tryii 
{jj 
varkk 
fundkk 
=kk 
awaitkk  
_fundsPersistkk! .
.kk. /
GetFundByCodeAsynckk/ A
(kkA B
oldFundCodekkB M
)kkM N
;kkN O
ifmm 
(mm 
fundmm 
==mm 
nullmm 
)mm  
throwmm! &
newmm' *
	Exceptionmm+ 4
(mm4 5
$strmm5 [
)mm[ \
;mm\ ]
fundoo 
.oo 
FundCodeoo 
=oo 
newFundCodeoo  +
;oo+ ,
_generalPersistqq 
.qq  
DetachLocalqq  +
<qq+ ,
Fundsqq, 1
>qq1 2
(qq2 3
xqq3 4
=>qq4 6
xqq6 7
.qq7 8
Idqq8 :
==qq; =
fundqq> B
.qqB C
IdqqC E
)qqE F
;qqF G
_generalPersistss 
.ss  
Updatess  &
<ss& '
Fundsss' ,
>ss, -
(ss- .
fundss. 2
)ss2 3
;ss3 4
awaituu 
_generalPersistuu %
.uu% &
SaveChangesAsyncuu& 6
(uu6 7
)uu7 8
;uu8 9
fundww 
=ww 
awaitww 
_fundsPersistww *
.ww* +
GetFundByCodeAsyncww+ =
(ww= >
newFundCodeww> I
)wwI J
;wwJ K
returnyy 
fundyy 
;yy 
}zz 
catch{{ 
({{ 
System{{ 
.{{ 
	Exception{{ #
ex{{$ &
){{& '
{|| 
throw}} 
new}} 
	Exception}} #
(}}# $
ex}}$ &
.}}& '
Message}}' .
)}}. /
;}}/ 0
}~~ 
} 	
public
�� 
async
�� 
Task
�� 
<
�� 
bool
�� 
>
�� 
AddFundsAsync
��  -
(
��- .
IEnumerable
��. 9
<
��9 :
DetailedFunds
��: G
>
��G H
detailedFunds
��I V
)
��V W
{
�� 	
try
�� 
{
�� 
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
{C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\FundsYieldService.cs
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
return99 
true99 
;99 
}:: 
catch;; 
(;; 
System;; 
.;; 
	Exception;; #
ex;;$ &
);;& '
{<< 
Console== 
.== 
	WriteLine== !
(==! "
ex==" $
.==$ %
Message==% ,
)==, -
;==- .
return>> 
false>> 
;>> 
}?? 
}@@ 	
}BB 
}CC �

�C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\helpers\InvestimentosProfile.cs
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
�C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\RankOfTheBestFundsService.cs
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
tryEE 
{FF 
varHH 
fundsHH 
=HH 
awaitHH ! 
_detailedFundServiceHH" 6
.HH6 7$
GetAllDetailedFundsAsyncHH7 O
(HHO P
)HHP Q
;HHQ R
varJJ 
	bestFundsJJ 
=JJ 
_mapperJJ  '
.JJ' (
MapJJ( +
<JJ+ ,
IEnumerableJJ, 7
<JJ7 8
RankOfTheBestFundsJJ8 J
>JJJ K
>JJK L
(JJL M
fundsJJM R
)JJR S
;JJS T
	bestFundsMM 
=MM 
	bestFundsMM %
.MM% &
WhereMM& +
(MM+ ,
xMM, -
=>MM. 0
xMM1 2
.MM2 3
	LiquidityMM3 <
>=MM= ?
$numMM@ G
)MMG H
;MMH I
	bestFundsPP 
=PP 
	bestFundsPP %
.PP% &
OrderByDescendingPP& 7
(PP7 8
xPP8 9
=>PP: <
xPP= >
.PP> ?
DividendYieldPP? L
)PPL M
;PPM N
forTT 
(TT 
intTT 
iTT 
=TT 
$numTT 
;TT 
iTT  !
<TT" #
	bestFundsTT$ -
.TT- .
CountTT. 3
(TT3 4
)TT4 5
;TT5 6
iTT7 8
++TT8 :
)TT: ;
{UU 
	bestFundsVV 
.VV 
	ElementAtVV '
(VV' (
iVV( )
)VV) *
.VV* + 
DividendYieldRankingVV+ ?
=VV@ A
iVVB C
+VVC D
$numVVD E
;VVE F
}WW 
	bestFunds[[ 
=[[ 
	bestFunds[[ %
.[[% &
OrderBy[[& -
([[- .
x[[. /
=>[[0 2
x[[3 4
.[[4 5
PriceEquityValue[[5 E
)[[E F
;[[F G
for]] 
(]] 
int]] 
i]] 
=]] 
$num]] 
;]] 
i]]  !
<]]" #
	bestFunds]]$ -
.]]- .
Count]]. 3
(]]3 4
)]]4 5
;]]5 6
i]]7 8
++]]8 :
)]]: ;
{^^ 
	bestFunds__ 
.__ 
	ElementAt__ '
(__' (
i__( )
)__) *
.__* +
	RankPrice__+ 4
=__5 6
i__7 8
+__8 9
$num__9 :
;__: ;
}`` 
fordd 
(dd 
intdd 
idd 
=dd 
$numdd 
;dd 
idd  !
<dd" #
	bestFundsdd$ -
.dd- .
Countdd. 3
(dd3 4
)dd4 5
;dd5 6
idd7 8
++dd8 :
)dd: ;
{ee 
	bestFundsff 
.ff 
	ElementAtff '
(ff' (
iff( )
)ff) *
.ff* +
MultiplierRankingff+ <
=ff= >
	bestFundsgg '
.gg' (
	ElementAtgg( 1
(gg1 2
igg2 3
)gg3 4
.gg4 5 
DividendYieldRankinggg5 I
+ggJ K
	bestFundsggL U
.ggU V
	ElementAtggV _
(gg_ `
igg` a
)gga b
.ggb c
	RankPriceggc l
;ggl m
}hh 
	bestFundsjj 
=jj 
	bestFundsjj %
.jj% &
OrderByjj& -
(jj- .
xjj. /
=>jj0 2
xjj3 4
.jj4 5
MultiplierRankingjj5 F
)jjF G
;jjG H
fornn 
(nn 
intnn 
inn 
=nn 
$numnn 
;nn 
inn  !
<nn" #
	bestFundsnn$ -
.nn- .
Countnn. 3
(nn3 4
)nn4 5
;nn5 6
inn7 8
++nn8 :
)nn: ;
{oo 
varqq 
resultqq 
=qq  
awaitqq! &
_fundsYeldServiceqq' 8
.qq8 9"
GetFundYeldByCodeAsyncqq9 O
(qqO P
	bestFundsqqP Y
.qqY Z
	ElementAtqqZ c
(qqc d
iqqd e
)qqe f
.qqf g
FundCodeqqg o
)qqo p
;qqp q
resultss 
=ss 
resultss #
.ss# $
OrderByDescendingss$ 5
(ss5 6
xss6 7
=>ss8 :
xss; <
.ss< =
LastComputedDatess= M
)ssM N
.ssN O
TakessO S
(ssS T
$numssT V
)ssV W
;ssW X
varuu 
standardDeviationuu )
=uu* +
resultuu, 2
.uu2 3
Selectuu3 9
(uu9 :
xuu: ;
=>uu< >
xuu? @
.uu@ A
ValueuuA F
)uuF G
.uuG H
StandardDeviationuuH Y
(uuY Z
)uuZ [
;uu[ \
varww 
averageww 
=ww  !
resultww" (
.ww( )
Selectww) /
(ww/ 0
xww0 1
=>ww2 4
xww5 6
.ww6 7
Valueww7 <
)ww< =
.ww= >
Averageww> E
(wwE F
)wwF G
;wwG H
varyy !
CoefficienOfVariationyy -
=yy. /
(yy0 1
standardDeviationyy1 B
/yyC D
averageyyE L
)yyL M
*yyN O
$numyyP S
;yyS T
	bestFunds{{ 
.{{ 
	ElementAt{{ '
({{' (
i{{( )
){{) *
.{{* +"
CoefficientOfVariation{{+ A
={{B C!
CoefficienOfVariation{{D Y
;{{Y Z
}|| 
	bestFunds~~ 
=~~ 
	bestFunds~~ %
.~~% &
Where~~& +
(~~+ ,
x~~, -
=>~~. 0
x~~1 2
.~~2 3"
CoefficientOfVariation~~3 I
<=~~J L
$num~~M O
)~~O P
;~~P Q
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
}�� �
wC:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\StocksService.cs
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
$" !
$str! e
{e f
namef j
}j k
$str	k �
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
}66 �	
vC:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\TesteService.cs
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
 
TesteService

 
:

 
ITesteService

  -
{ 
private 
readonly 
ITestePersist &
_testeService' 4
;4 5
public 
TesteService 
( 
ITestePersist )
testeService* 6
)6 7
{ 	
_testeService 
= 
testeService (
;( )
} 	
public 
async 
Task 
< 
double  
>  !
Soma" &
(& '
double' -
a. /
,/ 0
double1 7
b8 9
)9 :
{ 	
var 
result 
= 
await 
_testeService ,
., -
Soma- 1
(1 2
a2 3
,3 4
b5 6
)6 7
;7 8
return 
result 
; 
} 	
} 
} ��
�C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.Application\WebScrapingFundsAndYeldsService.cs
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
,S T
IDisposableU `
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
wait>> 
.>> 
Until>> 
(>> 
ExpectedConditions>> -
.>>- .,
 VisibilityOfAllElementsLocatedBy>>. N
(>>N O
By>>O Q
.>>Q R
XPath>>R W
(>>W X
$str>>X }
)>>} ~
)>>~ 
)	>> �
;
>>� �
await@@ 
VariablesManager@@ &
.@@& '
ConectionsWebSocket@@' :
.@@: ;
socketManager@@; H
.@@H I!
SendMessageToAllAsync@@I ^
(@@^ _
JsonConvert@@_ j
.@@j k
SerializeObject@@k z
(@@z {
driver	@@{ �
.
@@� �

PageSource
@@� �
)
@@� �
)
@@� �
;
@@� �
varBB 
rowsBB 
=BB 
driverBB !
.BB! "
FindElementsBB" .
(BB. /
ByBB/ 1
.BB1 2
XPathBB2 7
(BB7 8
$strBB8 ]
)BB] ^
)BB^ _
;BB_ `
intEE 
numberOfLinesEE %
=EE& '
$numEE( *
;EE* +
ConsoleJJ 
.JJ 
	WriteLineJJ !
(JJ! "
$"JJ" $
$strJJ$ 4
{JJ4 5
numberOfLinesJJ5 B
}JJB C
"JJC D
)JJD E
;JJE F
varLL 
columnsLL 
=LL 
driverLL $
.LL$ %
FindElementsLL% 1
(LL1 2
ByLL2 4
.LL4 5
XPathLL5 :
(LL: ;
$strLL; c
)LLc d
)LLd e
;LLe f
intNN 
numberOfColumnNN "
=NN# $
columnsNN% ,
.NN, -
CountNN- 2
;NN2 3
ConsolePP 
.PP 
	WriteLinePP !
(PP! "
$"PP" $
$strPP$ 5
{PP5 6
numberOfColumnPP6 D
}PPD E
"PPE F
)PPF G
;PPG H
ifRR 
(RR !
totalOfColumnExpectedRR (
!=RR) +
numberOfColumnRR, :
)RR: ;
{SS 
ConsoleTT 
.TT 
	WriteLineTT %
(TT% &
$"TT& (
$strTT( J
{TTJ K
numberOfColumnTTK Y
}TTY Z
"TTZ [
)TT[ \
;TT\ ]
}UU 
forWW 
(WW 
intWW 
jWW 
=WW 
$numWW 
;WW 
jWW  !
<=WW" $
numberOfColumnWW% 3
-WW4 5
$numWW6 7
;WW7 8
jWW9 :
++WW: <
)WW< =
{XX 
stringYY 
campoYY  
=YY! "
driverYY# )
.YY) *
FindElementYY* 5
(YY5 6
ByYY6 8
.YY8 9
XPathYY9 >
(YY> ?
$"YY? A
$strYYA e
{YYe f
$numYYf g
}YYg h
$strYYh m
{YYm n
jYYn o
}YYo p
$strYYp q
"YYq r
)YYr s
)YYs t
.YYt u
TextYYu y
;YYy z
if[[ 
([[ #
orderColumnTableOfFunds[[ .
[[[. /
j[[/ 0
-[[0 1
$num[[1 2
][[2 3
!=[[4 6
campo[[7 <
)[[< =
{\\ 
Console]] 
.]]  
Write]]  %
(]]% &
$"]]& (
$str]]( 8
{]]8 9#
orderColumnTableOfFunds]]9 P
[]]P Q
j]]Q R
-]]R S
$num]]S T
]]]T U
}]]U V
$str]]V `
{]]` a
campo]]a f
}]]f g
"]]g h
)]]h i
;]]i j
return^^ 
await^^ $
Task^^% )
.^^) *

FromResult^^* 4
<^^4 5
IEnumerable^^5 @
<^^@ A
DetailedFunds^^A N
>^^N O
>^^O P
(^^P Q
detailedFunds^^Q ^
)^^^ _
;^^_ `
}__ 
Consoleaa 
.aa 
Writeaa !
(aa! "
driveraa" (
.aa( )
FindElementaa) 4
(aa4 5
Byaa5 7
.aa7 8
XPathaa8 =
(aa= >
$"aa> @
$straa@ d
{aad e
$numaae f
}aaf g
$straag l
{aal m
jaam n
}aan o
$straao p
"aap q
)aaq r
)aar s
.aas t
Textaat x
+aay z
$str	aa{ �
)
aa� �
;
aa� �
}bb 
fordd 
(dd 
intdd 
idd 
=dd 
$numdd 
;dd 
idd  !
<=dd" $
numberOfLinesdd% 2
;dd2 3
idd4 5
++dd5 7
)dd7 8
{ee 
stringff 
[ff 
]ff 
objff  
=ff! "
newff# &
stringff' -
[ff- .
$numff. 0
]ff0 1
;ff1 2
objgg 
[gg 
$numgg 
]gg 
=gg 
igg 
.gg 
ToStringgg '
(gg' (
)gg( )
;gg) *
forii 
(ii 
intii 
jii 
=ii  
$numii! "
;ii" #
jii$ %
<=ii& (
numberOfColumnii) 7
-ii8 9
$numii: ;
;ii; <
jii= >
++ii> @
)ii@ A
{jj 
varkk 
ekk 
=kk 
driverkk  &
.kk& '
FindElementkk' 2
(kk2 3
Bykk3 5
.kk5 6
XPathkk6 ;
(kk; <
$"kk< >
$strkk> b
{kkb c
ikkc d
}kkd e
$strkke j
{kkj k
jkkk l
}kkl m
$strkkm n
"kkn o
)kko p
)kkp q
.kkq r
Textkkr v
;kkv w
objll 
[ll 
jll 
]ll 
=ll  
ell! "
;ll" #
}mm 
varoo 
fundoo 
=oo 
newoo "
DetailedFundsoo# 0
(oo0 1
)oo1 2
{oo2 3
FundCodepp  
=pp! "
objpp# &
[pp& '
$numpp' (
]pp( )
,pp) *
Segmentpp+ 2
=pp3 4
objpp5 8
[pp8 9
$numpp9 :
]pp: ;
,pp; <
	Quotationpp= F
=ppG H
ConvertppI P
.ppP Q
ToDoubleppQ Y
(ppY Z
objppZ ]
[pp] ^
$numpp^ _
]pp_ `
)pp` a
,ppa b
FFOYieldqq  
=qq! "
Convertqq# *
.qq* +
ToDoubleqq+ 3
(qq3 4
objqq4 7
[qq7 8
$numqq8 9
]qq9 :
.qq: ;
Replaceqq; B
(qqB C
$strqqC F
,qqF G
$strqqH J
)qqJ K
)qqK L
,qqL M
DividendYieldqqN [
=qq\ ]
Convertqq^ e
.qqe f
ToDoubleqqf n
(qqn o
objqqo r
[qqr s
$numqqs t
]qqt u
.qqu v
Replaceqqv }
(qq} ~
$str	qq~ �
,
qq� �
$str
qq� �
)
qq� �
)
qq� �
,
qq� �
PriceEquityValuerr (
=rr) *
Convertrr+ 2
.rr2 3
ToDoublerr3 ;
(rr; <
objrr< ?
[rr? @
$numrr@ A
]rrA B
)rrB C
,rrC D
ValueOfMarketrrE R
=rrS T
ConvertrrU \
.rr\ ]
ToDoublerr] e
(rre f
objrrf i
[rri j
$numrrj k
]rrk l
)rrl m
,rrm n
	Liquidityss !
=ss" #
Convertss$ +
.ss+ ,
ToDoubless, 4
(ss4 5
objss5 8
[ss8 9
$numss9 :
]ss: ;
)ss; <
,ss< =
NumberOfPropertiesss> P
=ssQ R
ConvertssS Z
.ssZ [
ToDoubless[ c
(ssc d
objssd g
[ssg h
$numssh i
]ssi j
)ssj k
,ssk l
SquareMeterPricett (
=tt) *
Converttt+ 2
.tt2 3
ToDoublett3 ;
(tt; <
objtt< ?
[tt? @
$numtt@ B
]ttB C
)ttC D
,ttD E
RentPerSquareMeterttF X
=ttY Z
Converttt[ b
.ttb c
ToDoublettc k
(ttk l
objttl o
[tto p
$numttp r
]ttr s
)tts t
,ttt u
CapRateuu 
=uu  !
Convertuu" )
.uu) *
ToDoubleuu* 2
(uu2 3
objuu3 6
[uu6 7
$numuu7 9
]uu9 :
.uu: ;
Replaceuu; B
(uuB C
$struuC F
,uuF G
$struuH J
)uuJ K
)uuK L
,uuL M
AverageVacancyuuN \
=uu] ^
Convertuu_ f
.uuf g
ToDoubleuug o
(uuo p
objuup s
[uus t
$numuut v
]uuv w
.uuw x
Replaceuux 
(	uu �
$str
uu� �
,
uu� �
$str
uu� �
)
uu� �
)
uu� �
}vv 
;vv 
awaitxx 
VariablesManagerxx *
.xx* +
ConectionsWebSocketxx+ >
.xx> ?
socketManagerxx? L
.xxL M!
SendMessageToAllAsyncxxM b
(xxb c
JsonConvertxxc n
.xxn o
SerializeObjectxxo ~
(xx~ 
fund	xx �
)
xx� �
)
xx� �
;
xx� �
Consoleyy 
.yy 
	WriteLineyy %
(yy% &
$"yy& (
{yy( )
JsonConvertyy) 4
.yy4 5
SerializeObjectyy5 D
(yyD E
fundyyE I
)yyI J
}yyJ K
"yyK L
)yyL M
;yyM N
detailedFunds{{ !
.{{! "
Add{{" %
({{% &
fund{{& *
){{* +
;{{+ ,
}|| 
driver~~ 
.~~ 
Close~~ 
(~~ 
)~~ 
;~~ 
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
�� 
$str
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
��* ,
$str
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
��* ,
$str
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
��. 0
$str
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
��C E
$str
��E c
{
��c d
$num
��d e
}
��e f
$str
��f k
{
��k l
j
��l m
}
��m n
$str
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
��* ,
$str
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
��Y Z
$str
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
��B D
$str
��D b
{
��b c
$num
��c d
}
��d e
$str
��e j
{
��j k
j
��k l
}
��l m
$str
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
��@ B
$str
��B `
{
��` a
i
��a b
}
��b c
$str
��c h
{
��h i
j
��i j
}
��j k
$str
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
�� 
;
�� 
Console
�� 
.
��  
	WriteLine
��  )
(
��) *
$"
��* ,
{
��, -
JsonConvert
��- 8
.
��8 9
SerializeObject
��9 H
(
��H I
fY
��I K
)
��K L
}
��L M
"
��M N
)
��N O
;
��O P
await
�� 
VariablesManager
�� .
.
��. /!
ConectionsWebSocket
��/ B
.
��B C
socketManager
��C P
.
��P Q#
SendMessageToAllAsync
��Q f
(
��f g
JsonConvert
��g r
.
��r s
SerializeObject��s �
(��� �
fY��� �
)��� �
)��� �
;��� �
if
�� 
(
�� 

lastDateDB
�� %
!=
��& (
null
��) -
)
��- .
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
�� 
void
�� 
Dispose
�� 
(
�� 
)
�� 
{
�� 	
driver
�� 
.
�� 
Quit
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
}
�� 
}�� 