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
		public var imageUrl: String;
				
		[Bindable]				
		public var isStar:Boolean;
		
		public function RelationItem(data:Object) {
			super(data.ID);
			
			this.name = data.Name;
			this.isStar = data.ObjectType == "STAR";
			this.data = data;
	   
		}
		
	}
}