namespace AngEx.Server
{
    public class Task
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public DateTime? created { get; set; }//displayed as string 
        public DateTime? deadl { get; set; }//may have no deadline
        /*public string createds
        {
            get { return created.ToString(); }
        }
        public string deadls
        {
            get { return deadl.ToString();}
        }*/
        public string? descr { get; set; }
        public string? tags { get; set; }//a ; separated list
        public int? priority { get; set; }
        public int? parent { get; set; }//parent task id
        public int state { get; set; }
        public Task() { 
            if (created==null) created=DateTime.Now;
            state = 0;
        }
    }
}
