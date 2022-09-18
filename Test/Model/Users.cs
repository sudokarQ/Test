using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Test.Model
{




    public class Users
    {



        public int Rank { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public int Steps { get; set; }
        public int AvgSteps { get; set; }
        public int MaxSteps { get; set; }
        public int MinSteps { get; set; }

        public static Dictionary<string, List<Users>> DicOfUser
        {
            get
            {
                return GetUsers();
            }

            set { }
        }

        

        

        public static Dictionary<string, List<Users>> GetUsers()
        {
                List<Users> list = new List<Users>();

                for (int i = 1; i < 31; i++)
                {
                    string path = Directory.GetCurrentDirectory() + "\\TestData\\day" + i + ".json";

                    var list1 = (JsonConvert.DeserializeObject<List<Users>>(File.ReadAllText(path)));
                    list.AddRange(list1);
                }

                Dictionary<string, List<Users>> dic = new Dictionary<string, List<Users>>();

                foreach (var item in list)
                {
                    if (!dic.ContainsKey(item.User))
                    {
                        dic.Add(item.User, new List<Users>());
                    }
                }
                foreach (var man in list)
                {
                    dic[man.User].Add(man);
                }

            foreach (var item in dic)
            {
                var liiist = item.Value;
                int max = 0;
                int min = 40000;
                int sum = 0;
                foreach (var us in liiist)
                {
                    if (us.Steps > max)
                        max = us.Steps;
                    if (us.Steps < min)
                        min = us.Steps;
                    sum += us.Steps;
                }
                liiist[0].AvgSteps = sum / 30;
                liiist[0].MaxSteps = max;
                liiist[0].MinSteps = min;
                
            }
                

                return dic;
        }

        public static string DirGet()
        {
            return Directory.GetCurrentDirectory() + "\\TestData\\";
            
        }
    }
}
