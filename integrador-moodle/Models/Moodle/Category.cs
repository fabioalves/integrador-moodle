using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace integrador_moodle.Models.Moodle
{
    public class Category
    {
        public int id;
        public string name;
        public string idnumber;
        public string description;
        public int descriptionformat;
        public int parent;
        public int sortorder;
        public int coursecount;
        public int visible;
        public int visibleold;
        public int timemodified;
        public int depth;
        public string path;
        public string theme;

    }
}