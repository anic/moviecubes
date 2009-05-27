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
		
		[Bindable]
		public var rank:int;
		
		private var onUpdate:Function = null;
		
		public function RelationItem(data:Object,rank:int = 0) {
			super(data.ID);
			
			this.name = data.Name;
			this.rank = rank;
			this.isStar = data.ObjectType == "STAR";
			this.data = data;
		}
		
		public function updateData(data:Object):void
		{
			var updated:Boolean = false;
			if (data.ObjectType == "STAR")
			{
				if (data.Movies.length > this.data.Movies.length)
				{
					updated = true;
					this.data.Movies = data.Movies;
				}
				
				if (data.Alias.length > this.data.Alias.length)
				{
					updated = true;
					this.data.Alias = data.Alias;
				}	
			}
			else
			{
				if (data.Stars.length > this.data.Stars.length)
				{
					updated = true;
					this.data.Stars = data.Stars;
				}
				
				if (data.Alias.length > this.data.Alias.length)
				{
					updated = true;
					this.data.Alias = data.Alias;
				}	
			}
			
			if (updated && this.onUpdate!=null )
				this.onUpdate();
		}
		
		public function setOnUpdate(func:Function):void
		{
			this.onUpdate = func;	
		}
	}
}