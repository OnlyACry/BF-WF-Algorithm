using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;

namespace ConsoleApplication1
{

    class Program
    {
        static int cnt = 0;
        static MemoryMessage Mes = new MemoryMessage();
        static List<MemoryMessage> LMes = new List<MemoryMessage>();
        static List<MemoryMessage> LUseMes = new List<MemoryMessage>();
        static void Main(string[] args)
        {

            Console.WriteLine("选择要进行的算法：1、最佳适应算法 2、最坏适应算法");
            int sw = Convert.ToInt32(Console.ReadLine());

            int n;
            Console.WriteLine("输入要分配的内存大小(kb)");

            n = Convert.ToInt32(Console.ReadLine());
            Mes.Length = n;
            Mes.StartAddress = 0;
            Mes.EndAddress = n-1;
            Mes.Status = "未分配";
            Mes.Id = 1;
            Mes.WorkId = "";
            cnt++;

            LMes.Add(Mes);

            if(sw == 1)
            {
                int flag = 0;
                while (flag != 3)
                {
                    Console.WriteLine("1.分配内存  2.回收内存  3.退出");
                    flag = Convert.ToInt32(Console.ReadLine());
                    switch (flag)
                    {
                        case 1:
                            {
                                if (LMes.FindAll(a => a.Status == "未分配").Count > 0) Distribution_BF();
                                else Console.WriteLine("空间全部分配，请先回收空间");
                            }
                            break;
                        case 2:
                            {
                                if (LMes.FindAll(a => a.Status == "已分配").Count > 0) Recycle_BF();
                                else Console.WriteLine("没有已分配空间！");
                            } break;
                        case 3: return;
                    }
                }
            }
            else
            {
                int flag = 0;
                while (flag != 3)
                {
                    Console.WriteLine("***********************************");
                    Console.WriteLine("1.分配内存  2.回收内存  3.退出");
                    flag = Convert.ToInt32(Console.ReadLine());
                    switch (flag)
                    {
                        case 1:
                            {
                                if (LMes.FindAll(a => a.Status == "未分配").Count > 0) Distribution_WF();
                                else Console.WriteLine("空间全部分配，请先回收空间");
                            }
                            break;
                        case 2:
                            {
                                if (LMes.FindAll(a => a.Status == "已分配").Count > 0) Recycle_BF();
                                else Console.WriteLine("没有已分配空间！");
                            } break;
                        case 3: return;
                    }
                }
            }
        }

        private static void Distribution_BF()
        {
            MemoryMessage Nmes = new MemoryMessage();
            //按最佳适应算法对空闲容量排序
            MemoryMessage.Sort_BF(LMes);
            Console.WriteLine("输入要分配的作业名和需要的空间大小");
            Nmes.WorkId = Console.ReadLine();
            Nmes.Length = Convert.ToInt32(Console.ReadLine());

            var cou = LMes.FindAll(a => a.WorkId == Nmes.WorkId).Count();
            if (cou > 0)
            {
                Console.WriteLine("***********************************");
                Console.WriteLine("当前作业已存在!");
                Console.WriteLine("***********************************");
            }
            else
            {
                //检查是否有充足的空间使用
                var Select = LMes.Find(a => a.Length >= Nmes.Length && a.Status == "未分配");
                if (Select != null)
                {
                    int m = Select.Id;

                    MemoryMessage.Sort_BO(LMes);

                    Nmes.Id = ++cnt;
                    Nmes.StartAddress = LMes[m - 1].StartAddress + Nmes.Length;    //剩余未分配空间
                    Nmes.EndAddress = LMes[m - 1].EndAddress;

                    //被占用
                    LMes[m - 1].EndAddress = LMes[m - 1].StartAddress + Nmes.Length - 1;
                    LMes[m - 1].Status = "已分配";
                    LMes[m - 1].Length = Nmes.Length;
                    LMes[m - 1].WorkId = Nmes.WorkId;

                    Nmes.Length = Nmes.EndAddress - Nmes.StartAddress + 1;

                    if (Nmes.Length == 0) return;
                    Nmes.Status = "未分配";
                    Nmes.WorkId = "空";

                    LMes.Add(Nmes);
                    ReCode();
                }
                else
                {
                    Console.WriteLine("***********************************");
                    Console.WriteLine("没有足够的空间");
                    Console.WriteLine("***********************************");
                }
            }
            Print();
        }

