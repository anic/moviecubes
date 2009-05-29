 package
{
	import com.adobe.flex.extras.controls.springgraph.*;
	
	import flash.display.Graphics;
	
	import mx.core.UIComponent;
	
	public class EdgeRender implements IEdgeRenderer
	{ 
		private static var Role_Director:String = "导演";
		private static var Role_Actor:String = "主演";
		private static var Role_Writer:String = "编剧";
		private static var Role_Unkown:String = "未知";
		
		

		public function draw(g:Graphics, 
		fromView:UIComponent, 
		toView:UIComponent, 
		fromX:int,fromY:int, 
		toX:int, toY:int, 
		graph:Graph):Boolean
		{
			var roles:Array = getRoles(fromView,toView);
			if (roles.length == 0)
			{
				g.lineStyle(1,getColor(Role_Unkown));
				g.moveTo(fromX,fromY);
				g.lineTo(toX,toY);
			}
			else
			{
				var dx:int = (toX - fromX)/roles.length;
				var dy:int = (toY - fromY)/roles.length;
				g.moveTo(fromX,fromY);
				for(var i:int = 0;i<roles.length;++i)
				{
					g.lineStyle(1,getColor(roles[i]));
					g.lineTo(fromX+(i+1)*dx,fromY+(i+1)*dy);
				}
			}
			return true;
		}
		
		private function getColor(type:String):uint
		{
			switch(type)
			{
				case Role_Director: return 0xff7f00;
				case Role_Writer: return 0xff007f;
				case Role_Actor: 
				default: 
					return 0xffffff;
			}
		}
		
		private function getRoles(fromView:UIComponent,toView:UIComponent):Array
		{
			var movie:Object = getObject(fromView,toView,"MOVIE");
			var star:Object = getObject(fromView,toView,"STAR");
			var roles:Array = new Array();
			if (star!=null && movie!=null)
			{
				for(var i:int = 0;i<movie.Stars.length;++i)
				{
					if (movie.Stars[i].Star.Name == star.Name)
						roles.push(movie.Stars[i].Role);
				}
			}
			return roles
		}
		
		
		
		private function getObject(fromView:UIComponent,toView:UIComponent,type:String):Object
		{
			if ((fromView as RelationItemView).data.data.ObjectType == type)
				return (fromView as RelationItemView).data.data;
			else
				return (toView as RelationItemView).data.data;
		}
		
		
	}
}