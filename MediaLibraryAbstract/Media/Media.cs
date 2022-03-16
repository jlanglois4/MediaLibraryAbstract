using System.Collections.Generic;

namespace MediaLibraryAbstract
{
    public abstract class Media
    {
        // global fields
        public int mediaID { get; set; }
        public string title { get; set; }
        
        //movie fields
        public List<string> genre { get; set; }
        
        // show fields
        public int? season { get; set; }
        public int? episode { get; set; }
        public List<string> writers { get; set; }
        
        // video fields
        public string format { get; set; }
        public int? length { get; set; }
        public List<int> regions { get; set; }
        
        // global methods
        public abstract string Display();
        public abstract void addMedia();
        public abstract void listMedia();
        public abstract void InitializeList();

    }
}