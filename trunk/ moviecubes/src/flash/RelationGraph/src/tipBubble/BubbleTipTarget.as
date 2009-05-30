package tipBubble
{
	import flash.display.DisplayObject;
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.geom.Point;
	import flash.geom.Rectangle;
	
	import mx.core.UIComponent;
	import mx.managers.PopUpManager;
	
	public class BubbleTipTarget
	{
		public var tipTarget:UIComponent;
		public var htmlTipText:String;
		public var bubbleTip:TipBubble;
		public var tipComponent:UIComponent;
		
		[Bindable]
		public var tipIsOpen:Boolean = false;
		
		private var onOpen:Function;
		private var onClose:Function;
		
		public function BubbleTipTarget(target:UIComponent,component:UIComponent,onOpen:Function,onClose:Function)
		{
			this.tipComponent = component;
			this.tipTarget = target;
			this.htmlTipText = "";
			this.tipTarget.addEventListener(MouseEvent.ROLL_OVER, openTip);
			this.tipTarget.addEventListener(MouseEvent.ROLL_OUT, closeTip);
			//this.tipTarget.addEventListener(MouseEvent.MOUSE_OVER, openTip);
			//this.tipTarget.addEventListener(MouseEvent.MOUSE_OUT, closeTip);
			
			this.onOpen = onOpen;
			this.onClose = onClose;
			
		}
		
		/*public function BubbleTipTarget(target:UIComponent, htmlText:String, component:UIComponent = null, clickNotification:String = "")
		{
			this.tipComponent = component;
			this.tipTarget = target;
			this.htmlTipText = htmlText;
			if(this.tipComponent != null){
				this.tipTarget.addEventListener(MouseEvent.CLICK, compOpen);
				if(clickNotification != ""){
					this.tipTarget.toolTip = clickNotification;
				}else{
					this.tipTarget.toolTip = "Click for more.";
				}
			}else{
				this.tipTarget.addEventListener(MouseEvent.ROLL_OVER, openTip);
				this.tipTarget.addEventListener(MouseEvent.ROLL_OUT, closeTip);
			}
		}*/
		
		public function compOpen(event:MouseEvent = null):void
		{
			openTip();
		}
		
		/*public function openTip(event:MouseEvent = null):void
		{
			if(!this.tipIsOpen){
				this.tipIsOpen = true;
				this.bubbleTip = TipBubble(PopUpManager.createPopUp(this.tipTarget as DisplayObject, TipBubble, false));
				if(this.tipComponent != null){
					this.bubbleTip.contentArea.addChild(this.tipComponent);
					doClose = false;
					this.bubbleTip.addEventListener("closeBubble", compClose);
					this.bubbleTip.closeBtn.visible = true;
					this.bubbleTip.closeBtn.width = 60;
				}
				this.bubbleTip.textField.htmlText = this.htmlTipText;
				this.tipTarget.addEventListener(Event.ENTER_FRAME, moveTip);
			}
		}*/
		
		public function openTip(event:MouseEvent = null):void
		{
			if(!this.tipIsOpen){
				this.tipIsOpen = true;
				this.bubbleTip = TipBubble(PopUpManager.createPopUp(this.tipTarget as DisplayObject, TipBubble, false));
				this.bubbleTip.textField.visible = false;
				this.bubbleTip.contentArea.addChild(this.tipComponent);
				this.tipTarget.addEventListener(Event.ENTER_FRAME, moveTip);

				//回调打开				
				if (this.onOpen!=null)
					this.onOpen();
			}
		}
		public var doClose:Boolean = true;
		
		/*public function compClose(event:Event = null):void
		{
			this.bubbleTip.removeEventListener("closeBubble", compClose);
			this.tipIsOpen = false;
			this.tipTarget.removeEventListener(Event.ENTER_FRAME, moveTip);
			PopUpManager.removePopUp(this.bubbleTip);
			this.initPosition = true;
		}*/
		
		public function closeTip(event:MouseEvent = null):void
		{
			var mX:Number = this.tipTarget.mouseX;
			var mY:Number = this.tipTarget.mouseY;
			var mousePoint:Point = new Point(mX, mY);
			
			var targetBounds:Rectangle = this.tipTarget.getBounds(this.tipTarget);
			if(!(targetBounds.containsPoint(mousePoint)) && this.doClose){
				this.tipIsOpen = false;
				this.tipTarget.removeEventListener(Event.ENTER_FRAME, moveTip);
				PopUpManager.removePopUp(this.bubbleTip);
				this.initPosition = true;
			}
			
			if (this.onClose!=null)
				this.onClose();
		}
		
		/*public function clearExtra():void
		{
			//if(this.tipComponent == null)
			{
				this.bubbleTip.removeEventListener("closeBubble", compClose);
				this.tipIsOpen = false;
				this.tipTarget.removeEventListener(Event.ENTER_FRAME, moveTip);
				PopUpManager.removePopUp(this.bubbleTip);
				this.initPosition = true;
				
			}
		}*/
		
		public var initPosition:Boolean = true;
		
		public function moveTip(event:Event = null):void
		{
			var mX:Number = this.tipTarget.mouseX;
			var mY:Number = this.tipTarget.mouseY;
			var mousePoint:Point = new Point(mX, mY);
			var targetBounds:Rectangle = this.tipTarget.getBounds(this.tipTarget);
			if(!(targetBounds.containsPoint(mousePoint))){
				this.closeTip(event as MouseEvent);
			}else{
				this.openTip();
			}
			
			var mainApp:DisplayObject = this.tipTarget.parentApplication as DisplayObject;
			var mX2:Number = mainApp.mouseX;
			var mY2:Number = mainApp.mouseY;
			if(this.initPosition){
				this.initPosition = false;
				this.bubbleTip.x = mX2;
				this.bubbleTip.y = mY2;
			}
			//修改
			if(true){
				this.bubbleTip.x = mX2;
				this.bubbleTip.y = mY2;
			}
		}
		
		public function clearTip():void
		{
			this.tipTarget.removeEventListener(MouseEvent.ROLL_OVER, openTip);
			this.tipTarget.removeEventListener(MouseEvent.ROLL_OUT, closeTip);
			//this.tipTarget.removeEventListener(MouseEvent.MOUSE_OVER, openTip);
			//this.tipTarget.removeEventListener(MouseEvent.MOUSE_OUT, closeTip);
			if(this.tipIsOpen){
				this.tipTarget.removeEventListener(Event.ENTER_FRAME, moveTip);
				PopUpManager.removePopUp(this.bubbleTip);
				this.tipIsOpen = false;
				
				if (this.onClose!=null)
					this.onClose();
			}
		}
	}
}