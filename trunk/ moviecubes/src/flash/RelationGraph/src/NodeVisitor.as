package
{
	import com.adobe.flex.extras.controls.springgraph.Graph;
	
	public class NodeVisitor
	{
		private var items:Graph;
		
		private var removedItems:Object;
		private var visitedTag:Object;
		private var rootNode:RelationItem;
		
		private var NOT_VISITED:int = 0;
		private var VISITING:int = 1;
		private var VISITED:int = 2;
		
		public function NodeVisitor(items:Graph)
		{
			this.items = items;
		}
		
		public function visit(startNode:RelationItem):Object
		{
			removedItems = new Object();
			visitedTag = new Object();
			this.rootNode = startNode;
			var neighbors:Object = items.neighbors(startNode.id);
			for(var id:String in neighbors)
			{
				if (this.getVisitedTag(id) == NOT_VISITED)
				{
					var item:RelationItem = items.find(id) as RelationItem;
					if (item != null && item.rank !=0)
						this.visitNext(startNode,item);
				}
			}
			return removedItems;
		}
		
		private function getVisitedTag(id:String):int
		{
			try
			{
				return visitedTag[id];
			}
			catch(e:Error)
			{
				
			}
			return NOT_VISITED;
		}
		
		private function setVisitedTag(id:String,value:int):void
		{
			visitedTag[id] = value;
		}
		
		private function visitNext(startNode:RelationItem,targetNode:RelationItem):Boolean
		{
			//没有邻居，只有一个点
			
			
			var neighbors:Object = items.neighbors(targetNode.id);
			var result:Boolean = true;
			setVisitedTag(targetNode.id,VISITING);
			for(var id:String in neighbors)
			{
				if (id == startNode.id)
					continue;
				
				var tag:int = getVisitedTag(id);
				if (tag == NOT_VISITED)
				{ 
					var item:RelationItem = items.find(id) as RelationItem;
					if (item!=null)
					{ 
						if(item.rank == 0) 
						{
							if (item != rootNode)
								result = false;
						}
						else
						{
							if (visitNext(targetNode,item) == false)
								result = false;
						}
					}
				}
				else if (tag == VISITING)
				{
					result = false;
				}
				//else if (tag == VISITED)
			}
			setVisitedTag(targetNode.id,VISITED);
			if (result)
				removedItems[targetNode.id] = targetNode; 
			return result;
		}

	}
}