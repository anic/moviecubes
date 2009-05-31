////////////////////////////////////////////////////////////////////////////////
//
//  Copyright (C) 2006 Adobe Macromedia Software LLC and its licensors.
//  All Rights Reserved. The following is Source Code and is subject to all
//  restrictions on such code as contained in the End User License Agreement
//  accompanying this product.
//
////////////////////////////////////////////////////////////////////////////////

package
{
	import com.adobe.flex.extras.controls.springgraph.Item;

	/**
	 * Represents a single Amazon item. When created, it uses the amazon web service
	 * to find out its title, icon, and similar products.
	 * 
	 * @author Mark Shepherd
	 */
	public class RelationItem extends Item
	{
		[Bindable]
		public var name: String;
		
		[Bindable]				
		public var isStar:Boolean;
		
		public var rank:int;
		
		private var onUpdate:Function = null;
		
		[Bindable]
		public var start:int;
		
		[Bindable]
		public var nextStart:int = 0;
		
		[Bindable]
		public var color:uint;
		
		public function RelationItem(data:Object,rank:int = 0) {
			super(data.ID);
			
			this.name = data.Name;
			this.rank = rank;
			this.isStar = data.ObjectType == "STAR";
			this.data = data;
			this.start = 0;
			updateColor();
		}
		
		private function updateColor():void
		{
			if (this.rank == 0)
			{
				this.color = 0xFEC107;
			}
			else
			{
				if (this.isStar)
				{
					this.color = 0xff0000;
				}
				else
				{
					this.color = 0x0000ff;
				}
			}
		}
		
		private function updateMovies(newData:Object):Boolean
		{
			var result:Boolean = false;
			for(var i:int = 0;i<newData.Movies.length;++i)
			{
				if(!hasMovie(newData.Movies[i].Movie.ID))
				{
					(this.data.Movies as Array).push(newData.Movies[i]);
					result = true;
				}
			}
			return result;
		}
		
		private function updateStars(newData:Object):Boolean
		{
			var result:Boolean = false;
			for(var i:int = 0;i<newData.Stars.length;++i)
			{
				if(!hasStar(newData.Stars[i].Star.ID))
				{
					(this.data.Stars as Array).push(newData.Stars[i]);
					result = true;
				}
			}
			return result;
		}
		
		private function updateAlias(newData:Object):Boolean
		{
			var result:Boolean = false;
			for(var i:int = 0;i<newData.Alias.length;++i)
			{
				if(!hasAlias(newData.Alias[i]))
				{
					(this.data.Alias as Array).push(newData.Alias[i]);
					result = true;
				}
			}
			return result;
		}
		
		
		private function hasMovie(id:String):Boolean
		{
			for(var i:int = 0;i<data.Movies.length;++i)
			{
				if (this.data.Movies[i].Movie.ID == id)
					return true;
			}
			return false;
		}
		
		private function hasAlias(role:String):Boolean
		{
			for(var i:int = 0;i<this.data.Alias.length;++i)
			{
				if (this.data.Alias[i] == role)
					return true;
			}
			return false;
		}
		
		
		private function hasStar(id:String):Boolean
		{
			for(var i:int = 0;i<data.Stars.length;++i)
			{
				if (this.data.Stars[i].Star.ID == id)
					return true;
			}
			return false;
		}
		
		public function updateData(data:Object,rank:int,start:int):void
		{
			var updated:Boolean = false;
			if (data.ObjectType == "STAR")
			{
				updated = updateMovies(data);
				
				if (this.data.TotalMovieNum < data.TotalMovieNum)
				{	
					this.data.TotalMovieNum = data.TotalMovieNum;
					updated = true;
				}
				
			}
			else
			{
				updated = updateStars(data);
				
				if (this.data.TotalStarNum < data.TotalStarNum)
				{
					this.data.TotalStarNum = data.TotalStarNum;
					updated = true;
				}
			}
			
			if (updateAlias(data))
				updated = true;
			
			
			if (this.rank > rank)
			{
				this.rank = rank;
				updateColor();
				updated = true;
			}
			
			if (this.start!=start || this.nextStart!= start)
			{
				this.start = start;
				this.nextStart = start;
				updated = true;
			}
			
							
			if (updated && this.onUpdate!=null )
				this.onUpdate();
		}
		
		public function setOnUpdate(func:Function):void
		{
			this.onUpdate = func;	
		}
		
		public function getRelatedIds():Array
		{
			var result:Array = new Array();
			var i:int = 0;
			if (this.isStar)
			{
				for(i=0;i<data.Movies.length;++i)
				{
					result.push(data.Movies[i].Movie.ID);
				}			
			}
			else
			{
				for(i=0;i<data.Stars.length;++i)
				{
					result.push(data.Stars[i].Star.ID);
				}
			}
			return result;
		}
	}
}