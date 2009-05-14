package tipBubble
{
	import mx.core.UIComponent;
	import flash.events.Event;
	import flash.display.DisplayObject;
	import mx.controls.Alert;
	
	public class BubbleTipManager
	{
		public var tipTargets:Array;
		
		public function BubbleTipManager()
		{
			tipTargets = new Array();
		}
		
		public function createBubbleTip(target:UIComponent, htmlTipText:String, component:UIComponent = null, clickNotification:String = ""):void
		{
			var tipTarg:BubbleTipTarget = new BubbleTipTarget(target, htmlTipText, component, clickNotification);
			var mainApp:DisplayObject = target.parentApplication as DisplayObject;
			mainApp.addEventListener(TipBubbleEvent.ROLL_OVER, clearExtraBubbles);
			tipTargets.push(tipTarg);
		}
		
		public function clearExtraBubbles(event:TipBubbleEvent):void
		{
			for(var i:int = 0; i <= this.tipTargets.length-1; i++){
				var tempTip:BubbleTipTarget = this.tipTargets[i] as BubbleTipTarget;
				if(event.targetTipBubble != tempTip.bubbleTip && tempTip.tipIsOpen){
					tempTip.clearExtra();
				}
			}
		}
		
		public function removeBubbleTip(target:UIComponent):void
		{
			for(var i:int = 0; i <= this.tipTargets.length-1; i++){
				var currentTipTarg:BubbleTipTarget = this.tipTargets[i] as BubbleTipTarget;
				if(target == currentTipTarg.tipTarget){
					currentTipTarg.clearTip();
				}
			}
		}
	}
}