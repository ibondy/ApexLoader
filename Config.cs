using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexLoader
{
    public class Config
    {
        public string Name { get;set;}
        public string Url { get;set;}
        public int Port { get;set;}
        public string User { get;set;}
        public string Password { get;set;}
        public bool Active { get;set;}
    }

    public class ApexConfigs
    {
        public Config Apex1 { get;set;} = new Config();
        public Config Apex2 { get;set;} = new Config();
    }

}
