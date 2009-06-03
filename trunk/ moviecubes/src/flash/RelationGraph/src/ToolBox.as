package
{
	import mx.core.UIComponent;

	public class ToolBox extends UIComponent
	{
		private var _color:int = 0x444444;
		
		public function ToolBox()
		{
			super();
			
		}
		
		override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void {
			// We could simply do graphics.beginFill(_color) here, to get a solid color fill.
			// But instead, we'll use a gradient fill to get a simulated 3D effect.
			// TODO: tweak the gradient fill settings to get a better looking gradient
			graphics.clear();
			graphics.beginFill(_color);
			graphics.drawRoundRect(0,0,this.width,this.height,5,5);
			graphics.endFill();
			
			graphics.beginFill(0x000000,0.5);
			graphics.drawRoundRect(5,5,this.width,this.height,5,5);
			graphics.endFill();
		}
		
	}
}