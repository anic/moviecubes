package tipBubble
{
	import flash.display.BitmapData;
	import mx.core.Container;
	import mx.events.ResizeEvent;
	import flash.geom.Matrix;
	
	
	
	public class EmbededBGImage
	{
		public var img:BitmapData;
		public var container:Container;
		public var type:String;
		
		public function EmbededBGImage(newContainer:Container = null, imageClass:Class = null, newType:String = "tile")
		{
			container = newContainer;
			type = newType;
			if(imageClass == null){
				container.graphics.clear();
				return;
			}
			img = new imageClass().bitmapData;
			container.addEventListener(ResizeEvent.RESIZE, fillIt);
			fillIt();
		}
		
		public function fillIt(ev:* = null):void
		{
			var sizeBy:* = img;
			if(type == "center"){
				sizeBy = container;
			}
			container.graphics.clear();
			var doTile:Boolean = true;
			if(type == "tile"){
				doTile = true;
			}else{
				doTile = false;
			}
			var sx:Number = 1;
			var sy:Number = 1;
			var tx:Number = 0;
			var ty:Number = 0;
			if(type == "center"){
				tx = (container.width/2)-(img.width/2);
				ty = (container.height/2)-(img.height/2);
				if(tx < 0){
					tx = 0;
				}
				if(ty < 0){
					ty = 0;
				}
			}
			if(type == "stretch"){
				sx = container.width/img.width;
				sy = container.height/img.height;
			}
			var mtx:Matrix = new Matrix(sx, 0, 0, sy, tx, ty);
			var szX:Number = sizeBy.width;
			var szY:Number = sizeBy.height;
			if(type == "center"){
				if(img.width > container.width){
					szX = container.width;
				}
				if(img.height > container.height){
					szY = container.height;
				}
			}
			//var bmd:BitmapData = new BitmapData(szX, szY, true,null);
			var bmd:BitmapData = new BitmapData(szX, szY, true,0);
			bmd.draw(img);
			container.graphics.beginBitmapFill(bmd, mtx, doTile, true);
			if(type == "center"){
				var rzX:Number = img.width;
				var rzY:Number = img.height;
				if(img.width > container.width){
					rzX = container.width;
				}
				if(img.height > container.height){
					rzY = container.height;
				}
				container.graphics.drawRect(tx, ty, rzX, rzY);
			}else{
				container.graphics.drawRect(0, 0, container.width, container.height);
			}
			container.graphics.endFill();
		}
	}
}