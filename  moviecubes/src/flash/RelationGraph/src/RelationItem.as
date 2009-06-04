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
	import com.adobe.flex.extras.controls.springgraph.Graph;
	import com.adobe.flex.extras.controls.springgraph.Item;	
	
	public class RelationItem extends Item
	{
		[Bindable]
		public var name: String;
		
		[Bindable]				
		public var isStar:Boolean;
		
		public var rank:int;
		
		
		
		[Bindable]
		public var start:int;
		
		[Bindable]
		public var nextStart:int = 0;
		
		[Bindable]
		public var color:uint;
		
		public var onSelected:Boolean = false;
		
		public var view:RelationItemView = null;
		
		//是否可以移动
		override public function okToMove():Boolean
		{
			/*if (this.rank == 0)
				return false;
			else*/
				return !onSelected;
		}
		
		public function RelationItem(data:Object,rank:int = 0) {
			super(data.ID);
			
			this.name = data.Name;
			this.rank = rank;
			this.isStar = data.ObjectType == "STAR";
			this.data = data;
			this.start = 0;
			updateColor();
		}
		
		//更新颜色
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
		
		//更新电影
		private function updateMovies(newData:Object):Boolean
		{
			var result:Boolean = false;
			for(var i:int = 0;i<newData.Movies.length;++i)
			{
				if(!hasMovie(newData.Movies[i].Movie.ID,newData.Movies[i].Role))
				{
					(this.data.Movies as Array).push(newData.Movies[i]);
					result = true;
				}
			}
			return result;
		}
		
		//更新明星
		private function updateStars(newData:Object):Boolean
		{
			var result:Boolean = false;
			for(var i:int = 0;i<newData.Stars.length;++i)
			{
				if(!hasStar(newData.Stars[i].Star.ID,newData.Stars[i].Role))
				{
					(this.data.Stars as Array).push(newData.Stars[i]);
					result = true;
				}
			}
			return result;
		}
		
		//更新别名
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
		
		//是否有某个角色的电影
		private function hasMovie(id:String,role:String):Boolean
		{
			for(var i:int = 0;i<data.Movies.length;++i)
			{
				if (this.data.Movies[i].Movie.ID == id
				&& this.data.Movies[i].Role == role)
					return true;
			}
			return false;
		}
		
		//是否有别名
		private function hasAlias(role:String):Boolean
		{
			for(var i:int = 0;i<this.data.Alias.length;++i)
			{
				if (this.data.Alias[i] == role)
					return true;
			}
			return false;
		}
		
		//是否有某个角色的明星
		private function hasStar(id:String,role:String):Boolean
		{
			for(var i:int = 0;i<data.Stars.length;++i)
			{
				if (this.data.Stars[i].Star.ID == id
				&& this.data.Stars[i].Role == role)
					return true;
			}
			return false;
		}
		
		//更新数据
		public function updateData(data:Object,rank:int,start:int):void
		{
			if (data.ObjectType == "STAR")
			{
				updateMovies(data);
				if (this.data.TotalMovieNum < data.TotalMovieNum)
					this.data.TotalMovieNum = data.TotalMovieNum;
			}
			else
			{
				updateStars(data);
				if (this.data.TotalStarNum < data.TotalStarNum)
					this.data.TotalStarNum = data.TotalStarNum;
					
				if (data.Language!=null && data.Language !="")
					this.data.Language = data.Language;
					
				if (data.Type !=null && data.Type.length>0)
					this.data.Type = data.Type;
				
				if (data.Area!=null && data.Area !="")
					this.data.Area = data.Area;
					
				if (data.Time !=null && data.Time !="")
					this.data.Time = data.Time;
			}
			updateAlias(data);
			
			//更新排名，这个排名不是rank，是关联度
			if (this.rank > rank)
			{
				this.rank = rank;
				updateColor();
			}
			
			if (this.start!=start || this.nextStart!= start)
			{
				this.start = start;
				this.nextStart = start;
			}
			
							
			if (this.view!=null)
				view.onDataUpdate();
		}

		/*public function getRelatedIdsFromGraph(g:Graph):Array
		{
			var result:Array = new Array();
			for(var id:String in g.neighbors(this.id))
			{
				result.push(id);
			}
			return result;
		}*/
		
		//获得关联项的ID
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
		
		/*public function canBeRemoved(g:Graph,fromItem:RelationItem,removedArray:Array):Boolean
		{
			if(this.rank == 0)
				return false;
			
			if (g.numLinks(this) == 1)
			{
				removedArray.push(this);
				return true;
			}
			
			var relatedIds:Object = g.neighbors(this.id);
			//this.getRelatedIdsFromGraph(g);
			//this.getRelatedIds();
			
			var result: Boolean = false;
			for(var id:String in relatedIds)
			{
				if (id == fromItem.id)
					continue;
				
				var relatedItem:RelationItem = relatedIds[id] as RelationItem;
				if(relatedItem!=null && !relatedItem.canBeRemoved(g,this,removedArray))
					return false;
					
			}
			removedArray.push(this);
			return result;
		}*/
		
		public function get TotalNum():int
		{
			if (this.isStar)
				return data.TotalMovieNum;
			else
				return data.TotalStarNum;
		}
		
		public function getPagesCount(countPerPage:int):int
		{
			var total:int = this.TotalNum;
			return (total%countPerPage != 0)? total/countPerPage + 1:  total/countPerPage;
		}
		
		public function getCurrentPage(countPerPage:int):int
		{
			return this.start/countPerPage + 1;
		}
		
		public function getCurrentPageCount(countPerPage:int):int
		{
			var total:int = this.TotalNum;
			return (start + countPerPage < total)? countPerPage: total - start;
		}
		
		public function get DataLength():int
		{
			if (this.isStar)
				return data.Movies.length;
			else
				return data.Stars.length;
				
		}
		
		public function getData(i:int):Object
		{
			if (this.isStar)
				return data.Movies[i].Movie;
			else
				return data.Stars[i].Star;
		}
		
		public function getRole(i:int):String
		{
			if (this.isStar)
				return data.Movies[i].Role;
			else
				return data.Stars[i].Role;
		}
		
		public function getArray():Array
		{
			if (this.isStar)
				return data.Movies;
			else
				return data.Stars;
		}
		
		
	}
}