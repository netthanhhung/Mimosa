if (typeof(window.RadControlsNamespace)=="\x75ndefined"){window.RadControlsNamespace=new Object(); } ; RadControlsNamespace.AppendStyleSheet= function (O,o,I){if (!I){return; }if (!O){document.write("\x3c"+"link"+"\x20rel=\047\x73ty\x6c\145s\x68\x65e\x74\047 \x74\x79pe\x3d\x27t\x65xt/c\x73\163\x27\x20hr\x65f=\047"+I+"\x27 />"); }else {var A=document.createElement("\x4cINK"); A.rel="\x73tylesheet"; A.type="\x74ext/c\x73\x73"; A.href=I; document.getElementById(o+"S\x74\x79leSheet\x48\x6flde\x72").appendChild(A); }} ; function RadComboItem(){ this.ComboBox=null; this.ClientID=null; this.Highlighted= false; this.Index=0; this.Enabled=1; this.Selected=0; this.Text=""; this.Value=null; this.U=new Array(); } ; RadComboItem.prototype.Initialize= function (Z){for (var z in Z){ this[z]=Z[z]; }} ; RadComboItem.prototype.W= function (){var w=0; var V=document.getElementById(this.ComboBox.ClientID+"\x5fDrop\x44\x6fwn"); if (V.offsetWidth!=V.scrollWidth+16){w=16; }if (this.ComboBox.Items.length>0){var totalHeight=0; for (var i=0; i<=this.Index; i++){var v=document.getElementById(this.ComboBox.Items[i].ClientID); if (v){totalHeight+=v.offsetHeight; }}V.scrollTop=totalHeight-V.offsetHeight+w; }} ; RadComboItem.prototype.T= function (){if (this.ComboBox.Items.length>0){var totalHeight=0; for (var i=0; i<this.Index; i++){var v=document.getElementById(this.ComboBox.Items[i].ClientID); if (v){totalHeight+=v.offsetHeight; }}var scrollTop=document.getElementById(this.ComboBox.ClientID+"_\x44\x72\157p\x44\x6fwn").scrollTop; if (scrollTop>totalHeight){document.getElementById(this.ComboBox.ClientID+"_Dr\x6f\x70Down").scrollTop=totalHeight; }}} ; RadComboItem.prototype.Highlight= function (){if (!this.Highlighted && this.Enabled){ this.ComboBox.UnHighlightAll(); if (!this.ComboBox.IsTemplated || this.ComboBox.HighlightTemplatedItems){var t=document.getElementById(this.ClientID); if (t){if (!this.ComboBox.HighlightedItem){if (t.className!=this.ComboBox.ItemCssClassHover){ this.S=t.className; }else { this.S=this.ComboBox.ItemCssClass; }}t.className=this.ComboBox.ItemCssClassHover; }} this.Highlighted= true; this.ComboBox.HighlightedItem=this ; }} ; RadComboItem.prototype.UnHighlight= function (){if (this.Highlighted && this.Enabled && document.getElementById(this.ClientID)){document.getElementById(this.ClientID).className=this.S; this.Highlighted= false; this.ComboBox.HighlightedItem=null; }} ; RadComboItem.prototype.Select= function (){ this.ComboBox.SelectedItem=this ; this.ComboBox.R(this ); this.ComboBox.HideDropDown(); this.ComboBox.r= true; this.ComboBox.Q(); } ; function RadComboBox(P,N,n){var M=window[N]; if (M!=null && !M.tagName){M.Dispose( true); }if (window.tlrkComboBoxes==null){window.tlrkComboBoxes=new Array(); }tlrkComboBoxes[tlrkComboBoxes.length]=this ; this.Items=new Array(); this.m= false; this.ID=P; this.ClientID=N; this.L=N; this.DropDownID=N+"\x5f\x44ropDown"; this.InputID=N+"_\x49\x6eput"; this.ImageID=N+"_Image"; this.DropDownPlaceholderID=N+"\x5f\x44ropDow\x6e\x50lac\x65\x68ol\x64\x65r"; this.MoreResultsBoxID=N+"_MoreResu\x6c\x74sBox"; this.MoreResultsBoxImageID=N+"\x5f\x4doreResu\x6c\x74sBox\x49\x6dag\x65"; this.MoreResultsBoxMessageID=N+"_MoreResul\x74\x73BoxMe\x73\x73ag\x65"; this.l=N+"_Hea\x64\x65r"; this.K=document.getElementById(this.InputID); this.k=document.getElementById(this.ImageID); this.J=document.getElementById(this.DropDownPlaceholderID); this.H=document.getElementById(this.ClientID+"_\x74\x65xt"); this.h=document.getElementById(this.ClientID+"\x5fvalue"); this.G=document.getElementById(this.ClientID+"_index"); this.g=document.getElementById(this.ClientID+"_clie\x6e\x74Width"); this.F=document.getElementById(this.ClientID+"\x5fclientHeight"); this.Enabled= true; this.DropDownVisible= false; this.LoadOnDemandUrl=null; this.PollTimeOut=0; this.HighlightedItem=null; this.SelectedItem=null; this.ItemRequestTimeout=300; this.EnableLoadOnDemand= false; this.AutoPostBack= false; this.ShowMoreResultsBox= false; this.OpenDropDownOnLoad= false; this.f= false; this.MarkFirstMatch= false; this.IsCaseSensitive= false; this.SelectOnTab= true; this.PostBackReference=null; this.LoadingMessage="Loading..."; this.ScrollDownImage=null; this.ScrollDownImageDisabled=null; this.D=null; this.RadComboBoxImagePosition="\x52ight"; this.ItemCssClass=null; this.ItemCssClassHover=null; this.ItemCssClassDisabled=null; this.ImageCssClass=null; this.ImageCssClassHover=null; this.InputCssClass=null; this.InputCssClassHover=null; this.LoadingMessageCssClass="Combo\x42\x6fxLoadi\x6e\x67Mes\x73\x61ge"; this.AutoCompleteSeparator=null; this.ExternalCallBackPage=null; this.OnClientSelectedIndexChanging=null; this.OnClientDropDownOpening=null; this.OnClientDropDownClosing=null; this.OnClientItemsRequesting=null; this.OnClientSelectedIndexChanged=null; this.OnClientItemsRequested=null; this.OnClientKeyPressing=null; this.Skin="\x43lassic"; this.HideTimeoutID=0; this.d=0; this.C= false; this.B=null; this.AllowCustomText= false; this.ExpandEffectString=null; this.HighlightTemplatedItems= false; this.CausesValidation= false; this.ClientDataString=null; this.ShowDropDownOnTextboxClick= true; this.ShowWhileLoading=n; this.o0= false; this.r= false; this.O0=-1; this.IsTemplated= false; this.l0=null; this.OffsetX=0; this.OffsetY=0; this.i0= false; var comboInstance=this ; this.I0(); this.o1= function (){comboInstance.HideOnClick(); } ; if (document.attachEvent){document.attachEvent("o\x6ecl\x69\x63k",this.o1); }else {document.addEventListener("\x63\154\x69\x63k",this.o1, false); } this.O1= function (e){comboInstance.l1(e || event); } ; if (document.attachEvent){document.getElementById(this.InputID).attachEvent("o\x6e\x62lur",this.O1); }else {document.getElementById(this.InputID).addEventListener("blur",this.O1, false); } this.i1= function (){comboInstance.I1(); } ; if (document.attachEvent){document.getElementById(this.InputID).attachEvent("\x6fnfocu\x73",this.i1); }else {document.getElementById(this.InputID).addEventListener("\x66ocus",this.i1, false); }document.getElementById(this.InputID).setAttribute("\141ut\x6f\x63omplet\x65","off"); document.getElementById(this.DropDownPlaceholderID).onselectstart= function (){return false; } ; if (typeof(RadCallbackNamespace)!="\x75ndefined"){window.setTimeout( function (){comboInstance.o2(document.getElementById(comboInstance.InputID), true);} ,100); }else {var O2= false; if (window.addEventListener){if (window.opera)window.addEventListener("\x6coad", function (){comboInstance.o2(document.getElementById(comboInstance.InputID), true); } , false); else this.o2(document.getElementById(this.InputID), true); }else {if (document.getElementById(this.ClientID).offsetWidth==0){window.attachEvent("onlo\x61\x64", function (){comboInstance.o2(document.getElementById(comboInstance.InputID), true); } ); }else if (!O2){ this.o2(document.getElementById(this.InputID), true); }else { this.l2(); }}}if (window.attachEvent){window.attachEvent("onun\x6c\157ad", function (){comboInstance.Dispose( false); } ); }else {window.addEventListener("unload", function (){comboInstance.Dispose( false); } , false); }} ; RadComboBox.prototype.ClearItems= function (){ this.Items=[]; document.getElementById(this.DropDownID).innerHTML=""; } ; RadComboBox.prototype.GetViewPortSize= function (){var width=0; var height=0; var i2=document.body; if (window.innerWidth){width=window.innerWidth; height=window.innerHeight; }else {if (document.compatMode && document.compatMode=="\x43SS1Compat"){i2=document.documentElement; }width=i2.clientWidth; height=i2.clientHeight; }width+=i2.scrollLeft; height+=i2.scrollTop; return {width:width-6,height:height-6 } ; } ; RadComboBox.prototype.I2= function (el){var parent=null; var o3= {x: 0,y: 0 } ; var box; if (el.getBoundingClientRect){box=el.getBoundingClientRect(); var scrollTop=document.documentElement.scrollTop || document.body.scrollTop; var scrollLeft=document.documentElement.scrollLeft || document.body.scrollLeft; o3.x=box.left+scrollLeft-2; o3.y=box.top+scrollTop-2; return o3; }else if (document.getBoxObjectFor){box=document.getBoxObjectFor(el); o3.x=box.x-2; o3.y=box.y-2; }else {o3.x=el.offsetLeft; o3.y=el.offsetTop; parent=el.offsetParent; if (parent!=el){while (parent){o3.x+=parent.offsetLeft; o3.y+=parent.offsetTop; parent=parent.offsetParent; }}}if (window.opera){parent=el.offsetParent; while (parent && parent.tagName!="\102\x4fDY" && parent.tagName!="\x48TML"){o3.x-=parent.scrollLeft; o3.y-=parent.scrollTop; parent=parent.offsetParent; }}else {parent=el.parentNode; while (parent && parent.tagName!="\x42\x4fDY" && parent.tagName!="\x48\x54ML"){o3.x-=parent.scrollLeft; o3.y-=parent.scrollTop; parent=parent.parentNode; }}return o3; } ; RadComboBox.prototype.Dispose= function (O3){try {if (O3){ this.HideDropDown(); }tlrkComboBoxes[this.ID]=null; this.Items=null; this.K=null; this.k=null; this.J=null; this.H=null; this.h=null; this.G=null; this.D=null; this.OnClientSelectedIndexChanging=null; this.OnClientDropDownOpening=null; this.OnClientDropDownClosing=null; this.OnClientItemsRequesting=null; this.OnClientSelectedIndexChanged=null; this.OnClientItemsRequested=null; this.OnClientKeyPressing=null; var comboInstance=this ; if (document.detachEvent){document.detachEvent("\157\x6eclick",this.o1); }else {document.removeEventListener("click",this.o1, false); }if (document.detachEvent){document.getElementById(this.InputID).detachEvent("\x6fnblur",this.O1); }else {document.getElementById(this.InputID).removeEventListener("blur",this.O1, false); }if (document.detachEvent){document.getElementById(this.InputID).detachEvent("\x6fnfocus",this.i1); }else {document.getElementById(this.InputID).removeEventListener("focus",this.i1, false); }if (window.removeEventListener){window.removeEventListener("\x6c\157ad", function (){comboInstance.o2(document.getElementById(comboInstance.InputID), true); } , false); }var input=document.getElementById(this.InputID); if (input!=null)input.onblur=null; input=null; var l3=document.getElementById(this.DropDownPlaceholderID); if (l3!=null){l3.onselectstart=null; }l3=null; }catch (e){} this.i0= true; } ; RadComboBox.prototype.Initialize= function (i3,Z){ this.I3(i3); this.o4(Z); this.O4(); this.l4(); } ; RadComboBox.prototype.I3= function (i3){for (var z in i3){ this[z]=i3[z]; }} ; RadComboBox.prototype.O4= function (){ this.ItemCssClass="Co\x6dboBoxItem\x5f"+this.Skin; this.ItemCssClassHover="\x43\x6fmboBo\x78\x49temH\x6f\x76er\x5f"+this.Skin; this.ItemCssClassDisabled="ComboBox\x49\x74emDi\x73\x61bled\x5f"+this.Skin; this.ImageCssClass="\x43\x6fmboBox\x49\x6dage_"+this.Skin; this.ImageCssClassHover="\x43omboBo\x78\x49mageH\x6f\x76er\x5f"+this.Skin; this.InputCssClass="\x43omboBo\x78\x49nput\x5f"+this.Skin; this.InputCssClassHover="\x43omboBoxInp\x75\x74Hov\x65\x72_"+this.Skin; this.LoadingMessageCssClass="Comb\x6f\x42oxLoad\x69\x6egMe\x73\163\x61\x67e_"+this.Skin; } ; RadComboBox.prototype.R= function (item){if (item!=null){ this.i4(item.Text); this.SetValue(item.Value); this.I4(item.Index); }else { this.SetText(""); this.SetValue(""); this.I4("-1"); }} ; RadComboBox.prototype.Q= function (){if (this.AutoPostBack){if (this.CausesValidation){if (typeof(WebForm_DoPostBackWithOptions)!="function" && !(typeof(Page_ClientValidate)!="\x66unc\x74\x69on" || Page_ClientValidate())){return; }}eval(this.PostBackReference); }} ; RadComboBox.prototype.l1= function (e){var o5=this.SelectedItem; var O5=this.HighlightedItem; if (o5!=null && O5!=null && o5!=O5){if (this.l5(this.OnClientSelectedIndexChanging,O5,e)== false){return; } this.R(O5); this.i5(); if (this.HighlightedItem==null){O5.Select(); this.l5(this.OnClientSelectedIndexChanged,O5,e); }}if (this.o0)return; var I5=this.l0; var o6=this.GetText(); if (I5!=o6 && this.AllowCustomText){ this.SetText(this.GetText()); if (!this.r){if (O5!=null || I5!=o6){if (this.l5(this.OnClientSelectedIndexChanging,O5,e)== false){return; }if (O5!=null){ this.SetText(O5.Text); this.SetValue(O5.Value); } this.Q(); }}else { this.r= false; }}} ; RadComboBox.prototype.I1= function (e){ this.l0=this.GetText(); this.O6(); };RadComboBox.prototype.l6= function (){var i6=document.getElementById(this.L); while (i6.tagName!="\x46ORM"){i6=i6.parentNode; }return i6; } ; RadComboBox.prototype.I6= function (){var l3=document.getElementById(this.DropDownPlaceholderID); var o7=l3.getElementsByTagName("input"); return o7.length>0; };RadComboBox.prototype.O7= function (){if ((!document.readyState || document.readyState=="comp\x6c\145t\x65") && (!this.C)){var parentElement=document.body; if (this.I6()){parentElement=this.l6(); }var l3=document.getElementById(this.L).getElementsByTagName("\x64\x69v")[0]; l3.parentNode.removeChild(l3); l3.style.marginLeft="\x30"; var l7=document.getElementById(this.DropDownPlaceholderID); if (l7){l7.parentNode.removeChild(l7); }if (parentElement.firstChild){parentElement.insertBefore(l3,parentElement.firstChild); }else {parentElement.appendChild(l3); } this.C= true; this.J=document.getElementById(this.DropDownPlaceholderID); }} ; RadComboBox.prototype.HideOnClick= function (){var comboInstance=this ; this.HideTimeoutID=window.setTimeout( function (){comboInstance.DoHideOnClick(); } ,5); } ; RadComboBox.prototype.DoHideOnClick= function (){if (this.HideTimeoutID){ this.HideDropDown(); }} ; RadComboBox.prototype.ClearHideTimeout= function (){ this.HideTimeoutID=0; } ; RadComboBox.prototype.i7= function (text){var lastIndex=-1; for (var i=0; i<this.AutoCompleteSeparator.length; i++){var I7=this.AutoCompleteSeparator.charAt(i); var o8=text.lastIndexOf(I7); if (o8>lastIndex){lastIndex=o8; }}return lastIndex; } ; RadComboBox.prototype.i4= function (O8){var l8=-1; var i8=this.GetText(); if (this.AutoCompleteSeparator!=null){l8=this.i7(i8); }var o6=i8.substring(0,l8+1)+O8; this.SetText(o6); } ; RadComboBox.prototype.ClearSelection= function (){ this.R(null); this.SelectedItem=null; this.I8=null; } ; RadComboBox.prototype.o4= function (Z){for (var i=0; i<Z.length; i++){var item=new RadComboItem(); item.ComboBox=this ; item.Index=this.Items.length; item.Initialize(Z[i]); this.Items[this.Items.length]=item; if (item.Selected && !this.AllowCustomText){ this.SetText(item.Text); this.SetValue(item.Value); }}} ; RadComboBox.prototype.l4= function (){if (this.SelectedItem!=null){ this.SelectedItem.Highlight(); }else {var o9; var O9=this.GetValue(); o9=this.FindItemByValue(O9); if (o9==null){var i8=this.GetText(); o9=this.FindItemByText(i8); }if (o9!=null){ this.SelectedItem=o9; this.SelectedItem.Highlight(); }} this.m= true; if (this.SelectedItem==null && this.O0==-1 && this.Items.length>0){ this.SelectedItem=this.Items[0]; this.Items[0].Selected= true; this.SelectedItem.Highlight(); }var comboInstance=this ; if (this.OpenDropDownOnLoad){if (window.attachEvent){window.attachEvent("onload", function (){comboInstance.ShowDropDown(); } ); }else {window.addEventListener("\x6c\x6fad", function (){comboInstance.ShowDropDown(); } , false); }}} ; RadComboBox.prototype.l9= function (Z,i9){if (!i9){ this.Items.length=0; } this.HighlightedItem=null; this.SelectedItem=null; this.m= false; if (this.Items.length>0){if (this.Items[0].Text==document.getElementById(this.InputID).value){ this.SetValue(this.Items[0].Value); }else { this.SetValue(""); } this.B=this.GetText(); } this.o4(Z); } ; RadComboBox.prototype.SetText= function (O8){document.getElementById(this.InputID).value=O8; this.H.value=O8; } ; RadComboBox.prototype.GetText= function (){return document.getElementById(this.InputID).value; } ; RadComboBox.prototype.SetValue= function (value){if (value || value==""){ this.h.value=value; }} ; RadComboBox.prototype.GetValue= function (){return this.h.value; } ; RadComboBox.prototype.I4= function (index){ this.G.value=index; } ; RadComboBox.prototype.I9= function (el){var parent=null; var o3=[]; var box; if (el.getBoundingClientRect){box=el.getBoundingClientRect(); var scrollTop=document.documentElement.scrollTop || document.body.scrollTop; var scrollLeft=document.documentElement.scrollLeft || document.body.scrollLeft; var x=box.left+scrollLeft-2; var y=box.top+scrollTop-2; return [x,y]; }else if (document.getBoxObjectFor){box=document.getBoxObjectFor(el); o3=[box.x-1,box.y-1]; }else {o3=[el.offsetLeft,el.offsetTop]; parent=el.offsetParent; if (parent!=el){while (parent){o3[0]+=parent.offsetLeft; o3[1]+=parent.offsetTop; parent=parent.offsetParent; }}}if (window.opera){parent=el.offsetParent; while (parent && parent.tagName.toUpperCase()!="B\x4f\x44\x59" && parent.tagName.toUpperCase()!="\x48TML"){o3[0]-=parent.scrollLeft; o3[1]-=parent.scrollTop; parent=parent.offsetParent; }}else {parent=el.parentNode; while (parent && parent.tagName.toUpperCase()!="BODY" && parent.tagName.toUpperCase()!="\x48TML"){o3[0]-=parent.scrollLeft; o3[1]-=parent.scrollTop; parent=parent.parentNode; }}return o3; } ; RadComboBox.prototype.oa= function (x,y){if (document.readyState && document.readyState!="comple\x74\x65"){return; }var Oa=(navigator.userAgent.toLowerCase().indexOf("\x73afari")!=-1); var la=window.opera; if (Oa || la || (!document.all)){return; }if (this.D==null){ this.D=document.createElement("\x49FRAME"); this.D.src="javascript:\x27\x27"; this.D.id=this.ClientID+"\x5fOverl\x61\x79"; this.D.frameBorder=0; this.D.style.position="ab\x73\x6flute"; this.D.style.display="\x6e\x6fne"; this.O7(); this.J.parentNode.insertBefore(this.D,this.J); this.D.style.zIndex=this.J.style.zIndex-1; } this.D.style.left=x; this.D.style.top=y; var ia=this.J.offsetWidth; var Ia=this.J.offsetHeight; this.D.style.width=ia+"px"; this.D.style.height=Ia+"\x70\x78"; this.D.style.display=""; } ; RadComboBox.prototype.ob= function (){var Oa=(navigator.userAgent.toLowerCase().indexOf("s\x61\146\x61\x72i")!=-1); var la=window.opera; if (Oa || la || (!document.all)){return; }if (this.D!=null){ this.D.style.display="none"; }} ; RadComboBox.prototype.Ob= function (){for (var i=0; i<tlrkComboBoxes.length; i++){if (tlrkComboBoxes[i].ClientID!=this.ClientID){tlrkComboBoxes[i].HideDropDown(); }}} ; RadComboBox.prototype.I0= function (){var el=document.getElementById(this.ClientID+"_wra\x70per"); while (el.tagName.toLowerCase()!="\x68\x74ml"){if (el.dir){ this.lb=(el.dir.toLowerCase()=="\x72tl"); return; }el=el.parentNode; } this.lb= false; };RadComboBox.prototype.ShowDropDown= function (){if (this.l5(this.OnClientDropDownOpening,this )== false){return; } this.Ob(); this.O7(); var ib; (this.RadComboBoxImagePosition=="\x52ight" && !this.lb)?ib=this.K:ib=document.getElementById(this.ImageID); var position=this.I9(ib); var x=position[0]+this.OffsetX; var y=position[1]+ib.offsetHeight+this.OffsetY; var Ib=document.getElementById(this.L); oc=Ib.offsetWidth; if (this.ExpandEffectString!=null && document.all){try { this.J.style.filter=this.ExpandEffectString; this.J.filters[0].Apply(); this.J.filters[0].Play(); }catch (e){}}if (this.lb){ this.J.dir="r\x74\154"; }var Oc=this.GetViewPortSize(); this.J.style.position="a\x62\x73olute"; if (window.netscape || window.opera){oc-=2; } this.J.style.width=oc+"\x70x"; this.J.style.display="\x62lock"; if (this.lc(Oc,this.J,ib)){var ic=y-this.J.offsetHeight-ib.offsetHeight; if (ic>0){y=ic; }} this.J.style.left=x+"\x70x"; this.J.style.top=y+"\x70\x78"; this.oa(x+"\x70x",y+"\x70x"); if (this.HighlightedItem!=null){ this.HighlightedItem.W(); }if (this.SelectedItem!=null){ this.SelectedItem.W(); } this.ClearHideTimeout(); this.DropDownVisible= true; try {document.getElementById(this.InputID).focus(); }catch (e){} ; if ((this.EnableLoadOnDemand) && (this.Items.length==0)){ this.Ic( true ,null); }if (this.SelectedItem!=null){ this.SelectedItem.Highlighted= false; this.SelectedItem.Highlight(); this.SelectedItem.T(); }} ; RadComboBox.prototype.lc= function (Oc,t,ib){var od=this.I2(ib).y+t.offsetHeight; return od>Oc.height; } ; RadComboBox.prototype.FindItemByText= function (O8){for (var i=0; i<this.Items.length; i++){if (this.Items[i].Text==O8){return this.Items[i]; }}return null; } ; RadComboBox.prototype.FindItemByValue= function (Od){for (var i=0; i<this.Items.length; i++){if (this.Items[i].Value==Od){return this.Items[i]; }}return null; } ; RadComboBox.prototype.HideDropDown= function (){if (this.DropDownVisible){if (this.l5(this.OnClientDropDownClosing,this )== false){return; }document.getElementById(this.DropDownPlaceholderID).style.display="\x6e\x6f\156\x65"; this.ob(); this.DropDownVisible= false; this.ld(); }} ; RadComboBox.prototype.ld= function (){ this.l5(this.OnClientBlur,this );};RadComboBox.prototype.O6= function (){ this.l5(this.OnClientFocus,this );};function trace(oe){document.body.appendChild(document.createTextNode(oe)); document.body.appendChild(document.createElement("H\x52")); }RadComboBox.prototype.ToggleDropDown= function (){ (this.DropDownVisible)?this.HideDropDown(): this.ShowDropDown(); } ; RadComboBox.prototype.Oe= function (le){if (le){while (le!=null){if (le.id && this.ie(le.id)){return le; }le=le.parentNode; }}return null; } ; RadComboBox.prototype.ie= function (Ie){for (var i=0; i<this.Items.length; i++){if (this.Items[i].ClientID==Ie){return true; }}return false; } ; RadComboBox.prototype.of= function (item){for (var i=0; i<this.Items.length; i++){if (this.Items[i].ClientID==item.id){return this.Items[i]; }}return null; } ; RadComboBox.prototype.Of= function (If){If.Highlight(); } ; RadComboBox.prototype.og= function (If){If.UnHighlight(); } ; RadComboBox.prototype.i5= function (eventArgs){var Og=this.HighlightedItem; if (Og!=null){if (this.l5(this.OnClientSelectedIndexChanging,Og,eventArgs)== false){return; }Og.Select(); this.l5(this.OnClientSelectedIndexChanged,Og,eventArgs); } this.HideDropDown(); } ; RadComboBox.prototype.HandleClick= function (eventArgs){ this.i5(eventArgs); } ; RadComboBox.prototype.lg= function (index){var i=index; var ig= false; while (i<this.Items.length-1){i=i+1; if (this.Items[i].Enabled){ig= true; break; }}if (ig)return i; return index; } ; RadComboBox.prototype.Ig= function (index){var i=index; var ig= false; while (i>0){i=i-1; if (this.Items[i].Enabled){ig= true; break; }}if (ig)return i; return index; } ; RadComboBox.prototype.oh= function (comboInstance,eventArgs){ this.l5(this.OnClientKeyPressing,this,eventArgs); var keyCode=eventArgs.keyCode; if (keyCode==40){if (eventArgs.altKey && (!this.DropDownVisible)){ this.ShowDropDown(); return; }var index=0; if (this.HighlightedItem!=null){index=this.lg(this.HighlightedItem.Index); }if (index>=0 && this.Items.length>0){if (this.l5(this.OnClientSelectedIndexChanging,this.Items[index],eventArgs)== false){return; } this.Items[index].Highlight(); this.Items[index].W(); this.R(this.Items[index]); this.Oh(eventArgs); }return; }if (keyCode==27 && this.DropDownVisible){ this.HideDropDown(); return; }if (keyCode==38){if (eventArgs.altKey && this.DropDownVisible){ this.HideDropDown(); return; }var index=-1; if (this.HighlightedItem!=null){index=this.Ig(this.HighlightedItem.Index); }if (index>=0){if (this.l5(this.OnClientSelectedIndexChanging,this.Items[index],eventArgs)== false){return; } this.Items[index].T(); this.Items[index].Highlight(); this.R(this.Items[index]); this.Oh(eventArgs); }return; }if ((keyCode==13 || keyCode==9) && this.DropDownVisible){if (keyCode==13){ this.Oh(eventArgs); }if (!this.SelectOnTab && keyCode==9){if (this.AutoPostBack){ this.Q(); } this.HideDropDown(); return; } this.i5(); return; }if (keyCode==9 && !this.DropDownVisible){ this.ld(); return; }if (keyCode==35 || keyCode==36 || keyCode==37 || keyCode==39){return; }if (this.EnableLoadOnDemand && (!eventArgs.altKey) && (!eventArgs.ctrlKey) && (!(keyCode==16))){if (!this.DropDownVisible){ this.ShowDropDown(); } this.lh( false ,keyCode); return; }if ((keyCode<32) || (keyCode>=33 && keyCode<=46) || (keyCode>=112 && keyCode<=123) || (eventArgs.altKey== true)){return; }var ih=this ; window.setTimeout( function (){ih.HighlightMatches();} ,20); } ; RadComboBox.prototype.Ih= function (eventArgs){if (eventArgs.preventDefault){if (eventArgs.keyCode==13 || (eventArgs.keyCode==32 && (!this.EnableLoadOnDemand))){eventArgs.preventDefault(); }}} ; RadComboBox.prototype.oi= function (s){if (typeof(encodeURIComponent)!="un\x64efi\x6e\x65d"){return encodeURIComponent(this.Oi(s)); }if (escape){return escape(this.Oi(s)); }} ; RadComboBox.prototype.Oi= function (text){if (typeof(text)!="n\x75\x6dber"){return text.replace(/\x27/g,"&squote"); }} ; RadComboBox.prototype.ii= function (){if (typeof(XMLHttpRequest)!="undefi\x6e\x65d"){return new XMLHttpRequest(); }if (typeof(ActiveXObject)!="un\x64\x65fined"){return new ActiveXObject("\x4dicrosoft.XML\x48\x54TP"); }} ; RadComboBox.prototype.Ii= function (oj,Oj,lj,ij){oj=oj.replace(/\x27/g,"\x26squote"); var url=window.unescape(this.LoadOnDemandUrl)+"&text="+this.oi(oj); url=url+"\x26comboTex\x74\x3d"+this.oi(Oj); url=url+"\x26comboVa\x6c\x75e="+this.oi(lj); url=url+"&skin="+this.oi(this.Skin); if (ij){url=url+"\x26itemCou\x6e\x74="+this.Items.length; }if (this.ExternalCallBackPage!=null){url=url+"\046\x65xternal=t\x72\x75e"; }if (this.ClientDataString!=null){url+="&cli\x65\x6etData\x53\x74ring\x3d"+this.oi(this.ClientDataString); }url=url+"\x26timeSta\x6d\x70="+encodeURIComponent((new Date()).getTime()); return url; } ; RadComboBox.prototype.Ij= function (ij,text,keyCode){if (!this.f){if (this.i0)return; this.f= true; var Oj=this.GetText(); var lj=this.GetValue(); var oj=(text)?text:Oj; var ok=this.Ii(oj,Oj,lj,ij); var Ok=this ; var xmlRequest=this.ii(); xmlRequest.onreadystatechange= function (){if (xmlRequest.readyState!=4)return; Ok.lk(xmlRequest.responseText,ij,oj,keyCode,xmlRequest.status,ok); } ; xmlRequest.open("GE\x54",ok, true); xmlRequest.send(""); }} ; RadComboBox.prototype.lk= function (ik,ij,oj,keyCode,status,url){if (status==500){alert("r.a\x2e\x64.comb\x6f\x62ox: \x53erver e\x72\x72or\x20\x69n \x74he I\x74emsRe\x71\165\x65sted \x65vent \x68\141\x6e\144l\x65r, p\x72ess\x20ok t\x6f vie\x77 the\x20res\x75lt."); document.body.innerHTML=ik; return; }if (status==404){alert("r.a\x2e\x64.combo\x62\x6fx: \x4c\157a\x64\x20On \x44\x65ma\x6e\x64 P\x61ge not\x20found:\x20"+url); var Ik="r.a.d.c\x6f\x6dbobox\x3a\x20Load\x20\117n\x20\x44ema\x6e\x64 P\x61\147e\x20\156o\x74\040\x66ound:\x20"+url+"<b\x72\x2f>"; Ik+="\x50lease,\x20\x74ry u\x73\x69ng \x45\x78ter\x6e\x61lCa\x6c\x6cBa\x63kPage \x74\157 \x6dap to\x20\164h\x65\040\x65xact \x6cocat\x69on o\x66 the\x20call\x62ackp\x61ge \x79ou a\x72e u\x73ing.";document.body.innerHTML=Ik; return; }try {eval("var c\x61\x6clBack\x44\x61ta =\x20"+ik+"\x3b"); }catch (e){alert("r.a.d.co\x6d\x62obox\x3a\x20load\x20\157n\x20\x64ema\x6e\144 \x63allbac\x6b\040\x65\x72ro\x72. Pre\x73\163 \x45nter \x66or m\x6fre i\x6eform\x61tion"); var Ik="\x49\x66 r.a.d\x2e\x63ombo\x62\x6fx i\x73\x20not\x20\151n\x69\x74ia\x6c\154y\x20\166i\x73\151b\x6c\145 \x6fn you\x72 ASP\x58\040\x70age,\x20you \x6day n\x65ed t\x6f us\x65 str\x65ame\x72s (\x74he E\x78ter\x6eal\x6cCal\x6cBac\x6bPa\x67e p\x72op\x65rty\x29"; Ik+="<br/>Ple\x61\x73e, r\x65\x61d ou\x72\040o\x6e\154i\x6e\x65 do\x63umenta\x74\151o\x6e\040\x6f\156 \x74his p\x72\157\x62\154e\x6d fo\x72 det\x61ils"; Ik+="<br/\x3e\x3ca href\x3d\x27htt\x70://www.\x74\x65ler\x69\x6b.c\x6fm/help\x2f\162a\x64\143o\x6d\142o\x62ox/v2\x255FNET\x32/?com\x62o_e\x78tern\x61\154\x63allb\x61ckpa\x67e.h\x74ml\047\076\x68ttp\x3a//w\x77w.t\x65ler\x69k.c\x6fm/\x68elp\x2fra\x64com\x62ob\x6fx/\x762\x255FN\x45T2\x2fco\x6db\x6f_e\x78te\x72na\x6cca\x6cl\x62ac\x6bp\x61ge\x2eh\x74ml\x3c/\x61>";document.body.innerHTML=Ik; return; }if (this.GetText()!=callBackData.Text){ this.f= false; this.lh( false ,null); return; }if (this.ShowMoreResultsBox){document.getElementById(this.MoreResultsBoxMessageID).innerHTML=callBackData.Message; }var ll=this.Items.length; this.l9(callBackData.Items,ij); if (ij){document.getElementById(this.DropDownID).removeChild(document.getElementById(this.ClientID+"_Lo\x61\x64ingDiv")); document.getElementById(this.DropDownID).innerHTML+=callBackData.DropDownHtml; if (this.Items[ll+1]!=null){ this.Items[ll+1].W(); }}else {document.getElementById(this.DropDownID).innerHTML=callBackData.DropDownHtml; } this.oa(this.J.style.left,this.J.style.top); this.l5(this.OnClientItemsRequested,this,oj,ij); this.f= false; var il=this.FindItemByText(this.GetText()); if (il!=null){il.Highlight(); il.W(); }if (!keyCode)return; if (keyCode<32 || (keyCode>=33 && keyCode<=46) || (keyCode>=112 && keyCode<=123) && keyCode!=8){return; } this.HighlightMatches(); };RadComboBox.prototype.Il= function (i8){var l8=-1; if (this.AutoCompleteSeparator!=null){l8=this.i7(i8); }var om=i8.substring(l8+1,i8.length); return om; } ; RadComboBox.prototype.Om= function (Im,On){if (!this.IsCaseSensitive){return (Im.toLowerCase()==On.toLowerCase()); }else {return (Im==On); }} ; RadComboBox.prototype.HighlightMatches= function (){if (!this.MarkFirstMatch)return; var i8=this.GetText(); var om=this.Il(i8); if (om.length==0){return; }for (var i=0; i<this.Items.length; i++){var In=this.Items[i].Text; if (In.length>=om.length){var oo=In.substring(0,om.length); if (this.Om(oo,om)){var l8=-1; if (this.AutoCompleteSeparator!=null){l8=this.i7(i8); }var o6=i8.substring(0,l8+1)+In; this.SetText(o6); this.SetValue(this.Items[i].Value); this.I4(this.Items[i].Index); if (this.l5(this.OnClientSelectedIndexChanging,this.Items[i],null)== false){return; } this.Items[i].Highlight(); this.Items[i].W(); var Oo=l8+om.length+1; var Io=o6.length-Oo; if (document.all){var op=document.getElementById(this.InputID).createTextRange(); op.moveStart("c\x68\x61\x72acter",Oo); op.moveEnd("character",Io); op.select(); }else {document.getElementById(this.InputID).setSelectionRange(Oo,Oo+Io); }return; }else { this.SetValue(""); this.I4(-1); if (this.HighlightedItem!=null){ this.HighlightedItem.UnHighlight(); }}}} this.SetValue(""); this.I4("\x2d\061"); if (!this.AllowCustomText){var Op=i8.substring(0,i8.length-1); if (this.B!=null){ this.SetText(this.B); return; } this.SetText(Op); this.HighlightMatches(); }} ; RadComboBox.prototype.lh= function (i9,keyCode){if (!this.f){var comboInstance=this ; if (this.d){window.clearTimeout(this.d); this.d=0; } this.d=window.setTimeout( function (){comboInstance.Ic(i9,keyCode);} ,this.ItemRequestTimeout); }} ; RadComboBox.prototype.Ic= function (i9,keyCode){var oj=document.getElementById(this.InputID).value; if (oj=="")oj= false; if (this.l5(this.OnClientItemsRequesting,this,oj,i9)== false){return; }if (!this.f){if (!document.getElementById(this.ClientID+"_L\x6fadingDiv")){document.getElementById(this.DropDownID).innerHTML="\x3cdiv id=\047"+this.ClientID+"_Loadin\x67\x44iv\047"+" class=\047"+this.LoadingMessageCssClass+" \x27\x3e"+this.LoadingMessage+"</div>"+document.getElementById(this.DropDownID).innerHTML; }}var comboInstance=this ; window.setTimeout( function (){comboInstance.Ij(i9,oj,keyCode);} ,20); } ; RadComboBox.prototype.RequestItems= function (text,i9){ this.Ij(i9,text,null); } ; RadComboBox.prototype.UnHighlightAll= function (){for (var i=0; i<this.Items.length; i++){if (this.Items[i].Highlighted){ this.Items[i].UnHighlight(); }}} ; RadComboBox.prototype.lp= function (){document.getElementById(this.InputID).className=this.InputCssClass; var ip=document.getElementById(this.ImageID); if (ip){ip.className=this.ImageCssClass; }} ; RadComboBox.prototype.Ip= function (){document.getElementById(this.InputID).className=this.InputCssClassHover; var ip=document.getElementById(this.ImageID); if (ip){ip.className=this.ImageCssClassHover; }} ; RadComboBox.prototype.oq= function (){document.getElementById(this.MoreResultsBoxImageID).style.cursor="default"; document.getElementById(this.MoreResultsBoxImageID).src=this.ScrollDownImageDisabled; this.o0= false; } ; RadComboBox.prototype.Oq= function (){document.getElementById(this.MoreResultsBoxImageID).style.cursor="hand"; document.getElementById(this.MoreResultsBoxImageID).src=this.ScrollDownImage; this.o0= true; } ; RadComboBox.prototype.lq= function (){ this.UnHighlightAll(); this.lh( true ,null); document.getElementById(this.InputID).focus(); } ; RadComboBox.prototype.iq= function (eventArgs){if (eventArgs.stopPropagation){eventArgs.stopPropagation(); }else {eventArgs.cancelBubble= true; }} ; RadComboBox.prototype.Oh= function (eventArgs){if (eventArgs.preventDefault){eventArgs.preventDefault(); }else {eventArgs.returnValue= false; }} ; RadComboBox.prototype.l5= function (Iq,a,b,or){if (!Iq)return true; RadComboBoxGlobalFirstParam=a; RadComboBoxGlobalSecondParam=b; RadComboBoxGlobalThirdParam=or; var s=Iq; s=s+"(RadCom\x62oBoxG\x6c\x6fbalF\x69rstPar\x61\155"; s=s+"\x2cRadComboB\x6f\x78Glob\x61\x6cSe\x63\x6fndP\x61\x72am"; s=s+"\x2cRadComb\x6f\x42oxGlo\x62\x61lT\x68\x69rdPa\x72\141m"; s=s+");"; return eval(s); } ; RadComboBox.prototype.HandleEvent= function (eventName,eventArgs){var If; var srcElement=(document.all)?eventArgs.srcElement:eventArgs.target; var item=this.Oe(srcElement); if (item!=null){If=this.of(item); }if (!this.Enabled){return; }switch (eventName){case "showdropdo\x77\x6e": this.iq(eventArgs); this.ShowDropDown(); break; case "\x68idedropd\x6f\x77n": this.iq(eventArgs); this.HideDropDown(); break; case "tog\x67\x6cedropd\x6f\x77n": this.iq(eventArgs); this.ToggleDropDown(); break; case "\x6d\157u\x73\x65over":if (If!=null)this.Of(If); break; case "mouseou\x74":if (If!=null)this.og(If); break; case "keypr\x65\x73s": this.oh(this,eventArgs); break; case "keydown": this.Ih(eventArgs); break; case "click": this.HandleClick(eventArgs); break; case "inputclick":{ this.iq(eventArgs); document.getElementById(this.InputID).select(); if (this.ShowDropDownOnTextboxClick)this.ShowDropDown(); break; }case "\x69nputimageou\x74": this.lp(); break; case "in\x70\x75timageh\x6f\x76er": this.Ip(); break; case "mo\x72\x65results\x69\x6dage\x63lick": this.iq(eventArgs); this.lq(); break; case "\155o\x72\x65result\x73\x69mage\x68\157v\x65\x72": this.Oq(); break; case "more\x72\x65sultsi\x6d\x61geo\x75\164": this.oq(); break; }} ; RadComboBox.prototype.Enable= function (){document.getElementById(this.InputID).disabled= false; this.Enabled= true; } ; RadComboBox.prototype.Disable= function (){document.getElementById(this.InputID).disabled="\x64isabled"; this.Enabled= false; this.H.value=this.GetText(); } ; RadComboBox.prototype.o2= function (Or,lr){{if ((this.g.value!="") && (this.F.value!="")){ this.l2(); return; }var ip=Or.parentNode.getElementsByTagName("\151\x6d\x67")[0]; if (lr && ip && (ip.offsetWidth==0)){var comboInstance=this ; if (document.attachEvent){if (document.readyState=="complete")window.setTimeout( function (){comboInstance.o2(Or, false); } ,100); else ip.attachEvent("\x6fnload", function (){comboInstance.o2(Or, false); } ); }else {ip.addEventListener("\x6coad", function (){comboInstance.o2(Or, false); } , false); }return; }var computedStyle=null; if (Or.currentStyle){computedStyle=Or.currentStyle; }else if (document.defaultView && document.defaultView.getComputedStyle){computedStyle=document.defaultView.getComputedStyle(Or,null); }if (computedStyle==null){ this.l2(); return; }var height=parseInt(computedStyle.height); var width=parseInt(Or.offsetWidth); var paddingTop=parseInt(computedStyle.paddingTop); var paddingBottom=parseInt(computedStyle.paddingBottom); var paddingLeft=parseInt(computedStyle.paddingLeft); var paddingRight=parseInt(computedStyle.paddingRight); var borderTop=parseInt(computedStyle.borderTopWidth); if (isNaN(borderTop)){borderTop=0; }var borderBottom=parseInt(computedStyle.borderBottomWidth); if (isNaN(borderBottom)){borderBottom=0; }var borderLeft=parseInt(computedStyle.borderLeftWidth); if (isNaN(borderLeft)){borderLeft=0; }var borderRight=parseInt(computedStyle.borderRightWidth); if (isNaN(borderRight)){borderRight=0; }if (document.compatMode && document.compatMode=="CSS1Comp\x61t"){if (!isNaN(height) && (this.F.value=="")){Or.style.height=height-paddingTop-paddingBottom-borderTop-borderBottom+"\x70x"; this.F.value=Or.style.height; }}if (!isNaN(width) && width && (this.g.value=="")){var ir=0; if (ip){ir=ip.offsetWidth; }if (document.compatMode && document.compatMode=="CSS1Com\x70\x61t"){var Ir=width-ir-paddingLeft-paddingRight-borderLeft-borderRight; if (Ir>=0)Or.style.width=Ir+"\x70\170"; this.g.value=Or.style.width; }else {Or.style.width=width-ir; }} this.l2(); }} ; RadComboBox.prototype.l2= function (){if (!this.ShowWhileLoading)document.getElementById(this.ClientID+"\x5fwrapper").style.visibility="visible"; } ; function rcbDispatcher(N,eventName,eventArgs){var comboInstance=null; try {comboInstance=window[N]; }catch (e){}if (typeof(comboInstance)=="unde\x66\x69ned" || comboInstance==null){return; }if (typeof(comboInstance.HandleEvent)!="\x75ndef\x69\x6eed"){comboInstance.HandleEvent(eventName,eventArgs); }} ; function rcbAppendStyleSheet(o,I){var os=(navigator.appName=="Microsof\x74\x20Inte\x72\x6eet E\x78plore\x72") && ((navigator.userAgent.toLowerCase().indexOf("mac")!=-1) || (navigator.appVersion.toLowerCase().indexOf("\155\x61\143")!=-1)); var Oa=(navigator.userAgent.toLowerCase().indexOf("s\x61\x66ari")!=-1); if (os || Oa){document.write("<"+"\x6cink"+"\x20rel=\x27\x73tyle\x73\x68eet\x27 type\x3d\047\x74ext/cs\x73\047\x20hre\x66=\047"+I+"\x27>"); }else {var A=document.createElement("LINK"); A.rel="s\x74\x79lesheet"; A.type="\x74ext/c\x73\x73"; A.href=I; document.getElementById(o+"\x53\x74yleSh\x65\x65tHol\x64\x65r").appendChild(A); }} ;
if (typeof(Sys) != "undefined"){if (Sys.Application != null && Sys.Application.notifyScriptLoaded != null){Sys.Application.notifyScriptLoaded();}}
