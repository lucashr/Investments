ä
‚C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.VariablesManager\ConectionsWebSocket.cs
	namespace 	
Investments
 
. 
VariablesManager &
{ 
public 

static 
class 
ConectionsWebSocket +
{ 
public 
static 
readonly $
WebScrapingSocketManager 7
socketManager8 E
=F G
newH K$
WebScrapingSocketManagerL d
(d e
)e f
;f g
public		 
static		  
ConcurrentDictionary		 *
<		* +
string		+ 1
,		1 2
	WebSocket		3 <
>		< =
sockets		> E
=		F G
new		H K 
ConcurrentDictionary		L `
<		` a
string		a g
,		g h
	WebSocket		i r
>		r s
(		s t
)		t u
;		u v
}

 
} ñ+
‡C:\Users\lucas\Desktop\Repositorios\Meus_repositorios\Investments\WEB\Back\src\Investments.VariablesManager\WebScrapingSocketManager.cs
	namespace		 	
Investments		
 
.		 
VariablesManager		 &
{

 
public 

class $
WebScrapingSocketManager )
{ 
public 
	WebSocket 
GetSocketById &
(& '
string' -
id. 0
)0 1
{ 	
return 
ConectionsWebSocket &
.& '
sockets' .
.. /
FirstOrDefault/ =
(= >
p> ?
=>@ B
pC D
.D E
KeyE H
==I K
idL N
)N O
.O P
ValueP U
;U V
} 	
public  
ConcurrentDictionary #
<# $
string$ *
,* +
	WebSocket, 5
>5 6
GetAll7 =
(= >
)> ?
{ 	
return 
ConectionsWebSocket &
.& '
sockets' .
;. /
} 	
public 
string 
GetId 
( 
	WebSocket %
socket& ,
), -
{ 	
return 
ConectionsWebSocket &
.& '
sockets' .
.. /
FirstOrDefault/ =
(= >
p> ?
=>@ B
pC D
.D E
ValueE J
==K M
socketN T
)T U
.U V
KeyV Y
;Y Z
} 	
public 
string 
	AddSocket 
(  
	WebSocket  )
socket* 0
)0 1
{ 	
var 
id 
= 
CreateConnectionId '
(' (
)( )
;) *
ConectionsWebSocket   
.    
sockets    '
.  ' (
TryAdd  ( .
(  . /
id  / 1
,  1 2
socket  3 9
)  9 :
;  : ;
return"" 
id"" 
;"" 
}## 	
public%% 
async%% 
Task%% 
RemoveSocket%% &
(%%& '
string%%' -
id%%. 0
)%%0 1
{&& 	
	WebSocket'' 
socket'' 
;'' 
ConectionsWebSocket(( 
.((  
sockets((  '
.((' (
	TryRemove((( 1
(((1 2
id((2 4
,((4 5
out((6 9
socket((: @
)((@ A
;((A B
await** 
socket** 
.** 

CloseAsync** #
(**# $
closeStatus**$ /
:**/ 0 
WebSocketCloseStatus**1 E
.**E F
NormalClosure**F S
,**S T
statusDescription++$ 5
:++5 6
$str++7 W
,++W X
cancellationToken,,$ 5
:,,5 6
CancellationToken,,7 H
.,,H I
None,,I M
),,M N
;,,N O
}-- 	
private// 
string// 
CreateConnectionId// )
(//) *
)//* +
{00 	
return11 
Guid11 
.11 
NewGuid11 
(11  
)11  !
.11! "
ToString11" *
(11* +
)11+ ,
;11, -
}22 	
public44 
async44 
Task44 !
SendMessageToAllAsync44 /
(44/ 0
string440 6
message447 >
)44> ?
{55 	
foreach66 
(66 
var66 
pair66 
in66  
ConectionsWebSocket66! 4
.664 5
sockets665 <
)66< =
{77 
if88 
(88 
pair88 
.88 
Value88 
.88 
State88 $
==88% '
WebSocketState88( 6
.886 7
Open887 ;
)88; <
await99 
SendMessageAsync99 *
(99* +
pair99+ /
.99/ 0
Value990 5
,995 6
message997 >
)99> ?
;99? @
}:: 
};; 	
private== 
async== 
Task== 
SendMessageAsync== +
(==+ ,
	WebSocket==, 5
socket==6 <
,==< =
string==> D
message==E L
)==L M
{>> 	
if?? 
(?? 
socket?? 
.?? 
State?? 
!=?? 
WebSocketState??  .
.??. /
Open??/ 3
)??3 4
return@@ 
;@@ 
awaitBB 
socketBB 
.BB 
	SendAsyncBB "
(BB" #
bufferBB# )
:BB) *
newBB+ .
ArraySegmentBB/ ;
<BB; <
byteBB< @
>BB@ A
(BBA B
arrayBBB G
:BBG H
EncodingBBI Q
.BBQ R
ASCIIBBR W
.BBW X
GetBytesBBX `
(BB` a
messageBBa h
)BBh i
,BBi j
offsetCCB H
:CCH I
$numCCJ K
,CCK L
countDDB G
:DDG H
messageDDI P
.DDP Q
LengthDDQ W
)DDW X
,DDX Y
messageTypeEE# .
:EE. / 
WebSocketMessageTypeEE0 D
.EED E
TextEEE I
,EEI J
endOfMessageFF# /
:FF/ 0
trueFF1 5
,FF5 6
cancellationTokenGG# 4
:GG4 5
CancellationTokenGG6 G
.GGG H
NoneGGH L
)GGL M
;GGM N
}HH 	
}II 
}KK 