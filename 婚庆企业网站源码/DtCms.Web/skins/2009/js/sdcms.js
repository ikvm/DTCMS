function killErrors() {return true;}
window.onerror = killErrors;
var Ajax_msg="��ȡʧ��";
$(pageInit);
var editor;
function pageInit()
{
	editor=$('#content').xheditor(true)[0].xheditor;
}
//��������	
function runcode(codeBtn)
{
	var codeText=codeBtn.parentNode.firstChild;
	var codeValue=codeText.value;
	var rng=window.open('','','');
		rng.opener=null;
		rng.document.write(codeValue);
		rng.document.close();
}

//���ƴ���
function copycode(codeBtn)
{
	var codeText=codeBtn.parentNode.firstChild;
	var rng=codeText.createTextRange();
		rng.moveToElementText(codeText);
		rng.scrollIntoView();
		rng.select();
		rng.execCommand("Copy");
		rng.collapse(false);
}

//�������
function savecode(codeBtn)
{	
	var winname=window.open('about:blank', '_blank', 'top=100');
		winname.document.open();
		winname.document.write(codeBtn.parentNode.firstChild.value);
		winname.document.execCommand('saveas','','runcode.htm.html');
		winname.close();		
}

//JS���Server.UrlEncode���뺯��
function urlEncode(str) 
{ 
    str = str.replace(/./g,function(sHex) 
    { 
        window.EnCodeStr = ""; 
        window.sHex = sHex; 
        window.execScript('window.EnCodeStr=Hex(Asc(window.sHex))',"vbscript"); 
        return window.EnCodeStr.replace(/../g,"%$&"); 
    }); 
    return str; 
} 

function trim(s){return  s.replace(/(^\s*)|(\s*$)/g,  "");} 

function setTab(name,cursel,n){
	for(i=1;i<=n;i++){
		var menu=$('#'+name+i);
		var con=$("#con_"+name+"_"+i);
		menu[0].className=i==cursel?"hover":"";
		con[0].style.display=i==cursel?"block":"none";
	}
}

function load_menu(t0,t1,t2)
{
	var t3=location.href;
	//alert(t3);
	if(t3.indexOf(t0)!="-1"){$("#"+t2).addClass(t1);}
}

function Get_Spider()
{
	$.ajax({
	type: "get",
	cache:false,
	url: webdir+"inc\\Spider.asp.html",
	timeout: 20000,
	error: function(){},
	success: function(){}
	});
}

function gourl(t0,t1,t2,t3)
{
	var t4=$("#gopage")[0].value;
	t4=parseInt(t4);
	if (isNaN(t4)){t4=1;}
	if (t4<=1){t4=1;}
	if (t4>=t0){t4=t0;}
	if (t3==1)
	{
		if (t4<=1){t5=t1+t2;}else{t4=t4-1;t5=t1+"_"+t4+t2;}
	}
	else{
		if (t4<=1){t5=t1+t2;}else{t5=t1+t4+t2;}
		}
	location.href=t5;
}

function get_hits(t0,t1,t2,t3)
{
	$('#'+t3).html("<img src="+webdir+"skins/2009/images/loading.gif>");
	$.ajax({
	type: "get",
	cache:false,
	url: webdir+"inc\\MS_12.html"+t0+"&action="+t1+"&t="+t2+"",
	timeout: 20000,
	error: function(){$('#'+t3).html(Ajax_msg);},
	success: function(t0){$('#'+t3).html(t0);}
	});
}

function Get_Digg(t0,t1)
{
	$('#'+t1).html("<img src="+webdir+"skins/2009/images/loading.gif>");
	$.ajax({
	type: "get",
	cache:false,
	url: webdir+"inc\\MS_13.html"+t0+"",
	timeout: 20000,
	error: function(){$('#'+t1).html(Ajax_msg);},
	success: function(t0){$('#'+t1).html(t0);}
	});
}

function Digg(t0,t1,t2)
{
	$('#'+t2).html("<img src="+webdir+"skins/2009/images/loading.gif>");
	$.ajax({
	type: "get",
	cache:false,
	url: webdir+"inc\\MS_13.html"+t0+"&action=Digg",
	timeout: 20000,
	error: function(){$('#'+t2).html(Ajax_msg);},
	success: function(t3){$('#'+t2).html(t3.substring(1));if(t3.substring(0,1)==0){Get_Digg(t0,t1)}}
	});
}

function Digg_Action(t0,t1,t2,t3,t4,t5)
{
	$.ajax({
	type: "get",
	cache:false,
	url: webdir+"Plug\\MS_14.html"+t0+"&action="+t1+"",
	timeout: 20000,
	error: function(){alert(Ajax_msg);},
	success: function(t6){
		var t7=t6.split(':');
		var sUp=parseInt(t7[0]);
		var sDown=parseInt(t7[1]);
		var sTotal=sUp+sDown;
		if(sTotal==0)
		{
			var spUp=0;var spDown=0;
		}
		else
		{
		var spUp=(sUp/sTotal)*100;
		spUp=Math.round(spUp*10)/10;
		var spDown=100-spUp;
		spDown=Math.round(spDown*10)/10;
		}
		var t8=t7[2];
		if (t8==1)
		{
			$('#'+t2).html(spUp+"%("+sUp+")");
			$('#'+t3).html(spDown+"%("+sDown+")")
			$('#'+t4)[0].style.width=spUp+'%';
			$('#'+t5)[0].style.width=spDown+'%';
		}
		else{alert('�������ѱ��̬���');}
		}
	});
}

