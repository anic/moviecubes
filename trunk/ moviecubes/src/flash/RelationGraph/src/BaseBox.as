package
{
	import mx.containers.HBox;
	import mx.controls.Text;
	import mx.core.Application;
	import mx.modules.Module;
	public class BaseBox extends Module
	{
		public function BaseBox()
		{
			super();
		}
		
		public virtual function fillData():void
		{
		}
		
		protected function get relationItem():RelationItem
		{
			return super.data as RelationItem;
		}
		
		protected function setName(txtName:Text):void
		{
			txtName.text = data.name;
		}
		
		protected function setRank(txtRank:Text,bxRank:HBox):void
		{
			if (relationItem.data.Rank == 0)
				bxRank.visible = false;	
			else
				txtRank.text = new String(relationItem.data.Rank).substr(0,4);
		}
		
		protected function setAlias(txtAlias:Text,bxAlias:HBox):void
		{
			if(relationItem.data.Alias.length == 0)
				bxAlias.visible = false;
			else
			{
				txtAlias.text = "";
				for(var i:int= 0;i<relationItem.data.Alias.length;++i)
				{
					if(i !=0)
						txtAlias.text += ",";
					txtAlias.text += relationItem.data.Alias[i];										
				}
			}
		}
		
		protected function setRelationContent(txtContent:Text,txtPage:Text):void
		{
			if (relationItem.DataLength == 0)
				{
					if(relationItem.rank!=0)
						txtContent.text = "双击节点查看..."
					else
						txtContent.text = ""
				}
			else
			{
				txtContent.text = "";
				
				var countPerPage:int = RelationGraph(Application.application).CountPerPage;
				var pages:int = relationItem.getPagesCount(countPerPage);
				var start:int = 0;
				var end:int = relationItem.DataLength-1;
				
				if (pages!=0)
				{
					var cur:int = relationItem.getCurrentPage(countPerPage);
					start = relationItem.start;
					end = start + countPerPage -1 < end? start + countPerPage -1 :end;
				}
				txtContent.text = "";
				for(var i:int = start;i<=end;++i)
				{
					txtContent.text += (i+1)+" "+relationItem.getRole(i)+ " "+ relationItem.getData(i).Name+"\n";										
				}
				
				if (pages>1 && relationItem.rank == 0)
					txtPage.text = cur+"/"+pages+"页，共"+relationItem.TotalNum+"项相关";
				else
				{
					if (relationItem.TotalNum > countPerPage)
						txtPage.text = "共"+relationItem.TotalNum+"项相关";
					else
						txtPage.text ="";
				}
			}
		}
		
	}
}