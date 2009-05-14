package tipBubble
{
	import flash.events.Event;

	public class TipBubbleEvent extends Event
	{
		public static const ROLL_OVER:String = "tipBubbleEvent";
		
		public var targetTipBubble:TipBubble;
		public var eventType:String;
		public var doesBubble:Boolean;
		
		public function TipBubbleEvent(targetBubble:TipBubble, type:String = TipBubbleEvent.ROLL_OVER, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			this.targetTipBubble = targetBubble;
			this.doesBubble = bubbles;
			super(type, bubbles, cancelable);
		}
		
		// Override the inherited clone() method. 
        override public function clone():Event {
            return new TipBubbleEvent(this.targetTipBubble, this.eventType, this.doesBubble);
        }
	}
}