function get_comment(t0,t1)
{
	$('#'+t1).html("<img src="+webdir+"skins/2009/images/loading.gif>");
	$.ajax({
	type: "get",
	cache:false,
	url: webdir+"inc\\MS_12.html"+t0+"&action=2",
	timeout: 20000,
	error: function(){$('#'+t1).html(Ajax_msg)},
	success: function(t0){$('#'+t1).html(t0);}
	})
}


function checksearch(theform)
{
	if (trim(theform.key.value)=='')
	{alert('�ؼ��ֲ���Ϊ��');
	theform.key.focus();
	theform.key.value='';
	return false
	}
	if (theform.key.value=='������ؼ���')
	{alert('�ؼ��ֲ���Ϊ��');
	theform.key.focus();
	theform.key.value='';
	return false
	}
	if(navigator.userAgent.indexOf("MSIE")>0){
	window.location.href=webdir+.html"search\\MS_15.html?/"+urlEncode(trim(theform.key.value))+"..\\..\\..\\index.html"+urlEncode(trim(theform.classid.value))+"..\\..\\..\\index.html";}
	else{window.location.href=webdir+.html"search\\MS_15.html?/"+trim(theform.key.value)+"..\\..\\..\\index.html"+urlEncode(trim(theform.classid.value))+"..\\..\\..\\index.html";}
	return false
}
function checkcomment(theform)
{
	if (trim(theform.username.value)=='')
	{alert('��������Ϊ��');
		theform.username.focus();
		theform.username.value='';
		return false
	}
	if (trim(editor.getSource())=='')
	{alert('���ݲ���Ϊ��');
		editor.focus();
		theform.content.value='';
		return false
	}
	if (trim(theform.yzm.value)=='')
	{   alert('��֤�벻��Ϊ��');
		theform.yzm.focus();
		theform.yzm.value='';
		return false
	}
	var param;
	param=webdir+"plug\\MS_16.html";
	param+="&username="+escape(trim(theform.username.value));
	param+="&content="+escape(trim(theform.content.value));
	param+="&yzm="+escape(trim(theform.yzm.value));
	param+="&id="+escape(trim(theform.id.value));
	$('#showmsg').html("<img src="+webdir+"skins/2009/images/loading.gif>");
	$.ajax({
	type:"get",
	cache:false,
	url:param,
	timeout:20000,
	error:function(){$('#showmsg').html(Ajax_msg);},
	success:function(t0)
	{
		$('#showmsg').html(t0.substring(1));
		if(t0.substring(0,1)==0){theform.username.value='';theform.yzm.value='';editor.setSource('');$("#yzm_num")[0].src = $("#yzm_num")[0].src+"&"+Math.random();get_comment(theform.id.value,'show_i_commentnum');load_comment(theform.id.value,'comment_list');}
		}
	});
	return false
}


function load_comment(t0,t1)
{
	$('#'+t1).html("<img src="+webdir+"skins/2009/images/loading.gif>");
	$.ajax({
	type: "get",
	cache:false,
	url:webdir+"plug\\MS_17.html"+t0+"&t0="+t1+"",
	timeout: 20000,
	error:function(){$('#'+t1).html(Ajax_msg)},
	success:function(t0){$('#'+t1).html(t0.substring(1));}
	})
}

function get_comment_page(t0,t1,t2)
{
	$('#'+t2).html("<img src="+webdir+"skins/2009/images/loading.gif>");
	$.ajax({
	type: "get",
	cache:false,
	url:webdir+"plug\\MS_17.html"+t1+"&page="+t0+"&t0="+t2+"",
	timeout:20000,
	error:function(){$('#'+t2).html(Ajax_msg)},
	success:function(t0){$('#'+t2).html(t0);}
	})
}

function checkbook(theform)
{  
	if (trim(theform.title.value)=='')
	{   alert('���ⲻ��Ϊ��');
		theform.title.focus();
		theform.title.value='';
		return false
	}
	if (trim(theform.username.value)=='')
	{   alert('��������Ϊ��');
		theform.username.focus();
		theform.username.value='';
		return false
	}
	if (trim(theform.content.value)=='')
	{   alert('���ݲ���Ϊ��');
		theform.content.focus();
		theform.content.value='';
		return false
	}
	if (trim(theform.yzm.value)=='')
	{   alert('��֤�벻��Ϊ��');
		theform.yzm.focus();
		theform.yzm.value='';
		return false
	}
}

