using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class MemoryMessage
    {
        public static void Sort_BF(List<MemoryMessage> Mes)
        {
            Mes.Sort(SortFunctoBestFit);
        }

        public static void Sort_WF(List<MemoryMessage> Mes)
        {
            Mes.Sort(SortFunctoWorstFit);
        }

        public static void Sort_BO(List<MemoryMessage> Mes)
        {
            Mes.Sort(SortbyOrder);
        }

        public int Id { get; set; }

        public string WorkId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 起始地址
        /// </summary>
        public int StartAddress { get; set; }

        /// <summary>
        /// 结束地址
        /// </summary>
        public int EndAddress { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }
        
        private static int SortFunctoBestFit(MemoryMessage x, MemoryMessage y)
        {
            if (x.Status == "未分配" && y.Status == "未分配")
            {
                if(x.Length < y.Length) return -1;
                else if(x.Length == y.Length) return 0;
                else return 1;
            }
            if (x.Status == "未分配" && y.Status == "已分配") return -1;
            else return 1;
        }

        private static int SortFunctoWorstFit(MemoryMessage x, MemoryMessage y)
        {
            if (x.Status == "未分配" && y.Status == "未分配")
            {
                if (x.Length < y.Length) return 1;
                else if (x.Length == y.Length) return 0;
                else return -1;
            }
            if (x.Status == "未分配" && y.Status == "已分配") return -1;
            else return 1;
        }
        private static int SortbyOrder(MemoryMessage x, MemoryMessage y)
        {
            if (x.Id > y.Id) return 1;
            else if (x.Id == y.Id) return 0;
            else return -1;
        }
    }
}