        private static void Distribution_WF()
        {
            MemoryMessage Nmes = new MemoryMessage();
            //按最佳适应算法对空闲容量排序
            MemoryMessage.Sort_WF(LMes);
            Console.WriteLine("输入要分配的作业名和需要的空间大小");
            Nmes.WorkId = Console.ReadLine();
            Nmes.Length = Convert.ToInt32(Console.ReadLine());

            var cou = LMes.FindAll(a => a.WorkId == Nmes.WorkId).Count();
            if (cou > 0)
            {
                Console.WriteLine("***********************************");
                Console.WriteLine("当前作业已存在!");
                Console.WriteLine("***********************************");
            }
            else
            {
                //检查是否有充足的空间使用
                var Select = LMes.Find(a => a.Length >= Nmes.Length && a.Status == "未分配");
                if (Select != null)
                {
                    int m = Select.Id;

                    MemoryMessage.Sort_BO(LMes);

                    Nmes.Id = ++cnt;
                    Nmes.StartAddress = LMes[m - 1].StartAddress + Nmes.Length;    //剩余未分配空间
                    Nmes.EndAddress = LMes[m - 1].EndAddress;

                    //被占用
                    LMes[m - 1].EndAddress = LMes[m - 1].StartAddress + Nmes.Length - 1;
                    LMes[m - 1].Status = "已分配";
                    LMes[m - 1].Length = Nmes.Length;
                    LMes[m - 1].WorkId = Nmes.WorkId;

                    Nmes.Length = Nmes.EndAddress - Nmes.StartAddress + 1;

                    if (Nmes.Length == 0) return;
                    Nmes.Status = "未分配";
                    Nmes.WorkId = "空";

                    LMes.Add(Nmes);
                    ReCode();
                }
                else
                {
                    Console.WriteLine("***********************************");
                    Console.WriteLine("没有足够的空间");
                    Console.WriteLine("***********************************");
                }
            }
            Print();
        }

        private static void Recycle_BF()
        {
            MemoryMessage Nmes = new MemoryMessage();
            List<MemoryMessage> distribute = LMes.FindAll(a => a.Status == "已分配").ToList();
            Console.WriteLine("输入要回收的作业名");
            string number = Console.ReadLine();

            MemoryMessage.Sort_BO(LMes);

            var find = LMes.FindAll(a => a.WorkId == number).Count();
            if (find > 0)
            {
                int num = LMes.Find(a => a.WorkId == number).Id;
                var thing = LMes[num - 1];

                var f1 = LMes.Find(a => a.EndAddress + 1 == thing.StartAddress && a.Status == "未分配");    //找区域连接上方是否空闲
                var f2 = LMes.Find(a => a.StartAddress == thing.EndAddress + 1 && a.Status == "未分配" );    //区域下方是否空闲

                if(f1 != null && f2 != null)
                {
                    //区域上下都空闲
                    LMes[f1.Id - 1].EndAddress = LMes[f2.Id - 1].EndAddress;
                    LMes[f1.Id - 1].Length = LMes[f1.Id - 1].EndAddress - LMes[f1.Id - 1].StartAddress + 1;
                    LMes[f1.Id - 1].WorkId = "空";
                    LMes.Remove(LMes[num - 1]);  //只留最上方空间
                    LMes.Remove(LMes[f2.Id - 2]);
                }
                else if(f1 != null)
                {
                    //区域上方空闲
                    LMes[f1.Id - 1].EndAddress = LMes[num - 1].EndAddress;
                    LMes[f1.Id - 1].Length = LMes[f1.Id - 1].EndAddress - LMes[f1.Id - 1].StartAddress + 1;
                    LMes[f1.Id - 1].WorkId = "空";
                    LMes.Remove(LMes[num - 1]);
                }
                else if(f2 != null)
                {
                    //区域下方空闲
                    LMes[f2.Id - 1].StartAddress = LMes[num - 1].StartAddress;
                    LMes[f2.Id - 1].Length = LMes[f2.Id - 1].EndAddress - LMes[f2.Id - 1].StartAddress + 1;
                    LMes[f2.Id - 1].WorkId = "空";
                    LMes.Remove(LMes[num - 1]);
                }
                else
                {
                    //区域上下都不空闲
                    Nmes = LMes[num - 1];
                    Nmes.Status = "未分配";
                    Nmes.WorkId = "空";
                }
                MemoryMessage.Sort_BO(LMes);
                ReCode();
            }
            else
            {
                Console.WriteLine("***********************************");
                Console.WriteLine("分区不存在！");
                Console.WriteLine("***********************************");
            }
            Print();
        }

        private static void Print()
        {
            Console.WriteLine("***********************************");
            Console.WriteLine("分区号" + "\t" +"作业名"+"\t" + "开始地址" + "\t" + "结束地址" + "\t" +"分区长度"+"\t"+ "分区状态" +"\t");
            for(int i=0; i<LMes.Count(); i++)
            {
                Console.WriteLine(LMes[i].Id + "\t" + LMes[i].WorkId + "\t" + LMes[i].StartAddress + "\t\t" + LMes[i].EndAddress + "\t\t" + LMes[i].Length + "\t\t" + LMes[i].Status);
            }

            Console.WriteLine("***********************************");
        }

        private static void ReCode()
        {
            //重新编号
            int i;
            for (i = 0; i < LMes.Count(); i++)
            {
                LMes[i].Id = i + 1;
            }
            cnt = i;
        }
    }
}
