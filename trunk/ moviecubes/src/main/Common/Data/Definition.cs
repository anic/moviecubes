﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieCube.Common.Data
{
    public class Definition
    {
        //节点搜索最大深度
        public const int Max_Node_Layer = 2;
        
        //周围节点最大个数
        public const int Max_Surround_Node_Num = 3;



        public const string Role_Director = "导演";
        public const string Role_Writer = "编剧";
        public const string Role_Actor = "主演";
    }
}
