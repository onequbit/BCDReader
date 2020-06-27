using Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace App
{

    public enum BcdItemTypeEnum
    {
        PropertyString,
        GuidValue,
        Separator,
        ObjectTitle,
        BootLoader,
        BootManager,
        ParseError,
        NotSet

    }

    public class BcdProperty
    {
        private string sourceString;

        private Guid guidValue = new Guid("{00000000-0000-0000-0000-000000000000}");

        public BcdProperty(string str)
        {
            this.sourceString = str.Trim();
            this.EnumType = this.GetBcdPropertyType();
        }

        public string Title;

        public string Key;

        public string Value;

        public bool IsGuid
        {
            get
            {   
                Guid newGuid;
                return Guid.TryParse(this.sourceString, out newGuid);                
            }            
        }

        public bool IsCollection
        {
            get
            {
                return this.EnumType == BcdItemTypeEnum.PropertyString &&
                        (
                            this.Key.ToLower().EndsWith("order") ||
                            this.Key.ToLower().EndsWith("sequence")
                        );
                        
            }
        }
        
        public BcdItemTypeEnum EnumType = BcdItemTypeEnum.NotSet;

        public bool IsNewObject
        {
            get
            {
                return (this.EnumType == BcdItemTypeEnum.BootLoader ||
                        this.EnumType == BcdItemTypeEnum.BootManager);
            }
        }
       
        public Guid GuidValue
        {
            get
            {
                return this.guidValue;                
            }
        }

        private static string separator(int size)
        {
            return new string((char)45, size);
        }

        private BcdItemTypeEnum GetBcdPropertyType()
        {
            string[] parts = this.sourceString.Split(' ');
            if (parts.Length == 3 && parts[0].Equals("Windows") && parts[1].Equals("Boot"))
            {
                this.Title = this.sourceString;
                if (parts[2].Equals("Loader")) return BcdItemTypeEnum.BootLoader;
                else if (parts[2].Equals("Manager")) return BcdItemTypeEnum.BootManager;
                else 
                return BcdItemTypeEnum.ParseError;
            }
            else if (parts.Length == 1)
            {
                if (this.IsGuid)
                {
                    this.guidValue = Guid.Parse(this.sourceString);
                    return BcdItemTypeEnum.GuidValue;
                } 
                if (this.sourceString.Equals(separator(sourceString.Length)))
                {                    
                    return BcdItemTypeEnum.Separator;
                }
                else return BcdItemTypeEnum.ParseError;
            }
            else
            {
                this.Key = parts[0].Trim();
                this.Value = this.sourceString.Replace(this.Key,"").Trim();
                return BcdItemTypeEnum.PropertyString;
            }            
        }

        public override string ToString()
        {
            string name = this.EnumType.Name();
            if (this.EnumType == BcdItemTypeEnum.BootLoader)
            {
                return $"[[{this.Title}]]";
            }
            else 
            if (this.EnumType == BcdItemTypeEnum.PropertyString)
            {
                return $"[{this.Key}]: '{this.Value}'";
            }
            else             
            return "";            
        }
    }

    public class BcdObject
    {
        public string TypeName = "";
        Dictionary<string,object> Properties = new Dictionary<string, object>();

        public BcdObject()
        {
            
        }

        public static bool IsEmpty(BcdObject obj)
        {
            return obj.Properties.Count == 0;
        }

        public static List<BcdObject> ParseCollection(string data)
        {            
            List<BcdObject> collection = new List<BcdObject>();
            BcdObject obj = new BcdObject();
            BcdProperty previous = new BcdProperty("---");
            try
            {            
                foreach(string line in data.Split('\n'))
                {                
                    if (line.Length > 1)
                    {                        
                        BcdProperty bps = new BcdProperty(line);
                        if (bps.IsNewObject)
                        {
                            if (!IsEmpty(obj)) collection.Add(obj);
                            obj = new BcdObject();
                            obj.TypeName = bps.Title;
                        } else
                        if (bps.EnumType == BcdItemTypeEnum.PropertyString)
                        {
                            if (bps.Key.Length > 0 && bps.Value.Length > 0)
                            {                                
                                obj.Properties[bps.Key] = bps.Value;
                                if (bps.IsCollection)
                                {
                                    obj.Properties[bps.Key] = new List<Guid>();
                                    ((List<Guid>)obj.Properties[bps.Key]).Add(new Guid(bps.Value));
                                    previous = bps;
                                }                                
                            }
                        } else 
                        if (bps.IsGuid && previous.IsCollection)
                        {
                            ((List<Guid>)obj.Properties[previous.Key]).Add(bps.GuidValue);
                        }
                    }
                }
                collection.Add(obj);                
            } 
            catch (Exception ex)
            {
                 Console.WriteLine($"{ex.Message}\n{ex.StackTrace}\n{ex.InnerException}");  
            }
            return collection;
        }

        private string GetGuidList(List<Guid> collection)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var guid in collection)
            {                
                sb.Append("{" + $"{guid.ToString()}" + "},");
            }
            return sb.ToString().TrimEnd(',');
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.TypeName);
            foreach(string key in this.Properties.Keys)
            {                
                string value = this.Properties[key].ToString();
                if (typeof(List<Guid>) == this.Properties[key].GetType())
                    value = GetGuidList((List<Guid>)this.Properties[key]);
                sb.AppendLine($"{key}: '{value}'");
            }
            return sb.ToString();
        }
    }

    public class BcdReader
    {            

        [STAThread] // required for Windows Forms
        public static void Main(string[] args)
        {            
            try
            {   
                string bcdInfo = Lib.GetAdminCmdOutput("bcdedit /enum /v");                
                var objs = BcdObject.ParseCollection(bcdInfo);
                
                Console.WriteLine($"{objs.Count} objects parsed");
                foreach(var obj in objs)
                {
                    Console.WriteLine(obj);
                }
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");                
            }                        
        }
    }
}///