function checkLink(theform)
{  
	if (trim(theform.t0.value)=='')
	{   alert('��վ����Ϊ��');
		theform.t0.focus();
		theform.t0.value='';
		return false
	}
	if (trim(theform.t1.value)=='')
	{   alert('��ַ����Ϊ��');
		theform.t1.focus();
		theform.t1.value='';
		return false
	}
	if (trim(theform.t3.value)=='')
	{   alert('��֤�벻��Ϊ��');
		theform.t3.focus();
		theform.t3.value='';
		return false
	}
	var param;
	param=webdir+"plug\\MS_18.html";
	param+="&t0="+escape(trim(theform.t0.value));
	param+="&t1="+escape(trim(theform.t1.value));
	param+="&t2="+escape(trim(theform.t2.value));
	param+="&t3="+escape(trim(theform.t3.value));
	$('#showmsg').html("<img src="+webdir+"skins/2009/images/loading.gif>");
	$.ajax({
	type:"post",
	cache:false,
	url:param,
	timeout:20000,
	error:function(){$('#showmsg').html(Ajax_msg);},
	success:function(t0)
	{
		$('#showmsg').html(t0.substring(1));
		if(t0.substring(0,1)==0){theform.t0.value='';theform.t1.value='';theform.t2.value='';theform.t3.value='';$("#yzm_num")[0].src = $("#yzm_num")[0].src+"&"+Math.random();$('#showmsg').html(t0.substring(1));}
		}
	});
	return false
}

function checkvote(theform,t1)
{  
	var temp=""; 
	for(var i=0;i<theform.vote.length;i++) 
	{ 
	if(theform.vote[i].checked) 
	temp+=theform.vote[i].value+","; 
	}
	if(temp==""){
		$("#showvote").html("����ѡ��һ��ѡ��");
	return false
	}
	var param;
	param=webdir+"plug\\MS_19.html";
	param+="&t0="+escape(trim(temp));
	param+="&id="+escape(trim(theform.vote_id.value));
	$('#showvote').html("<img src="+webdir+"skins/2009/images/loading.gif>");
	$.ajax({
	type:"post",
	cache:false,
	url:param,
	timeout:20000,
	error:function(){$('#showvote').html(Ajax_msg);},
	success:function(t0)
	{
		$('#showvote').html(t0.substring(1));
		if(t0.substring(0,1)==0){$('#showvote').html(t0.substring(1));if(t1==1){window.location.href='MS_11.html'+theform.vote_id.value+'';}}
		}
	});
	return false
}

function checkPublish(theform)
{  
	if (trim(theform.t0.value)=='')
	{   alert('���ⲻ��Ϊ��');
		theform.t0.focus();
		theform.t0.value='';
		return false
	}
	if (trim(theform.t1.value)=='')
	{   alert('���߲���Ϊ��');
		theform.t1.focus();
		theform.t1.value='';
		return false
	}
	if (trim(theform.t2.value)=='')
	{   alert('��Դ����Ϊ��');
		theform.t2.focus();
		theform.t2.value='';
		return false
	}
	if (trim(theform.t3.value)=='0')
	{   alert('��ѡ����Ŀ');
		theform.t3.focus();
		return false
	}
	if (trim(editor.getSource())=='')
	{   alert('���ݲ���Ϊ��');
		editor.focus();
		theform.content.value='';
		return false
	}
	if (trim(theform.yzm.value)=='')
	{   alert('��֤�벻��Ϊ��');
		theform.yzm.focus();
		theform.yzm.value='';
		return false
	}
}

//����ҳ������ַ
function copyurl(id){
var testCode=$("#"+id)[0].value;
	if(copy2Clipboard(testCode)!=false)
		{
			$("#"+id).select();
			alert("�Ѹ��ƣ�ʹ��Ctrl+Vճ����������������Ѱ�`");
		}
}
copy2Clipboard=function(txt)
{
if(window.clipboardData)
{
	window.clipboardData.clearData();
	window.clipboardData.setData("Text",txt);
}
else if(navigator.userAgent.indexOf("Opera")!=-1)
{
	window.location=txt;
}
else if(window.netscape)
{
	try{
	   netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
	}
catch(e){
   alert("����firefox��ȫ�������������м�������������'about:config'��signed.applets.codebase_principal_support'����Ϊtrue'֮�����ԡ�");
   return false;
}
var clip=Components.classes['@mozilla.org/widget/clipboard;1'].createInstance

(Components.interfaces.nsIClipboard);
if(!clip)return;
var trans=Components.classes['@mozilla.org/widget/transferable;1'].createInstance

(Components.interfaces.nsITransferable);
if(!trans)return;
trans.addDataFlavor('text/unicode');
var str=new Object();
var len=new Object();
var str=Components.classes["@mozilla.org/supports-string;1"].createInstance
(Components.interfaces.nsISupportsString);
var copytext=txt;str.data=copytext;
trans.setTransferData("text/unicode",str,copytext.length*2);
var clipid=Components.interfaces.nsIClipboard;
if(!clip)return false;
clip.setData(trans,null,clipid.kGlobalClipboard);
}
}